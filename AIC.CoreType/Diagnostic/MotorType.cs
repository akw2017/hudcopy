using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.CoreType
{
    public enum MotorType
    {
        [Description("交流")]
        AC = 0,
        [Description("直流")]
        DC
    }
}
