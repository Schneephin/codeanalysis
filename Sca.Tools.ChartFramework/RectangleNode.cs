using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Sca.Tools.ChartFramework
{
    public enum ConnectorPoint{Top, Right, Bottom, Left}

    public class RectangleNode
    {
        public Rectangle rectangle;
        public String label;

        public RectangleNode(int left, int top, int width, int height, String label)
        {
            this.rectangle = new Rectangle(new Point(left, top), new Size(width, height));
            this.label = label;
        }

        public Point getNodeConnector(ConnectorPoint connectorPoint)
        {
            Point connector = new Point(0,0);
            switch (connectorPoint)
            {
                case ConnectorPoint.Top:
                    connector = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y);
                    break;
                case ConnectorPoint.Right:
                    connector = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height / 2);
                    break;
                case ConnectorPoint.Bottom:
                    connector = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height);
                    break;
                case ConnectorPoint.Left:
                    connector = new Point(rectangle.X, rectangle.Y + rectangle.Height / 2);
                    break;
            }
            return connector;
        }

    }
}
