namespace AIC.Domain
{
    public class VibrationParm
    {
        public VibrationParm(
            double ams,
            double peakValue,
            double peakPeakValue,
            double slope,
            double kurtosis,
            double kurtosisIndex,
            double waveIndex,
            double peakIndex,
            double impulsionIndex,
            double rootAmplitude,
            double toleranceIndex)
        {
            AMS = ams;
            PeakValue = peakValue;
            PeakPeakValue = peakPeakValue;
            Slope = slope;
            Kurtosis = kurtosis;
            KurtosisIndex = kurtosisIndex;
            WaveIndex = waveIndex;
            PeakIndex = peakIndex;
            ImpulsionIndex = impulsionIndex;
            RootAmplitude = rootAmplitude;
            ToleranceIndex = toleranceIndex;
        }

        //有效值
        public double AMS { get; }
        //峰值
        public double PeakValue { get; }
        //峰峰值
        public double PeakPeakValue { get; }
        //斜度
        public double Slope { get; }
        //峭度
        public double Kurtosis { get; }
        //峭度指标
        public double KurtosisIndex { get; }
        //波形指标
        public double WaveIndex { get; }
        //峰值指标
        public double PeakIndex { get; }
        //脉冲指标
        public double ImpulsionIndex { get; }
        //方根幅值
        public double RootAmplitude { get; }
        //裕度指标
        public double ToleranceIndex { get; }
    }
}
