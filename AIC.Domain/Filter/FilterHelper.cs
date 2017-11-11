using AIC.MatlabMath;
using System;

namespace AIC.Domain
{
    public static class FilterHelper
    {
        #region BandPass
        public static double[] Filter(this BandPassFilter filter, double[] input, int samplePoint, double sampleFre, double rpm = 0)
        {
            if (filter.BPPassbandFreHigh + filter.TransitionBandwidth > sampleFre / 2.56)
            {
                throw new Exception(string.Format("带通高阻带频率必须小于等于采样频率的1/2.56({0})", sampleFre / 2.56));
            }
            return Algorithm.Instance.BandPassFilter(input, samplePoint, sampleFre,
                   filter.PassbandAttenuationDB,
                   filter.StopbandAttenuationDB,
                   filter.BPStopbandFreLow,
                   filter.BPPassbandFreLow,
                   filter.BPPassbandFreHigh,
                   filter.BPStopBandFreHigh);
        }

        public static double[] FilterUponRPM(this BandPassFilter filter, double[] input, int samplePoint, double sampleFre, double rpm = 0)
        {
            if ((filter.BPPassbandFreHigh + filter.TransitionBandwidth) * (rpm / 60) > sampleFre / 2.56)
            {
                throw new Exception(string.Format("带通高阻带频率必须小于等于采样频率的1/2.56({0})", sampleFre / 2.56));
            }
            return Algorithm.Instance.BandPassFilter(input, samplePoint, sampleFre,
                filter.PassbandAttenuationDB,
                filter.StopbandAttenuationDB,
                filter.BPStopbandFreLow * (rpm / 60),
                filter.BPPassbandFreLow * (rpm / 60),
                filter.BPPassbandFreHigh * (rpm / 60),
                filter.BPStopBandFreHigh * (rpm / 60));
        }

        public static BandPassFilter Clone(this BandPassFilter filter)
        {
            return new BandPassFilter()
            {
                PassbandAttenuationDB = filter.PassbandAttenuationDB,
                StopbandAttenuationDB = filter.StopbandAttenuationDB,
                BPPassbandFreLow = filter.BPPassbandFreLow,
                BPPassbandFreHigh = filter.BPPassbandFreHigh,
                TransitionBandwidth = filter.TransitionBandwidth,
            };
        }
        #endregion

        #region HighPass
        public static double[] Filter(this HighPassFilter filter, double[] input, int samplePoint, double sampleFre, double rpm = 0)
        {
            if (filter.PassbandFre > sampleFre / 2.56)
            {
                throw new Exception(string.Format("通带频率必须小于等于采样频率的1/2.56({0})", sampleFre / 2.56));
            }
            return Algorithm.Instance.HighPassFilter(input, samplePoint, sampleFre,
                filter.PassbandFre,
                filter.StopbandFre,
                filter.PassbandAttenuationDB,
                filter.StopbandAttenuationDB);
        }

        public static double[] FilterUponRPM(this HighPassFilter filter, double[] input, int samplePoint, double sampleFre, double rpm = 0)
        {
            if (filter.PassbandFre * (rpm / 60) > sampleFre / 2.56)
            {
                throw new Exception(string.Format("通带频率必须小于等于采样频率的1/2.56({0})", sampleFre / 2.56));
            }
            return Algorithm.Instance.HighPassFilter(input, samplePoint, sampleFre,
                 filter.PassbandFre * (rpm / 60),
                filter.StopbandFre * (rpm / 60),
                filter.PassbandAttenuationDB,
                filter.StopbandAttenuationDB);
        }

        public static HighPassFilter Clone(this HighPassFilter filter)
        {
            return new HighPassFilter()
            {
                PassbandAttenuationDB = filter.PassbandAttenuationDB,
                StopbandAttenuationDB = filter.StopbandAttenuationDB,
                PassbandFre = filter.PassbandFre,
                StopbandFre = filter.StopbandFre
            };
        }
        #endregion

        #region LowPass
        public static double[] Filter(this LowPassFilter filter, double[] input, int samplePoint, double sampleFre, double rpm = 0)
        {
            if (filter.PassbandFre > sampleFre / 2.56)
            {
                throw new Exception(string.Format("通带频率必须小于等于采样频率的1/2.56({0})", sampleFre / 2.56));
            }
            return Algorithm.Instance.LowPassFilter(input, samplePoint, sampleFre,
                filter.PassbandFre,
                filter.StopbandFre,
                filter.PassbandAttenuationDB,
                filter.StopbandAttenuationDB);
        }

        public static double[] FilterUponRPM(this LowPassFilter filter, double[] input, int samplePoint, double sampleFre, double rpm = 0)
        {
            if (filter.PassbandFre * (rpm / 60) > sampleFre / 2.56)
            {
                throw new Exception(string.Format("通带频率必须小于等于采样频率的1/2.56({0})", sampleFre / 2.56));
            }
            return Algorithm.Instance.LowPassFilter(input, samplePoint, sampleFre,
                 filter.PassbandFre * (rpm / 60),
                filter.StopbandFre * (rpm / 60),
                filter.PassbandAttenuationDB,
                filter.StopbandAttenuationDB);
        }

        public static LowPassFilter Clone(this LowPassFilter filter)
        {
            return new LowPassFilter()
            {
                PassbandAttenuationDB = filter.PassbandAttenuationDB,
                StopbandAttenuationDB = filter.StopbandAttenuationDB,
                PassbandFre = filter.PassbandFre,
                StopbandFre = filter.StopbandFre
            };
        }
        #endregion
    }
}
