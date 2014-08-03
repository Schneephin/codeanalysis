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
    internal partial class FilterPanel : UserControl
    {
        private Control ctrl;
        public List<RadioButton> filterButtons;

        internal FilterPanel(Control ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
            this.filterButtons = new List<RadioButton>();
        }

        internal void setElementDropdown(List<Sca.Tools.CodeAnalysis.Utilities.TreeNode> elements)
        {
            this.cb_selectNode.Items.Clear();
            foreach (Sca.Tools.CodeAnalysis.Utilities.TreeNode element in elements)
            {
                this.cb_selectNode.Items.Add(new ComboBoxItem(element));
            }
        }

        private void cb_selectNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.FilterChanged);
        }

        internal String getSelectedFilterValue()
        {
            return this.cb_selectNode.SelectedItem != null ? this.cb_selectNode.SelectedItem.ToString() : "";
        }

        internal void addFilters(List<Filter> filters)
        {
            int distanceToTop = 60;
            RadioButton rb;
            Panel panel;
            Label lbl;
            Boolean first;
            foreach (Filter filter in filters)
            {
                distanceToTop += 30;
                first = true;
                lbl = new Label() { Name = filter.filterName, Left = 10, Text = filter.filterName, Top = distanceToTop, Height = 13 };
                this.Controls.Add(lbl);

                panel = new Panel() { Location = new Point(10, distanceToTop + 15), Size = new Size(300, filter.filterValues.Count * 15) };
                this.Controls.Add(panel);

                int distanceToTopInPanel = 0;
                foreach (String filterValue in filter.filterValues)
                {
                    rb = new RadioButton() { Name = filter.filterName + "_" + filterValue, Text = filterValue, Left = 10, Height = 13,
                        Checked = first, Top = distanceToTopInPanel };
                    rb.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
                    panel.Controls.Add(rb);
                    this.filterButtons.Add(rb);
                    first = false;
                    distanceToTopInPanel += 15;
                    distanceToTop += 15;
                }
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.FilterChanged);
        }


    }
}
