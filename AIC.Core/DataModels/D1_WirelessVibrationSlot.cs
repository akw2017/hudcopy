using AIC.M9600.Common.DTO.Device;
using AIC.M9600.Common.SlaveDB.Generated;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DataModels
{
    public class D1_WirelessVibrationSlot : WirelessVibrationSlotData, IBaseDivfreSlot, INotifyPropertyChanged//无线振动表
    {
        //public int id { get; set; } //自增ID
        // public Guid? TestPointID { get; set; } //
        // public string ChannelHDID { get; set; } //通道硬件ID
        // public DateTime? ACQDatetime { get; set; } //采集时间
        // public int AlarmGrade { get; set; } //报警级别
        // public int ACQ_Unit_Type { get; set; } //数采器类型
        // public int AsySyn { get; set; } //异步/同步
        // public int MainCardCode { get; set; } //主板代码
        // public int SynWaveCode { get; set; } //波形代码
        // public bool IsHdBypass { get; set; } //硬件旁路
        // public bool IsHdMultiplication { get; set; } //硬件倍增
        // public string AlarmLimit { get; set; } //报警限值
        // //public int SubCHNum { get; set; } //子通道号
        // public Guid? RecordLab { get; set; } //guid
        // public Guid? SaveLab { get; set; } //guid
        // public Guid? ContinueLab { get; set; } //guid
        // //public string DebugInfo { get; set; } //调试信息
        // public object ExtraInfo { get; set; } //扩张信息
        // public int MountDegree { get; set; } //安装角度
        // public int TPDirCode { get; set; } //测点方向
        // public float Result { get; set; } //结果数据
        // public bool IsNotOK { get; set; } //通道是否正常
        // public bool IsValidCH { get; set; } //有效通道
        // public bool IsValidWave { get; set; } //有效通道
        // public string WaveUnit { get; set; } //波形单位
        // public string Unit { get; set; } //单位
        // public float RPM { get; set; } //转速
        // public bool IsMultiplication { get; set; } //是否倍增
        // public float MultiplicationCor { get; set; } //倍增系数
        // public DateTime? UploadDatetime { get; set; } //上传时间
        // public float BiasVoltHigh { get; set; } //偏置高电压
        // public float BiasVoltLow { get; set; } //偏置低电压
        // public float DisplacementCalibration { get; set; } //位移校准
        // public int Integration { get; set; } //积分
        // public bool IsBypass { get; set; } //是否旁路
        // public string Remarks { get; set; } //备注
        // public int SVTypeCode { get; set; } //单值类型代码
        // public int SampleFreCode { get; set; } //采样频率
        // public int SamplePointCode { get; set; } //采样点数
        // public float Sensitivity { get; set; } //灵敏度
        // //public int SlotNum { get; set; } //槽号
        // public Guid? T_Item_Guid { get; set; } //测点Guid
        // public float VelocityCalibration { get; set; } //速度校准
        // public string SlaveIdentifier { get; set; } //发送卡ID
        // public string MasterIdentifier { get; set; } //接受卡ID
        // public float BatteryEnergy { get; set; } //电量
        // //public int CHNum { get; set; } //通道号
        // public string ReceiveCardName { get; set; } //名称
        // public int SleepTime { get; set; } //休眠时间
        // public string TransmissionName { get; set; } //名称
        // public int TransmissionType { get; set; } //传输类型
        // public string Version { get; set; } //版本号
        // public int WorkTime { get; set; } //工作时间

        public new double? BatteryEnergy //用户登录名
        {
            get
            {
                return base.BatteryEnergy;
            }
            set
            {
                //name = value;
                base.BatteryEnergy = value;
                NotifyPropertyChange("BatteryEnergy");
            }
        }

        public new AlarmLimit[] AlarmLimit //
        {
            get
            {
                return base.AlarmLimit;
            }
            set
            {
                //name = value;
                base.AlarmLimit = value;
                NotifyPropertyChange("AlarmLimit");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
