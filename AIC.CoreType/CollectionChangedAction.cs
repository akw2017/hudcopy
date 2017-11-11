using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum CollectionChangedAction
    {
        [EnumDescription(EnumValue = CollectionChangedAction.Add)]
        Add,
        [EnumDescription(EnumValue = CollectionChangedAction.Remove)]
        Remove
    }
}
