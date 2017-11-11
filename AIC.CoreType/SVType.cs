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
    public enum SVType
    {
        [EnumDescription(EnumValue = SVType.RMS)]
        RMS,

        [EnumDescription(EnumValue = SVType.Peak)]
        Peak,

        [EnumDescription(EnumValue = SVType.PeakToPeak)]
        PeakToPeak,

        [EnumDescription(EnumValue = SVType.RPM)]
        RPM,

        [EnumDescription(EnumValue = SVType.AxialTranslation)]
        AxialTranslation
    }
}
