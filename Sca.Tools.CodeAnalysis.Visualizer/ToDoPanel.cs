using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sca.Tools.CodeAnalysis.Utilities;

namespace Sca.Tools.CodeAnalysis.Visualizer
{
    internal partial class ToDoPanel : UserControl
    {
        private Control ctrl;

        internal ToDoPanel(Control ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
        }

        internal void setElementDropdown(List<String> elements)
        {
            this.cb_type.Items.Clear();
            this.cb_type.Items.AddRange(elements.ToArray<String>());
        }

        internal String getSelectedType()
        {
            return this.cb_type.SelectedItem.ToString();
        }

        internal void setToDos(List<ExtendedAttribute> toDos)
        {
            this.lv_results.Items.Clear();
            foreach (ExtendedAttribute toDo in toDos)
            {
                ListViewItem item = new ListViewItem(new[] { toDo.parentNode.nodeContent, toDo.attributeInformation });
                this.lv_results.Items.Add(item);
            }
        }

        private void cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.ShowToDos);
        }
    }
}
