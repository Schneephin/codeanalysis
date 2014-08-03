using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Sca.Tools.CodeAnalysis.Utilities;

namespace Sca.Tools.CodeAnalysis.Framework
{
    public class XMLWriter
    {
        private static XmlDocument XmlDocument { get; set; }

        public static void CreateXmlDocument(TreeNode rootNode, String filename)
        {
            XmlDocument = new XmlDocument();

            XmlNode root = XmlDocument.CreateElement("root");
            XmlDocument.AppendChild(root);

            root.AppendChild(XMLWriter.recurseTreeNodes(rootNode));

            XmlDocument.Save(filename);
        }

        private static XmlNode recurseTreeNodes(TreeNode parent)
        {
            XmlNode node = XMLWriter.createNode(parent);
            XMLWriter.appendExtendedAttributes(node, parent);

            foreach (TreeNode childNode in parent.childNodes)
            {
                node.AppendChild(XMLWriter.recurseTreeNodes(childNode));
            }

            return node;
        }

        private static XmlNode createNode(TreeNode treeNode)
        {
            XmlNode node = XmlDocument.CreateElement("node");
            XmlAttribute type = XmlDocument.CreateAttribute("type");
            type.Value = Enum.GetName(typeof(NodeTypes), treeNode.nodeType);
            node.Attributes.Append(type);

            XmlAttribute guid = XmlDocument.CreateAttribute("guid");
            guid.Value = treeNode.nodeId.ToString();
            node.Attributes.Append(guid);

            XmlAttribute content = XmlDocument.CreateAttribute("content");
            content.Value = treeNode.nodeContent;
            node.Attributes.Append(content);

            XmlAttribute category = XmlDocument.CreateAttribute("category");
            category.Value = Enum.GetName(typeof(NodeCategories), treeNode.nodeCategory);
            node.Attributes.Append(category);

            return node;
        }

        private static void appendExtendedAttributes(XmlNode node, TreeNode treeNode)
        {
            foreach (ExtendedAttribute ea in treeNode.nodeAttributes)
            {
                XmlNode eaNode = XmlDocument.CreateElement("extendedAttribute");
                XmlAttribute type = XmlDocument.CreateAttribute("type");
                type.Value = ea.attributeType;
                eaNode.Attributes.Append(type);

                XmlAttribute name = XmlDocument.CreateAttribute("name");
                name.Value = ea.attributeName;
                eaNode.Attributes.Append(name);

                XmlAttribute information = XmlDocument.CreateAttribute("information");
                information.Value = ea.attributeInformation;
                eaNode.Attributes.Append(information);

                XmlAttribute comment = XmlDocument.CreateAttribute("comment");
                comment.Value = ea.attributeComment;
                eaNode.Attributes.Append(comment);

                node.AppendChild(eaNode);
            }
        }
    }
}
