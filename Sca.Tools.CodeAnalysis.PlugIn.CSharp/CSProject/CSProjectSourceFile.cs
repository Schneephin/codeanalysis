using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Sca.Tools.CodeAnalysis.PlugIn.CSharp
{
    public class CSProjectSourceFile
    {
        public String Source { get; set; }
        public String DependsOn { get; set; }
        public SourceSubTypes SubType { get; set; }
        public bool AutoGen { get; set; }
        public bool DesignTime { get; set; }

        public CSProjectSourceFile()
        {
            Source = "";
            DependsOn = "";
            SubType = SourceSubTypes.None;
            AutoGen = false;
            DesignTime = false;
        }

        public CSProjectSourceFile(XmlNode objNodeSourceInformation)
            : this()
        {
            Source = objNodeSourceInformation.Attributes["Include"].Value;

            foreach (XmlNode objChildNode in objNodeSourceInformation)
            {
                if (objChildNode.Name == "SubType")
                {
                    switch (objChildNode.InnerText)
                    {
                        case "Form":
                            SubType = SourceSubTypes.Form;
                            break;
                        case "UserControl":
                            SubType = SourceSubTypes.UserControl;
                            break;
                        case "Component":
                            SubType = SourceSubTypes.Component;
                            break;
                        default:
                            SubType = SourceSubTypes.None;
                            break;
                    }
                }
                else if (objChildNode.Name == "DependentUpon")
                    DependsOn = objChildNode.InnerText;
                else if (objChildNode.Name == "AutoGen")
                    AutoGen = objChildNode.InnerText == "True" ? true : false;
                else if (objChildNode.Name == "DesignTime")
                    DesignTime = objChildNode.InnerText == "True" ? true : false;
            }
        }
    }
}
