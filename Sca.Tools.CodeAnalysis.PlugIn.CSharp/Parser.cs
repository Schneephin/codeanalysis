using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.LanguageDependent;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.BaseClasses;
using Sca.Tools.CodeAnalysis.Utilities;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent;
using System.IO;

namespace Sca.Tools.CodeAnalysis.PlugIn.CSharp
{
    public class Parser
    {
        private ParseableTokenStream tokenStream;
        private int overallDepth;
        private Dictionary<int, TreeNode> blockNodeCloseIds;
        
        public Parser()
        {
            this.initializeLexer();
        }

        public TreeNode parseSourceProject(String folder)
        {
            TreeNode projectRoot = new TreeNode() { nodeContent = "ProjectRoot", nodeType = NodeTypes.Default };

            this.parseForCSProjFiles(folder, projectRoot);

            this.cleanFunctionCalls(projectRoot);

            if (projectRoot.childNodes.Count == 0)
            {
                return null;
            }

            return projectRoot;
        }

        private void parseForCSProjFiles(String folder, TreeNode projectRoot)
        {
            List<String> csprojFiles = Directory.GetFiles(folder, "*.csproj", SearchOption.AllDirectories).ToList<String>();
            foreach (String csprojFile in csprojFiles)
            {
                CSProject csproj = new CSProject(csprojFile);
                TreeNode projectNode = new TreeNode() { nodeType = NodeTypes.ProjectDeclaration, nodeContent = Path.GetFileNameWithoutExtension(csprojFile) };
                projectRoot.addChildNode(projectNode);
                foreach (CSProjectReference reference in csproj.References)
                {
                    String referencePath = "";
                    if (!String.IsNullOrEmpty(reference.HintPath) && reference.HintPath.EndsWith(".dll"))
                    {
                       referencePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(csprojFile), reference.HintPath));   
                    }
                    else if (!String.IsNullOrEmpty(reference.Include))
                    {
                        referencePath = reference.Include;
                    }
                    projectNode.nodeAttributes.Add(new ExtendedAttribute() { attributeType = "reference", attributeInformation = referencePath });
                }
                foreach (CSProjectSourceFile sourcefile in csproj.SourceFiles)
                {
                    if (!sourcefile.Source.Contains("Properties\\"))
                    {
                        this.parseSourceFile(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(csprojFile), sourcefile.Source)), projectNode);
                    }
                }
            }
        }

        private void parseSourceFile(String filename, TreeNode root)
        {
            this.overallDepth = 0;
            this.blockNodeCloseIds = new Dictionary<int, TreeNode>();

            String sourceContent = SourceFileReader.getSourceFileContent(filename);
            this.tokenStream = new ParseableTokenStream(new Lexer(sourceContent));
            TreeNode fileRoot = new TreeNode() { nodeType = NodeTypes.Default, nodeContent = Path.GetFileNameWithoutExtension(filename) };
            root.addChildNode(fileRoot);

            TreeNode parentNode = fileRoot;

            while (this.tokenStream.Peek(1).TokenType != TokenType.EOF)
            {
                parentNode = this.recurseTokenList(this.tokenStream.Current, parentNode);
            }
        }

        private TreeNode recurseTokenList(Token token, TreeNode parentNode)
        {
            TreeNode nextParent = parentNode;
            switch (token.TokenType)
            {
                case TokenType.Include:
                    {
                        String descriptor = "";
                        while(this.tokenStream.Peek(1).TokenType != TokenType.SemiColon)
                        {
                            this.tokenStream.Consume();
                            descriptor += this.tokenStream.Current.TokenValue;
                        }
                        this.tokenStream.Consume();
                        parentNode.addChildNode(new TreeNode() { nodeType = NodeTypes.Include, nodeContent = descriptor });
                    }
                    break;
                case TokenType.NamespaceDeclaration:
                    {
                        String descriptor = "";
                        while (!this.tokenStream.Peek(1).TokenType.IsOneOf<TokenType>(TokenType.LBracket, TokenType.NewLine))
                        {
                            this.tokenStream.Consume();
                            descriptor += this.tokenStream.Current.TokenValue;
                        }
                        if (!this.tokenStream.Current.TokenType.IsOneOf<TokenType>(TokenType.LBracket))
                        {
                            this.tokenStream.Consume();
                        }
                        TreeNode namespaceNode = new TreeNode() { nodeType = NodeTypes.NamespaceDeclaration, nodeContent = descriptor };
                        parentNode.addChildNode(namespaceNode);
                        nextParent = namespaceNode;
                    }
                    break;
                case TokenType.ClassDeclaration:
                    {
                        TreeNode classNode = this.handleClassDeclaration();
                        parentNode.addChildNode(classNode);
                        nextParent = classNode;
                    }
                    break;
                case TokenType.LBracket:
                    this.overallDepth++;
                    int closeBracketIndex = this.tokenStream.PeekAheadUntilMatchingClose(TokenType.LBracket, TokenType.RBracket);
                    if (closeBracketIndex > 0 && !this.blockNodeCloseIds.ContainsValue(parentNode))
                    {
                        this.blockNodeCloseIds.Add(closeBracketIndex, parentNode);
                    }
                    this.tokenStream.Consume();
                    break;
                case TokenType.RBracket:
                    this.overallDepth--;
                    if (this.blockNodeCloseIds.ContainsKey(this.tokenStream.getCurrentIndex()))
                    {
                        nextParent = this.blockNodeCloseIds[this.tokenStream.getCurrentIndex()].parentNode;
                    }
                    this.tokenStream.Consume();
                    break;
                case TokenType.Word:

                    Dictionary<String, String> methodSignature = this.parseMethodSignature();
                    if (methodSignature != null)
                    {
                        TreeNode functionNode = new TreeNode() { nodeType = NodeTypes.FunctionDeclaration, nodeContent = methodSignature["methodname"] };
                        parentNode.addChildNode(functionNode);
                        functionNode.nodeAttributes.Add(new ExtendedAttribute()
                        {
                            attributeInformation = (this.tokenStream.CountTokenTypeUntilMatchingClose(TokenType.LBracket, TokenType.RBracket, TokenType.NewLine) - 1).ToString(),
                            attributeName = "linesofcode", attributeType = "intcodesmell"
                        });
                        functionNode.nodeAttributes.Add(new ExtendedAttribute()
                        {
                            attributeInformation = this.tokenStream.CountTokenTypeUntilMatchingClose(TokenType.LBracket, TokenType.RBracket, TokenType.LBracket).ToString(),
                            attributeName = "codedepths", attributeType = "intcodesmell"
                        });
                        String[] parameters = methodSignature["parameters"].Split(',');
                        foreach (String parameter in parameters)
                        {
                            if(!String.IsNullOrEmpty(parameter))
                            {
                                functionNode.nodeAttributes.Add(new ExtendedAttribute()
                                {
                                    attributeInformation = parameter.Split(' ')[0].Trim(),
                                    attributeName = "parameter",
                                    attributeType = "parameter"
                                });
                            }
                        }
                        if (this.getClassForNode(functionNode) != null)
                        {
                            functionNode.nodeAttributes.Add(new ExtendedAttribute()
                            {
                                attributeInformation = this.getClassForNode(functionNode).nodeContent,
                                attributeName = "Class",
                                attributeType = "filter"
                            });
                        }
                        nextParent = functionNode;
                    }
                    else
                    {
                        this.tokenStream.Consume();
                    }
                    break;
                case TokenType.OpenParenth:
                    Dictionary<String, String> methodCall = this.parseMethodCall();
                    if (methodCall != null)
                    {
                        TreeNode methodCallNode = new TreeNode() { nodeType = NodeTypes.FunctionCall, nodeContent = methodCall["methodname"] };
                        String[] parameters = methodCall["parameters"].Split(',');
                        foreach (String parameter in parameters)
                        {
                            if (!String.IsNullOrEmpty(parameter))
                            {
                                methodCallNode.nodeAttributes.Add(new ExtendedAttribute()
                                {
                                    attributeInformation = parameter.Trim(),
                                    attributeName = "parameter",
                                    attributeType = "parameter"
                                });
                            }
                        }
                        parentNode.addChildNode(methodCallNode);
                    }
                    else
                    {
                        this.tokenStream.Consume();
                    }
                    break;
                case TokenType.Equals:
                case TokenType.SemiColon:
                    Dictionary<String, String> variableDeclaration = this.parseVariableDeclaration(token.TokenType);
                    if (variableDeclaration != null)
                    {
                        TreeNode variableDeclarationNode = new TreeNode() { nodeType = NodeTypes.VariableDeclaration, nodeContent = variableDeclaration["variablename"] };
                        variableDeclarationNode.nodeAttributes.Add(new ExtendedAttribute()
                        {
                            attributeInformation = variableDeclaration["variabletype"],
                            attributeName = "type",
                            attributeType = "type"
                        });
                        parentNode.addChildNode(variableDeclarationNode);
                    }
                    else
                    {
                        this.tokenStream.Consume();
                    }
                    break;
                case TokenType.If:
                case TokenType.Foreach:
                case TokenType.For:
                case TokenType.While:
                    TreeNode node = this.handleLoopAndIfBlocks();
                    parentNode.addChildNode(node);
                    nextParent = node;
                    break;
                default:
                    this.tokenStream.Consume();
                    break;
            }
            return nextParent;
        }

        private void cleanFunctionCalls(TreeNode rootNode)
        {
            List<TreeNode> functionCalls = this.recurseParseNodesOfType(rootNode, NodeTypes.FunctionCall);
            List<TreeNode> functionDeclarations = this.recurseParseNodesOfType(rootNode, NodeTypes.FunctionDeclaration);
            Dictionary<String, TreeNode> functionFullNames = this.getFunctionFullNames(functionDeclarations);

            foreach (TreeNode functionCall in functionCalls)
            {
                String functionCallFullName = this.getFunctionCallFullName(functionCall);
                if (!String.IsNullOrEmpty(functionCallFullName))
                {
                    if (functionFullNames.ContainsKey(functionCallFullName))
                    {
                        functionCall.nodeAttributes.Add(new ExtendedAttribute()
                        {
                            attributeInformation = functionFullNames[functionCallFullName].nodeId.ToString(),
                            attributeName = "functionid",
                            attributeType = "functionid"
                        });
                        foreach (ExtendedAttribute ea in functionFullNames[functionCallFullName].nodeAttributes)
                        {
                            if (ea.attributeType.Equals("filter"))
                            {
                                functionCall.addAttribute(ea);
                            }
                        }
                    }
                }
            }
        }

        private String getFunctionCallFullName(TreeNode functionCall)
        {
            String functionCallFullName = "";
            String className = "";
            String functionName = "";
            String functionCallString = functionCall.nodeContent;

            if (functionCall.nodeContent.Split('.')[0].Equals("this"))
            {
                functionCallString = functionCall.nodeContent.Replace("this.", "");
            }

            if (!functionCallString.Contains('.'))
            {
                className  = this.getClassForNode(functionCall).nodeContent;
            }
            else if (functionCallString.Contains('.'))
            {
                className = this.tryGetTypeForVariable(functionCallString.Split('.')[0], functionCall);
            }

            if (functionCallString.Contains('.'))
            {
                functionName = functionCallString.Split('.')[1];
            }
            else
            {
                functionName = functionCall.nodeContent;
            }

            int count = 0;
            foreach (ExtendedAttribute ea in functionCall.nodeAttributes)
            {
                if (ea.attributeType.Equals("parameter"))
                {
                    count++;
                }
            }
            functionCallFullName = className + "." + functionName + "|" + count;
            return functionCallFullName;
        }

        private String tryGetTypeForVariable(String variableName, TreeNode startpoint)
        {
            String type = "";
            if (startpoint.parentNode == null) return type;

            foreach (TreeNode node in startpoint.parentNode.childNodes)
            {
                if (node.nodeType == NodeTypes.VariableDeclaration && node.nodeContent.Equals(variableName))
                {
                    foreach(ExtendedAttribute ea in node.nodeAttributes)
                    {
                        if(ea.attributeType.Equals("type"))
                        {
                            type = ea.attributeInformation;
                            return type;
                        }
                    }
                }
            }
            type = this.tryGetTypeForVariable(variableName, startpoint.parentNode);

            return type;
        }

        private Dictionary<String, TreeNode> getFunctionFullNames(List<TreeNode> functions)
        {
            Dictionary<String, TreeNode> functionFullNames = new Dictionary<String, TreeNode>();

            foreach (TreeNode function in functions)
            {
                int count = 0;
                foreach (ExtendedAttribute ea in function.nodeAttributes)
                {
                    if (ea.attributeType.Equals("parameter"))
                    {
                        count++;
                    }
                }
                String fullName = function.parentNode.nodeContent + "." + function.nodeContent + "|" + count;
                if(!functionFullNames.ContainsKey(fullName))
                {
                    functionFullNames.Add(fullName, function);
                }
            }

            return functionFullNames;
        }

        private List<TreeNode> recurseParseNodesOfType(TreeNode node, NodeTypes type)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            if (node.nodeType == type)
            {
                nodes.Add(node);
            }
            else
            {
                foreach (TreeNode childNode in node.childNodes)
                {
                    nodes.AddRange(this.recurseParseNodesOfType(childNode, type));
                }
            }
            return nodes;
        }

        private TreeNode getClassForNode(TreeNode node)
        {
            if (node.parentNode != null && node.parentNode.nodeType == NodeTypes.ClassDeclaration)
            {
                return node.parentNode;
            }
            else if (node.parentNode != null)
            {
                return this.getClassForNode(node.parentNode);
            }
            return null;
        }

        private TreeNode handleLoopAndIfBlocks()
        {
            TreeNode blockNode = new TreeNode();
            String content = "";

            if (this.tokenStream.Current.TokenType.IsOneOf<TokenType>(TokenType.If))
            {
                blockNode.nodeType = NodeTypes.Conditional;
            }
            else
            {
                blockNode.nodeType = NodeTypes.Loop;
            }

            do
            {
                this.tokenStream.Consume();
            }
            while (!this.tokenStream.Current.TokenType.IsOneOf<TokenType>(TokenType.OpenParenth));
            this.tokenStream.Consume();
            do
            {
                content += this.tokenStream.Current.TokenValue;
                this.tokenStream.Consume();
            }
            while (!this.tokenStream.Current.TokenType.IsOneOf<TokenType>(TokenType.CloseParenth));
            this.tokenStream.Consume();
            blockNode.nodeContent = content;

            return blockNode;
        }

        private TreeNode handleClassDeclaration()
        {
            String descriptor = "";
            while (!this.tokenStream.Peek(1).TokenType.IsOneOf<TokenType>(TokenType.LBracket, TokenType.NewLine))
            {
                this.tokenStream.Consume();
                descriptor += this.tokenStream.Current.TokenValue;
            }
            do
            {
                this.tokenStream.Consume();
            }
            while (!this.tokenStream.Current.TokenType.IsOneOf<TokenType>(TokenType.LBracket));

            String className = descriptor.Split(':')[0];
            TreeNode classNode = new TreeNode() { nodeType = NodeTypes.ClassDeclaration, nodeContent = className };
            if (descriptor.Contains(':'))
            {
                String[] interfaces = descriptor.Split(':')[1].Split(',');
                foreach (String entry in interfaces)
                {
                    classNode.addAttribute(new ExtendedAttribute() { attributeName = "interface", attributeInformation = entry });
                }
            }
            return classNode;
        }

        private Dictionary<String, String> parseMethodSignature()
        {
            Dictionary<String, String> methodSignature = new Dictionary<String, String>();
            int openParenthPosition = this.tokenStream.PeekUntil(TokenType.OpenParenth, 15, TokenType.LBracket, TokenType.SemiColon, TokenType.Equals);
            int closeParenthPosition = this.tokenStream.PeekUntil(TokenType.CloseParenth, openParenthPosition, 1000);
            
            if (openParenthPosition > 0)
            {
                if(this.tokenStream.Peek(openParenthPosition - 1).TokenType == TokenType.Word)
                {
                    methodSignature["methodname"] = this.tokenStream.Peek(openParenthPosition - 1).TokenValue;
                }
                if (this.tokenStream.Peek(openParenthPosition - 2).TokenType.IsOneOf<TokenType>(TokenType.Word, TokenType.Void))
                {
                    methodSignature["returntype"] = this.tokenStream.Peek(openParenthPosition - 2).TokenValue;
                }
                else if (this.tokenStream.Peek(openParenthPosition - 2).TokenType == TokenType.RSquareBracket)
                {
                    methodSignature["returntype"] = this.tokenStream.Peek(openParenthPosition - 4).TokenValue;
                }
                else if (this.tokenStream.Peek(openParenthPosition - 2).TokenType == TokenType.GreaterThan)
                {
                    int openPosition = this.tokenStream.PeekBackUntilMatchingOpen(TokenType.LessThan, TokenType.GreaterThan, openParenthPosition - 2);
                    if (openPosition > 0)
                    {
                        methodSignature["returntype"] = this.tokenStream.Peek(openPosition - 1).TokenValue;
                    }
                }

                if (openParenthPosition > 0 && closeParenthPosition > openParenthPosition)
                {
                    methodSignature["parameters"] = this.tokenStream.getPeekValues(openParenthPosition + 1, closeParenthPosition - 1, " ");
                }

                for (int i = 0; i < openParenthPosition; i++)
                {
                    if (this.tokenStream.Peek(i).TokenValue.IsOneOf<String>("private", "public", "protected", "internal"))
                    {
                        methodSignature["accessmodifier"] = this.tokenStream.Peek(i).TokenValue;
                    }
                    else if (this.tokenStream.Peek(i).TokenValue.IsOneOf<String>("static"))
                    {
                        methodSignature["static"] = this.tokenStream.Peek(i).TokenValue;
                    }
                    else if (this.tokenStream.Peek(i).TokenValue.IsOneOf<String>("static"))
                    {
                        methodSignature["static"] = this.tokenStream.Peek(i).TokenValue;
                    }
                }
            }

            if (openParenthPosition > 0 && closeParenthPosition > openParenthPosition && methodSignature.ContainsKey("returntype")
                && methodSignature.ContainsKey("methodname"))
            {
                for (int i = 0; i <= closeParenthPosition; i++)
                {
                    this.tokenStream.Consume(); 
                }
            }
            else
            {
                return null;
            }

            return methodSignature;
        }

        private Dictionary<String, String> parseMethodCall()
        {
            Dictionary<String, String> methodCall = new Dictionary<String, String>();
            int openParenthPosition = 0;
            int closeParenthPosition = this.tokenStream.PeekUntil(TokenType.CloseParenth, openParenthPosition, 1000);

            if (!this.tokenStream.PeekBack(1).TokenType.IsOneOf<TokenType>(TokenType.Word))
            {
                return null;
            }

            methodCall["methodname"] = this.tokenStream.getPeekBackValuesOfType(1, TokenType.Dot, TokenType.Word);
            

            if (closeParenthPosition > openParenthPosition)
            {
                methodCall["parameters"] = this.tokenStream.getPeekValues(openParenthPosition + 1, closeParenthPosition - 1);
            }

            if (closeParenthPosition > openParenthPosition && methodCall.ContainsKey("methodname"))
            {
                for (int i = 0; i <= closeParenthPosition; i++)
                {
                    this.tokenStream.Consume();
                }
            }
            else
            {
                return null;
            }

            return methodCall;
        }

        private Dictionary<String, String> parseVariableDeclaration(TokenType startType)
        {
            Dictionary<String, String> variableDeclaration = new Dictionary<String, String>();
            int semicolonPosition = -1;
            int assignmentPosition = -1;

            if (startType == TokenType.Equals)
            {
                assignmentPosition = 0;
                semicolonPosition = this.tokenStream.PeekUntil(TokenType.SemiColon, 50);
            }
            else if (startType == TokenType.SemiColon)
            {
                semicolonPosition = 0;
            }
            else
            {
                return null;
            }

            if (this.tokenStream.PeekBack(1).TokenType == TokenType.Word)
            {
                variableDeclaration["variablename"] = this.tokenStream.PeekBack(1).TokenValue;
            }

            if (this.tokenStream.PeekBack(2).TokenType.IsOneOf<TokenType>(TokenType.Word, TokenType.String, TokenType.Boolean, TokenType.Double, TokenType.Int, TokenType.Float))
            {
                variableDeclaration["variabletype"] = this.tokenStream.PeekBack(2).TokenValue;
            }

            if (this.tokenStream.PeekBack(3) != null && this.tokenStream.PeekBack(3).TokenValue.IsOneOf<String>("private", "public", "protected", "internal"))
            {
                variableDeclaration["accessmodifier"] = this.tokenStream.PeekBack(3).TokenValue;
            }

            if (semicolonPosition > -1 && variableDeclaration.ContainsKey("variablename") && variableDeclaration.ContainsKey("variabletype"))
            {
                for (int i = 0; i <= semicolonPosition; i++)
                {
                    this.tokenStream.Consume();
                }
            }
            else
            {
                return null;
            }

            return variableDeclaration;
        }

        private void initializeLexer()
        {
            LanguageDependentMatchKeywords.setKeywords(CSharpMatchKeywords.getMatchKeywords(), CSharpMatchKeywords.getSpecialCharacters());
        }
    }


}
