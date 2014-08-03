using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Sca.Tools.CodeAnalysis.PlugIn.CSharp
{
    public class CSProjectResourceFile
    {
        public String ResourceFile { get; set; }
        public String DependsOn { get; set; }
        public String Generator { get; set; }
        public String LastGenOutput { get; set; }
        public ResourceSubTypes SubType { get; set; }

        public CSProjectResourceFile()
        {
            ResourceFile = "";
            DependsOn = "";
            Generator = "";
            LastGenOutput = "";
            SubType = ResourceSubTypes.None;
        }

        public CSProjectResourceFile(XmlNode objNodeResourceInformation)
            : this()
        {
            ResourceFile = objNodeResourceInformation.Attributes["Include"].Value;

            foreach (XmlNode objChildNode in objNodeResourceInformation)
            {
                if (objChildNode.Name == "SubType")
                {
                    switch (objChildNode.InnerText)
                    {
                        case "Designer":
                            SubType = ResourceSubTypes.Designer;
                            break;

                        default:
                            SubType = ResourceSubTypes.None;
                            break;
                    }
                }
                else if (objChildNode.Name == "DependentUpon")
                    DependsOn = objChildNode.InnerText;
                else if (objChildNode.Name == "Generator")
                    Generator = objChildNode.InnerText;
                else if (objChildNode.Name == "LastGenOutput")
                    LastGenOutput = objChildNode.InnerText;
            }
        }
    }
}
