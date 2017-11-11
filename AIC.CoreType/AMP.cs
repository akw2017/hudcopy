using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum AMP
    {
        [EnumDescription(EnumValue = AMP.Auto)]
        Auto=0,
        [EnumDescription(EnumValue = AMP.One)]
        One=1,
        [EnumDescription(EnumValue = AMP.Two)]
        Two=2,
        [EnumDescription(EnumValue = AMP.Five)]
        Five=5,
        [EnumDescription(EnumValue = AMP.Ten)]
        Ten =10
    }
}
