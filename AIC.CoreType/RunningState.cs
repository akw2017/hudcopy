using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum RunningState
    {
        [EnumDescription(EnumValue = RunningState.Start)]
        Start = 0,
        [EnumDescription(EnumValue = RunningState.Stop)]
        Stop,
        [EnumDescription(EnumValue = RunningState.Stable)]
        Stable,
    }
}
