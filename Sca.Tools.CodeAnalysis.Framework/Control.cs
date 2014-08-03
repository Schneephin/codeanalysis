using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Sca.Tools.CodeAnalysis.Utilities;

namespace Sca.Tools.CodeAnalysis.Framework
{
    enum Actions { AddLanguagePlugins, SelectPath, Analyze, SaveResults, End }

    public class Control
    {
        private List<LanguagePlugin> loadedLanguagePlugins;
        private CodeAnalyzer codeAnalyzer;
        private Sca.Tools.CodeAnalysis.Utilities.TreeNode analysisResults;

        public Control()
        {
            this.loadedLanguagePlugins = new List<LanguagePlugin>();
            this.codeAnalyzer = new CodeAnalyzer(this);

            //Testcode
            //this.loadedLanguagePlugins.AddRange(PluginReflector.addLanguagePlugin(@"D:\LegacyCodeAnalysis\Final\Sca.Tools.CodeAnalysisFramework\Sca.Tools.CodeAnalysis.PlugIn.CSharp\bin\Debug\Sca.Tools.CodeAnalysis.PlugIn.CSharp.dll"));
            //this.codeAnalyzer.fillLanguageDropdown(this.loadedLanguagePlugins);

            this.codeAnalyzer.ShowDialog();
            
        }

        internal void doAction(Actions action)
        {
            switch (action)
            {
                case Actions.AddLanguagePlugins:
                    this.addLanguagePlugin();
                    break;
                case Actions.SelectPath:
                    this.fillSourceFiles();
                    break;
                case Actions.Analyze:
                    this.analyzeCode();
                    break;
                case Actions.SaveResults:
                    this.saveAnalysisResults();
                    break;
                case Actions.End:
                    this.codeAnalyzer.Close();
                    break;
            }
        }

        private void addLanguagePlugin()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach(String filename in ofd.FileNames)
                {
                    this.loadedLanguagePlugins.AddRange(PluginReflector.addLanguagePlugin(filename));
                }
                this.codeAnalyzer.fillLanguageDropdown(this.loadedLanguagePlugins);
            }
        }

        private void analyzeCode()
        {
            LanguagePlugin languagePlugin = null;
            String path = "";
            try
            {
                languagePlugin = this.getSelectedLanguagePlugin(this.codeAnalyzer.getSelectedLanguage());
                path = this.codeAnalyzer.getSelectedPath();
            }
            catch (Exception)
            { }
            if (languagePlugin != null && Directory.Exists(path))
            {
                this.analysisResults = PluginReflector.analyzeSourceCode(languagePlugin, path);
                if (this.analysisResults != null)
                {
                    this.fillPreview();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please provide both source code and a language plug-in to start the analysis.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void saveAnalysisResults()
        {
            if (this.analysisResults != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    XMLWriter.CreateXmlDocument(this.analysisResults, sfd.FileName);
                }
            }
        }

        private LanguagePlugin getSelectedLanguagePlugin(String language)
        {
            foreach (LanguagePlugin languagePlugin in this.loadedLanguagePlugins)
            {
                if (languagePlugin.className.Equals(language))
                {
                    return languagePlugin;
                }
            }
            return null;
        }

        private void fillSourceFiles()
        {
            String filePath = this.codeAnalyzer.getSelectedPath();
            if (Directory.Exists(filePath))
            {
                String[] files = Directory.GetFiles(filePath, "*", SearchOption.AllDirectories).Select(path => Path.GetFileName(path)).ToArray();
                this.codeAnalyzer.fillSourceFiles(files.ToList<String>());
            }
        }

        private void fillPreview()
        {
            this.codeAnalyzer.fillPreview(this.analysisResults);
        }
    }
}
