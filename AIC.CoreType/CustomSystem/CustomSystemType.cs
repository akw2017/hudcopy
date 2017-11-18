using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.CoreType
{
    public enum CustomSystemType
    {
        [Description("报警")]
        Alarm = 201,
        [Description("断线")]
        DisConnect = 202,
        [Description("通道异常")]
        IsNotOK = 203,
    }
}
