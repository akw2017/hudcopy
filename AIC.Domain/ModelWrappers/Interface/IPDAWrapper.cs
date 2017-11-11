using System.Collections.Generic;

namespace AIC.Domain
{
    public interface IPDAWrapper
    {
        string IP { get; set; }
        int Count { get; set; }
        IList<ICardWrapper> Cards { get; }
    }
}
