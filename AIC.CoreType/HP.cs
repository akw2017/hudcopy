using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum HP
    {
        [EnumDescription(EnumValue = HP.Not)]
        Not,
        [EnumDescription(EnumValue = HP.Ten)]
        Ten,
        [EnumDescription(EnumValue = HP.FourHundred)]
        FourHundred,
        [EnumDescription(EnumValue = HP.OneThousand)]
        OneThousand
    }
}
