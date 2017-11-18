using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.CoreType
{
    public enum CustomSystemDegree
    {
        [Description("异常")]
        Abnormal = 0, //异常
        [Description("正常")]
        Normal = 1,//正常
        [Description("预警")]
        PreAlarm = 2,//预警告
        [Description("警告")]
        Alarm = 3,//警告
        [Description("危险")]
        Danger = 4,//危险
        [Description("信息")]
        Information = 5,//信息
    }
}
