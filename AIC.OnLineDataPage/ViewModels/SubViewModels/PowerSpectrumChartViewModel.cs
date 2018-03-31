


using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using Arction.Wpf.Charting;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class PowerSpectrumChartViewModel : ChartViewModelBase
    {
        public PowerSpectrumChartViewModel(BaseAlarmSignal signal, bool isfilter = false, SignalPreProccessType signalPreType = SignalPreProccessType.None) : base(signal, isfilter, signalPreType)
        {
          
        }

        public override void AddProcessor()
        {
            if (Signal is BaseWaveSignal)
            {
                SubAddProcessor(IsFilter, SignalPreProccessType);
            }
        }

        public override void RemoveProcessor()
        {
            if (Signal is BaseWaveSignal)
            {
                SubRemoveProcessor(IsFilter, SignalPreProccessType);
            }
        }

        public override void ChangeProcessor(SignalPreProccessType oldsignalPreType, SignalPreProccessType newsignalPreType)//切换预处理模式
        {
            base.ChangeProcessor(oldsignalPreType, newsignalPreType);
            if (Signal is BaseWaveSignal)
            {
                SubRemoveProcessor(IsFilter, oldsignalPreType);
                SubAddProcessor(IsFilter, newsignalPreType);
            }
        }

        public override void ChangeFilter(bool oldisFilter, bool newisFilter)
        {
            base.ChangeFilter(oldisFilter, newisFilter);
            if (Signal is BaseWaveSignal)
            {
                SubRemoveProcessor(oldisFilter, SignalPreProccessType);
                SubAddProcessor(newisFilter, SignalPreProccessType);
            }
        }

        private void SubAddProcessor(bool isfilter, SignalPreProccessType signalPreType)
        {
            if (isfilter == false)
            {
                switch (signalPreType)
                {
                    case SignalPreProccessType.None:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.VData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.PowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.Cepstrum:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.CepstrumVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.CepstrumPowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.TFF:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.TFFVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.TFFPowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.Envelope:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.EnvelopeVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.EnvelopePowerSpectrum);
                            break;
                        }
                }
            }
            else
            {
                switch (signalPreType)
                {
                    case SignalPreProccessType.None:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterPowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.Cepstrum:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterCepstrumVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterCepstrumPowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.TFF:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterTFFVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.TFFPowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.Envelope:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterEnvelopeVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterEnvelopePowerSpectrum);
                            break;
                        }
                }
            }
        }

        private void SubRemoveProcessor(bool isfilter, SignalPreProccessType signalPreType)
        {
            if (isfilter == false)
            {
                switch (signalPreType)
                {
                    case SignalPreProccessType.None:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.VData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.PowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.Cepstrum:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.CepstrumVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.CepstrumPowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.TFF:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.TFFVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.TFFPowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.Envelope:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.EnvelopeVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.EnvelopePowerSpectrum);
                            break;
                        }
                }
            }
            else
            {
                switch (signalPreType)
                {
                    case SignalPreProccessType.None:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterPowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.Cepstrum:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterCepstrumVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterCepstrumPowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.TFF:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterTFFVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterTFFPowerSpectrum);
                            break;
                        }
                    case SignalPreProccessType.Envelope:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterEnvelopeVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterEnvelopePowerSpectrum);
                            break;
                        }
                }
            }
        }
    }
}
