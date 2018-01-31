using AIC.Core.SignalModels;
using AIC.CoreType;
using System;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class RMSTrendChartViewModel : ChartViewModelBase
    {
        public RMSTrendChartViewModel(BaseAlarmSignal signal)
            : base(signal)
        {
          
        }

        public override void AddProcessor()
        {
            if (Signal is BaseAlarmSignal)
            {
                Signal.SignalProcessorTrend.Add(true);
                Console.WriteLine(Signal.SignalProcessorTrend.Count.ToString());
            }
        }

        public override void RemoveProcessor()
        {
            if (Signal is BaseAlarmSignal)
            {
                Signal.SignalProcessorTrend.Remove(true);
                Console.WriteLine(Signal.SignalProcessorTrend.Count.ToString());
            }
        }
    }
}
