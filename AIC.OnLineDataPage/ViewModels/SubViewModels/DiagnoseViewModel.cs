using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using Arction.Wpf.Charting;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class DiagnoseViewModel : ChartViewModelBase
    {
        public DiagnoseViewModel(BaseAlarmSignal signal)
            : base(signal)
        {

        }

    }
}
