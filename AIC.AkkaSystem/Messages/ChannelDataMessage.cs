

namespace AIC.AkkaSystem.Messages
{
    public class ChannelDataMessage
    {
        public ChannelDataMessage(ChannelDataContract channelData)
        {
            ChannelData = channelData;
        }

        public ChannelDataContract ChannelData { get; }
    }
}
