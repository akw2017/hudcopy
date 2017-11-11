//using NullGuard;
using AIC.CoreType;
using System;
using System.Collections.Generic;

namespace AIC.Domain
{
    public class Device : IMach
    {
        public Device()
        {
            HeadDivFreThreshold = 0.1;
            KurtosisIndexThreshold = 50;
            PeakIndexThreshold = 50;
            PulseIndexThreshold = 50;
            IsDeviceDiagnosis = true;
            FrePeakFilterInterval = 5.0;
            FreDiagnosisSetupInterval = 1;
        }
        public int ID { get; set; }
        public Guid DeviceID { get; set; }
        //[AllowNull]
        public string Name { get; set; }
        //[AllowNull]
        public List<ShaftComponent> Shafts { get; set; }
        //总分频门槛值，如DivFreThresholdProportionInfo.Threshold分频存在，忽略HeadDivFreThreshold，否则起作用。
        public double HeadDivFreThreshold { get; set; }
        //=1为多个测点诊断一台设备；=0一个测点诊断一台设备。
        public bool IsDeviceDiagnosis { get; set; }
        //峭度指标阀值
        public double KurtosisIndexThreshold { get; set; }
        //脉冲指标阀值
        public double PulseIndexThreshold { get; set; }
        //峰值指标阀值
        public double PeakIndexThreshold { get; set; }
        //频谱峰值筛选间隔，默认值为5
        public double FrePeakFilterInterval { get; set; }
        //频率诊断设置间隔，默认值为1
        public double FreDiagnosisSetupInterval { get; set; }
        //是否显示故障概率
        public bool IsFaultprobability { get; set; }
        public DiagnosisMethod DiagnosisMethod { get; set; }
    }
}
