using AIC.Cloud.Applications;
using AIC.CoreType;
using AIC.Server.Storage.Contract;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AIC.DataModelProvider
{
    public class LMAnInfo : BindableBase, ILMObject
    {
        public LMAnInfo(LMAnInfoTableContract contract)
        {
            Contract = contract;
            channelID = contract.ChannelID;
            ip = contract.IP;
            groupCOName = contract.GroupCOName;
            corporationName = contract.CorporationName;
            workShopName = contract.WorkShopName;
            devName = contract.DevName;
            devSN = contract.DevSN;
            chName = contract.CHName;
            chMSSN = contract.CHMSSN;
            chaN = contract.ChaN + 1;
            slotNum = contract.SlotNum + 1;
            alarmStrategt = (AlarmStrategt)Enum.Parse(typeof(AlarmStrategt), contract.AlarmStrategt.ToString());
            signalType = (SignalType)Enum.Parse(typeof(SignalType), contract.SignalType.ToString());
            thresholdVolt = contract.ThresholdVolt ?? 0;
            hysteresisVolt = contract.HysteresisVolt ?? 0;
            thresholdMode = (ThresholdMode)Enum.Parse(typeof(ThresholdMode), contract.ThresholdMode.ToString());
            weight = contract.Weight ?? 0;
            computeHost = (ComputeHost)Enum.Parse(typeof(ComputeHost), contract.ComputeHost.ToString());
            lowDanger = contract.LowDanger;
            lowAlert = contract.LowAlert;
            lowNormal = contract.LowNormal;
            higtNormal = contract.HighNormal;
            highAlert = contract.HighAlert;
            highDanger = contract.HighDanger;
            alarmSaveStr = (AlarmSaveStr)Enum.Parse(typeof(AlarmSaveStr), contract.AlarmSaveStr.ToString());
            intevalTime = contract.IntevalTime;
            unit = (Unit)Enum.Parse(typeof(Unit), contract.Unit.ToString());
            isUpload = contract.IsUpload;
            isForwardRotation = contract.IsForwardRotation ?? true;
            defaultRPM = contract.DefaultRPM;
            teethNumber = contract.TeethNumber;
            expression = contract.Expression;
            JudgeHidden();
        }

        private Guid channelID;
        public Guid ChannelID
        {
            get { return channelID; }
            set
            {
                if (channelID != value)
                {
                    channelID = value;
                    Contract.ChannelID = value;
                }
            }
        }

        public string GetGlobalID()
        {
            return BitConverter.ToString(SHA1Managed.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Concat(GroupCOName, CorporationName, WorkShopName, DevName, DevSN, CHName, CHMSSN)))).Replace("-", string.Empty);
        }

        private void JudgeHidden()
        {
            IsHidden = string.IsNullOrEmpty(groupCOName) && string.IsNullOrEmpty(corporationName) && string.IsNullOrEmpty(workShopName) && string.IsNullOrEmpty(devName) && string.IsNullOrEmpty(devSN);// && (string.IsNullOrEmpty(chName) || string.IsNullOrEmpty(chMSSN));
        }

        #region Property IP
        private string ip;
        public string IP
        {
            get { return ip; }
            set
            {
                if (value != ip)
                {
                    ip = value;
                    Contract.IP = value;
                }
            }
        }
        #endregion

        #region Property GroupCOName
        private string groupCOName;
        [DisplayName("总厂")]
        public string GroupCOName
        {
            get { return groupCOName; }
            set
            {
                if (value != groupCOName)
                {
                    groupCOName = value;
                    OnPropertyChanged("GroupCOName");
                    Contract.GroupCOName = value;
                    JudgeHidden();
                }
            }
        }
        #endregion

        #region Property CorporationName
        private string corporationName;
        [DisplayName("分厂")]
        public string CorporationName
        {
            get { return corporationName; }
            set
            {
                if (value != corporationName)
                {
                    corporationName = value;
                    OnPropertyChanged("CorporationName");
                    Contract.CorporationName = value;
                    JudgeHidden();
                }
            }
        }
        #endregion

        #region Property WorkShopName
        private string workShopName;
        [DisplayName("车间")]
        public string WorkShopName
        {
            get { return workShopName; }
            set
            {
                if (value != workShopName)
                {
                    workShopName = value;
                    OnPropertyChanged("WorkShopName");
                    Contract.WorkShopName = value;
                    JudgeHidden();
                }
            }
        }
        #endregion

        //设备名称
        #region Property DevName
        private string devName;
        public string DevName
        {
            get { return devName; }
            set
            {
                if (value != devName)
                {
                    devName = value;
                    this.OnPropertyChanged("DevName");
                    Contract.DevName = value;
                    JudgeHidden();
                }
            }
        }
        #endregion

        //设备编号
        #region Property DevSN
        private string devSN;
        public string DevSN
        {
            get { return devSN; }
            set
            {
                if (value != devSN)
                {
                    devSN = value;
                    this.OnPropertyChanged("DevSN");
                    Contract.DevSN = value;
                    JudgeHidden();
                }
            }
        }
        #endregion

        //测点名称
        #region Property CHName
        private string chName;
        public string CHName
        {
            get { return chName; }
            set
            {
                if (value != chName)
                {
                    chName = value;
                    this.OnPropertyChanged("CHName");
                    Contract.CHName = value;
                    JudgeHidden();
                }
            }
        }
        #endregion

        //测点编号
        #region Property CHMSSN
        private string chMSSN;
        public string CHMSSN
        {
            get { return chMSSN; }
            set
            {
                if (value != chMSSN)
                {
                    chMSSN = value;
                    this.OnPropertyChanged("CHMSSN");
                    Contract.CHMSSN = value;
                    JudgeHidden();
                }
            }
        }
        #endregion

        //插槽号
        #region Property SlotNum
        private int slotNum;
        public int SlotNum
        {
            get { return slotNum; }
            set
            {
                if (value != slotNum)
                {
                    slotNum = value;
                    this.OnPropertyChanged("SlotNum");
                    Contract.SlotNum = value-1;
                }
            }
        }
        #endregion

        //通道号
        #region Property ChaN
        private int chaN ;
        public int ChaN
        {
            get { return chaN; }
            set
            {
                if (value != chaN)
                {
                    chaN = value;
                    this.OnPropertyChanged("ChaN");
                    Contract.ChaN = value-1;
                }
            }
        }
        #endregion

        //报警策略
        #region Property AlarmStrategt
        private AlarmStrategt alarmStrategt;
        public AlarmStrategt AlarmStrategt
        {
            get { return alarmStrategt; }
            set
            {
                if (value != alarmStrategt)
                {
                    alarmStrategt = value;
                    this.OnPropertyChanged("AlarmStrategt");
                    Contract.AlarmStrategt = (int)value;
                }
            }
        }
        #endregion

        //信号类型
        #region Property SignalType
        private SignalType signalType;
        public SignalType SignalType
        {
            get { return signalType; }
            set
            {
                if (value != signalType)
                {
                    signalType = value;
                    this.OnPropertyChanged("SignalType");
                    Contract.SignalType = (int)value;
                }
            }
        }
        #endregion

        //门槛值电压
        #region Property ThresholdVolt
        private double thresholdVolt;
        public double ThresholdVolt
        {
            get { return thresholdVolt; }
            set
            {
                if (value != thresholdVolt)
                {
                    thresholdVolt = value;
                    this.OnPropertyChanged("ThresholdVolt");
                    Contract.ThresholdVolt = value;
                }
            }
        }
        #endregion

        //迟滞电压
        #region Property HysteresisVolt
        private double hysteresisVolt;
        public double HysteresisVolt
        {
            get { return hysteresisVolt; }
            set
            {
                if (hysteresisVolt != value)
                {
                    hysteresisVolt = value;
                    this.OnPropertyChanged("HysteresisVolt");
                    Contract.HysteresisVolt = value;
                }
            }
        }
        #endregion

        //门槛值选取模式
        #region Property ThresholdMode
        private ThresholdMode thresholdMode;
        public ThresholdMode ThresholdMode
        {
            get { return thresholdMode; }
            set
            {
                if (thresholdMode != value)
                {
                    thresholdMode = value;
                    this.OnPropertyChanged("ThresholdMode");
                    Contract.ThresholdMode = (int)value;
                }
            }
        }
        #endregion

        //加权值
        #region Weight
        private double weight;
        public double Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    OnPropertyChanged("Weight");
                    Contract.Weight = value;
                }
            }
        }
        #endregion

        //计算模式
        #region ComputeHost
        private ComputeHost computeHost;
        public ComputeHost ComputeHost
        {
            get { return computeHost; }
            set
            {
                if (computeHost != value)
                {
                    computeHost = value;
                    OnPropertyChanged("ComputeHost");
                    Contract.ComputeHost = (int)value;
                }
            }
        }
        #endregion

        //是否启用低报警值
        #region Property AllowLowLimit
        private bool allowLowLimit;
        public bool AllowLowLimit
        {
            get { return allowLowLimit; }
            set
            {
                if (allowLowLimit != value)
                {
                    allowLowLimit = value;
                    this.OnPropertyChanged("AllowLowLimit");
                }
            }
        }
        #endregion

        //低危
        #region LowDanger
        private double lowDanger;
        public double LowDanger
        {
            get { return lowDanger; }
            set
            {
                if (lowDanger != value)
                {
                    lowDanger = value;
                    OnPropertyChanged("LowDanger");
                    Contract.LowDanger = value;
                }
            }
        }
        #endregion

        //低警
        #region LowAlert
        private double lowAlert;
        public double LowAlert
        {
            get { return lowAlert; }
            set
            {
                if (lowAlert != value)
                {
                    lowAlert = value;
                    OnPropertyChanged("LowAlert");
                    Contract.LowAlert = value;
                }
            }
        }
        #endregion

        //正常(低)
        #region LowNormal
        private double lowNormal;
        public double LowNormal
        {
            get { return lowNormal; }
            set
            {
                if (lowNormal != value)
                {
                    lowNormal = value;
                    OnPropertyChanged("LowNormal");
                    Contract.LowNormal = value;
                }
            }
        }
        #endregion

        //正常(高)
        #region HighNormal
        private double higtNormal;
        public double HighNormal
        {
            get { return higtNormal; }
            set
            {
                if (higtNormal != value)
                {
                    higtNormal = value;
                    OnPropertyChanged("HighNormal");
                    Contract.HighNormal = value;
                }
            }
        }
        #endregion

        //高警
        #region HighAlert
        private double highAlert;
        public double HighAlert
        {
            get { return highAlert; }
            set
            {
                if (highAlert != value)
                {
                    highAlert = value;
                    OnPropertyChanged("HighAlert");
                    Contract.HighAlert = value;
                }
            }
        }
        #endregion

        //高危
        #region HighDanger
        private double highDanger;
        public double HighDanger
        {
            get { return highDanger; }
            set
            {
                if (highDanger != value)
                {
                    highDanger = value;
                    OnPropertyChanged("HighDanger");
                    Contract.HighDanger = value;
                }
            }
        }
        #endregion

        //低危方程
        #region Property FormulaLowDanger
        private string formulaLowDanger;
        public string FormulaLowDanger
        {
            get { return formulaLowDanger; }
            set
            {
                if (value != formulaLowDanger)
                {
                    formulaLowDanger = value;
                    OnPropertyChanged("FormulaLowDanger");
                    Contract.FormulaLowDanger = value;
                }
            }
        }
        #endregion

        //低警方程
        #region Property FormulaLowAlert
        private string formulaLowAlert;
        public string FormulaLowAlert
        {
            get { return formulaLowAlert; }
            set
            {
                if (value != formulaLowAlert)
                {
                    formulaLowAlert = value;
                    OnPropertyChanged("FormulaLowAlert");
                    Contract.FormulaLowAlert = value;
                }
            }
        }
        #endregion

        //正常方程(低)
        #region Property FormulaLowNormal
        private string formulaLowNormal;
        public string FormulaLowNormal
        {
            get { return formulaLowNormal; }
            set
            {
                if (value != formulaLowNormal)
                {
                    formulaLowNormal = value;
                    OnPropertyChanged("FormulaLowNormal");
                    Contract.FormulaLowNormal = value;
                }
            }
        }
        #endregion

        //正常方程(高)
        #region Property FormulaHighNormal
        private string formulaHighNormal;
        public string FormulaHighNormal
        {
            get { return formulaHighNormal; }
            set
            {
                if (value != formulaHighNormal)
                {
                    formulaHighNormal = value;
                    OnPropertyChanged("FormulaHighNormal");
                    Contract.FormulaHighNormal = value;
                }
            }
        }
        #endregion

        //高警方程
        #region Property FormulaHighAlert
        private string formulaHighAlert;
        public string FormulaHighAlert
        {
            get { return formulaHighAlert; }
            set
            {
                if (value != formulaHighAlert)
                {
                    formulaHighAlert = value;
                    OnPropertyChanged("FormulaHighAlert");
                    Contract.FormulaHighAlert = value;
                }
            }
        }
        #endregion

        //高危方程
        #region Property FormulaHighDanger
        private string formulaHighDanger;
        public string FormulaHighDanger
        {
            get { return formulaHighDanger; }
            set
            {
                if (value != formulaHighDanger)
                {
                    formulaHighDanger = value;
                    OnPropertyChanged("FormulaHighDanger");
                    Contract.FormulaHighDanger = value;
                }
            }
        }
        #endregion

        #region Property AlarmSaveStr
        private AlarmSaveStr alarmSaveStr;
        [DisplayName("数据处理策略")]
        public AlarmSaveStr AlarmSaveStr
        {
            get { return alarmSaveStr; }
            set
            {
                if (value != alarmSaveStr)
                {
                    alarmSaveStr = value;
                    this.OnPropertyChanged("AlarmSaveStr");
                    Contract.AlarmSaveStr = (int)value;
                }
            }
        }
        #endregion

        #region Property IntevalTime
        private int intevalTime;
        [DisplayName("发送间隔")]
        public int IntevalTime
        {
            get { return intevalTime; }
            set
            {
                if (value != intevalTime)
                {
                    intevalTime = value;
                    this.OnPropertyChanged("IntevalTime");
                    Contract.IntevalTime = value;
                }
            }
        }
        #endregion

        #region Property Unit
        private Unit unit;
        [DisplayName("单位")]
        public Unit Unit
        {
            get { return unit; }
            set
            {
                if (value != unit)
                {
                    unit = value;
                    this.OnPropertyChanged("Unit");
                    Contract.Unit = (int)value;
                }
            }
        }
        #endregion

        #region Property IsUpload
        private bool isUpload;
        [DisplayName("是否上传")]
        public bool IsUpload
        {
            get { return isUpload; }
            set
            {
                if (value != isUpload)
                {
                    isUpload = value;
                    this.OnPropertyChanged("IsUpload");
                    Contract.IsUpload = value;
                }
            }
        }
        #endregion

        #region Property IsForwardRotation
        private bool isForwardRotation;
        [DisplayName("键相转向")]
        public bool IsForwardRotation
        {
            get { return isForwardRotation; }
            set
            {
                if (value != isForwardRotation)
                {
                    isForwardRotation = value;
                    this.OnPropertyChanged("IsForwardRotation");
                    Contract.IsForwardRotation = value;
                }
            }
        }
        #endregion
        

        #region Property DefaultRPM
        private double defaultRPM;
        public double DefaultRPM
        {
            get { return defaultRPM; }
            set
            {
                if (value != defaultRPM)
                {
                    defaultRPM = value;
                    this.OnPropertyChanged("DefaultRPM");
                    Contract.DefaultRPM = value;
                }
            }
        }
        #endregion

        #region Property teethNumber
        private double? teethNumber;
        public double? TeethNumber
        {
            get { return teethNumber; }
            set
            {
                if (value != teethNumber)
                {
                    teethNumber = value;
                    this.OnPropertyChanged("TeethNumber");
                    Contract.TeethNumber = value;
                }
            }
        }
        #endregion

        #region Property Expression
        private string expression;
        public string Expression
        {
            get { return expression; }
            set
            {
                if (value != expression)
                {
                    expression = value;
                    this.OnPropertyChanged("Expression");
                    Contract.Expression = value;
                }
            }
        }
        #endregion

        #region Property IsHidden
        private bool isHidden;
        public bool IsHidden
        {
            get { return isHidden; }
            set
            {
                if (value != isHidden)
                {
                    isHidden = value;
                    this.OnPropertyChanged("IsHidden");
                }
            }
        }
        #endregion
        public LMAnInfoTableContract Contract { get;private set; }
    }
}
