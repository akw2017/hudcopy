﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticFilterModels
{
    [Serializable]
    public class DgBandPassFilter
    {
        public DgBandPassFilter()
        {
            PassbandAttenuationDB = 0.2;
            StopbandAttenuationDB = 60;
            BPPassbandFreLow = 400;
            BPPassbandFreHigh = 600;
            TransitionBandwidth = 100;
        }

        //通带衰减，建议值0.2
        public double PassbandAttenuationDB { get; set; }
        //阻带衰减，建议值60
        public double StopbandAttenuationDB { get; set; }
        //带通低逼近通带频率
        public double BPPassbandFreLow { get; set; }
        //带通高逼近通带频率;
        public double BPPassbandFreHigh { get; set; }
        //逼近带宽
        public double TransitionBandwidth { get; set; }
        //带通低阻带频率
        public double BPStopbandFreLow => BPPassbandFreLow - TransitionBandwidth;
        //带通高阻带频率
        public double BPStopBandFreHigh => BPPassbandFreHigh + TransitionBandwidth;

        public DgBandPassFilter Clone()
        {
            return new DgBandPassFilter()
            {
                PassbandAttenuationDB = this.PassbandAttenuationDB,
                StopbandAttenuationDB = this.StopbandAttenuationDB,
                BPPassbandFreLow = this.BPPassbandFreLow,
                BPPassbandFreHigh = this.BPPassbandFreHigh,
            };
        }
    }
}
