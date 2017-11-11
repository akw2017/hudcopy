using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows;

namespace DiagramDesigner
{
    public abstract class ConnectorInfoBase : INPCBase
    {
        private static double connectorWidth = 8;
        private static double connectorHeight = 8;

        public ConnectorInfoBase(ConnectorOrientation orientation)
        {
            this.Orientation = orientation;
        }


        private ConnectorOrientation orientation;
        public ConnectorOrientation Orientation
        {
            get { return orientation; }
            set
            {
                if (orientation != value)
                {
                    orientation = value;
                    NotifyChanged("Orientation");
                }
            }
        }

        public static double ConnectorWidth
        {
            get { return connectorWidth; }
        }

        public static double ConnectorHeight
        {
            get { return connectorHeight; }
        }
    }
}
