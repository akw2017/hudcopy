namespace AIC.AkkaSystem.Messages
{
    public class ByteToSingleMessage
    {
        public ByteToSingleMessage(double[] data)
        {
            Data = data;
        }

        public double[] Data { get; }
    }
}
