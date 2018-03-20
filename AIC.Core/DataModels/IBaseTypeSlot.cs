using AIC.M9600.Common.DTO.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DataModels
{
    public interface IBaseTypeSlot
    {
        long id { get; set; } //自增ID
        //Guid TestPointID { get; set; } //
        string ChannelHDID { get; set; } //通道硬件ID
        DateTime ACQDatetime { get; set; } //采集时间
        int AlarmGrade { get; set; } //报警级别
        byte? ACQ_Unit_Type { get; set; } //数采器类型
        byte? AsySyn { get; set; } //异步/同步
        byte? MainCardCode { get; set; } //主板代码
        byte? SynWaveCode { get; set; } //波形代码
        bool? IsHdBypass { get; set; } //硬件旁路
        bool? IsHdMultiplication { get; set; } //硬件倍增

        Guid? RecordLab { get; set; } //guid
        Guid? SaveLab { get; set; } //guid
        Guid? ContinueLab { get; set; } //guid      
        ExtraInfo ExtraInfo { get; set; } //扩张信息
        double? Result { get; set; } //结果数据
        bool? IsNotOK { get; set; } //通道是否正常
        bool? IsValidCH { get; set; } //有效通道
        string Unit { get; set; } //单位
        //byte? InSignalCode { get; set; } //输入信号代码
        bool? IsBypass { get; set; } //是否旁路
        string Remarks { get; set; } //备注
        AlarmLimit[] AlarmLimit { get; set; }
        //DateTime UploadDatetime { get; set; } //采集时间
    }

    public interface IBaseWaveSlot : IBaseAlarmSlot
    {
        string WaveUnit { get; set; } //波形单位   
        IWaveformData Waveform { get; set; } //波形数据
        bool? IsValidWave { get; set; }
        //double? SampleFre { get; set; }
        //int? SamplePoint { get; set; }
    }

    public interface IBaseDivfreSlot : IBaseWaveSlot
    {
        int? MountDegree { get; set; } //安装角度
        byte? TPDirCode { get; set; } //测点方向
        double? RPM { get; set; } //转速      
        bool? IsMultiplication { get; set; } //是否倍增
        double? MultiplicationCor { get; set; } //倍增系数
        double? BiasVoltHigh { get; set; } //偏置高电压
        double? BiasVoltLow { get; set; } //偏置低电压

        //WirelessVibrationSlotData_DivFreInfo[] DivFreInfo { get; set; }
    }

    public interface IBaseAlarmSlot : IBaseTypeSlot
    {
    }
}
