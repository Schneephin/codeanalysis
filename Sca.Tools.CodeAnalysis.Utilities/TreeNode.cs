using System;
using System.Collections.Generic;

namespace Sca.Tools.CodeAnalysis.Utilities
{
    public class TreeNode
    {
        public Guid nodeId { get; private set; }
        public TreeNode parentNode { get; set; }
        public List<TreeNode> childNodes { get; private set; }

        public String nodeContent { get; set; }
        public NodeTypes nodeType { get; set; } 
        public NodeCategories nodeCategory { get; set; } 

        public List<ExtendedAttribute> nodeAttributes { get; private set; }

        public TreeNode()
        {
            this.nodeId = Guid.NewGuid();
            this.parentNode = null;
            this.childNodes = new List<TreeNode>();
            this.nodeAttributes = new List<ExtendedAttribute>();
        }

        public TreeNode(Guid guid)
            : this()
        {
            this.nodeId = guid;
        }

        public void addChildNode(TreeNode childNode)
        {
            this.childNodes.Add(childNode);
            childNode.parentNode = this;
        }

        public void addAttribute(ExtendedAttribute attribute)
        {
            this.nodeAttributes.Add(attribute);
        }
    }
}
