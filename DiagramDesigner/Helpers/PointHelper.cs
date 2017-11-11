using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace DiagramDesigner
{
    public class PointHelper
    {
        public static Point GetPointForConnector(FullyCreatedConnectorInfo connector)
        {
            return new Point(connector.DataItem.Left + connector.DataItem.ItemWidth * connector.XRatio, connector.DataItem.Top + connector.DataItem.ItemHeight * connector.YRatio);
        }

        public static Point GetSinkPointForConnector(FullyCreatedConnectorInfo connector)
        {
            Point point = new Point();

            double left = connector.DataItem.Left;
            double top = connector.DataItem.Top;
            double with = connector.DataItem.ItemWidth;
            double height = connector.DataItem.ItemHeight;

            switch (connector.Orientation)
            {
                case ConnectorOrientation.Top:
                    point = new Point(left + with * connector.XRatio + ConnectorInfoBase.ConnectorWidth / 2, top + height * connector.YRatio);
                    break;
                case ConnectorOrientation.Bottom:
                    point = new Point(left + with * connector.XRatio + ConnectorInfoBase.ConnectorWidth / 2, top + height * connector.YRatio + ConnectorInfoBase.ConnectorHeight);
                    break;
                case ConnectorOrientation.Left:
                    point = new Point(left + with * connector.XRatio, top + height * connector.YRatio + ConnectorInfoBase.ConnectorHeight / 2);
                    break;
                case ConnectorOrientation.Right:
                    point = new Point(left + with * connector.XRatio + ConnectorInfoBase.ConnectorWidth, top + height * connector.YRatio + ConnectorInfoBase.ConnectorHeight / 2);
                    break;
            }

            return point;
        }

    }
}
