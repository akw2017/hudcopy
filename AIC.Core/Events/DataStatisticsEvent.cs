using AIC.Core.SignalModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Events
{
    public class DataStatisticsEvent : PubSubEvent<List<BaseAlarmSignal>>
    {
    }
}
