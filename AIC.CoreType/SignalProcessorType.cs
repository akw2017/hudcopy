using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum SignalProcessorType
    {
        VData = 0,
        Time,
        Frequency,
        //Filter,
        //Envelope,
        //TFF,
        //Cepstrum,
        PowerSpectrum,
        PowerSpectrumDensity,

        FilterVData = 0,
        FilterTime,
        FilterFrequency,
        FilterPowerSpectrum,
        FilterPowerSpectrumDensity,

        //包络
        EnvelopeVData,
        EnvelopeTime,
        EnvelopeFrequency,
        EnvelopePowerSpectrum,
        EnvelopePowerSpectrumDensity,

        //TFF
        TFFVData,
        TFFTime,
        TFFFrequency,
        TFFPowerSpectrum,
        TFFPowerSpectrumDensity,

        //倒频谱
        CepstrumVData,
        CepstrumTime,
        CepstrumFrequency,
        CepstrumPowerSpectrum,
        CepstrumPowerSpectrumDensity,

        //过滤后包络
        FilterEnvelopeVData,
        FilterEnvelopeTime,
        FilterEnvelopeFrequency,
        FilterEnvelopePowerSpectrum,
        FilterEnvelopePowerSpectrumDensity,

        //过滤后TFF
        FilterTFFVData,
        FilterTFFTime,
        FilterTFFFrequency,
        FilterTFFPowerSpectrum,
        FilterTFFPowerSpectrumDensity,

        //过滤后倒频谱
        FilterCepstrumVData,
        FilterCepstrumTime,
        FilterCepstrumFrequency,
        FilterCepstrumPowerSpectrum,
        FilterCepstrumPowerSpectrumDensity,
    }
}
