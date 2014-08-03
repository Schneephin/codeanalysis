using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sca.Tools.CodeAnalysis.Visualizer
{
    internal partial class CodeVisualizer : Form
    {
        private Control ctrl;

        internal CodeVisualizer(Control ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
        }

        internal void setCenterPanel(UserControl uc)
        {
            this.centerPanel.Controls.Clear();
            this.centerPanel.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }

        internal void setLeftPanel(UserControl uc)
        {
            this.leftPanel.Controls.Clear();
            this.leftPanel.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }

        internal void setBottomPanel(UserControl uc)
        {
            this.bottomPanel.Controls.Clear();
            this.bottomPanel.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.LoadAnalysisResult);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.End);
        }

        private void toDosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.SwitchToToDoView);
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.SwitchToDetailView);
        }

        private void dependenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrl.doAction(Actions.SwitchToDependencyView);
        }


    }
}
