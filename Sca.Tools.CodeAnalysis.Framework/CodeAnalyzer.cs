using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sca.Tools.CodeAnalysis.Utilities;

namespace Sca.Tools.CodeAnalysis.Framework
{
    public partial class CodeAnalyzer : Form
    {
        private Control ctrl;

        public CodeAnalyzer(Control ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;

            //Testcode
            //this.tb_path.Text = @"D:\LegacyCodeAnalysis\Final\Sca.Tools.CodeAnalysis.TestProject\Sca.Tools.CodeAnalysis.TestProject";
        }

        private void btn_path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tb_path.Text = fbd.SelectedPath;
                this.ctrl.doAction(Actions.SelectPath);
            }
        }

        public void fillLanguageDropdown(List<LanguagePlugin> languages)
        {
            cb_language.Items.Clear();
            foreach (LanguagePlugin language in languages)
            {
                cb_language.Items.Add(language.className);
            }
        }

        public void fillSourceFiles(List<String> sourceFiles)
        {
            lb_sourcefiles.Items.Clear();
            foreach (String file in sourceFiles)
            {
                lb_sourcefiles.Items.Add(file);
            }
        }

        public void fillPreview(Sca.Tools.CodeAnalysis.Utilities.TreeNode node)
        {
            System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode("Parsed Tree");
            this.recurseTreeNodes(root, node);
            this.tv_preview.Nodes.Add(root);
        }

        public void recurseTreeNodes(System.Windows.Forms.TreeNode parentNode, Sca.Tools.CodeAnalysis.Utilities.TreeNode analysisNode)
        {
            System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode(Enum.GetName(typeof(NodeTypes), analysisNode.nodeType));
            parentNode.Nodes.Add(node);
            foreach(Sca.Tools.CodeAnalysis.Utilities.TreeNode child in analysisNode.childNodes)
            {
                this.recurseTreeNodes(node, child);
            }

        }

        private void btn_startAnalyze_Click(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.Analyze);
        }

        private void sprachpluginsHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.AddLanguagePlugins);
        }

        public String getSelectedLanguage()
        {
            return cb_language.SelectedItem.ToString();
        }

        public String getSelectedPath()
        {
            return tb_path.Text;
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.SaveResults);
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.End);
        }

    }
}
