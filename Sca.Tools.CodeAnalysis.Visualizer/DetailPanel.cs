using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sca.Tools.CodeAnalysis.Visualizer
{
    internal partial class DetailPanel : UserControl
    {
        private Control ctrl;

        internal DetailPanel(Control ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
        }

        internal void setElementDropdown(List<Sca.Tools.CodeAnalysis.Utilities.TreeNode> elements)
        {
            this.cb_selectNode.Items.Clear();
            foreach (Sca.Tools.CodeAnalysis.Utilities.TreeNode element in elements)
            {
                this.cb_selectNode.Items.Add(new ComboBoxItem(element));
            }
        }

        internal String getSelectedNode()
        {
            return this.cb_selectNode.SelectedItem != null ? this.cb_selectNode.SelectedItem.ToString() : "";
        }

        internal void setDetails(List<Tuple<String, String>> details)
        {
            this.lv_results.Items.Clear();
            foreach (Tuple<String,String> detail in details)
            {
                ListViewItem item = new ListViewItem(new[] { detail.Item1, detail.Item2});
                this.lv_results.Items.Add(item);
            }
        }

        private void cb_selectNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.ShowDetails);
        }

    }
}
