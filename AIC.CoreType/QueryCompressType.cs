using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum QueryCompressType
    {
        [EnumDescription(EnumValue = QueryCompressType.MaxAndMin_Normal)]
        MaxAndMin_Normal = 0,
        [EnumDescription(EnumValue = QueryCompressType.MaxAndMin_Absolute)]
        MaxAndMin_Absolute = 1,
        [EnumDescription(EnumValue = QueryCompressType.Sequence)]
        Sequence = 2,
    }
}
