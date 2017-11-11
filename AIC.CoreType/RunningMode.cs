using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum RunningMode
    {
        [EnumDescription(EnumValue = RunningMode.Auto)]
        Auto = 0,
        [EnumDescription(EnumValue = RunningMode.Manual)]
        Manual,
    }
}
