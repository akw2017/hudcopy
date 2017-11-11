
using AIC.Core.SignalModels;
using AIC.Domain;
using DiagramDesigner;



namespace AIC.OnLineDataPage.Models
{
    public class ValueBaseDesigner : DesignerItemViewModelBase
    {
        public ValueBaseDesigner(string id):base(id)
        {
            TopConnector = new FullyCreatedConnectorInfo(this, ConnectorOrientation.Top) { XRatio = 0.5, YRatio = 0 };
            BottomConnector = new FullyCreatedConnectorInfo(this, ConnectorOrientation.Bottom) { XRatio = 0.5, YRatio = 1 };
            LeftConnector = new FullyCreatedConnectorInfo(this, ConnectorOrientation.Left) { XRatio = 0, YRatio = 0.5 };
            RightConnector = new FullyCreatedConnectorInfo(this, ConnectorOrientation.Right) { XRatio = 1, YRatio = 0.5 };

            AddConnector(TopConnector);
            AddConnector(BottomConnector);
            AddConnector(LeftConnector);
            AddConnector(RightConnector);   
        }

        public FullyCreatedConnectorInfo TopConnector { get; private set; }
        public FullyCreatedConnectorInfo BottomConnector { get; private set; }
        public FullyCreatedConnectorInfo LeftConnector{ get; private set; }
        public FullyCreatedConnectorInfo RightConnector{ get; private set; }


        public virtual DesignElement DesignElement { get; set; }
        public virtual BaseAlarmSignal Signal { get; set; }
    }
}
