using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using Akka.Actor;
using Arction.Wpf.Charting;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class BodeChartViewModel : ChartViewModelBase
    {
        public BodeChartViewModel(BaseAlarmSignal signal)
            : base(signal)
        {
          
        }
    }
}
