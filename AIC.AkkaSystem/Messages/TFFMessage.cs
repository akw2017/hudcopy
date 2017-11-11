namespace AIC.AkkaSystem.Messages
{
    public class TFFMessage
    {
        public TFFMessage(double[] data)
        {
            Data = data;
        }
        public double[] Data { get; }
    }
}
