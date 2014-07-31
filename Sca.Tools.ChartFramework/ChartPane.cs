using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Sca.Tools.ChartFramework
{
    public partial class ChartPane : UserControl
    {
        public Dictionary<Guid, RectangleNode> nodes {get; private set;}

        public ChartPane()
        {
            InitializeComponent();
            this.nodes = new Dictionary<Guid, RectangleNode>();
        }

        public void addNode(Guid nodeId, RectangleNode node)
        {
            this.nodes.Add(nodeId, node);
            this.drawNode(node);
        }

        public bool intersectsWith(RectangleNode node1)
        {
            foreach (RectangleNode node2 in this.nodes.Values)
            {
                if (node1.rectangle.IntersectsWith(node2.rectangle))
                {
                    return true;
                }
            }
            return false;
        }

        public void addConnection(Guid node1, Guid node2, CustomLineCap cap1, CustomLineCap cap2)
        {
            if (this.nodes.ContainsKey(node1) && this.nodes.ContainsKey(node2))
            {
                Connector con = new Connector(this.nodes[node1], this.nodes[node2], cap1, cap2);
                this.drawConnectorLine(con);
            }
        }

        public void drawRecursiveCurve(Guid guid)
        {
            if (this.nodes.ContainsKey(guid))
            {
                RectangleNode rn = this.nodes[guid];
                Pen pen = new Pen(Color.Black, 2);
                AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                pen.CustomEndCap = bigArrow;

                Point[] points = new Point[]
                {
                    new Point(rn.rectangle.Right - 10, rn.rectangle.Bottom),
                    new Point(rn.rectangle.Right + 10, rn.rectangle.Bottom + 10),
                    new Point(rn.rectangle.Right, rn.rectangle.Bottom - 10)
                };

                Graphics g = this.CreateGraphics();
                g.DrawCurve(pen, points);
            }
        }

        private void drawNode(RectangleNode rn)
        {
            Graphics g = this.CreateGraphics();
            g.DrawRectangle(new Pen(Color.Black), rn.rectangle);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(rn.label, new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, rn.rectangle, stringFormat);
        }

        private void drawConnectorLine(Connector connector)
        {
            Pen pen = new Pen(Color.Black, 2);
            if (connector.node1Cap != null)
            {
                pen.CustomStartCap = connector.node1Cap;
            }
            if (connector.node2Cap != null)
            {
                pen.CustomEndCap = connector.node2Cap;
            }
            
            Graphics g = this.CreateGraphics();
            g.DrawLine(pen, connector.connectorPoint1, connector.connectorPoint2);
        }
    }
}
