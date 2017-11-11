namespace AIC.AkkaSystem.Messages
{
    public class EnvelopeMessage
    {
        public EnvelopeMessage(double[] data)
        {
            Data = data;
        }

        public double[] Data { get; }
    }
}
