using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticFilterModels
{
    public class DgLowPassFilter : BindableBase
    {
        public DgLowPassFilter()
        {
            PassbandAttenuationDB = 0.2;
            StopbandAttenuationDB = 60;
            PassbandFre = 400;
            StopbandFre = 600;
        }

        //通带衰减，建议值0.2
        public double PassbandAttenuationDB { get; set; }
        //阻带衰减，建议值60
        public double StopbandAttenuationDB { get; set; }
        //通带频率
        public double PassbandFre { get; set; }
        //阻带频率
        public double StopbandFre { get; set; }

        public DgLowPassFilter Clone()
        {
            return new DgLowPassFilter()
            {
                PassbandAttenuationDB = this.PassbandAttenuationDB,
                StopbandAttenuationDB = this.StopbandAttenuationDB,
                PassbandFre = this.PassbandFre,
                StopbandFre = this.StopbandFre,
            };
        }
    }
}
