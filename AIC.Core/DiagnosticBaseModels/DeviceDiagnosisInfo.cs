using AIC.Core.DiagnosticFilterModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticBaseModels
{
    public class DeviceDiagnosisInfo//设备诊断模型
    {
        public int DiagnosisMethod;//诊断方法;=0为能量方式 =1为频率峰值方式 默认为0.
        public bool IsFaultprobability; //是否显示故障概率
        public double HeadDivFreThreshold; //总分频门槛值，如DivFreThresholdProportionInfo.Threshold分频存在，忽略HeadDivFreThreshold，否则起作用。
        public double FreDiagnosisSetupInterval;//频率诊断设置间隔，默认值为1
        public double FrePeakFilterInterval;// 频率峰值过滤间隔，默认值为5
        public bool IsDeviceDiagnosis;//=1为多个测点诊断一台设备；=0一个测点诊断一台设备。
        public double KurtosisIndexThreshold;//峭度指标门槛值。
        public double MeanThreshold;//绝对平均值门槛值
        public double PeakIndexThreshold;//峰值指标门槛值
        public double PeakThreshold;//峰值门槛值
        public double PulseIndexThreshold;//脉冲指标门槛值
        public double RMSThreshold;//有效值门槛值。
        public ShaftInfo[] ShaftInfos;
        public SingleTestPointInfo SingleTestPointInfo;

    }

    public class DiagnoseResultInfo
    {
        public FaultInfo[] description;
        public string Error;
        public string Warning;
    }

    public class FaultInfo
    {
        public int Code;
        public string Fault;
        public string Harm;
        public string Proposal;
    }

    public class BearingInfo  //轴承信息
    {
        public string Name;
        public double PitchDiameter;  //节圆直径
        public double RollerDiameter;//滚子直径
        public double ContactAngle; //接触角
        public int NumberOfRoller; //滚子个数
        public double OuterRingDiameter;//外圈直径
        public double InnerRingDiameter; //内圈直径
        public int NumberOfColumns;//列数
    };
    public class BeltInfo  //皮带信息
    {
        public string Name;
        public double BeltLength;//皮带长度
        public double PulleyDiameter;//皮带轮长度。
    };
    public class GearInfo //齿轮信息
    {
        public string Name;
        public int TeethNumber;
    };
    public class ImpellerInfo //叶轮信息
    {
        public string Name;
        public int VaneNumber;//叶片数
    };
    public class MotorInfo//电机信息
    {
        public string Name;
        public double LineFrequency; //电网工频,单位HZ,默认值50
        public int Poles; //磁极数,必须为大于等于2的偶数，如2,4,6,8，… 默认值2
        public int RotorBars;//转子条数
        public int StatorCoils;//定子线圈数(同步电机)
        public int WindingSlots; //绕组槽数
        public int SCRs;//可控硅整流器数,只有3与6两种选择。默认值6
        public int MotorType; //电机类型,默认交流电机
    };
    public class NegationDivFreStrategyInfo //否定分频诊断策略
    {
        public int Code;  //故障代码
        public string Name;//故障名称
        public double RelativeY; //垂直相对值
        public double RelativeX;  //水平相对值
        public double RelativeZ;  //轴向相对值
    };
    public class NaturalFreInfo  //特征频率
    {
        public int DivFreType;//分频类型，=0倍频，=1固定分频，=2分频在一段范围
        public string Name; //故障名称
        public double Value1; //DivFreType=0时表示倍频，如1；DivFreType=1时表示固定分频，如80；DivFreType=2时频率起始值，如40；
        public double Value2;//DivFreType=2时频率结束值，如80；其它两种分频值为此项忽略。
        public string Proposal;//建议
        public string Harm;//危害
    };
    public class DivFreThresholdProportionInfo//分频门槛与加权
    {
        public int DivFreType;//分频类型，=0倍频，=1固定分频，=2分频在一段范围
        public string Name; //故障名称
        public double Value1; //DivFreType=0时表示倍频，如1；DivFreType=1时表示固定分频，如80；DivFreType=2时频率起始值，如40；
        public double Value2;//DivFreType=2时频率结束值，如80；其它两种分频值为此项忽略。
        public double Proportion;//加权值，必须>=0
        public double Threshold;//门槛值，>=0且<=100%
    };
    public class TestPointGroupInfo
    {
        public TestPointInfo X;
        public TestPointInfo Y;
        public TestPointInfo Z;
    }
    public class TestPointInfo //测点信息
    {
        public string Name; //测点名称
        public double SampleFre;//采样频率
        public int SamplePoint;//采样点数
        public string VData;// 经过反BASE64转换的data，即原始时域数据。
    };
    public class BandPassFilterInfo
    {
        public double PassbandAttenuationDB;
        //阻带衰减，建议值60
        public double StopbandAttenuationDB;
        //带通低逼近通带频率
        public double BPPassbandFreLow;
        //带通高逼近通带频率
        public double BPPassbandFreHigh;
        //带通低阻带频率
        public double BPStopbandFreLow;
        //带通高阻带频率
        public double BPStopbandFreHigh;
    }
    public class HighPassFilterInfo
    {
        //通带衰减，建议值0.2
        public double PassbandAttenuationDB;
        //阻带衰减，建议值60
        public double StopbandAttenuationDB;
        //通带频率
        public double PassbandFre;
        //阻带频率
        public double StopbandFre;
    }
    public class LowPassFilterInfo
    {
        //通带衰减，建议值0.2
        public double PassbandAttenuationDB;
        //阻带衰减，建议值60
        public double StopbandAttenuationDB;
        //通带频率
        public double PassbandFre;
        //阻带频率
        public double StopbandFre;
    }
    public class ShaftInfo
    {
        public string Name;//轴名称；
        public double DeltaRPM;//转速差；
        public double RPM;//轴转速
        public double RPMCoeff;//转速系数，默认值为1
        public bool IsSlidingBearing;//是否滑动轴承
        public BearingInfo[] BearingInfos;
        public GearInfo[] GearInfos;
        public BeltInfo[] BeltInfos;
        public ImpellerInfo[] ImpellerInfos;
        public MotorInfo[] MotorInfos;
        public NaturalFreInfo[] AddNaturalFres;
        public NaturalFreInfo[] DeleteNaturalFres;
        public DivFreThresholdProportionInfo[] DivFreThresholdProportions;
        public NegationDivFreStrategyInfo[] NegationDivFreStrategies;
        public TestPointGroupInfo[] TestPointGroupInfos;
        public int FilterType;//滤波类型，=0无滤波，=1带通滤波，=2高通滤波，=3低通滤波
        public bool BindRPMForFilter;
        public BandPassFilterInfo BandPassFilter;
        public HighPassFilterInfo HighPassFilter;
        public LowPassFilterInfo LowPassFilter;
    };
    public class SingleTestPointInfo
    {
        public double RPM;
        public string ShaftName;
        public TestPointGroupInfo TestPointGroupInfo;
    };
}
