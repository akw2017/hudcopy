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
    public class LMVInfo : BindableBase, ILMObject
    {
        public LMVInfo(LMVInfoTableContract contract)
        {
            DivFres = new System.Collections.ObjectModel.ObservableCollection<LMDivFre>();
            Contract = contract;
            ip = contract.IP;
            channelID = contract.ChannelID;
            groupCOName = contract.GroupCOName;
            corporationName = contract.CorporationName;
            workShopName = contract.WorkShopName;
            devName = contract.DevName;
            devSN = contract.DevSN;
            chName = contract.CHName;
            chMSSN = contract.CHMSSN;
            vmsDir = (VMSDir)Enum.Parse(typeof(VMSDir), contract.VMSDir.ToString());
            chaN = contract.ChaN + 1;
            slotNum = contract.SlotNum + 1;
            rChaN = contract.RChaN + 1;
            rSlotNum = contract.RSlotNum + 1;
            svType = (SVType)Enum.Parse(typeof(SVType), contract.SVType.ToString());
            alarmStrategt = (AlarmStrategt)Enum.Parse(typeof(AlarmStrategt), contract.AlarmStrategt.ToString());
            biasVoltHigh = contract.BiasVoltHigh.Value;
            biasVoltLow = contract.BiasVoltLow.Value;
            sensitivity = contract.Sensitivity.Value;
            zeroValue = contract.ZeroValue.Value;
            allowLowLimit = contract.AllowLowLimit;
            lowDanger = contract.LowDanger;
            lowAlert = contract.LowAlert;
            lowNormal = contract.LowNormal;
            higtNormal = contract.HighNormal;
            highAlert = contract.HighAlert;
            highDanger = contract.HighDanger;
            alarmSaveStr = (VAlarmSaveStr)Enum.Parse(typeof(VAlarmSaveStr), contract.AlarmSaveStr.ToString());
            intevalTime = contract.IntevalTime;
            curveType = (CurveType)Enum.Parse(typeof(CurveType), contract.CurveType.ToString());
            operatingModePara = contract.OperatingModePara;
            operatingModeUnit = contract.OperatingModeUnit;
            comparativePercent = contract.ComparativePercent;
            formulaLowDanger = contract.FormulaLowDanger;
            formulaLowAlert = contract.FormulaLowAlert;
            formulaLowNormal = contract.FormulaLowNormal;
            formulaHighNormal = contract.FormulaHighNormal;
            formulaHighAlert = contract.FormulaHighAlert;
            formulaHighDanger = contract.FormulaHighDanger;
            expression = contract.Expression;
            isUpload = contract.IsUpload;
            isAxleDirectionForward = contract.IsAxleDirectionForward ?? true;
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
                    this.OnPropertyChanged("IP");
                    Contract.IP = value;
                    foreach (var div in DivFres)
                    {
                        div.IP = value;
                    }
                }
            }
        }
        #endregion

        //总厂
        #region Property GroupCOName
        private string groupCOName;
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

        //分厂
        #region Property CorporationName
        private string corporationName;
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

        //车间
        #region Property WorkShopName
        private string workShopName;
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

        #region Property VMSDir
        private VMSDir vmsDir;
        [DisplayName("振动方向")]
        public VMSDir VMSDir
        {
            get { return vmsDir; }
            set
            {
                if (value != vmsDir)
                {
                    vmsDir = value;
                    this.OnPropertyChanged("VMSDir");
                    Contract.VMSDir = (int)value;
                }
            }
        }
        #endregion

        #region Property SVType
        private SVType svType;
        [DisplayName("单值类型")]
        public SVType SVType
        {
            get { return svType; }
            set
            {
                if (value != svType)
                {
                    svType = value;
                    this.OnPropertyChanged("SVType");
                    Contract.SVType = (int)value;
                }
            }
        }
        #endregion

        #region Property AlarmStrategt
        private AlarmStrategt alarmStrategt;
        [DisplayName("报警策略")]
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

        //偏置电压(高)
        #region Property BiasVoltHigh
        private double biasVoltHigh;
        public double BiasVoltHigh
        {
            get { return biasVoltHigh; }
            set
            {
                if (biasVoltHigh != value)
                {
                    biasVoltHigh = value;
                    this.OnPropertyChanged("BiasVoltHigh");
                    Contract.BiasVoltHigh = value;
                }
            }
        }
        #endregion

        //偏置电压(低)
        #region Property BiasVoltLow
        private double biasVoltLow;
        public double BiasVoltLow
        {
            get { return biasVoltLow; }
            set
            {
                if (biasVoltLow != value)
                {
                    biasVoltLow = value;
                    this.OnPropertyChanged("BiasVoltLow");
                    Contract.BiasVoltLow = value;
                }
            }
        }
        #endregion

        //灵敏度(大于0，默认值0.01)
        #region Property Sensitivity
        private double sensitivity = 0.01;
        public double Sensitivity
        {
            get { return sensitivity; }
            set
            {
                if (sensitivity != value)
                {
                    sensitivity = value;
                    this.OnPropertyChanged("Sensitivity");
                    Contract.Sensitivity = value;
                }
            }
        }
        #endregion

        //零点值(电涡流传感器时表示零点电压)
        #region Property ZeroValue
        private double zeroValue;
        public double ZeroValue
        {
            get { return zeroValue; }
            set
            {
                if (zeroValue != value)
                {
                    zeroValue = value;
                    this.OnPropertyChanged("ZeroValue");
                    Contract.ZeroValue = value;
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
                    Contract.AllowLowLimit = value;
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
        private VAlarmSaveStr alarmSaveStr;
        [DisplayName("数据处理策略")]
        public VAlarmSaveStr AlarmSaveStr
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
        [DisplayName("相对时间间隔")]
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

        #region Property CurveType
        private CurveType curveType;
        [DisplayName("曲线类型")]
        public CurveType CurveType
        {
            get { return curveType; }
            set
            {
                if (value != curveType)
                {
                    curveType = value;
                    this.OnPropertyChanged("CurveType");
                    Contract.CurveType = (int)value;
                }
            }
        }
        #endregion

        #region Property IsUpload
        private bool isUpload;
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
        //键相反转
        #region Property IsAxleDirectionForward
        private bool isAxleDirectionForward = true;
        public bool IsAxleDirectionForward
        {
            get { return isAxleDirectionForward; }
            set
            {
                if (value != isAxleDirectionForward)
                {
                    isAxleDirectionForward = value;
                    this.OnPropertyChanged("IsAxleDirectionForward");
                    Contract.IsAxleDirectionForward = value;
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
                    Contract.SlotNum = value - 1;
                    foreach (var div in DivFres)
                    {
                        div.SlotNum = value;
                    }
                }
            }
        }
        #endregion

        //通道号
        #region Property ChaN
        private int chaN;
        public int ChaN
        {
            get { return chaN; }
            set
            {
                if (value != chaN)
                {
                    chaN = value;
                    this.OnPropertyChanged("ChaN");
                    Contract.ChaN = value - 1;
                    foreach (var div in DivFres)
                    {
                        div.ChaN = value;
                    }
                }
            }
        }
        #endregion

        #region Property RSlotNum
        private int? rSlotNum;
        public int? RSlotNum
        {
            get { return rSlotNum; }
            set
            {
                if (value != rSlotNum)
                {
                    rSlotNum = value;
                    this.OnPropertyChanged("RSlotNum");
                    Contract.RSlotNum = value - 1;
                }
            }
        }
        #endregion

        #region Property RChaN
        private int? rChaN;
        public int? RChaN
        {
            get { return rChaN; }
            set
            {
                if (value != rChaN)
                {
                    rChaN = value;
                    this.OnPropertyChanged("RChaN");
                    Contract.RChaN = value-1;
                }
            }
        }
        #endregion

        #region Property AvailableRSlotNums
        private int[] availableRSlotNums;
        public int[] AvailableRSlotNums
        {
            get { return availableRSlotNums; }
            set
            {
                if (availableRSlotNums != value)
                {
                    availableRSlotNums = value;
                    OnPropertyChanged("AvailableRSlotNums");
                }
            }
        }
        #endregion

        #region Property AvailableRChaNs
        private int[] availableRChaNs;
        public int[] AvailableRChaNs
        {
            get { return availableRChaNs; }
            set
            {
                if (availableRChaNs != value)
                {
                    availableRChaNs = value;
                    OnPropertyChanged("AvailableRChaNs");
                }
            }
        }
        #endregion

        #region Property OperatingModeUnit
        private string operatingModeUnit;
        public string OperatingModeUnit
        {
            get { return operatingModeUnit; }
            set
            {
                if (value != operatingModeUnit)
                {
                    operatingModeUnit = value;
                    this.OnPropertyChanged("OperatingModeUnit");
                    Contract.OperatingModeUnit = value;
                }
            }
        }
        #endregion

        #region Property OperatingModePara
        private string operatingModePara;
        public string OperatingModePara
        {
            get { return operatingModePara; }
            set
            {
                if (value != operatingModePara)
                {
                    operatingModePara = value;
                    this.OnPropertyChanged("OperatingModePara");
                    Contract.OperatingModePara = value;
                }
            }
        }
        #endregion

        #region Property ComparativePercent
        private double? comparativePercent;
        public double? ComparativePercent
        {
            get { return comparativePercent; }
            set
            {
                if (value != comparativePercent)
                {
                    comparativePercent = value;
                    this.OnPropertyChanged("ComparativePercent");
                    Contract.ComparativePercent = value;
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

        private LMDivFre selectedLMDivFre;
        public LMDivFre SelectedLMDivFre
        {
            get { return selectedLMDivFre; }
            set
            {
                if (value != selectedLMDivFre)
                {
                    selectedLMDivFre = value;
                    this.OnPropertyChanged("SelectedLMDivFre");
                }
            }
        }

        public System.Collections.ObjectModel.ObservableCollection<LMDivFre> DivFres { get; set; }

        public LMVInfoTableContract Contract { get; private set; }


    }
}
