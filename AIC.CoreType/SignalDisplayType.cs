using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum SignalDisplayType
    {
        [EnumDescription(EnumValue = SignalDisplayType.Value)]
        Value = 0,
        [EnumDescription(EnumValue = SignalDisplayType.TimeDomain)]
        TimeDomain,
        [EnumDescription(EnumValue = SignalDisplayType.FrequencyDomain)]
        FrequencyDomain,
        [EnumDescription(EnumValue = SignalDisplayType.AMSTrend)]
        AMSTrend,
        [EnumDescription(EnumValue = SignalDisplayType.MultiDivFre)]
        MultiDivFre,
        [EnumDescription(EnumValue = SignalDisplayType.SingleDivFre)]
        SingleDivFre,
        [EnumDescription(EnumValue = SignalDisplayType.Ortho)]
        Ortho,
        [EnumDescription(EnumValue = SignalDisplayType.Bode)]
        Bode,
        [EnumDescription(EnumValue = SignalDisplayType.Nyquist)]
        Nyquist,
        [EnumDescription(EnumValue = SignalDisplayType.Time3DSpectrum)]
        Time3DSpectrum,
        [EnumDescription(EnumValue = SignalDisplayType.RPM3D)]
        RPM3D,
        [EnumDescription(EnumValue = SignalDisplayType.PowerSpectrum)]
        PowerSpectrum,
        [EnumDescription(EnumValue = SignalDisplayType.PowerSpectrumDensity)]
        PowerSpectrumDensity,
        [EnumDescription(EnumValue = SignalDisplayType.OffDesignCondition)]
        OffDesignCondition,
        [EnumDescription(EnumValue = SignalDisplayType.OrderAnalysis)]
        OrderAnalysis,
        [EnumDescription(EnumValue = SignalDisplayType.AlarmPointTrend)]
        AlarmPointTrend,
    }
}
