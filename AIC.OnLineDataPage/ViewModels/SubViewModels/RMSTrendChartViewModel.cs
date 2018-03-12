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
                Signal.AddProcessorTrend();
            }
        }

        public override void RemoveProcessor()
        {
            if (Signal is BaseAlarmSignal)
            {
                Signal.RemoveProcessorTrend();
            }
        }
    }
}
