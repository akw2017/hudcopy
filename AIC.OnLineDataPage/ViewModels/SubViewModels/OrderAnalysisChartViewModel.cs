


using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using Akka.Actor;
using Arction.Wpf.Charting;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class OrderAnalysisChartViewModel : ChartViewModelBase
    {
        public OrderAnalysisChartViewModel(BaseAlarmSignal signal)
            : base(signal)
        {
          
        }

        public OrderAnalysisChartViewModel(BaseAlarmSignal signal, bool isupdate) : this(signal)           
        {
            IsUpdated = isupdate;
        }

        public override void AddProcessor()
        {
            if (Signal is BaseWaveSignal)
            {
                ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.VData);
                ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.Frequency);
            }
        }

        public override void RemoveProcessor()
        {
            if (Signal is BaseWaveSignal)
            {
                ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.VData);
                ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.Time);
            }
        }
    }
}
