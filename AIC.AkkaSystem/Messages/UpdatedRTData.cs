

namespace AIC.AkkaSystem.Messages
{
    public class UpdatedRTData
    {
        public UpdatedRTData(ChannelDataContract[] data)
        {
            Data = data;
        }
        public ChannelDataContract[] Data { get; }
    };
}
