using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.Utilities;
using Sca.Tools.ChartFramework;
using System.Drawing.Drawing2D;

namespace Sca.Tools.CodeAnalysis.Visualizer
{
    internal enum Actions { LoadAnalysisResult, End, SwitchToClassMode, SwitchToFunctionMode, ShowToDos, ShowDetails, SwitchToToDoView, SwitchToDetailView, SwitchToDependencyView, FilterChanged }
    internal enum Mode { FunctionView, ClassView }

    internal class Control
    {
        private CodeVisualizer codeVisualizer;
        private TreeNode analysisResult;
        private Mode currentMode;
        private List<TreeNode> classNodes;
        private List<TreeNode> functionNodes;
        private List<TreeNode> functionCalls;
        private List<Filter> filters;
        private Dictionary<String, System.Windows.Forms.UserControl> panels;
        private Dictionary<String, List<ExtendedAttribute>> toDos;
        
        internal Control()
        {
            this.codeVisualizer = new CodeVisualizer(this);

            this.initializeControl();

            this.codeVisualizer.ShowDialog();
        }

        internal void initializeControl()
        {
            this.classNodes = new List<TreeNode>();
            this.functionNodes = new List<TreeNode>();
            this.functionCalls = new List<TreeNode>();
            this.filters = new List<Filter>();

            this.initializePanels();

            this.codeVisualizer.setBottomPanel(this.panels["button"]);
            this.codeVisualizer.setCenterPanel(this.panels["tree"]);
        }

        internal void doAction(Actions action)
        {
            switch (action)
            {
                case Actions.End:
                    this.codeVisualizer.Close();
                    break;
                case Actions.LoadAnalysisResult:
                    this.loadAnalysisResult();
                    break;
                case Actions.SwitchToClassMode:
                case Actions.SwitchToFunctionMode:
                case Actions.SwitchToToDoView:
                case Actions.SwitchToDetailView:
                case Actions.SwitchToDependencyView:
                    this.setPanels(action);
                    break;
                case Actions.ShowToDos:
                    this.showToDos();
                    break;
                case Actions.ShowDetails:
                    this.showDetails();
                    break;
                case Actions.FilterChanged:
                    this.updateTreeView();
                    break;
            }
        }

        private void showToDos()
        {
            List<ExtendedAttribute> toDos = this.getTopTodosForCategory(((ToDoPanel)this.panels["todo"]).getSelectedType(), 50);
            ((ToDoPanel)this.panels["todo"]).setToDos(toDos);
        }

        private void showDetails()
        {
            if (this.currentMode == Mode.FunctionView)
            {
                this.displayDependencyViewForFunctions(((DetailPanel)this.panels["detail"]).getSelectedNode(), new Dictionary<string, string>());
                List<Tuple<String, String>> details = this.getDetailsForFunction(this.getNodeForFunctionName(((DetailPanel)this.panels["detail"]).getSelectedNode()));
                ((DetailPanel)this.panels["detail"]).setDetails(details);
            }
            else if (this.currentMode == Mode.ClassView)
            {
                this.displayDependencyViewForClasses(((DetailPanel)this.panels["detail"]).getSelectedNode(), new Dictionary<string, string>());
                List<Tuple<String, String>> details = this.getDetailsForClass(this.getNodeForClassName(((DetailPanel)this.panels["detail"]).getSelectedNode()));
                ((DetailPanel)this.panels["detail"]).setDetails(details);
            }
        }

        private void initializePanels()
        {
            this.panels = new Dictionary<String, System.Windows.Forms.UserControl>();
            this.panels.Add("filter", new FilterPanel(this));
            this.panels.Add("detail", new DetailPanel(this));
            this.panels.Add("todo", new ToDoPanel(this));
            this.panels.Add("button", new ButtonPanel(this));
            this.panels.Add("tree", new TreeView(this));
        }

        private void setPanels(Actions action)
        {
            switch (action)
            {
                case Actions.SwitchToClassMode:
                    this.currentMode = Mode.ClassView;
                    ((FilterPanel)this.panels["filter"]).setElementDropdown(classNodes);
                    ((DetailPanel)this.panels["detail"]).setElementDropdown(classNodes);
                    this.setFilters();
                    break;
                case Actions.SwitchToFunctionMode:
                    this.currentMode = Mode.FunctionView;
                    ((FilterPanel)this.panels["filter"]).setElementDropdown(functionNodes);
                    ((DetailPanel)this.panels["detail"]).setElementDropdown(functionNodes);
                    this.setFilters();
                    break;
                case Actions.SwitchToDependencyView:
                    this.setFilters();
                    this.codeVisualizer.setLeftPanel((FilterPanel)this.panels["filter"]);
                    break;
                case Actions.SwitchToToDoView:
                    this.getToDos(this.functionNodes);
                    ((ToDoPanel)this.panels["todo"]).setElementDropdown(this.toDos.Keys.ToList<String>());
                    this.codeVisualizer.setLeftPanel(this.panels["todo"]);
                    break;
                case Actions.SwitchToDetailView:
                    this.showDetails();
                    this.codeVisualizer.setLeftPanel(this.panels["detail"]);
                    break;
            }
            this.updateTreeView();
        }

        private void setFilters()
        {
            List<Filter> reducedFilters = new List<Filter>();
            foreach (Filter filter in this.filters)
            {
                if (filter.mode == this.currentMode)
                {
                    reducedFilters.Add(filter);
                }
            }
            ((FilterPanel)this.panels["filter"]).addFilters(reducedFilters);
        }

        private void updateTreeView()
        {
            String selectedNode = ((FilterPanel)this.panels["filter"]).getSelectedFilterValue();
            Dictionary<String, String> filters = getSelectedFilters();
            if (this.currentMode == Mode.FunctionView)
            {
                this.displayDependencyViewForFunctions(selectedNode, filters);
            }
            else if (this.currentMode == Mode.ClassView)
            {
                this.displayDependencyViewForClasses(selectedNode, filters);
            }
        }

        private Dictionary<String, String> getSelectedFilters()
        {
            Dictionary<String, String> selectedFilters = new Dictionary<String, String>();
            foreach (System.Windows.Forms.RadioButton rb in ((FilterPanel)this.panels["filter"]).filterButtons)
            {
                if (rb.Checked && !rb.Name.ToLower().Contains("_all"))
                {
                    selectedFilters.Add(rb.Name.Split('_')[0], rb.Name.Split('_')[1]);
                }
            }
            return selectedFilters;
        }

        private void displayDependencyViewForFunctions(String node, Dictionary<String, String> filters)
        {
            ((TreeView)this.panels["tree"]).clearPanel();
            this.resetRectancleValues();
            TreeNode function = this.getNodeForFunctionName(node);
            
            if (function != null)
            {
                System.Drawing.Size panelSize = ((TreeView)this.panels["tree"]).chartPane.Size;
                ((TreeView)this.panels["tree"]).chartPane.addNode(function.nodeId, new RectangleNode(panelSize.Width / 2 - 75, panelSize.Height / 2 - 15, 150, 30, function.nodeContent));
                this.displayDependencyViewCallingFunctions(function, filters);
                this.displayDependencyViewCalledFunctions(function, filters);
            }
        }

        private void displayDependencyViewCallingFunctions(TreeNode function, Dictionary<String, String> filters)
        {
            AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
            List<TreeNode> callingFunctions = this.getCallingFunctions(function, filters);
          
            foreach (TreeNode callingFunction in callingFunctions)
            {
                TreeNode callingFunctionDeclaration = this.getCallingFunctionForFunctionCall(callingFunction);
                if (callingFunctionDeclaration.nodeId == function.nodeId)
                {
                    ((TreeView)this.panels["tree"]).chartPane.drawRecursiveCurve(function.nodeId);
                }
                else
                {
                    if (!((TreeView)this.panels["tree"]).chartPane.nodes.ContainsKey(callingFunctionDeclaration.nodeId))
                    {
                        RectangleNode rect = null;
                        do
                        {
                            rect = this.createNextRectangle(callingFunctionDeclaration.nodeContent);
                        }
                        while (((TreeView)this.panels["tree"]).chartPane.intersectsWith(rect));
                        ((TreeView)this.panels["tree"]).chartPane.addNode(callingFunctionDeclaration.nodeId, rect);
                    }
                    ((TreeView)this.panels["tree"]).chartPane.addConnection(function.nodeId, callingFunctionDeclaration.nodeId, bigArrow, null);
                }
            }
        }

        private void displayDependencyViewCalledFunctions(TreeNode function, Dictionary<String, String> filters)
        {
            AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
            List<TreeNode> calledFunctions = this.getCalledFunctions(function, filters);
          
            foreach (TreeNode calledFunction in calledFunctions)
            {
                TreeNode calledFunctionDeclaration = this.getFunctionDeclarationForFunctionCall(calledFunction);
                if (calledFunctionDeclaration != null && calledFunctionDeclaration.nodeId == function.nodeId)
                {
                    ((TreeView)this.panels["tree"]).chartPane.drawRecursiveCurve(function.nodeId);
                }
                else if(calledFunctionDeclaration != null)
                {
                    if (!((TreeView)this.panels["tree"]).chartPane.nodes.ContainsKey(calledFunctionDeclaration.nodeId))
                    {
                        RectangleNode rect = null;
                        do
                        {
                            rect = this.createNextRectangle(calledFunctionDeclaration.nodeContent);
                        }
                        while (((TreeView)this.panels["tree"]).chartPane.intersectsWith(rect));
                        ((TreeView)this.panels["tree"]).chartPane.addNode(calledFunctionDeclaration.nodeId, rect);
                    }
                    ((TreeView)this.panels["tree"]).chartPane.addConnection(function.nodeId, calledFunctionDeclaration.nodeId, null, bigArrow);
                }
            }
        }

        private void displayDependencyViewForClasses(String node, Dictionary<String, String> filters)
        {
            ((TreeView)this.panels["tree"]).clearPanel();
            this.resetRectancleValues();
            TreeNode classNode = this.getNodeForClassName(node);

            if(classNode != null)
            {
                List<TreeNode> functionDeclarations = this.getFunctionsInClass(classNode, filters);
                System.Drawing.Size panelSize = ((TreeView)this.panels["tree"]).chartPane.Size;

                ((TreeView)this.panels["tree"]).chartPane.addNode(classNode.nodeId, new RectangleNode(panelSize.Width / 2 - 75, panelSize.Height / 2 - 15, 150, 30, classNode.nodeContent));

                foreach (TreeNode function in functionDeclarations)
                {
                    RectangleNode rect = null;
                    do 
                    {
                        rect = this.createNextRectangle(function.nodeContent);
                    }
                    while(((TreeView)this.panels["tree"]).chartPane.intersectsWith(rect));

                    ((TreeView)this.panels["tree"]).chartPane.addNode(function.nodeId, rect);
                    ((TreeView)this.panels["tree"]).chartPane.addConnection(classNode.nodeId, function.nodeId, null, null);
                }
            }
        }

        private List<TreeNode> getFunctionsInClass(TreeNode classNode, Dictionary<String, String> filters)
        {
            List<TreeNode> functionDeclarations = new List<TreeNode>();
            foreach (TreeNode child in classNode.childNodes)
            {
                if (child.nodeType == NodeTypes.FunctionDeclaration && this.checkIfNodeIsWithinFilters(child, filters))
                {
                    functionDeclarations.Add(child);
                }
            }
            return functionDeclarations;
        }

        private bool checkIfNodeIsWithinFilters(TreeNode node, Dictionary<String, String> filters)
        {
            bool isWithinFilter = true;
            foreach (ExtendedAttribute ea in node.nodeAttributes)
            {
                if (ea.attributeType.Equals("filter") && filters.ContainsKey(ea.attributeName) && !filters[ea.attributeName].Equals(ea.attributeInformation))
                {
                    isWithinFilter = false;
                }
            }
            return isWithinFilter;
        }

        private TreeNode getCallingFunctionForFunctionCall(TreeNode call)
        {
            if (call.parentNode != null && call.parentNode.nodeType == NodeTypes.FunctionDeclaration)
            {
                return call.parentNode;
            }
            else if(call.parentNode != null)
            {
                return this.getCallingFunctionForFunctionCall(call.parentNode);
            }
            return null;
        }

        private TreeNode getFunctionDeclarationForFunctionCall(TreeNode call)
        {
            Guid functionId = new Guid();
            foreach (ExtendedAttribute ea in call.nodeAttributes)
            {
                if(ea.attributeType.Equals("functionid"))
                {
                    functionId = Guid.Parse(ea.attributeInformation);
                    break;
                }
            }
            foreach (TreeNode function in this.functionNodes)
            {
                if (function.nodeId == functionId)
                {
                    return function;
                }
            }
            return null;
        }

        private int rectangleCount;
        private int radius;
        private int arc;
        private void resetRectancleValues()
        {
            rectangleCount = 0;
            radius = 200;
            arc = 60;
        }

        private RectangleNode createNextRectangle(String label)
        {
            RectangleNode node = null;
            System.Drawing.Size panelSize = ((TreeView)this.panels["tree"]).chartPane.Size;
            System.Drawing.Point middle = new System.Drawing.Point(
                (int)Math.Floor(panelSize.Width / 2 + radius * Math.Cos(rectangleCount * arc * Math.PI / 180F)),
                (int)Math.Floor(panelSize.Height / 2 + radius * Math.Sin(rectangleCount * arc * Math.PI / 180F)));
            node = new RectangleNode(middle.X - 75, middle.Y - 15, 150, 50, label);
            rectangleCount++;
            if (rectangleCount % 6 == 0)
            {
                radius += 70;
                arc += 30;
            }

            return node;
        }

        private TreeNode getNodeForClassName(String name)
        {
            foreach (TreeNode node in this.classNodes)
            {
                if (node.nodeContent.Equals(name))
                {
                    return node;
                }
            }
            return null;
        }

        private TreeNode getNodeForFunctionName(String name)
        {
            foreach (TreeNode node in this.functionNodes)
            {
                if (node.nodeContent.Equals(name))
                {
                    return node;
                }
            }
            return null;
        }

        private List<TreeNode> getCallingFunctions(TreeNode functionNode, Dictionary<String, String> filters)
        {
            List<TreeNode> callingFunctions = new List<TreeNode>();

            foreach (TreeNode function in this.functionCalls)
            {
                foreach (ExtendedAttribute ea in function.nodeAttributes)
                {
                    if (ea.attributeType.Equals("functionid") && ea.attributeInformation.Equals(functionNode.nodeId.ToString())
                         && this.checkIfNodeIsWithinFilters(this.getCallingFunctionForFunctionCall(function), filters))
                    {
                        callingFunctions.Add(function);
                    }
                }
            }

            return callingFunctions;
        }

        private List<TreeNode> getCalledFunctions(TreeNode functionNode, Dictionary<String, String> filters)
        {
            List<TreeNode> calledFunctions = new List<TreeNode>();

            foreach (TreeNode function in functionNode.childNodes)
            {
                if (function.nodeType == NodeTypes.FunctionCall && this.checkIfNodeIsWithinFilters(function, filters))
                {
                    calledFunctions.Add(function);
                }
                else
                {
                    calledFunctions.AddRange(this.getCalledFunctions(function, filters));
                }
            }

            return calledFunctions;
        }

        private void loadAnalysisResult()
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.initializeControl();
                this.analysisResult = XMLReader.LoadXMLDocument(ofd.FileName);
                this.recurseParseClassNodes(this.analysisResult);
                this.recurseParseFunctionNodes(this.analysisResult);
                this.recurseParseFunctionCallNodes(this.analysisResult);
                this.getFilters();
                this.setPanels(Actions.SwitchToClassMode);
                this.setPanels(Actions.SwitchToDependencyView);
            }
        }

        private void recurseParseClassNodes(TreeNode node)
        {
            if (node.nodeType == NodeTypes.ClassDeclaration)
            {
                this.classNodes.Add(node);
            }
            else
            {
                foreach (TreeNode childNode in node.childNodes)
                {
                    this.recurseParseClassNodes(childNode);
                }
            }
        }

        private void recurseParseFunctionNodes(TreeNode node)
        {
            if (node.nodeType == NodeTypes.FunctionDeclaration)
            {
                this.functionNodes.Add(node);
            }
            else
            {
                foreach (TreeNode childNode in node.childNodes)
                {
                    this.recurseParseFunctionNodes(childNode);
                }
            }
        }

        private void recurseParseFunctionCallNodes(TreeNode node)
        {
            if (node.nodeType == NodeTypes.FunctionCall)
            {
                this.functionCalls.Add(node);
            }
            else
            {
                foreach (TreeNode childNode in node.childNodes)
                {
                    this.recurseParseFunctionCallNodes(childNode);
                }
            }
        }

        private void getFilters()
        {
            List<TreeNode> functions = this.functionCalls;
            functions.AddRange(this.functionNodes);
            this.getFiltersFrom(functions, Mode.FunctionView);
            this.getFiltersFrom(this.classNodes, Mode.ClassView);
        }

        private void getFiltersFrom(List<TreeNode> nodes, Mode cMode)
        {
            Dictionary<String, Filter> tFilters = new Dictionary<String, Filter>();
            foreach (TreeNode node in nodes)
            {
                foreach (ExtendedAttribute ea in node.nodeAttributes)
                {
                    if (ea.attributeType.Equals("filter"))
                    {
                        if (!tFilters.ContainsKey(ea.attributeName))
                        {
                            tFilters.Add(ea.attributeName, new Filter() { mode = cMode, filterName = ea.attributeName });
                            tFilters[ea.attributeName].filterValues.Add("All");
                        }
                        if (!tFilters[ea.attributeName].filterValues.Contains(ea.attributeInformation))
                        {
                            tFilters[ea.attributeName].filterValues.Add(ea.attributeInformation);
                        }
                    }
                }
            }
            this.filters.AddRange(tFilters.Values);
        }

        private void getToDos(List<TreeNode> nodes)
        {
            this.toDos = new Dictionary<string, List<ExtendedAttribute>>();

            foreach (TreeNode node in nodes)
            {
                foreach (ExtendedAttribute ea in node.nodeAttributes)
                {
                    if (ea.attributeType.Equals("intcodesmell"))
                    {
                        if (!this.toDos.ContainsKey(ea.attributeName))
                        {
                            this.toDos.Add(ea.attributeName, new List<ExtendedAttribute>());
                        }
                        this.toDos[ea.attributeName].Add(ea);
                    }
                }
            }
        }

        private List<Tuple<String, String>> getDetailsForFunction(TreeNode node)
        {
            List<Tuple<String, String>> details = new List<Tuple<String, String>>();
            if (node != null)
            {
                foreach (ExtendedAttribute ea in node.nodeAttributes)
                {
                    if (ea.attributeType.IsOneOf<String>("intcodesmell", "detail"))
                    {
                        details.Add(new Tuple<String, String>(ea.attributeName, ea.attributeInformation));
                    }
                }
                details.Add(new Tuple<String, String>(" ", " "));
                details.Add(new Tuple<String, String>("Number of IF Constructs", this.getChildNodeCountOfType(node, NodeTypes.Conditional).ToString()));
                details.Add(new Tuple<String, String>("Number of LOOP Constructs", this.getChildNodeCountOfType(node, NodeTypes.Loop).ToString()));
                details.Add(new Tuple<String, String>(" ", " "));
                details.Add(new Tuple<String, String>("Number of Variable Declarations", this.getChildNodeCountOfType(node, NodeTypes.VariableDeclaration).ToString()));
                details.Add(new Tuple<String, String>("Number of Function Calls", this.getChildNodeCountOfType(node, NodeTypes.FunctionCall).ToString()));
            }
            return details;
        }

        private List<Tuple<String, String>> getDetailsForClass(TreeNode node)
        {
            List<Tuple<String, String>> details = new List<Tuple<String, String>>();
            if (node != null)
            {
                foreach (ExtendedAttribute ea in node.nodeAttributes)
                {
                    if (ea.attributeType.IsOneOf<String>("intcodesmell", "detail"))
                    {
                        details.Add(new Tuple<String, String>(ea.attributeName, ea.attributeInformation));
                    }
                }
                details.Add(new Tuple<String, String>(" ", " "));
                details.Add(new Tuple<String, String>("Number of Function Declarations", this.getChildNodeCountOfType(node, NodeTypes.FunctionDeclaration).ToString()));
                details.Add(new Tuple<String, String>(" ", " "));
                details.Add(new Tuple<String, String>("Number of IF Constructs", this.getChildNodeCountOfType(node, NodeTypes.Conditional).ToString()));
                details.Add(new Tuple<String, String>("Number of LOOP Constructs", this.getChildNodeCountOfType(node, NodeTypes.Loop).ToString()));
                details.Add(new Tuple<String, String>(" ", " "));
                details.Add(new Tuple<String, String>("Number of Variable Declarations", this.getChildNodeCountOfType(node, NodeTypes.VariableDeclaration).ToString()));
                details.Add(new Tuple<String, String>("Number of Function Calls", this.getChildNodeCountOfType(node, NodeTypes.FunctionCall).ToString()));
            }
            return details;
        }

        private int getChildNodeCountOfType(TreeNode node, NodeTypes type)
        {
            int count = 0;
            foreach (TreeNode child in node.childNodes)
            {
                if (child.nodeType == type)
                {
                    count++;
                }
                count += this.getChildNodeCountOfType(child, type);
            }
            return count;
        }

        private List<ExtendedAttribute> getTopTodosForCategory(String category, int count)
        {
            List<ExtendedAttribute> topToDos = new List<ExtendedAttribute>();

            topToDos = this.toDos[category].OrderBy(p => Int32.Parse(p.attributeInformation)).ToList<ExtendedAttribute>();
            topToDos.Reverse();
            return topToDos.Take<ExtendedAttribute>(count).ToList<ExtendedAttribute>();
        }
    }
}
