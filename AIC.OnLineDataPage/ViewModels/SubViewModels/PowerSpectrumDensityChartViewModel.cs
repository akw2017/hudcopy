


using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using Akka.Actor;
using Arction.Wpf.Charting;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class PowerSpectrumDensityChartViewModel : ChartViewModelBase
    {
        public PowerSpectrumDensityChartViewModel(BaseAlarmSignal signal)
            : base(signal)
        {
          
        }

        public PowerSpectrumDensityChartViewModel(BaseAlarmSignal signal, bool isupdate) : this(signal)           
        {
            IsUpdated = isupdate;
        }

        public override void AddProcessor()
        {
            if (Signal is BaseWaveSignal)
            {
                ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.VData);
                ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.PowerSpectrumDensity);
            }
        }

        public override void RemoveProcessor()
        {
            if (Signal is BaseWaveSignal)
            {
                ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.VData);
                ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.PowerSpectrumDensity);
            }
        }
    }
}
