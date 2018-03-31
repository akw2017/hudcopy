


using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using Arction.Wpf.Charting;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class RPM3DChartViewModel : ChartViewModelBase
    {
        public RPM3DChartViewModel(BaseAlarmSignal signal)
            : base(signal)
        {
          
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
                ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.Frequency);
            }
        }
    }
}
