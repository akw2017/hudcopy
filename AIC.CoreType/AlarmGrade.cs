using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace AIC.CoreType
{
    public enum AlarmGrade
    {
        [EnumDescription(EnumValue = AlarmGrade.DisConnect)]
        DisConnect = -1,
        [EnumDescription(EnumValue = AlarmGrade.None)]
        None = 0,
        [EnumDescription(EnumValue = AlarmGrade.LowDanger)]
        LowDanger = 0x10000,//65536 + 4
        [EnumDescription(EnumValue = AlarmGrade.LowAlarm)]
        LowAlarm = 0x8000,//32768 + 3
        [EnumDescription(EnumValue = AlarmGrade.LowPreAlarm)]
        LowPreAlarm = 0x4000,//16384 + 2
        [EnumDescription(EnumValue = AlarmGrade.LowNormal)]
        LowNormal = 0x2000,//8192 + 1
        [EnumDescription(EnumValue = AlarmGrade.Invalid)]
        Invalid= 0x100,//256
        [EnumDescription(EnumValue = AlarmGrade.HighNormal)]
        HighNormal= 0x200,//512 + 1
        [EnumDescription(EnumValue = AlarmGrade.HighPreAlarm)]
        HighPreAlarm = 0x400,//1024 + 2
        [EnumDescription(EnumValue = AlarmGrade.HighAlarm)]
        HighAlarm = 0x800,//2048 + 3
        [EnumDescription(EnumValue = AlarmGrade.HighDanger)]
        HighDanger = 0x1000,//4096 + 4
        [EnumDescription(EnumValue = AlarmGrade.Abnormal)]
        Abnormal = 0x100,
        [EnumDescription(EnumValue = AlarmGrade.Normal)]
        Normal = 0x200,
        [EnumDescription(EnumValue = AlarmGrade.PreAlarm)]
        PreAlarm = 0x400,
        [EnumDescription(EnumValue = AlarmGrade.Alarm)]
        Alarm = 0x800,
        [EnumDescription(EnumValue = AlarmGrade.Danger)]
        Danger = 0x1000,
    }

    //AlarmGrade位划分：

    //第0--7位: 给服务用
    enum ServerAlarmGradeenum
    {
        ServerABNORMALCode = 0, //异常
        ServerNORMALCode = 1,//正常
        ServerPREWARNINGCode = 2,//预警告
        ServerWARNINGCode = 3,//警告
        ServerDANGERCode = 4//危险
    };

    //第8--23位: 上位机用--绝对值报警。
    enum LMAbsoluteAlarmGradeenum
    {
        LMAbsoluteABNORMALCode = 0x100,//异常,占用第8位

        LMAbsoluteNORMALCode = 0x200,//正常，占用第9位。
        LMAbsoluteHighNormalCode = LMAbsoluteNORMALCode,//高正常，占用第9位。

        LMAbsolutePreAlertCode = 0x400,//预警告，占用第10位。
        LMAbsoluteHighPreAlertCode = LMAbsolutePreAlertCode,//高预警告，占用第10位。

        LMAbsoluteAlertCode = 0x800,//警告，占用第11位。
        LMAbsoluteHighAlertCode = LMAbsoluteAlertCode,//高警告，占用第11位。

        LMAbsoluteDangerCode = 0x1000,//危险，占用第12位。
        LMAbsoluteHighDangerCode = LMAbsoluteDangerCode,//高危险，占用第12位。

        LMAbsoluteLowNormalCode = 0x2000,//低正常，占用第13位。
        LMAbsoluteLowPreAlertCode = 0x4000,//低预警告，占用第14位。
        LMAbsoluteLowAlertCode = 0x8000,//低警告，占用第15位。
        LMAbsoluteLowDangerCode = 0x10000,//低危险，占用第16位。
    };

    //第24--31位：上位机用--相对值报警。
    enum LMComparativeAlarmGradeenum
    {
        LMComparativeNORMALCode = 0x1000000,
        LMComparativeAlertCode = 0x2000000
    };


}
