using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;
using System.ComponentModel;

namespace AIC.CoreType
{
    public enum AVS
    {
        [EnumDescription(EnumValue = AVS.Acceleration)]
        Acceleration=0,

        [EnumDescription(EnumValue = AVS.Speed)]
        Speed,

        [EnumDescription(EnumValue = AVS.Displacement)]
        Displacement
    }
}
