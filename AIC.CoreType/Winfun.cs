using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum Winfun
    {
        [EnumDescription(EnumValue = Winfun.None)]
        None=0,
        [EnumDescription(EnumValue = Winfun.Hamming)]
        Hamming,
        [EnumDescription(EnumValue = Winfun.Haining)]
        Haining,
        [EnumDescription(EnumValue = Winfun.Triangle)]
        Triangle,
        [EnumDescription(EnumValue = Winfun.Blackman)]
        Blackman 
    }
}
