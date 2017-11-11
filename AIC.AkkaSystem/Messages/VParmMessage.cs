namespace AIC.AkkaSystem.Messages
{
    public class VParmMessage
    {
        public VParmMessage(double[] data)
        {
            Data = data;
        }

        public double[] Data { get; }
    }
}
