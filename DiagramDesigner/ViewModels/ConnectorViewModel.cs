using AIC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace DiagramDesigner
{
    public class ConnectorViewModel : SelectableDesignerItemViewModelBase
    {
        private Point sourceB;
        private Point sourceA;
        private List<Point> connectionPoints;
        private Point endPoint;
        private Rect area;

        private IDisposable sorucePropertyChangedSubscription;
        private IDisposable sinkPropertyChangedSubscription;
        private IDisposable sinkeOrientationChangedSubscription;

        public ConnectorViewModel(FullyCreatedConnectorInfo sourceConnectorInfo, FullyCreatedConnectorInfo sinkConnectorInfo) : base(string.Empty)
        {
            Init(sourceConnectorInfo, sinkConnectorInfo);
            DeleteConnectionCommand = new SimpleCommand(DeleteConnection);
        }

        public ConnectorViewModel(FullyCreatedConnectorInfo sourceConnectorInfo, ConnectorInfoBase sinkConnectorInfo)
        {
            Init(sourceConnectorInfo, sinkConnectorInfo);
        }

        private void DeleteConnection(object args)
        {
            if(this.Parent is DiagramViewModel)
            {
                var diagramVM = this.Parent as DiagramViewModel;
                diagramVM.RemoveItemCommand.Execute(this);
            }
        }

        public SimpleCommand DeleteConnectionCommand { get; set; }

        public  ConnectionElement DesignElement { get; set; }

        public static IPathFinder PathFinder { get; set; }

        public bool IsFullConnection
        {
            get { return sinkConnectorInfo is FullyCreatedConnectorInfo; }
        }

        public Point SourceA
        {
            get { return sourceA; }
            set
            {
                if (sourceA != value)
                {
                    sourceA = value;
                    UpdateArea();
                    NotifyChanged("SourceA");
                }
            }
        }

        public Point SourceB
        {
            get { return sourceB; }
            set
            {
                if (sourceB != value)
                {
                    sourceB = value;
                    UpdateArea();
                    NotifyChanged("SourceB");
                }
            }
        }

        public List<Point> ConnectionPoints
        {
            get
            {
                return connectionPoints;
            }
            private set
            {
                if (connectionPoints != value)
                {
                    connectionPoints = value;
                    NotifyChanged("ConnectionPoints");
                }
            }
        }

        public Point EndPoint
        {
            get
            {
                return endPoint;
            }
            private set
            {
                if (endPoint != value)
                {
                    endPoint = value;
                    NotifyChanged("EndPoint");
                }
            }
        }

        public Rect Area
        {
            get
            {
                return area;
            }
            private set
            {
                if (area != value)
                {
                    area = value;
                    UpdateConnectionPoints();
                    NotifyChanged("Area");
                }
            }
        }

        public ConnectorInfo ConnectorInfo(ConnectorOrientation orientation, double left, double top, Point position,double width,double height)
        {

            return new ConnectorInfo()
            {
                Orientation = orientation,
                DesignerItemSize = new Size(width, height),
                DesignerItemLeft = left,
                DesignerItemTop = top,
                Position = position

            };
        }

        private FullyCreatedConnectorInfo sourceConnectorInfo;
        public FullyCreatedConnectorInfo SourceConnectorInfo
        {
            get { return sourceConnectorInfo; }
            set
            {
                if (sourceConnectorInfo != value)
                {
                    sourceConnectorInfo = value;
                    SourceA = PointHelper.GetPointForConnector(this.SourceConnectorInfo);
                    NotifyChanged("SourceConnectorInfo");
                    if (sorucePropertyChangedSubscription!=null)
                    {
                        sorucePropertyChangedSubscription.Dispose();
                    }
                    sorucePropertyChangedSubscription = sourceConnectorInfo.DataItem.WhenPropertyChanged.Where(o => o.ToString() == "Left" || o.ToString() == "Top" || o.ToString() == "ItemWidth" || o.ToString() == "ItemHeight").Subscribe(OnSourcePropertyChanged);
                }
            }
        }

        private ConnectorInfoBase sinkConnectorInfo;
        public ConnectorInfoBase SinkConnectorInfo
        {
            get { return sinkConnectorInfo; }
            set
            {
                if (sinkConnectorInfo != value)
                {
                    sinkConnectorInfo = value;
                    if (SinkConnectorInfo is FullyCreatedConnectorInfo)
                    {
                        SourceB = PointHelper.GetSinkPointForConnector((FullyCreatedConnectorInfo)SinkConnectorInfo);
                    }
                    else
                    {
                        SourceB = ((PartCreatedConnectionInfo)SinkConnectorInfo).CurrentLocation;
                    }
                    NotifyChanged("SinkConnectorInfo");

                    if (SinkConnectorInfo is FullyCreatedConnectorInfo)
                    {
                        if (sinkPropertyChangedSubscription!=null)
                        {
                            sinkPropertyChangedSubscription.Dispose();
                        }
                        if (sinkeOrientationChangedSubscription != null)
                        {
                            sinkeOrientationChangedSubscription.Dispose();
                        }
                        sinkPropertyChangedSubscription = ((FullyCreatedConnectorInfo)sinkConnectorInfo).DataItem.WhenPropertyChanged.Where(o => o.ToString() == "Left" || o.ToString() == "Top" || o.ToString() == "ItemWidth" || o.ToString() == "ItemHeight").Subscribe(OnSinkePropertyChanged);
                        sinkeOrientationChangedSubscription = ((FullyCreatedConnectorInfo)sinkConnectorInfo).WhenPropertyChanged.Where(o => o.ToString() == "Orientation").Subscribe(OnSinkeOrientationChanged);
                    }
                }
            }
        }

        private void OnSourcePropertyChanged(string propertyName)
        {
            SourceA = PointHelper.GetPointForConnector(this.SourceConnectorInfo);
        }

        private void OnSinkePropertyChanged(string propertyName)
        {
            SourceB = PointHelper.GetSinkPointForConnector((FullyCreatedConnectorInfo)this.SinkConnectorInfo);
        }

        private void OnSinkeOrientationChanged(string propertyName)
        {
            SourceB = PointHelper.GetSinkPointForConnector((FullyCreatedConnectorInfo)this.SinkConnectorInfo);
        }

        private void UpdateArea()
        {
            Area = new Rect(SourceA, SourceB); 
        }

        private void UpdateConnectionPoints()
        {
            ConnectionPoints = new List<Point>()
                                   {                                     
                                       new Point( SourceA.X  <  SourceB.X ? 0d : Area.Width, SourceA.Y  <  SourceB.Y ? 0d : Area.Height ), 
                                       new Point(SourceA.X  >  SourceB.X ? 0d : Area.Width, SourceA.Y  >  SourceB.Y ? 0d : Area.Height)
                                   };

            ConnectorInfo sourceInfo = ConnectorInfo(SourceConnectorInfo.Orientation,
                                            ConnectionPoints[0].X,
                                            ConnectionPoints[0].Y,
                                            ConnectionPoints[0],
                                            SourceConnectorInfo.DataItem.ItemWidth,
                                            SourceConnectorInfo.DataItem.ItemHeight);

            if(IsFullConnection)
            {
                EndPoint = ConnectionPoints.Last();
                ConnectorInfo sinkInfo = ConnectorInfo(SinkConnectorInfo.Orientation,
                                  ConnectionPoints[1].X,
                                  ConnectionPoints[1].Y,
                                  ConnectionPoints[1],
                                  ((FullyCreatedConnectorInfo)SinkConnectorInfo).DataItem.ItemWidth,
                                  ((FullyCreatedConnectorInfo)SinkConnectorInfo).DataItem.ItemHeight);

                ConnectionPoints = PathFinder.GetConnectionLine(sourceInfo, sinkInfo, true);
            }
            else
            {
                ConnectionPoints = PathFinder.GetConnectionLine(sourceInfo, ConnectionPoints[1], ConnectorOrientation.Left);
                EndPoint = new Point();
            }
        }

        private void Init(FullyCreatedConnectorInfo sourceConnectorInfo, ConnectorInfoBase sinkConnectorInfo)
        {
            this.Parent = sourceConnectorInfo.DataItem.Parent;
            this.SourceConnectorInfo = sourceConnectorInfo;
            this.SinkConnectorInfo = sinkConnectorInfo;
          
      
            //sourceA = PointHelper.GetPointForConnector(sourceConnectorInfo);
            //if (sinkConnectorInfo is FullyCreatedConnectorInfo)
            //{
            //    sourceB = PointHelper.GetPointForConnector((FullyCreatedConnectorInfo)sinkConnectorInfo);
            //}
            //else
            //{
            //    sourceB = ((PartCreatedConnectionInfo)sinkConnectorInfo).CurrentLocation;
            //}
            //UpdateArea();
            PathFinder = new OrthogonalPathFinder();
        }

    }
}
