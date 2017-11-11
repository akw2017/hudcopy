


using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using Akka.Actor;
using Arction.Wpf.Charting;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class PowerSpectrumChartViewModel : ChartViewModelBase
    {
        public PowerSpectrumChartViewModel(BaseAlarmSignal signal)
            : base(signal)
        {
          
        }

        public PowerSpectrumChartViewModel(BaseAlarmSignal signal, bool isupdate) : this(signal)           
        {
            IsUpdated = isupdate;
        }

        public override void AddProcessor()
        {
            if (Signal is BaseWaveSignal)
            {
                ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.VData);
                ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.PowerSpectrum);
            }
        }

        public override void RemoveProcessor()
        {
            if (Signal is BaseWaveSignal)
            {
                ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.VData);
                ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.PowerSpectrum);
            }
        }
    }
}
