


using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using Akka.Actor;
using Arction.Wpf.Charting;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class MultiDivFreChartViewModel : ChartViewModelBase
    {
        public MultiDivFreChartViewModel(BaseAlarmSignal signal)
            : base(signal)
        {
          
        }
    }
}
