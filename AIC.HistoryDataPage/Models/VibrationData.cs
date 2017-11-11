using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HistoryDataPage.Models
{
    public class VibrationData
    {
        public Guid SaveLab { get; set; }
        public DateTime ACQDatetime { get; set; }
        public string Unit { get; set; }
        public string RelatedChannelGlobalIndex { get; set; }
        public double SampleFre { get; set; }
        public int SamplePoint { get; set; }
        public double[] Waveform { get; set; }
        public double[] FilterWaveform { get; set; }
        public double[] PowerSpectrum { get; set; }
        public double[] PowerSpectrumDensity { get; set; }
        public int FFTLength { get { return Frequency != null ? Frequency.Length : 0; } }
        public double[] Frequency { get; set; }
        public double[] Amplitude { get; set; }
        public double[] Phase { get; set; }
        public double RPM { get; set; }
        public double TeethNumber { get; set; }
        public TriggerType Trigger { get; set; }

        //有效值
        public double AMS { get; set; }
        public double RMSValue { get; set; }
        public double PeakValue { get; set; }
        public double PeakPeakValue { get; set; }
        //斜度
        public double Slope { get; set; }
        //峭度
        public double Kurtosis { get; set; }
        //峭度指标
        public double KurtosisValue { get; set; }
        //波形指标
        public double WaveIndex { get; set; }
        //峰值指标
        public double PeakIndex { get; set; }
        //脉冲指标
        public double ImpulsionIndex { get; set; }
        //方根幅值
        public double RootAmplitude { get; set; }
        //裕度指标
        public double ToleranceIndex { get; set; }

    }
}
