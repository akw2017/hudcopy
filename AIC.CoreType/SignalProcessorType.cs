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
        Filter,
        Envelope,
        TFF,
        Cepstrum,
        PowerSpectrum,
        PowerSpectrumDensity,

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
    }
}
