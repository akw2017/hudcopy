namespace AIC.Domain
{
    public interface IChannelWrapper
    {
        string IP { get; set; }
        int CardNum { get; set; }
        int ChannelNum { get; set; }
    }
}
