using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum VAlarmSaveStr
    {
        [EnumDescription(EnumValue = VAlarmSaveStr.LM)]
        LM=0,
        [EnumDescription(EnumValue = VAlarmSaveStr.HostIntervalWave)]
        HostIntervalWave,
        [EnumDescription(EnumValue = VAlarmSaveStr.HostNotIntervalWave)]
        HostNotIntervalWave,
        [EnumDescription(EnumValue = VAlarmSaveStr.HostNotIntervalPoint)]
        HostNotIntervalPoint,
        [EnumDescription(EnumValue = VAlarmSaveStr.HostIntervalPoint)]
        HostIntervalPoint,
        [EnumDescription(EnumValue = VAlarmSaveStr.HostContinuousWave)]
        HostContinuousWave
    }
}
