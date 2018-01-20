using AIC.CoreType;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using AIC.M9600.Common.DTO.Device;
using Prism.Events;
using Microsoft.Practices.ServiceLocation;
using AIC.Core.Events;
using AIC.Core.Models;
using System;
using AIC.Core.LMModels;

namespace AIC.Core.SignalModels
{
    public class BaseAlarmSignal : BaseTypeSignal
    {
        private IDisposable alarmGradeChangedSubscription;
        private IDisposable isNotOKChangedSubscription;
        private static IEventAggregator _eventAggregator;

        public BaseAlarmSignal(Guid guid)
        {
            Guid = guid;
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            BufferData = new List<TrendPointData>();
        }

        public void SubscribeAlarmGrade(double alarmDelay)
        {
            if (alarmGradeChangedSubscription != null)
            {
                alarmGradeChangedSubscription.Dispose();
            }
            alarmGradeChangedSubscription = WhenPropertyChanged.Where(o => o.ToString() == "AlarmGrade").Throttle(TimeSpan.FromMilliseconds(alarmDelay)).Subscribe(OnAlarmGradeChanged);
        }

        public void SubscribeIsNotOK(double notOKDelayAlarmTime)
        {
            if (isNotOKChangedSubscription != null)
            {
                isNotOKChangedSubscription.Dispose();
            }
            isNotOKChangedSubscription = WhenPropertyChanged.Where(o => o.ToString() == "IsNotOK").Throttle(TimeSpan.FromMilliseconds(notOKDelayAlarmTime)).Subscribe(OnIsNotOKChanged);
        }

        private void OnAlarmGradeChanged(string propertyName)
        {
            string alarmstring = GetAlarmEventString();
            short itemtype = GetSignType();
            CustomSystemDegree degree = (this.AlarmGrade == AlarmGrade.DisConnect)? CustomSystemDegree.Information : (CustomSystemDegree)(this.Low8Alarm);
            CustomSystemType alarm = (this.AlarmGrade == AlarmGrade.DisConnect) ? CustomSystemType.DisConnect : CustomSystemType.Alarm;

            DelayAlarmGrade = AlarmGrade;
            PublishMessage(alarm, degree, alarmstring, this.Guid, itemtype, this.ServerIP);
        }

        private void OnIsNotOKChanged(string propertyName)
        {
            string alarmstring = (IsNotOK == true) ? DeviceItemName + "." + "通道异常事件发生" : DeviceItemName + "." + "通道异常事件消失";
            short itemtype = GetSignType();

            DelayIsNotOK = IsNotOK;           
            PublishMessage(CustomSystemType.IsNotOK, CustomSystemDegree.Information, alarmstring, this.Guid, itemtype, this.ServerIP);
        }

        private string GetAlarmEventString()
        {
            string str = null;
            switch (DelayAlarmGrade)
            {
                case AlarmGrade.DisConnect:
                    switch (AlarmGrade)
                    {
                        case AlarmGrade.Invalid:
                        case AlarmGrade.LowNormal:
                        case AlarmGrade.HighNormal:
                            str = DeviceItemName + "." +  "断线事件恢复";
                            break;
                        case AlarmGrade.LowPreAlarm:
                        case AlarmGrade.HighPreAlarm:
                            str = DeviceItemName + "." +  "断线->预警事件发生";
                            break;
                        case AlarmGrade.LowAlarm:
                        case AlarmGrade.HighAlarm:
                            str = DeviceItemName + "." +  "断线->警告事件发生";
                            break;
                        case AlarmGrade.LowDanger:
                        case AlarmGrade.HighDanger:
                            str = DeviceItemName + "." +  "断线->危险事件发生";
                            break;
                    }
                    break;
                case AlarmGrade.Invalid:
                case AlarmGrade.LowNormal:
                case AlarmGrade.HighNormal:
                    switch (AlarmGrade)
                    {
                        case AlarmGrade.DisConnect:
                            str = DeviceItemName + "." +  "断线事件发生";
                            break;
                        case AlarmGrade.LowPreAlarm:
                        case AlarmGrade.HighPreAlarm:
                            str = DeviceItemName + "." +  "预警事件发生";
                            break;
                        case AlarmGrade.LowAlarm:
                        case AlarmGrade.HighAlarm:
                            str = DeviceItemName + "." +  "警告事件发生";
                            break;
                        case AlarmGrade.LowDanger:
                        case AlarmGrade.HighDanger:
                            str = DeviceItemName + "." +  "危险事件发生";
                            break;
                    }
                    break;
                case AlarmGrade.LowPreAlarm:
                case AlarmGrade.HighPreAlarm:
                    switch (AlarmGrade)
                    {
                        case AlarmGrade.DisConnect:
                            str = DeviceItemName + "." +  "断线事件发生";
                            break;
                        case AlarmGrade.Invalid:
                        case AlarmGrade.LowNormal:
                        case AlarmGrade.HighNormal:
                            str = DeviceItemName + "." +  "预警事件消失";
                            break;
                        case AlarmGrade.LowAlarm:
                        case AlarmGrade.HighAlarm:
                            str = DeviceItemName + "." +  "预警->警告事件发生";
                            break;
                        case AlarmGrade.LowDanger:
                        case AlarmGrade.HighDanger:
                            str = DeviceItemName + "." +  "预警->危险事件发生";
                            break;
                    }
                    break;
                case AlarmGrade.LowAlarm:
                case AlarmGrade.HighAlarm:
                    switch (AlarmGrade)
                    {
                        case AlarmGrade.DisConnect:
                            str = DeviceItemName + "." +  "断线事件发生";
                            break;
                        case AlarmGrade.Invalid:
                        case AlarmGrade.LowNormal:
                        case AlarmGrade.HighNormal:
                            str = DeviceItemName + "." +  "警告事件消失";
                            break;
                        case AlarmGrade.LowPreAlarm:
                        case AlarmGrade.HighPreAlarm:
                            str = DeviceItemName + "." +  "警告->预警事件发生";
                            break;
                        case AlarmGrade.LowDanger:
                        case AlarmGrade.HighDanger:
                            str = DeviceItemName + "." +  "警告->危险事件发生";
                            break;
                    }
                    break;
                case AlarmGrade.LowDanger:
                case AlarmGrade.HighDanger:
                    switch (AlarmGrade)
                    {
                        case AlarmGrade.DisConnect:
                            str = DeviceItemName + "." +  "断线事件发生";
                            break;
                        case AlarmGrade.Invalid:
                        case AlarmGrade.LowNormal:
                        case AlarmGrade.HighNormal:
                            str = DeviceItemName + "." +  "危险事件消失";
                            break;
                        case AlarmGrade.LowPreAlarm:
                        case AlarmGrade.HighPreAlarm:
                            str = DeviceItemName + "." +  "危险->预警事件发生";
                            break;
                        case AlarmGrade.LowAlarm:
                        case AlarmGrade.HighAlarm:
                            str = DeviceItemName + "." +  "危险->警告事件发生";
                            break;
                    }
                    break;
            }
            return str;
        }

        private short GetSignType()
        {
            if (this is IEPEChannelSignal)
            {
                return (short)ChannelType.IEPEChannelInfo;
            }
            else if (this is EddyCurrentDisplacementChannelSignal)
            {
                return (short)ChannelType.EddyCurrentDisplacementChannelInfo;
            }
            else if(this is EddyCurrentKeyPhaseChannelSignal)
            {
                return (short)ChannelType.EddyCurrentKeyPhaseChannelInfo;
            }
            else if (this is EddyCurrentTachometerChannelSignal)
            {
                return (short)ChannelType.EddyCurrentTachometerChannelInfo;
            }
            else if (this is DigitTachometerChannelSignal)
            {
                return (short)ChannelType.DigitTachometerChannelInfo;
            }
            else if (this is AnalogRransducerInChannelSignal)
            {
                return (short)ChannelType.AnalogRransducerInChannelInfo;
            }
            else if (this is AnalogRransducerOutChannelSignal)
            {
                return (short)ChannelType.AnalogRransducerOutChannelInfo;
            }
            else if (this is DigitRransducerInChannelSignal)
            {
                return (short)ChannelType.DigitRransducerInChannelInfo;
            }
            else if (this is DigitRransducerOutChannelSignal)
            {
                return (short)ChannelType.DigitRransducerOutChannelInfo;
            }
            else if (this is RelayChannelSignal)
            {
                return (short)ChannelType.RelayChannelInfo;
            }           
            else if (this is WirelessScalarChannelSignal)
            {
                return (short)ChannelType.WirelessScalarChannelInfo;
            }
            else if (this is WirelessVibrationChannelSignal)
            {
                return (short)ChannelType.WirelessVibrationChannelInfo;
            }
            return (short)ChannelType.None;
        }

        private void PublishMessage(CustomSystemType type, CustomSystemDegree grade, string alarmstring, Guid guid, short itemtype, string serverIP)
        {
            if (alarmstring == null || itemtype == (int)ChannelType.None || ACQDatetime == null)
            {
                return;
            }
            T1_SystemEvent systemEvent = new T1_SystemEvent()
            {
                Type = (int)type,
                Degree = (int)grade,
                EventTime = ACQDatetime.Value,
                Remarks = alarmstring,
                T_Item_Guid = guid,
                T_Item_Type = itemtype,
            };
            Tuple<string, T1_SystemEvent> ex = new Tuple<string, T1_SystemEvent>(serverIP, systemEvent);
            _eventAggregator.GetEvent<CustomSystemEvent>().Publish(ex);
        }
        #region 属性
        private DateTime? aCQDatetime;
        public DateTime? ACQDatetime//采集时间     
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

        public int Low8Alarm { get { return (int)AlarmGrade & 0xff; } }//低8位
        //报警限值  
        private AlarmLimit[] alarmLimit;
        public AlarmLimit[] AlarmLimit
        {
            get { return alarmLimit; }
            set
            {
                if (alarmLimit != value)
                {
                    alarmLimit = value;
                    OnPropertyChanged("AlarmLimit");
                    OnPropertyChanged("AlarmLimitString");
                    if (alarmLimit != null && alarmLimit.Length > 0)
                    {
                        AlarmMax = alarmLimit.Select(p => p.Limit).Max();
                    }
                }
            }
        }

        public string AlarmLimitString
        {
            get
            {
                if (AlarmLimit != null)
                {
                    return string.Join(" ", AlarmLimit.Select(p => p.Name + ":" + p.Limit.ToString("f1")));
                }
                else
                {
                    return null;
                }
            }
        }

        private double alarmMax = 100;
        public double AlarmMax
        {
            get { return alarmMax; }
            set
            {
                if (alarmMax != value)
                {
                    alarmMax = value;
                    OnPropertyChanged("AlarmMax");
                }
            }
        }       

        public byte? ACQ_Unit_Type { get; set; }//数采器类型
        public byte? AsySyn { get; set; }//异步/同步
        public byte? MainCardCode { get; set; }//主板代码
        public byte? SynWaveCode { get; set; }//波形代码
        public bool? IsHdBypass { get; set; }//硬件旁路
        public bool? IsHdMultiplication { get; set; }//硬件倍增
        public Guid? RecordLab { get; set; }//guid
        public Guid? SaveLab { get; set; }//guid
        public Guid? ContinueLab { get; set; }//guid      
        public object ExtraInfo { get; set; }//扩张信息

        private double? result;
        public double? Result //结果数据  
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

        public List<TrendPointData> BufferData { get; set; }//缓存数据

        private bool? isValidCH;
        public bool? IsValidCH //有效通道   
        {
            get { return isValidCH; }
            set
            {
                if (isValidCH != value)
                {
                    isValidCH = value;
                    OnPropertyChanged("IsValidCH");
                }
            }
        }

        public string ChannelHDID { get; set; }
        //public DateTime? UploadDatetime { get { return (this.IBaseAlarmSlot).UploadDatetime; } } //上传时间

        private string unit;
        public string Unit  //单位      
        {
            get { return unit; }
            set
            {
                if (unit != value)
                {
                    unit = value;
                    OnPropertyChanged("Unit");
                }
            }
        }

        private double alarmCount;
        public double AlarmCount  //报警次数      
        {
            get { return alarmCount; }
            set
            {
                if (alarmCount != value)
                {
                    alarmCount = value;
                    OnPropertyChanged("AlarmCount");
                }
            }
        }

        private double alarmTimeLength;
        public double AlarmTimeLength  //报警时间      
        {
            get { return alarmTimeLength; }
            set
            {
                if (alarmTimeLength != value)
                {
                    alarmTimeLength = value;
                    OnPropertyChanged("AlarmTimeLength");
                }
            }
        }

        private double dangerCount;
        public double DangerCount  //危险次数      
        {
            get { return dangerCount; }
            set
            {
                if (dangerCount != value)
                {
                    dangerCount = value;
                    OnPropertyChanged("DangerCount");
                }
            }
        }

        private double dangerTimeLength;
        public double DangerTimeLength  //危险时间    
        {
            get { return dangerTimeLength; }
            set
            {
                if (dangerTimeLength != value)
                {
                    dangerTimeLength = value;
                    OnPropertyChanged("DangerTimeLength");
                }
            }
        }

        private double invalidCount;
        public double InvalidCount  //无效次数      
        {
            get { return invalidCount; }
            set
            {
                if (invalidCount != value)
                {
                    invalidCount = value;
                    OnPropertyChanged("InvalidCount");
                }
            }
        }

        private double invalidTimeLength;
        public double InvalidTimeLength  //无效时间    
        {
            get { return invalidTimeLength; }
            set
            {
                if (invalidTimeLength != value)
                {
                    invalidTimeLength = value;
                    OnPropertyChanged("InvalidTimeLength");
                }
            }
        }

        private double normalCount;
        public double NormalCount  //正常次数      
        {
            get { return normalCount; }
            set
            {
                if (normalCount != value)
                {
                    normalCount = value;
                    OnPropertyChanged("NormalCount");
                }
            }
        }

        private double normalTimeLength;
        public double NormalTimeLength  //正常时间    
        {
            get { return normalTimeLength; }
            set
            {
                if (normalTimeLength != value)
                {
                    normalTimeLength = value;
                    OnPropertyChanged("NormalTimeLength");
                }
            }
        }

        private double preAlarmCount;
        public double PreAlarmCount  //预警次数      
        {
            get { return preAlarmCount; }
            set
            {
                if (preAlarmCount != value)
                {
                    preAlarmCount = value;
                    OnPropertyChanged("PreAlarmCount");
                }
            }
        }

        private double preAlarmTimeLength;
        public double PreAlarmTimeLength  //预警时间    
        {
            get { return preAlarmTimeLength; }
            set
            {
                if (preAlarmTimeLength != value)
                {
                    preAlarmTimeLength = value;
                    OnPropertyChanged("PreAlarmTimeLength");
                }
            }
        }

        private DateTime firstUploadTime;
        public DateTime FirstUploadTime      
        {
            get { return firstUploadTime; }
            set
            {
                if (firstUploadTime != value)
                {
                    firstUploadTime = value;
                    OnPropertyChanged("FirstUploadTime");
                }
            }
        }

        private DateTime lastUploadTime;
        public DateTime LastUploadTime
        {
            get { return lastUploadTime; }
            set
            {
                if (lastUploadTime != value)
                {
                    lastUploadTime = value;
                    OnPropertyChanged("LastUploadTime");
                }
            }
        }

        private bool alarmAck = false;
        public bool AlarmAck
        {
            get { return alarmAck; }
            set
            {
                if (alarmAck != value)
                {
                    alarmAck = value;
                    OnPropertyChanged("AlarmAck");
                }
            }
        }

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

        ////报警类型
        //private int alarmType;
        //public int AlarmType
        //{
        //    get { return alarmType; }
        //    set
        //    {
        //        if (alarmType != value)
        //        {
        //            alarmType = value;
        //            OnPropertyChanged("AlarmType");
        //        }
        //    }
        //}

        //延迟报警级别
        private AlarmGrade delayAlarmGrade = AlarmGrade.HighNormal;
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

        public double DelayAlarmTime { get; set; }
        public double NotOKDelayAlarmTime { get; set; }
        #endregion



        //旋转方向
        //private ForwardRotation forwardRotation;
        //public ForwardRotation ForwardRotation
        //{
        //    get { return forwardRotation; }
        //    set
        //    {
        //        if (forwardRotation != value)
        //        {
        //            forwardRotation = value;
        //            OnPropertyChanged("ForwardRotation");
        //        }
        //    }
        //}

    }
}
