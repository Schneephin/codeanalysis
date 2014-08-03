using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Sca.Tools.CodeAnalysis.PlugIn.CSharp
{
    public class CSProjectReference
    {
        public String Include { get; set; }
        public String HintPath { get; set; }
        public bool SpecificVersion { get; set; }
        public bool CopyLocal { get; set; }
        public String RequiredTargetFramework { get; set; }
        public String EmbedInteropTypes { get; set; }

        public CSProjectReference()
        {
            Include = "";
            HintPath = "";
            RequiredTargetFramework = "";
            SpecificVersion = false;
            CopyLocal = false;
            EmbedInteropTypes = "";
        }

        public CSProjectReference(XmlNode objNodeReferenceInformation)
            : this()
        {
            Include = objNodeReferenceInformation.Attributes["Include"].Value;

            foreach (XmlNode objChildNode in objNodeReferenceInformation)
            {
              if (objChildNode.Name == "Private")
                CopyLocal = objChildNode.InnerText == "False" ? false : true;
              else if (objChildNode.Name == "SpecificVersion")
                SpecificVersion = objChildNode.InnerText == "False" ? false : true;
              else if (objChildNode.Name == "HintPath")
                HintPath = objChildNode.InnerText;
              else if (objChildNode.Name == "RequiredTargetFramework")
                RequiredTargetFramework = objChildNode.InnerText;
              else if (objChildNode.Name == "EmbedInteropTypes")
                  EmbedInteropTypes = objChildNode.InnerText;
            }
        }
    }
}
