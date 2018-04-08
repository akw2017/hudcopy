using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.Models
{
    public enum DiagnosisMethod
    {
        //[EnumDescription(EnumValue = DiagnosisMethod.Energy)]
        [Description("能量")]
        Energy = 0,
        //[EnumDescription(EnumValue = DiagnosisMethod.FrequencyPeakValue)]
        [Description("频率峰值")]
        FrequencyPeakValue
    }
}
