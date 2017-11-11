using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum ThresholdMode
    {
        [EnumDescription(EnumValue = ThresholdMode.None)]
        None = 0,
        [EnumDescription(EnumValue = ThresholdMode.Manual)]
        Manual,
        [EnumDescription(EnumValue = ThresholdMode.Auto)]
        Auto,
    }
}
