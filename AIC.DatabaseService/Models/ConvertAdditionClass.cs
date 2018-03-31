using AIC.Core.HardwareModels;
using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService.Models
{
    class WaveInfo
    {
        public List<HighPassCategory> HighPassCategory { get; set; }//高通滤波
        public int HighPassCode { get; set; }//高通代码
        public List<WaveCategory> WaveCategory { get; set; }//波形种类
        public int WaveCode { get; set; } //波形代码
    }

    class EddyCurrentRPMSampleInfo
    {
        public List<EddyCurrentRPMSample> EddyCurrentRPMSample { get; set; }//采样方式
        public int EddyCurrentRPMCode { get; set; }//代码
    }

    class CalibrationlInfo
    {
        public float VelocityCalibration { get; set; }// 速度校准
        public float DisplacementCalibration { get; set; }//位移校准
    }

    class OtherInfo
    {
        public int RPMCardNum { get; set; }//转速卡号
        public int RPMSlotNum { get; set; }//转速槽号
        public int RPMCHNum { get; set; }//转速通道号
        public bool IsMultiplication { get; set; }//是否倍增
        public float MultiplicationCor { get; set; }//倍增系数
        public bool IsSaveWaveToSD { get; set; }//是否在SD存储波形
        public bool IsUploadWave { get; set; }//是否上传波形
        public float DefaultRPM { get; set; }//默认转速
    }

    class ThresholdInfo
    {
        public float ThresholdVolt { get; set; }//门槛值电压
        public float HysteresisVolt { get; set; }//迟滞电压
        public List<ThresholdModeCategory> ThresholdModeCategory { get; set; }//门槛值种类
        public int ThresholdModeCode { get; set; }//门槛值代码
    }

    class RPMCouplingInfo
    {
        public List<RPMCouplingCategory> RPMCouplingCategory { get; set; }//转速耦合种类
        public int RPMCouplingCode { get; set; }//耦合代码
    }

    class DigitRransducerInfo
    {
        public List<SwitchCategory> SwitchCategory { get; set; }//开关量总类
        public int SwitchCode { get; set; }//开关代码
        public List<ModBusFunCategory> ModBusFunCategory { get; set; }//功能码种类
        public int ModBusFunCode { get; set; }//功能代码
    }
}
