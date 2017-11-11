using AIC.Core.SignalModels;
using System;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class RMSTrendChartViewModel : ChartViewModelBase
    {
        public RMSTrendChartViewModel(BaseAlarmSignal signal)
            : base(signal)
        {
          
        }

        public RMSTrendChartViewModel(BaseAlarmSignal signal, bool isupdate) : this(signal)           
        {
            IsUpdated = isupdate;
        }

        protected override bool Filter(object message)
        {
            return true;
        }
    }
}
