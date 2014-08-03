using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Sca.Tools.CodeAnalysis.PlugIn.CSharp
{
    public class CSProject
    {
        public List<CSProjectReference> References { get; private set; }
        public List<CSProjectResourceFile> Resources { get; private set; }
        public List<CSProjectSourceFile> SourceFiles { get; private set; }
        protected XmlDocument m_objXmlDocProject = null;
        protected XmlNamespaceManager m_objXmlNameSpaceManager = null;

        internal CSProject(String strProject)            
        {
            References = new List<CSProjectReference>();
            SourceFiles = new List<CSProjectSourceFile>();
            Resources = new List<CSProjectResourceFile>();

            LoadProject(strProject);
        }

        public bool LoadProject(String strProject)
        {
            bool bRet = false;

            m_objXmlDocProject = null;

            if (File.Exists(strProject))
            {
                try
                {
                    m_objXmlDocProject = new XmlDocument();
                    m_objXmlDocProject.Load(strProject);

                    m_objXmlNameSpaceManager = new XmlNamespaceManager(m_objXmlDocProject.NameTable);
                    m_objXmlNameSpaceManager.AddNamespace("def", m_objXmlDocProject.DocumentElement.NamespaceURI);

                    LoadReferences();
                    LoadResources();
                    LoadSourceFiles();

                    bRet = true;
                }
                catch (Exception)
                {
                    m_objXmlDocProject = null;
                }
            }

            return bRet;
        }

        private void LoadReferences()
        {
            References.Clear();

            XmlNodeList objNodeListReferences = m_objXmlDocProject.SelectNodes("./def:Project/def:ItemGroup/def:Reference", m_objXmlNameSpaceManager);
            foreach (XmlNode objNodeReference in objNodeListReferences)
            {
                CSProjectReference objVSReference = new CSProjectReference(objNodeReference);
                References.Add(objVSReference);
            }
        }

        private void LoadSourceFiles()
        {
            SourceFiles.Clear();

            XmlNodeList objNodeListSourceFiles = m_objXmlDocProject.SelectNodes("./def:Project/def:ItemGroup/def:Compile", 
                m_objXmlNameSpaceManager);
            foreach (XmlNode objNodeSourceFile in objNodeListSourceFiles)
            {
                CSProjectSourceFile objCSSource = new CSProjectSourceFile(objNodeSourceFile);
                SourceFiles.Add(objCSSource);
            }
        }

        private void LoadResources()
        {
            Resources.Clear();

            XmlNodeList objNodeListResource = m_objXmlDocProject.SelectNodes("./def:Project/def:ItemGroup/def:EmbeddedResource", m_objXmlNameSpaceManager);
            foreach (XmlNode objNodeResource in objNodeListResource)
            {
                CSProjectResourceFile objVSReference = new CSProjectResourceFile(objNodeResource);
                Resources.Add(objVSReference);
            }
        }
    }
}
