using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Sca.Tools.ChartFramework
{
    public class Connector
    {
        public RectangleNode node1;
        public RectangleNode node2;
        public CustomLineCap node1Cap;
        public CustomLineCap node2Cap;
        public Point connectorPoint1;
        public Point connectorPoint2;

        public Connector(RectangleNode node1, RectangleNode node2, CustomLineCap node1Cap, CustomLineCap node2Cap)
        {
            this.node1 = node1;
            this.node2 = node2;
            this.node1Cap = node1Cap;
            this.node2Cap = node2Cap;
            this.calculateConnectorPoints();
        }

        private void calculateConnectorPoints()
        {
            Point topleft = node1.rectangle.Location;
            Point topright = new Point(node1.rectangle.X, node1.rectangle.Y + node1.rectangle.Height);
            Point bottomleft = new Point(node1.rectangle.X + node1.rectangle.Width, node1.rectangle.Y);
            Point bottomright = new Point(node1.rectangle.X + node1.rectangle.Width, node1.rectangle.Y + node1.rectangle.Height);
            Point middle = new Point(node2.rectangle.X + node2.rectangle.Width / 2, node2.rectangle.Y + node2.rectangle.Height / 2);

            if(this.isLeft(topleft, bottomright, middle) && this.isLeft(bottomleft, topright, middle))
            {
                this.connectorPoint1 = node1.getNodeConnector(ConnectorPoint.Left);
                this.connectorPoint2 = node2.getNodeConnector(ConnectorPoint.Right);
            }
            else if (this.isLeft(topleft, bottomright, middle) && !this.isLeft(bottomleft, topright, middle))
            {
                this.connectorPoint1 = node1.getNodeConnector(ConnectorPoint.Bottom);
                this.connectorPoint2 = node2.getNodeConnector(ConnectorPoint.Top);
            }
            else if (!this.isLeft(topleft, bottomright, middle) && this.isLeft(bottomleft, topright, middle))
            {
                this.connectorPoint1 = node1.getNodeConnector(ConnectorPoint.Top);
                this.connectorPoint2 = node2.getNodeConnector(ConnectorPoint.Bottom);
            }
            else if (!this.isLeft(topleft, bottomright, middle) && !this.isLeft(bottomleft, topright, middle))
            {
                this.connectorPoint1 = node1.getNodeConnector(ConnectorPoint.Right);
                this.connectorPoint2 = node2.getNodeConnector(ConnectorPoint.Left);
            }
        }

        private bool isLeft(Point a, Point b, Point c)
        {
            bool isLeft = ((b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X)) > 0;
            return isLeft;
        }

    }
}
