using System.Collections.Generic;

namespace AIC.Domain
{
    public interface ICardWrapper
    {
        string IP { get; set; }
        int CardNum { get; set; }
        int Count { get; set; }
        IList<IChannelWrapper> Channels { get; }
    }
}
