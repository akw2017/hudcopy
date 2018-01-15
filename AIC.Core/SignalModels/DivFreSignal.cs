using AIC.Core.DataModels;
using AIC.CoreType;
using AIC.M9600.Common.SlaveDB.Generated;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class DivFreSignal : BindableBase
    {
        public DivFreSignal()
        {

        }      

        public DivFreSignal(Guid guid, string name)
        {
            Guid = guid;
            DisPlayName = name;
        }

        public D1_DivFreInfo D_DivFreInfo { get; set; }
        public Guid Guid { get; set; }

        //采集时间
        private DateTime aCQDatetime;
        public DateTime ACQDatetime
        {
            get { return aCQDatetime; }
            set
            {
                if (aCQDatetime != value)
                {
                    aCQDatetime = value;
                    OnPropertyChanged("ACQDatetime");
                }
            }
        }

        //报警级别
        private AlarmGrade alarmGrade;
        public AlarmGrade AlarmGrade
        {
            get { return alarmGrade; }
            set
            {
                if (alarmGrade != value)
                {
                    alarmGrade = value;
                    OnPropertyChanged("AlarmGrade");
                }
            }
        }

        private Guid recordLab;
        public Guid RecordLab
        {
            get { return recordLab; }
            set
            {
                if (recordLab != value)
                {
                    recordLab = value;
                }
            }
        }

        public string Code { get; set; }

        private string descriptionFre;
        public string DescriptionFre
        {
            get { return descriptionFre; }
            set
            {
                if (descriptionFre != value)
                {
                    descriptionFre = value;
                    OnPropertyChanged("DescriptionFre");
                }
            }
        }
        public string Name { get; set; }

        private double result;
        public double Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    OnPropertyChanged("Result");
                }
            }
        }

        private double phase;
        public double Phase
        {
            get { return phase; }
            set
            {
                if (phase != value)
                {
                    phase = value;
                    OnPropertyChanged("Phase");
                }
            }
        }

        //报警类型
        private int alarmType;
        public int AlarmType
        {
            get { return alarmType; }
            set
            {
                if (alarmType != value)
                {
                    alarmType = value;
                    OnPropertyChanged("AlarmType");
                }
            }
        }

        //考虑删除
        private string disPlayName;
        public string DisPlayName
        {
            get { return disPlayName; }
            set
            {
                if (disPlayName != value)
                {
                    disPlayName = value;
                    OnPropertyChanged("DisPlayName");
                }
            }
        }


        private int channelGlobalIndex;
        public int ChannelGlobalIndex
        {
            get { return channelGlobalIndex; }
            set
            {
                if (channelGlobalIndex != value)
                {
                    channelGlobalIndex = value;
                    OnPropertyChanged("ChannelGlobalIndex");
                }
            }
        }

        private double freV;
        public double FreV
        {
            get { return freV; }
            set
            {
                if (freV != value)
                {
                    freV = value;
                    OnPropertyChanged("FreV");
                }
            }
        }

        private double freMV;
        public double FreMV
        {
            get { return freMV; }
            set
            {
                if (freMV != value)
                {
                    freMV = value;
                    OnPropertyChanged("freMV");
                }              
            }
        }

      

        private DivFreType divFreType;
        public DivFreType DivFreType
        {
            get { return divFreType; }
            set
            {
                if (divFreType != value)
                {
                    divFreType = value;
                    OnPropertyChanged("DivFreType");
                }
            }
        }
       

        private double? base1Fre;
        public double? Base1Fre
        {
            get { return base1Fre; }
            set
            {
                if (base1Fre != value)
                {
                    base1Fre = value;
                    OnPropertyChanged("Base1Fre");
                }
            }
        }

        private double? base2Fre;
        public double? Base2Fre
        {
            get { return base2Fre; }
            set
            {
                if (base2Fre != value)
                {
                    base2Fre = value;
                    OnPropertyChanged("Base2Fre");
                }
            }
        }

        private double? multiFre;
        public double? MultiFre
        {
            get { return multiFre; }
            set
            {
                if (multiFre != value)
                {
                    multiFre = value;
                    OnPropertyChanged("MultiFre");
                }
            }
        }

        private double? base1FrePercent;
        public double? Base1FrePercent
        {
            get { return base1FrePercent; }
            set
            {
                if (base1FrePercent != value)
                {
                    base1FrePercent = value;
                    OnPropertyChanged("Base1FrePercent");
                }
            }
        }

        private int? divFreStrategt;
        public int? DivFreStrategt
        {
            get { return divFreStrategt; }
            set
            {
                if (divFreStrategt != value)
                {
                    divFreStrategt = value;
                    OnPropertyChanged("DivFreStrategt");
                }
            }
        }

       

        private int? maxFreNum;
        public int? MaxFreNum
        {
            get { return maxFreNum; }
            set
            {
                if (maxFreNum != value)
                {
                    maxFreNum = value;
                    OnPropertyChanged("MaxFreNum");
                }
            }
        }

        private double rpm;
        public double RPM
        {
            get { return rpm; }
            set
            {
                if (rpm != value)
                {
                    rpm = value;
                    OnPropertyChanged("RPM");
                }
            }
        }

        //间隔时间
        private int intevalTime;
        public int IntevalTime
        {
            get { return intevalTime; }
            set
            {
                if (intevalTime != value)
                {
                    intevalTime = value;
                    OnPropertyChanged("IntevalTime");
                }
            }
        }

        //是否上传
        private bool isUpload;
        public bool IsUpload
        {
            get { return isUpload; }
            set
            {
                if (isUpload != value)
                {
                    isUpload = value;
                    OnPropertyChanged("IsUpload");
                }
            }
        }

        //特征频率
        private double characteristicFre;
        public double CharacteristicFre
        {
            get { return characteristicFre; }
            set
            {
                if (characteristicFre != value)
                {
                    characteristicFre = value;
                    OnPropertyChanged("CharacteristicFre");
                }
            }
        }

        //默认转速
        private double defaultR;
        public double DefaultR
        {
            get { return defaultR; }
            set
            {
                if (defaultR != value)
                {
                    defaultR = value;
                    OnPropertyChanged("DefaultR");
                }
            }
        }



     

        //延迟报警级别
        private AlarmGrade delayAlarmGrade;
        public AlarmGrade DelayAlarmGrade
        {
            get { return delayAlarmGrade; }
            set
            {
                if (delayAlarmGrade != value)
                {
                    delayAlarmGrade = value;
                    OnPropertyChanged("DelayAlarmGrade");
                }
            }
        }

       

        ////有效值趋势值
        //private List<DivPoint> freMVRecordValues = new List<DivPoint>();
        //public List<DivPoint> FreMVRecordValues
        //{
        //    get { return freMVRecordValues; }
        //    set
        //    {
        //        if (freMVRecordValues != value)
        //        {
        //            freMVRecordValues = value;
        //        }
        //    }
        //}

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
                    OnPropertyChanged("OperatingModeUnit");
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
                    OnPropertyChanged("OperatingModePara");
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
                    OnPropertyChanged("ComparativePercent");
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
                }
            }
        }
        #endregion

        //低警
        #region LowAlarm
        private double lowAlert;
        public double LowAlarm
        {
            get { return lowAlert; }
            set
            {
                if (lowAlert != value)
                {
                    lowAlert = value;
                    OnPropertyChanged("LowAlarm");
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
                }
            }
        }
        #endregion

        //高警
        #region HighAlarm
        private double highAlert;
        public double HighAlarm
        {
            get { return highAlert; }
            set
            {
                if (highAlert != value)
                {
                    highAlert = value;
                    OnPropertyChanged("HighAlarm");
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
                }
            }
        }
        #endregion

        private bool isRead;
        public bool IsRead
        {
            get { return isRead; }
            set
            {
                if (isRead != value)
                {
                    isRead = value;
                    OnPropertyChanged("IsRead");
                }
            }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    OnPropertyChanged("IsConnected");
                }
            }
        }
    }
}
