using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum CurveType
    {
        [EnumDescription(EnumValue = CurveType.SignalTransient)]
        SignalTransient = 0,
        [EnumDescription(EnumValue = CurveType.DoubleCoupling)]
        DoubleCoupling,
        [EnumDescription(EnumValue = CurveType.Bode)]
        Bode,
        [EnumDescription(EnumValue = CurveType.Nyquist)]
        Nyquist,
        [EnumDescription(EnumValue = CurveType.Time3D)]
        Time3D,
        [EnumDescription(EnumValue = CurveType.RPM3D)]
        RPM3D,
        [EnumDescription(EnumValue = CurveType.SingleContinuous)]
        SingleContinuous
    
    }
}
