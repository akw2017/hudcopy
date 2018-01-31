using AIC.Core.Filters;
using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class BaseWaveSignal : BaseAlarmSignal
    {
        public BaseWaveSignal(Guid guid) : base(guid)
        {            
            SignalProcessTypes = new List<SignalProcessorType>();
        }
        protected override IEnumerable<SignalDisplayType> CreateSupportFunView()
        {
            var list = new List<SignalDisplayType>();
            list.AddRange(Enum.GetValues(typeof(SignalDisplayType)).OfType<SignalDisplayType>());
            return list;
        }

        public void AddProcess(SignalProcessorType tp)
        {            
            SignalProcessTypes.Add(tp);
            NeedProcess = true;
            Console.WriteLine(SignalProcessTypes.Count.ToString());
        }

        public void RemoveProcess(SignalProcessorType tp)
        {
            if (SignalProcessTypes.Contains(tp))
            {
                SignalProcessTypes.Remove(tp);
                if (SignalProcessTypes.Count == 0)
                {
                    NeedProcess = false;
                }
            }
            Console.WriteLine(SignalProcessTypes.Count.ToString());
        }

        #region 属性 
        public int MountDegree { get; set; }//安装角度
        public int TPDirCode { get; set; }//测点方向

        public bool IsValidWave { get; set; }//有效通道

        private string waveUnit;
        public string WaveUnit//波形单位        
        {
            get
            {
                return waveUnit;
            }
            set
            {
                waveUnit = value;
                OnPropertyChanged("WaveUnit");
            }
        }
        public float RPM { get; set; }//转速
        public bool IsMultiplication { get; set; }//是否倍增
        public float MultiplicationCor { get; set; }//倍增系数
        public float BiasVoltHigh { get; set; }//偏置高电压
        public float BiasVoltLow { get; set; }//偏置低电压  

        #region 诊断
        public bool IsDiagnostic { get; set; }

        private string diagnosticInfo = null;
        public string DiagnosticInfo//诊断信息        
        {
            get
            {
                return diagnosticInfo;
            }
            set
            {
                diagnosticInfo = value;
                OnPropertyChanged("DiagnosticInfo");
            }
        }

        private string diagnosticAdvice = null;
        public string DiagnosticAdvice//诊断信息        
        {
            get
            {
                return diagnosticAdvice;
            }
            set
            {
                diagnosticAdvice = value;
                OnPropertyChanged("DiagnosticAdvice");
            }
        }

        private DateTime? diagnosticTime;
        public DateTime? DiagnosticTime
        {
            get
            {
                return diagnosticTime;
            }
            set
            {
                diagnosticTime = value;
                OnPropertyChanged("DiagnosticTime");
            }
        }

        private double? diagnosticResult;
        public double? DiagnosticResult //结果数据  
        {
            get { return diagnosticResult; }
            set
            {
                if (diagnosticResult != value)
                {
                    diagnosticResult = value;
                    OnPropertyChanged("DiagnosticResult");
                }
            }
        }

        private AlarmGrade diagnosticGrade;
        public AlarmGrade DiagnosticGrade
        {
            get { return diagnosticGrade; }
            set
            {
                if (diagnosticGrade != value)
                {
                    diagnosticGrade = value;
                    OnPropertyChanged("DiagnosticGrade");
                }
            }
        }

        private string diagnosticUnit = null;
        public string DiagnosticUnit        
        {
            get
            {
                return diagnosticUnit;
            }
            set
            {
                diagnosticUnit = value;
                OnPropertyChanged("DiagnosticUnit");
            }
        }
        #endregion

        #region 波形

        public Dictionary<string, double[]> WaveformList { get; set; }
        public Dictionary<string, double[]> FrequencyList { get; set; }
        public Dictionary<string, double[]> AmplitudeList { get; set; }
        public Dictionary<string, double[]> PhaseList { get; set; }
        public Dictionary<string, double[]> PowerSpectrumList { get; set; }
        public Dictionary<string, double[]> PowerSpectrumDensityList { get; set; }
        //有效值
        public Dictionary<string,double> RmsValueList { get; set; }
        //峰值
        public Dictionary<string, double> PeakValueList { get; set; }
        //峰峰值
        public Dictionary<string, double> PeakPeakValueList { get; set; }
        //斜度
        public Dictionary<string, double> SlopeList { get; set; }
        //峭度
        public Dictionary<string, double> KurtosisList { get; set; }
        //峭度指标
        public Dictionary<string, double> KurtosisValueList { get; set; }
        //波形指标
        public Dictionary<string, double> WaveIndexList { get; set; }
        //峰值指标
        public Dictionary<string, double> PeakIndexList { get; set; }
        //脉冲指标
        public Dictionary<string, double> ImpulsionIndexList { get; set; }
        //方根幅值
        public Dictionary<string, double> RootAmplitudeList { get; set; }
        //裕度指标
        public Dictionary<string, double> ToleranceIndexList { get; set; }

        public int ProcessingFFTLength(string name)
        {
            if (FrequencyList != null && FrequencyList.Keys.Contains(name))
            {
                return FrequencyList[name].Length;
            }
            else
            {
                return 0;
            }
        }

        # region 过滤波形
        public double[] FilterWaveform { get; set; }
        public int FilterFFTLength { get { return FilterFrequency != null ? FilterFrequency.Length : 0; } }
        public double[] FilterFrequency { get; set; }
        public double[] FilterAmplitude { get; set; }
        public double[] FilterPhase { get; set; }
        public double[] FilterPowerSpectrum { get; set; }
        public double[] FilterPowerSpectrumDensity { get; set; }

        //有效值
        public double FilterRmsValue { get; set; }
        //峰值
        public double FilterPeakValue { get; set; }
        //峰峰值
        public double FilterPeakPeakValue { get; set; }
        //斜度
        public double FilterSlope { get; set; }
        //峭度
        public double FilterKurtosis { get; set; }
        //峭度指标
        public double FilterKurtosisValue { get; set; }
        //波形指标
        public double FilterWaveIndex { get; set; }
        //峰值指标
        public double FilterPeakIndex { get; set; }
        //脉冲指标
        public double FilterImpulsionIndex { get; set; }
        //方根幅值
        public double FilterRootAmplitude { get; set; }
        //裕度指标
        public double FilterToleranceIndex { get; set; }
        #endregion

        public double[] Waveform { get; set; }
        public int FFTLength { get { return Frequency != null ? Frequency.Length : 0; } }
        public double[] Frequency { get; set; }
        public double[] Amplitude { get; set; }
        public double[] Phase { get; set; }
        public double[] PowerSpectrum { get; set; }
        public double[] PowerSpectrumDensity { get; set; }

        private bool isPowerSpectrumDB = true;
        public bool IsPowerSpectrumDB
        {
            get { return isPowerSpectrumDB; }
            set
            {
                if (value != isPowerSpectrumDB)
                {
                    isPowerSpectrumDB = value;
                    OnPropertyChanged("IsPowerSpectrumDB");
                }
            }
        }       

        private bool isPowerSpectrumDensityDB = true;
        public bool IsPowerSpectrumDensityDB
        {
            get { return isPowerSpectrumDensityDB; }
            set
            {
                if (value != isPowerSpectrumDensityDB)
                {
                    isPowerSpectrumDensityDB = value;
                    OnPropertyChanged("isPowerSpectrumDensityDB");
                }
            }
        }

        //有效值
        public double RmsValue { get; set; }
        //峰值
        public double PeakValue { get; set; }
        //峰峰值
        public double PeakPeakValue { get; set; }
        //斜度
        public double Slope { get; set; }
        //峭度
        public double Kurtosis { get; set; }
        //峭度指标
        public double KurtosisValue { get; set; }
        //波形指标
        public double WaveIndex { get; set; }
        //峰值指标
        public double PeakIndex { get; set; }
        //脉冲指标
        public double ImpulsionIndex { get; set; }
        //方根幅值
        public double RootAmplitude { get; set; }
        //裕度指标
        public double ToleranceIndex { get; set; }

        private BandPassFilter bpFilter;
        public BandPassFilter BPFilter
        {
            get
            {
                if (bpFilter == null)
                {
                    bpFilter = new BandPassFilter(SampleFre);
                }
                return bpFilter;
            }
        }

        private HighPassFilter hpFilter;
        public HighPassFilter HPFilter
        {
            get
            {
                if (hpFilter == null)
                {
                    hpFilter = new HighPassFilter(SampleFre);
                }
                return hpFilter;
            }
        }

        private LowPassFilter lpFilter;
        public LowPassFilter LPFilter
        {
            get
            {
                if (lpFilter == null)
                {
                    lpFilter = new LowPassFilter(SampleFre);
                }
                return lpFilter;
            }
        }

        private FilterType filterType = FilterType.BandPass;
        public FilterType FilterType
        {
            get { return filterType; }
            set
            {
                if (filterType != value)
                {
                    filterType = value;
                    OnPropertyChanged("FilterType");
                }
            }
        }
       
        public List<SignalProcessorType> SignalProcessTypes { get ; set; }
        public bool NeedProcess { get;  set; }

        private double gap;
        public double Gap
        {
            get { return gap; }
            set
            {
                if (gap != value)
                {
                    gap = value;
                    OnPropertyChanged("Gap");
                }
            }
        }       

        private SampleType sampleType;
        public SampleType SampleType
        {
            get { return sampleType; }
            set
            {
                if (value != sampleType)
                {
                    sampleType = value;
                    OnPropertyChanged("SampleType");
                }
            }
        }

        private int? hibernationTime;
        public int? HibernationTime
        {
            get { return hibernationTime; }
            set
            {
                if (value != hibernationTime)
                {
                    hibernationTime = value;
                    OnPropertyChanged("HibernationTime");
                }
            }
        }

        private int? workTime;
        public int? WorkTime
        {
            get { return workTime; }
            set
            {
                if (value != workTime)
                {
                    workTime = value;
                    OnPropertyChanged("WorkTime");
                }
            }
        }

        private string wrelessMAC;
        public string WirelessMAC
        {
            get { return wrelessMAC; }
            set
            {
                if (value != wrelessMAC)
                {
                    wrelessMAC = value;
                    OnPropertyChanged("WirelessMAC");
                }
            }
        }

        #region For Vibration Signal
        //数据源类型
        private nType ntype;
        public nType nType
        {
            get { return ntype; }
            set
            {
                if (ntype != value)
                {
                    ntype = value;
                    OnPropertyChanged("nType");
                }
            }
        }

        //传感器类型
        private TPDir vMSDir;
        public TPDir VMSDir
        {
            get { return vMSDir; }
            set
            {
                if (vMSDir != value)
                {
                    vMSDir = value;
                    OnPropertyChanged("VMSDir");
                }
            }
        }

        //灵敏度
        private double sensitivity;
        public double Sensitivity
        {
            get { return sensitivity; }
            set
            {
                if (sensitivity != value)
                {
                    sensitivity = value;
                    OnPropertyChanged("Sensitivity");
                }
            }
        }

        //传感器类型
        private SensorType sensorType;
        public SensorType SensorType
        {
            get { return sensorType; }
            set
            {
                if (sensorType != value)
                {
                    sensorType = value;
                    OnPropertyChanged("SensorType");
                }
            }
        }

        //单值类型
        private SVType sVType;
        public SVType SVType
        {
            get { return sVType; }
            set
            {
                if (sVType != value)
                {
                    sVType = value;
                    OnPropertyChanged("SVType");
                }
            }
        }       

        private int slotNum;
        public int SlotNum
        {
            get { return slotNum; }
            set
            {
                if (slotNum != value)
                {
                    slotNum = value;
                    OnPropertyChanged("SlotNum");
                }
            }
        }

        private int chaN;
        public int ChaN
        {
            get { return chaN; }
            set
            {
                if (chaN != value)
                {
                    chaN = value;
                    OnPropertyChanged("ChaN");
                }
            }
        }

        private int rSlotNum;
        public int RSlotNum
        {
            get { return rSlotNum; }
            set
            {
                if (rSlotNum != value)
                {
                    rSlotNum = value;
                    OnPropertyChanged("RSlotNum");
                }
            }
        }

        private int rChaN;
        public int RChaN
        {
            get { return rChaN; }
            set
            {
                if (rChaN != value)
                {
                    rChaN = value;
                    OnPropertyChanged("RChaN");
                }
            }
        }

        private int hdRSlotNum;
        public int HDRSlotNum
        {
            get { return hdRSlotNum; }
            set
            {
                if (hdRSlotNum != value)
                {
                    hdRSlotNum = value;
                    OnPropertyChanged("HDRSlotNum");
                }
            }
        }

        private int hdRChaN;
        public int HDRChaN
        {
            get { return hdRChaN; }
            set
            {
                if (hdRChaN != value)
                {
                    hdRChaN = value;
                    OnPropertyChanged("HDRChaN");
                }
            }
        }

        private int tSpan;
        public int TSpan
        {
            get { return tSpan; }
            set
            {
                if (tSpan != value)
                {
                    tSpan = value;
                    OnPropertyChanged("TSpan");
                }
            }
        }

        private string operatingModeUnit;
        public string OperatingModeUnit
        {
            get { return operatingModeUnit; }
            set
            {
                if (operatingModeUnit != value)
                {
                    operatingModeUnit = value;
                    OnPropertyChanged("OperatingModeUnit");
                }
            }
        }

        private string operatingModePara;
        public string OperatingModePara
        {
            get { return operatingModePara; }
            set
            {
                if (operatingModePara != value)
                {
                    operatingModePara = value;
                    OnPropertyChanged("OperatingModePara");
                }
            }
        }

        private double? comparativePercent;
        public double? ComparativePercent
        {
            get { return comparativePercent; }
            set
            {
                if (comparativePercent != value)
                {
                    comparativePercent = value;
                    OnPropertyChanged("ComparativePercent");
                }
            }
        }

        #endregion

        private double teethNumber;
        public double TeethNumber
        {
            get { return teethNumber; }
            set
            {
                if (teethNumber != value)
                {
                    teethNumber = value;
                    OnPropertyChanged("TeethNumber");
                }
            }
        }

        //振动曲线数据
        public byte[] Bytes { get; set; }

        //采样点数
        private int samplePoint;
        public int SamplePoint
        {
            get { return samplePoint; }
            set
            {
                if (samplePoint != value)
                {
                    samplePoint = value;
                    OnPropertyChanged("SamplePoint");
                }
            }
        }

        //采样频率
        private double sampleFre;
        public double SampleFre
        {
            get { return sampleFre; }
            set
            {
                if (sampleFre != value)
                {
                    sampleFre = value;
                    OnPropertyChanged("SampleFre");
                }
            }
        }

        //高通
        #region Property HP
        private HP hp;
        public HP HP
        {
            get { return hp; }
            set
            {
                if (value != hp)
                {
                    hp = value;
                    OnPropertyChanged("HP");
                }
            }
        }
        #endregion

        //包络
        #region Property IsEnp
        private bool isEnp;
        public bool IsEnp
        {
            get { return isEnp; }
            set
            {
                if (value != isEnp)
                {
                    isEnp = value;
                    OnPropertyChanged("IsEnp");
                }
            }
        }
        #endregion

        //触发类型
        #region TriggerN
        private TriggerType triggerN;
        public TriggerType TriggerN
        {
            get { return triggerN; }
            set
            {
                if (triggerN != value)
                {
                    triggerN = value;
                    OnPropertyChanged("TriggerN");
                }
            }
        }
        #endregion

        //抗混
        #region Property IsAntiAliase
        private bool isAntiAliase;
        public bool IsAntiAliase
        {
            get { return isAntiAliase; }
            set
            {
                if (value != isAntiAliase)
                {
                    isAntiAliase = value;
                    OnPropertyChanged("IsAntiAliase");
                }
            }
        }
        #endregion

        //加窗
        #region Property Winfun
        private Winfun winfun;
        public Winfun Winfun
        {
            get { return winfun; }
            set
            {
                if (value != winfun)
                {
                    winfun = value;
                    OnPropertyChanged("Winfun");
                }
            }
        }
        #endregion

        //间隔时间
        #region Property IntevalTime
        private int intevalTime;
        public int IntevalTime
        {
            get { return intevalTime; }
            set
            {
                if (value != intevalTime)
                {
                    intevalTime = value;
                    OnPropertyChanged("IntevalTime");
                }
            }
        }
        #endregion

        //是否上传
        //#region Property IsUpload
        //private bool isUpload;
        //public bool IsUpload
        //{
        //    get { return isUpload; }
        //    set
        //    {
        //        if (value != isUpload)
        //        {
        //            isUpload = value;
        //            OnPropertyChanged("IsUpload");
        //        }
        //    }
        //}
        //#endregion  

       

        public void Filter()
        {
            if (FilterType == FilterType.BandPass)
            {
                FilterWaveform = BPFilter.Filter(Waveform, SamplePoint, SampleFre, RPM);
            }
            else if (FilterType == FilterType.HighPass)
            {
                FilterWaveform = HPFilter.Filter(Waveform, SamplePoint, SampleFre, RPM);
            }
            else if (FilterType == FilterType.LowPass)
            {
                FilterWaveform = LPFilter.Filter(Waveform, SamplePoint, SampleFre, RPM);
            }
        }
        #endregion
        #endregion
    }
}
