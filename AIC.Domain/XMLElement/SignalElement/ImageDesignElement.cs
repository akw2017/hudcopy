namespace AIC.Domain
{
    public class ImageDesignElement : DesignElement
    {
        public ConnectorElementCollection Connectors;
        public ImageDesignElement()
        {
            Connectors = new ConnectorElementCollection();
        }

        public string BackgroundImage { get; set; }
    }
}
