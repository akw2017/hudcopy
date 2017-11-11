//using AIC.CoreType;
//using Prism.Mvvm;
//using System;
//using System.ComponentModel;
//using System.Linq;
//using System.Reactive.Linq;

//namespace AIC.Domain
//{
//    public class DivFreModel : BindableBase
//    {
//        private IDisposable alarmGradeChangedSubscription;

//        public DivFreModel(DivFre divFre)
//        {
//            DivFre = divFre;

//        }

//        public DivFre DivFre { get; }

//        private double freV;
//        public double FreV
//        {
//            get { return freV; }
//            set
//            {
//                if (SetProperty(ref freV, value))
//                    DivFre.FreV = value;
//            }
//        }

//        private double freMV;
//        public double FreMV
//        {
//            get { return freMV; }
//            set
//            {
//                if (SetProperty(ref freMV, value))
//                    DivFre.FreMV = value;
//            }
//        }

//        private double phase;
//        public double Phase
//        {
//            get { return phase; }
//            set
//            {
//                if (SetProperty(ref phase, value))
//                    DivFre.Phase = value;
//            }
//        }

//        private DivFreType divFreType;
//        public DivFreType DivFreType
//        {
//            get { return divFreType; }
//            set
//            {
//                if (SetProperty(ref divFreType, value))
//                    DivFre.DivFreType = (int)value;
//            }
//        }

//        private string name;
//        public string Name
//        {
//            get { return name; }
//            set
//            {
//                if (SetProperty(ref name, value))
//                    DivFre.Name = value;
//            }
//        }

//        private double? base1Fre;
//        public double? Base1Fre
//        {
//            get { return base1Fre; }
//            set
//            {
//                if (SetProperty(ref base1Fre, value))
//                    DivFre.Base1Fre = value;
//            }
//        }

//        private double? base2Fre;
//        public double? Base2Fre
//        {
//            get { return base2Fre; }
//            set
//            {
//                if (base2Fre != value)
//                {
//                    base2Fre = value;
//                    NotifyPropertyChanged("Base2Fre");
//                }
//            }
//        }

//        private double? multiFre;
//        public double? MultiFre
//        {
//            get { return multiFre; }
//            set
//            {
//                if (multiFre != value)
//                {
//                    multiFre = value;
//                    NotifyPropertyChanged("MultiFre");
//                }
//            }
//        }

//        private double? base1FrePercent;
//        public double? Base1FrePercent
//        {
//            get { return base1FrePercent; }
//            set
//            {
//                if (base1FrePercent != value)
//                {
//                    base1FrePercent = value;
//                    NotifyPropertyChanged("Base1FrePercent");
//                }
//            }
//        }

//        private int? divFreStrategt;
//        public int? DivFreStrategt
//        {
//            get { return divFreStrategt; }
//            set
//            {
//                if (divFreStrategt != value)
//                {
//                    divFreStrategt = value;
//                    NotifyPropertyChanged("DivFreStrategt");
//                }
//            }
//        }

//        //采集时间
//        private DateTime sTime;
//        public DateTime STIME
//        {
//            get { return sTime; }
//            set
//            {
//                if (sTime != value)
//                {
//                    sTime = value;
//                    NotifyPropertyChanged("STIME");
//                }
//            }
//        }

//        private int? maxFreNum;
//        public int? MaxFreNum
//        {
//            get { return maxFreNum; }
//            set
//            {
//                if (maxFreNum != value)
//                {
//                    maxFreNum = value;
//                    NotifyPropertyChanged("MaxFreNum");
//                }
//            }
//        }

//        private double rpm;
//        public double RPM
//        {
//            get { return rpm; }
//            set
//            {
//                if (rpm != value)
//                {
//                    rpm = value;
//                    NotifyPropertyChanged("RPM");
//                }
//            }
//        }

//        //间隔时间
//        private int intevalTime;
//        public int IntevalTime
//        {
//            get { return intevalTime; }
//            set
//            {
//                if (intevalTime != value)
//                {
//                    intevalTime = value;
//                    NotifyPropertyChanged("IntevalTime");
//                }
//            }
//        }

//        //是否上传
//        private bool isUpload;
//        public bool IsUpload
//        {
//            get { return isUpload; }
//            set
//            {
//                if (isUpload != value)
//                {
//                    isUpload = value;
//                    NotifyPropertyChanged("IsUpload");
//                }
//            }
//        }

//        //特征频率
//        private double characteristicFre;
//        public double CharacteristicFre
//        {
//            get { return characteristicFre; }
//            set
//            {
//                if (characteristicFre != value)
//                {
//                    characteristicFre = value;
//                    NotifyPropertyChanged("CharacteristicFre");
//                }
//            }
//        }

//        //默认转速
//        private double defaultR;
//        public double DefaultR
//        {
//            get { return defaultR; }
//            set
//            {
//                if (defaultR != value)
//                {
//                    defaultR = value;
//                    NotifyPropertyChanged("DefaultR");
//                }
//            }
//        }



//        //报警级别
//        private AlarmGrade alarmGrade;
//        public AlarmGrade AlarmGrade
//        {
//            get { return alarmGrade; }
//            set
//            {
//                if (alarmGrade != value)
//                {
//                    alarmGrade = value;
//                    NotifyPropertyChanged("AlarmGrade");
//                }
//            }
//        }

//        //延迟报警级别
//        private AlarmGrade delayAlarmGrade;
//        public AlarmGrade DelayAlarmGrade
//        {
//            get { return delayAlarmGrade; }
//            set
//            {
//                if (delayAlarmGrade != value)
//                {
//                    delayAlarmGrade = value;
//                    NotifyPropertyChanged("DelayAlarmGrade");
//                }
//            }
//        }

//        //报警类型
//        private int alarmType;
//        public int AlarmType
//        {
//            get { return alarmType; }
//            set
//            {
//                if (alarmType != value)
//                {
//                    alarmType = value;
//                    NotifyPropertyChanged("AlarmType");
//                }
//            }
//        }

//        ////有效值趋势值
//        //private List<DivPoint> freMVRecordValues = new List<DivPoint>();
//        //public List<DivPoint> FreMVRecordValues
//        //{
//        //    get { return freMVRecordValues; }
//        //    set
//        //    {
//        //        if (freMVRecordValues != value)
//        //        {
//        //            freMVRecordValues = value;
//        //        }
//        //    }
//        //}

//        #region Property OperatingModeUnit
//        private string operatingModeUnit;
//        public string OperatingModeUnit
//        {
//            get { return operatingModeUnit; }
//            set
//            {
//                if (value != operatingModeUnit)
//                {
//                    operatingModeUnit = value;
//                    NotifyPropertyChanged("OperatingModeUnit");
//                }
//            }
//        }
//        #endregion

//        #region Property OperatingModePara
//        private string operatingModePara;
//        public string OperatingModePara
//        {
//            get { return operatingModePara; }
//            set
//            {
//                if (value != operatingModePara)
//                {
//                    operatingModePara = value;
//                    NotifyPropertyChanged("OperatingModePara");
//                }
//            }
//        }
//        #endregion

//        #region Property ComparativePercent
//        private double? comparativePercent;
//        public double? ComparativePercent
//        {
//            get { return comparativePercent; }
//            set
//            {
//                if (value != comparativePercent)
//                {
//                    comparativePercent = value;
//                    NotifyPropertyChanged("ComparativePercent");
//                }
//            }
//        }
//        #endregion

//        //低危
//        #region LowDanger
//        private double lowDanger;
//        public double LowDanger
//        {
//            get { return lowDanger; }
//            set
//            {
//                if (lowDanger != value)
//                {
//                    lowDanger = value;
//                    NotifyPropertyChanged("LowDanger");
//                }
//            }
//        }
//        #endregion

//        //低警
//        #region LowAlert
//        private double lowAlert;
//        public double LowAlert
//        {
//            get { return lowAlert; }
//            set
//            {
//                if (lowAlert != value)
//                {
//                    lowAlert = value;
//                    NotifyPropertyChanged("LowAlert");
//                }
//            }
//        }
//        #endregion

//        //正常(低)
//        #region LowNormal
//        private double lowNormal;
//        public double LowNormal
//        {
//            get { return lowNormal; }
//            set
//            {
//                if (lowNormal != value)
//                {
//                    lowNormal = value;
//                    NotifyPropertyChanged("LowNormal");
//                }
//            }
//        }
//        #endregion

//        //正常(高)
//        #region HighNormal
//        private double higtNormal;
//        public double HighNormal
//        {
//            get { return higtNormal; }
//            set
//            {
//                if (higtNormal != value)
//                {
//                    higtNormal = value;
//                    NotifyPropertyChanged("HighNormal");
//                }
//            }
//        }
//        #endregion

//        //高警
//        #region HighAlert
//        private double highAlert;
//        public double HighAlert
//        {
//            get { return highAlert; }
//            set
//            {
//                if (highAlert != value)
//                {
//                    highAlert = value;
//                    NotifyPropertyChanged("HighAlert");
//                }
//            }
//        }
//        #endregion

//        //高危
//        #region HighDanger
//        private double highDanger;
//        public double HighDanger
//        {
//            get { return highDanger; }
//            set
//            {
//                if (highDanger != value)
//                {
//                    highDanger = value;
//                    NotifyPropertyChanged("HighDanger");
//                }
//            }
//        }
//        #endregion

//        //低危方程
//        #region Property FormulaLowDanger
//        private string formulaLowDanger;
//        public string FormulaLowDanger
//        {
//            get { return formulaLowDanger; }
//            set
//            {
//                if (value != formulaLowDanger)
//                {
//                    formulaLowDanger = value;
//                    NotifyPropertyChanged("FormulaLowDanger");
//                }
//            }
//        }
//        #endregion

//        //低警方程
//        #region Property FormulaLowAlert
//        private string formulaLowAlert;
//        public string FormulaLowAlert
//        {
//            get { return formulaLowAlert; }
//            set
//            {
//                if (value != formulaLowAlert)
//                {
//                    formulaLowAlert = value;
//                    NotifyPropertyChanged("FormulaLowAlert");
//                }
//            }
//        }
//        #endregion

//        //正常方程(低)
//        #region Property FormulaLowNormal
//        private string formulaLowNormal;
//        public string FormulaLowNormal
//        {
//            get { return formulaLowNormal; }
//            set
//            {
//                if (value != formulaLowNormal)
//                {
//                    formulaLowNormal = value;
//                    NotifyPropertyChanged("FormulaLowNormal");
//                }
//            }
//        }
//        #endregion

//        //正常方程(高)
//        #region Property FormulaHighNormal
//        private string formulaHighNormal;
//        public string FormulaHighNormal
//        {
//            get { return formulaHighNormal; }
//            set
//            {
//                if (value != formulaHighNormal)
//                {
//                    formulaHighNormal = value;
//                    NotifyPropertyChanged("FormulaHighNormal");
//                }
//            }
//        }
//        #endregion

//        //高警方程
//        #region Property FormulaHighAlert
//        private string formulaHighAlert;
//        public string FormulaHighAlert
//        {
//            get { return formulaHighAlert; }
//            set
//            {
//                if (value != formulaHighAlert)
//                {
//                    formulaHighAlert = value;
//                    NotifyPropertyChanged("FormulaHighAlert");
//                }
//            }
//        }
//        #endregion

//        //高危方程
//        #region Property FormulaHighDanger
//        private string formulaHighDanger;
//        public string FormulaHighDanger
//        {
//            get { return formulaHighDanger; }
//            set
//            {
//                if (value != formulaHighDanger)
//                {
//                    formulaHighDanger = value;
//                    NotifyPropertyChanged("FormulaHighDanger");
//                }
//            }
//        }
//        #endregion

//        private bool isRead;
//        public bool IsRead
//        {
//            get { return isRead; }
//            set
//            {
//                if (isRead != value)
//                {
//                    isRead = value;
//                    NotifyPropertyChanged("IsRead");
//                }
//            }
//        }

//        private bool isConnected;
//        public bool IsConnected
//        {
//            get { return isConnected; }
//            set
//            {
//                if (isConnected != value)
//                {
//                    isConnected = value;
//                    NotifyPropertyChanged("IsConnected");
//                }
//            }
//        }

//        #region INotifyPropertyChanged

//        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
//        {
//            add { this.propertyChanged += value; }
//            remove { this.propertyChanged -= value; }
//        }

//        protected event PropertyChangedEventHandler propertyChanged;

//        public IObservable<string> WhenPropertyChanged
//        {
//            get
//            {
//                return Observable
//                    .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
//                        h => this.propertyChanged += h,
//                        h => this.propertyChanged -= h)
//                    .Select(x => x.EventArgs.PropertyName);
//            }
//        }
//        public virtual void NotifyPropertyChanged(params string[] propertyNames)
//        {
//            foreach (string name in propertyNames)
//            {
//                OnPropertyChanged(new PropertyChangedEventArgs(name));
//            }
//        }
//        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
//        {
//            PropertyChangedEventHandler eventHandler = this.propertyChanged;
//            if (eventHandler != null)
//            {
//                eventHandler(this, e);
//            }
//        }

//        #endregion
//    }
//}
