using AIC.Core.SignalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Events
{
    public delegate void StatisticalInformationDataChangedEvent(Dictionary<string, List<Tuple<DateTime, int, int, int>>> statisticalresult);
}
