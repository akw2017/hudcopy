namespace AIC.AkkaSystem.Messages
{
    public class WaveformMessage
    {
        public WaveformMessage(double[] vData,int samplePoint,double sampleFre)
        {
            VData = vData;
            SamplePoint = samplePoint;
            SampleFre = sampleFre;
        }

        public double[] VData { get; }
        public int SamplePoint { get; }
        public double SampleFre { get; }
    }
}
