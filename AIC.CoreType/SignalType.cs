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
    public enum SignalType
    {
        //[EnumDescription(EnumValue = SignalType.None)]
        None = -1,
        [EnumDescription(EnumValue = SignalType.Vibration)]
        Vibration = 0,
        [EnumDescription(EnumValue = SignalType.Analog)]
        Analog,
        [EnumDescription(EnumValue = SignalType.Digital)]
        Digital,
        [EnumDescription(EnumValue = SignalType.Composition)]
        Composition,
        [EnumDescription(EnumValue = SignalType.Video)]
        Video,
    }
}
