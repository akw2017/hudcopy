namespace AIC.AkkaSystem.Messages
{
    public class VParmRequestMessage
    {
        public VParmRequestMessage(double[] data)
        {
            Data = data;
        }

        public double[] Data { get; }
    }
}
