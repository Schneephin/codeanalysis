using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.Interfaces;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.BaseClasses;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent;

namespace Sca.Tools.CodeAnalysis.PlugIn.CSharp
{
    class CSharpMatchKeywords
    {
        public static List<IMatcher> getMatchKeywords()
        {
            List<IMatcher> keywords = new List<IMatcher> 
                                  {
                                      new MatchKeyword(TokenType.ClassDeclaration, "class"),
                                      new MatchKeyword(TokenType.NamespaceDeclaration, "namespace"),
                                      new MatchKeyword(TokenType.Include, "using"),
                                      
                                      new MatchKeyword(TokenType.Void, "void"),
                                      new MatchKeyword(TokenType.Int, "int"),
                                      new MatchKeyword(TokenType.Int, "Integer"),
                                      new MatchKeyword(TokenType.If, "if"),
                                      new MatchKeyword(TokenType.Foreach, "foreach"),
                                      new MatchKeyword(TokenType.Else, "else"),
                                      new MatchKeyword(TokenType.While, "while"),
                                      new MatchKeyword(TokenType.For, "for"),
                                      new MatchKeyword(TokenType.Return, "return"),
                                      new MatchKeyword(TokenType.True, "true"),
                                      new MatchKeyword(TokenType.False, "false"),
                                      new MatchKeyword(TokenType.Boolean, "bool"),
                                      new MatchKeyword(TokenType.Boolean, "Boolean"),
                                      new MatchKeyword(TokenType.String, "string"),
                                      new MatchKeyword(TokenType.New, "new"),
                                      new MatchKeyword(TokenType.Try, "try"),
                                      new MatchKeyword(TokenType.Catch, "catch")
                                  };


            var specialCharacters = CSharpMatchKeywords.getSpecialCharacters();

            keywords.ForEach(keyword =>
            {
                var current = (keyword as MatchKeyword);
                current.AllowAsSubString = false;
                current.SpecialCharacters = specialCharacters.Select(i => i as MatchKeyword).ToList();
            });

            return keywords;
        }

        public static List<IMatcher> getSpecialCharacters()
        {
            return new List<IMatcher>
                                    {
                                        new MatchKeyword(TokenType.LBracket, "{"),
                                        new MatchKeyword(TokenType.RBracket, "}"),
                                        new MatchKeyword(TokenType.LSquareBracket, "["),
                                        new MatchKeyword(TokenType.RSquareBracket, "]"),
                                        new MatchKeyword(TokenType.Plus, "+"),
                                        new MatchKeyword(TokenType.Minus, "-"),
                                        new MatchKeyword(TokenType.NotCompare, "!="),
                                        new MatchKeyword(TokenType.Compare, "=="),
                                        new MatchKeyword(TokenType.Equals, "="),
                                        new MatchKeyword(TokenType.HashTag, "#"),
                                        new MatchKeyword(TokenType.Comma, ","),
                                        new MatchKeyword(TokenType.OpenParenth, "("),
                                        new MatchKeyword(TokenType.CloseParenth, ")"),
                                        new MatchKeyword(TokenType.Asterix, "*"),
                                        new MatchKeyword(TokenType.Slash, "/"),
                                        new MatchKeyword(TokenType.Carat, "^"),
                                        new MatchKeyword(TokenType.Ampersand, "&"),
                                        new MatchKeyword(TokenType.GreaterThan, ">"),
                                        new MatchKeyword(TokenType.LessThan, "<"),
                                        new MatchKeyword(TokenType.Or, "||"),
                                        new MatchKeyword(TokenType.SemiColon, ";"),
                                        new MatchKeyword(TokenType.Dot, "."),
                                        new MatchKeyword(TokenType.NewLine, Environment.NewLine),
                                    };
        }
    }
}


