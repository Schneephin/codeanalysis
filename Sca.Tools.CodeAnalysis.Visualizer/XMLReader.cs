using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Sca.Tools.CodeAnalysis.Utilities;
using System.IO;

namespace Sca.Tools.CodeAnalysis.Visualizer
{
    class XMLReader
    {
        private static XmlDocument xmlDocument { get; set; }

        public static TreeNode LoadXMLDocument(String filename)
        {
            xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(File.ReadAllText(filename));

            XmlNode root = xmlDocument.DocumentElement;

            TreeNode rootNode = XMLReader.recurseTreeNodes(root.ChildNodes[0]);

            return rootNode;
        }

        private static TreeNode recurseTreeNodes(XmlNode node)
        {
            TreeNode treeNode = new TreeNode(Guid.Parse(node.Attributes["guid"].Value));
            treeNode.nodeType = (NodeTypes)Enum.Parse(typeof(NodeTypes), node.Attributes["type"].Value);
            treeNode.nodeContent = node.Attributes["content"].Value;
            treeNode.nodeCategory = (NodeCategories)Enum.Parse(typeof(NodeCategories), node.Attributes["category"].Value);
            
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.LocalName.Equals("extendedAttribute"))
                {
                    ExtendedAttribute ea = XMLReader.readExtendedAttributeNode(childNode);
                    ea.parentNode = treeNode;
                    treeNode.addAttribute(ea);
                }
                else if (childNode.LocalName.Equals("node"))
                {
                    treeNode.addChildNode(XMLReader.recurseTreeNodes(childNode));
                }
            }

            return treeNode;
        }

        private static ExtendedAttribute readExtendedAttributeNode(XmlNode node)
        {
            ExtendedAttribute ea = new ExtendedAttribute();
            ea.attributeComment = node.Attributes["comment"].Value;
            ea.attributeInformation = node.Attributes["information"].Value;
            ea.attributeName = node.Attributes["name"].Value;
            ea.attributeType = node.Attributes["type"].Value;

            return ea;
        }
    }
}
