using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sca.Tools.ChartFramework;
using System.Drawing.Drawing2D;

namespace Sca.Tools.CodeAnalysis.Visualizer
{
    internal partial class TreeView : UserControl
    {
        public ChartPane chartPane;
        private Control ctrl;

        internal TreeView(Control ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
            this.clearPanel();
        }

        public void clearPanel()
        {
            this.tvPanel.Controls.Clear();
            this.chartPane = new ChartPane() { Dock = DockStyle.Fill };
            this.tvPanel.Controls.Add(this.chartPane);
        }

        public void addRectancle(Guid nodeId, RectangleNode node)
        {
            this.chartPane.addNode(nodeId, node);
        }

        public void addConnection(Guid node1, Guid node2, CustomLineCap cap1, CustomLineCap cap2)
        {
            this.chartPane.addConnection(node1, node2, cap1, cap2);
        }
    }
}
