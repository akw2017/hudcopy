namespace AIC.AkkaSystem.Messages
{
    public class CepstrumMessage
    {
        public CepstrumMessage(double[] data)
        {
            Data = data;
        }
        public double[] Data { get; }
    }
}
