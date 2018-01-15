using AIC.Core.Models;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.PDAPage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public interface I_AbstractChannelInfo
    {

    }
    public class T2_IEPEChannelInfo : T1_IEPEChannelInfo, I_BaseChannelInfo//IEPE通道信息
    {
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
    }
    public class T2_EddyCurrentDisplacementChannelInfo : T1_EddyCurrentDisplacementChannelInfo, I_BaseChannelInfo//电涡流位移通道信息
    {
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
    }
    public class T2_EddyCurrentKeyPhaseChannelInfo : T1_EddyCurrentKeyPhaseChannelInfo, I_BaseChannelInfo//电涡流键相通道信息
    {
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
    }
    public class T2_EddyCurrentTachometerChannelInfo : T1_EddyCurrentTachometerChannelInfo, I_BaseChannelInfo//电涡流转速表通道信息
    {
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
    }
    public class T2_DigitTachometerChannelInfo : T1_DigitTachometerChannelInfo, I_BaseChannelInfo//数字转速表通道信息
    {
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
    }
    public class T2_AnalogRransducerInChannelInfo : T1_AnalogRransducerInChannelInfo, I_BaseChannelInfo//模拟变送器输入通道信息
    {
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
    }
    public class T2_RelayChannelInfo : T1_RelayChannelInfo, I_BaseChannelInfo//继电器通道信息
    {
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
    }
    public class T2_DigitRransducerInChannelInfo : T1_DigitRransducerInChannelInfo, I_BaseChannelInfo//数字变送器输入通道信息
    {
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
    }
    public class T2_DigitRransducerOutChannelInfo : T1_DigitRransducerOutChannelInfo, I_BaseChannelInfo//数字变送器输出通道信息
    {
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
    }
    public class T2_AnalogRransducerOutChannelInfo : T1_AnalogRransducerOutChannelInfo, I_BaseChannelInfo//模拟变送器输出通道信息
    {
        public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
    }

    public interface I_WirelessChannelExport
    {
        //T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
        string T_AbstractChannelInfo_Code { get; set; }
        string T_Item_Name { get; set; }
        int CHNum { get; set; }
        int SubCHNum { get; set; }
        bool IsBypass { get; set; }
        string Unit { get; set; }
        int DelayAlarmTime { get; set; }
        int NotOKDelayAlarmTime { get; set; }
        string MainControlCardIP { get; set; }
        string SlaveIdentifier { get; set; }//发送卡ID
        int SlotNum { get; set; }//槽号
        float? DangerValue { get; set; }
        bool? DangerIsAllow { get; set; }
        bool? DangerIsACQWave { get; set; }
        float? AlarmValue { get; set; }
        bool? AlarmIsAllow { get; set; }
        bool? AlarmIsACQWave { get; set; }
        float? PreAlarmValue { get; set; }
        bool? PreAlarmIsAllow { get; set; }
        bool? PreAlarmIsACQWave { get; set; }
        float? NormalValue { get; set; }
        bool? NormalIsAllow { get; set; }
        bool? NormalIsACQWave { get; set; }
        float? LowNormalValue { get; set; }
        bool? LowNormalIsAllow { get; set; }
        bool? LowNormalIsACQWave { get; set; }
        float? LowPreAlarmValue { get; set; }
        bool? LowPreAlarmIsAllow { get; set; }
        bool? LowPreAlarmIsACQWave { get; set; }
        float? LowAlarmValue { get; set; }
        bool? LowAlarmIsAllow { get; set; }
        bool? LowAlarmIsACQWave { get; set; }
        float? LowDangerValue { get; set; }
        bool? LowDangerIsAllow { get; set; }
        bool? LowDangerIsACQWave { get; set; }
        List<AlarmCategory> AlarmCategory{ get;set;}
        ImportOperate Operate { get; set; }//操作，增，删，改
        string Hint { get; set; }//提示
    }

    public class T2_WirelessScalarChannelInfo : T1_WirelessScalarChannelInfo, I_WirelessChannelExport//标量通道信息
    {
        //public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
        public string T_Item_Name { get; set; }
        public int CHNum { get; set; }
        public int SubCHNum { get; set; }
        public bool IsBypass { get; set; }
        public string Unit { get; set; }
        public int DelayAlarmTime { get; set; }
        public int NotOKDelayAlarmTime { get; set; }
        public string MainControlCardIP { get; set; }
        public string SlaveIdentifier { get; set; }//发送卡ID
        public int SlotNum { get; set; }//槽号
        public float? DangerValue { get; set; }
        public bool? DangerIsAllow { get; set; }
        public bool? DangerIsACQWave { get; set; }
        public float? AlarmValue { get; set; }
        public bool? AlarmIsAllow { get; set; }
        public bool? AlarmIsACQWave { get; set; }
        public float? PreAlarmValue { get; set; }
        public bool? PreAlarmIsAllow { get; set; }
        public bool? PreAlarmIsACQWave { get; set; }
        public float? NormalValue { get; set; }
        public bool? NormalIsAllow { get; set; }
        public bool? NormalIsACQWave { get; set; }
        public float? LowNormalValue { get; set; }
        public bool? LowNormalIsAllow { get; set; }
        public bool? LowNormalIsACQWave { get; set; }
        public float? LowPreAlarmValue { get; set; }
        public bool? LowPreAlarmIsAllow { get; set; }
        public bool? LowPreAlarmIsACQWave { get; set; }
        public float? LowAlarmValue { get; set; }
        public bool? LowAlarmIsAllow { get; set; }
        public bool? LowAlarmIsACQWave { get; set; }
        public float? LowDangerValue { get; set; }
        public bool? LowDangerIsAllow { get; set; }
        public bool? LowDangerIsACQWave { get; set; }
        public List<AlarmCategory> AlarmCategory { get; set; }
        public ImportOperate Operate { get; set; }//操作，增，删，改
        public string Hint { get; set; }//提示
      
    }

    public class T2_WirelessVibrationChannelInfo : T1_WirelessVibrationChannelInfo, I_WirelessChannelExport//振动通道信息
    {
        public float DefaultRPM { get; set; }//默认转速
        public float VelocityCalibration { get; set; }//速度校准       
        public float DisplacementCalibration { get; set; } //位移校准
        //public T1_AbstractChannelInfo T_AbstractChannelInfo { get; set; }
        public string T_Item_Name { get; set; }
        public int CHNum { get; set; }
        public int SubCHNum { get; set; }
        public bool IsBypass { get; set; }
        public string Unit { get; set; }
        public int DelayAlarmTime { get; set; }
        public int NotOKDelayAlarmTime { get; set; }
        public string MainControlCardIP { get; set; }
        public string SlaveIdentifier { get; set; }//发送卡ID
        public int SlotNum { get; set; }//槽号
        public float? DangerValue { get; set; }
        public bool? DangerIsAllow { get; set; }
        public bool? DangerIsACQWave { get; set; }
        public float? AlarmValue { get; set; }
        public bool? AlarmIsAllow { get; set; }
        public bool? AlarmIsACQWave { get; set; }
        public float? PreAlarmValue { get; set; }
        public bool? PreAlarmIsAllow { get; set; }
        public bool? PreAlarmIsACQWave { get; set; }
        public float? NormalValue { get; set; }
        public bool? NormalIsAllow { get; set; }
        public bool? NormalIsACQWave { get; set; }
        public float? LowNormalValue { get; set; }
        public bool? LowNormalIsAllow { get; set; }
        public bool? LowNormalIsACQWave { get; set; }
        public float? LowPreAlarmValue { get; set; }
        public bool? LowPreAlarmIsAllow { get; set; }
        public bool? LowPreAlarmIsACQWave { get; set; }
        public float? LowAlarmValue { get; set; }
        public bool? LowAlarmIsAllow { get; set; }
        public bool? LowAlarmIsACQWave { get; set; }
        public float? LowDangerValue { get; set; }
        public bool? LowDangerIsAllow { get; set; }
        public bool? LowDangerIsACQWave { get; set; }
        public List<AlarmCategory> AlarmCategory { get; set; }
        public ImportOperate Operate { get; set; }//操作，增，删，改
        public string Hint { get; set; }//提示
    }
}
