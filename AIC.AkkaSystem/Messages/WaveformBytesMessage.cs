namespace AIC.AkkaSystem.Messages
{
    public class WaveformBytesMessage
    {
        public WaveformBytesMessage(byte[] bytes)
        {
            Bytes = bytes;
        }

        public byte[] Bytes { get; }
    }
}
