


using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using Arction.Wpf.Charting;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class PowerSpectrumDensityChartViewModel : ChartViewModelBase
    {
        public PowerSpectrumDensityChartViewModel(BaseAlarmSignal signal, bool isfilter = false, SignalPreProccessType signalPreType = SignalPreProccessType.None) : base(signal, isfilter, signalPreType)
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
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.PowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.Cepstrum:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.CepstrumVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.CepstrumPowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.TFF:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.TFFVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.TFFPowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.Envelope:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.EnvelopeVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.EnvelopePowerSpectrumDensity);
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
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterPowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.Cepstrum:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterCepstrumVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterCepstrumPowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.TFF:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterTFFVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterTFFPowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.Envelope:
                        {
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterEnvelopeVData);
                            ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.FilterEnvelopePowerSpectrumDensity);
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
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.PowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.Cepstrum:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.CepstrumVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.CepstrumPowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.TFF:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.TFFVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.TFFPowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.Envelope:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.EnvelopeVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.EnvelopePowerSpectrumDensity);
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
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterPowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.Cepstrum:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterCepstrumVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterCepstrumPowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.TFF:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterTFFVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterTFFPowerSpectrumDensity);
                            break;
                        }
                    case SignalPreProccessType.Envelope:
                        {
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterEnvelopeVData);
                            ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.FilterEnvelopePowerSpectrumDensity);
                            break;
                        }
                }
            }
        }
    }
}
