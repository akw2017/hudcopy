using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum SampleType
    {
        [EnumDescription(EnumValue = SampleType.Transient)]
        Transient = 0,
        [EnumDescription(EnumValue = SampleType.Cycle)]
        Cycle = 1,
        [EnumDescription(EnumValue = SampleType.Continuous)]
        Continuous = 2,
    }
}
