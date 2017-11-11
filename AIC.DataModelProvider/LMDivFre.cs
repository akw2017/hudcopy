using AIC.Cloud.Applications;
using AIC.CoreType;
using AIC.Server.Storage.Contract;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.DataModelProvider
{
    public class LMDivFre : BindableBase
    {
        private int id;

        public LMDivFre():this(null)
        { 
        }

        public LMDivFre(LMDivFreTableContract contract)
        {
            if (contract == null)
            {
                Contract = new LMDivFreTableContract();
                IsUpload = true;
                DefaultR = 0;
                MultiFre = 1;
                MaxFreNum = 1;
                IntevalTime = 1000;
            }
            else
            {
                Contract = contract;
                id = contract.id;
                ip = contract.IP;
                channelID = contract.ChannelID;
                freDescription = contract.FreDescription;
                divFreType = (DivFreType)Enum.Parse(typeof(DivFreType), contract.DivFreType.ToString());
                characteristicFre = contract.CharacteristicFre;
                defaultR = contract.DefaultR;
                base1Fre = contract.Base1Fre;
                base2Fre = contract.Base2Fre;
                multiFre = contract.MultiFre;
                base1FrePercent = contract.Base1FrePercent;
                divFreStrategt = contract.DivFreStrategt;
                maxFreNum = contract.MaxFreNum;
                slotNum = contract.SlotNum + 1;
                chaN = contract.ChaN + 1;
                alarmStrategt = (AlarmStrategt)Enum.Parse(typeof(AlarmStrategt), contract.AlarmStrategt.ToString());

                lowDanger = contract.LowDanger;
                lowAlert = contract.LowAlert;
                lowNormal = contract.LowNormal;
                higtNormal = contract.HighNormal;
                highAlert = contract.HighAlert;
                highDanger = contract.HighDanger;

                alarmSaveStr = (VAlarmSaveStr)Enum.Parse(typeof(VAlarmSaveStr), contract.AlarmSaveStr.ToString());
                intevalTime = contract.IntevalTime;

                formulaLowDanger = contract.FormulaLowDanger;
                formulaLowAlert = contract.FormulaLowAlert;
                formulaLowNormal = contract.FormulaLowNormal;
                formulaHighNormal = contract.FormulaHighNormal;
                formulaHighAlert = contract.FormulaHighAlert;
                formulaHighDanger = contract.FormulaHighDanger;

                operatingModeUnit = contract.OperatingModeUnit;
                operatingModePara = contract.OperatingModePara;
                comparativePercent = contract.ComparativePercent;
                isUpload = contract.IsUpload;
            }
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
       
        public int ID
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    Contract.id = value;
                }
            }
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
                }
            }
        }
        #endregion
        #region Property FreDescription
        private string freDescription;
        public string FreDescription
        {
            get { return freDescription; }
            set
            {
                if (value != freDescription)
                {
                    freDescription = value;
                    this.OnPropertyChanged("FreDescription");
                    Contract.FreDescription = value;
                }
            }
        }
        #endregion
        #region Property CharacteristicFre
        private double? characteristicFre;
        public double? CharacteristicFre
        {
            get { return characteristicFre; }
            set
            {
                if (value != characteristicFre)
                {
                    characteristicFre = value;
                    this.OnPropertyChanged("CharacteristicFre");
                    Contract.CharacteristicFre = value;
                }
            }
        }
        #endregion

        #region Property DefaultR
        private double? defaultR;
        public double? DefaultR
        {
            get { return defaultR; }
            set
            {
                if (value != defaultR)
                {
                    defaultR = value;
                    this.OnPropertyChanged("DefaultR");
                    Contract.DefaultR = value;
                }
            }
        }
        #endregion

        #region Property Base1Fre
        private double? base1Fre;
        public double? Base1Fre
        {
            get { return base1Fre; }
            set
            {
                if (value != base1Fre)
                {
                    base1Fre = value;
                    this.OnPropertyChanged("Base1Fre");
                    Contract.Base1Fre = value;
                }
            }
        }
        #endregion

        #region Property Base2Fre
        private double? base2Fre;
        public double? Base2Fre
        {
            get { return base2Fre; }
            set
            {
                if (value != base2Fre)
                {
                    base2Fre = value;
                    this.OnPropertyChanged("Base2Fre");
                    Contract.Base2Fre = value;
                }
            }
        }
        #endregion

        #region Property MultiFre
        private double? multiFre;
        public double? MultiFre
        {
            get { return multiFre; }
            set
            {
                if (value != multiFre)
                {
                    multiFre = value;
                    this.OnPropertyChanged("MultiFre");
                    Contract.MultiFre = value;
                }
            }
        }
        #endregion

        #region Property Base1FrePercent
        private double? base1FrePercent;
        public double? Base1FrePercent
        {
            get { return base1FrePercent; }
            set
            {
                if (value != base1FrePercent)
                {
                    base1FrePercent = value;
                    this.OnPropertyChanged("Base1FrePercent");
                    Contract.Base1FrePercent = value;
                }
            }
        }
        #endregion

        #region Property DivFreStrategt
        private int? divFreStrategt;
        public int? DivFreStrategt
        {
            get { return divFreStrategt; }
            set
            {
                if (value != divFreStrategt)
                {
                    divFreStrategt = value;
                    this.OnPropertyChanged("DivFreStrategt");
                    Contract.DivFreStrategt = value;
                }
            }
        }
        #endregion

        #region Property DivFreType
        private DivFreType divFreType;
        public DivFreType DivFreType
        {
            get { return divFreType; }
            set
            {
                if (value != divFreType)
                {
                    divFreType = value;
                    this.OnPropertyChanged("DivFreType");
                    Contract.DivFreType = (int)value;
                }
            }
        }
        #endregion

        #region Property MaxFreNum
        private int? maxFreNum;
        public int? MaxFreNum
        {
            get { return maxFreNum; }
            set
            {
                if (value != maxFreNum)
                {
                    maxFreNum = value;
                    this.OnPropertyChanged("MaxFreNum");
                    Contract.MaxFreNum = value;
                }
            }
        }
        #endregion

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
                    Contract.SlotNum = value-1;
                }
            }
        }
        #endregion

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
                    Contract.ChaN = value-1;
                }
            }
        }
        #endregion

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

        #region Property AlarmSaveStr
        private VAlarmSaveStr alarmSaveStr;
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
        
        public LMDivFreTableContract Contract { get;private set; }

    }
}
