using AIC.Core.OrganizationModels;
using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DeviceDataPage.Models
{
    public class DeviceRunInfo : BindableBase
    {
        private DateTime startTime;
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
                OnPropertyChanged("StartTime");
            }
        }

        private DateTime endTime;
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
                OnPropertyChanged("EndTime");
            }
        }

        public List<RunInfo> RunInfo { get; set; }

        private double runHours;
        public double RunHours
        {
            get
            {
                return runHours;
            }
            set
            {
                runHours = value;
                OnPropertyChanged("RunHours");
            }
        }

        private double stopHours;
        public double StopHours
        {
            get
            {
                return stopHours;
            }
            set
            {
                stopHours = value;
                OnPropertyChanged("StopHours");
            }
        }

        private double totalHours;
        public double TotalHours
        {
            get
            {
                return totalHours;
            }
            set
            {
                totalHours = value;
                OnPropertyChanged("TotalHours");
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

        private double maxResult;
        public double MaxResult  //最大值      
        {
            get { return maxResult; }
            set
            {
                if (maxResult != value)
                {
                    maxResult = value;
                    OnPropertyChanged("MaxResult");
                }
            }
        }

        private string diagnosticInfo;
        public string DiagnosticInfo
        {
            get { return diagnosticInfo; }
            set
            {
                if (diagnosticInfo != value)
                {
                    diagnosticInfo = value;
                    OnPropertyChanged(() => DiagnosticInfo);
                }
            }
        }

        private string diagnosticAdvice;
        public string DiagnosticAdvice
        {
            get { return diagnosticAdvice; }
            set
            {
                if (diagnosticAdvice != value)
                {
                    diagnosticAdvice = value;
                    OnPropertyChanged(() => DiagnosticAdvice);
                }
            }
        }
        private string unit;
        public string Unit
        {
            get { return unit; }
            set
            {
                if (unit != value)
                {
                    unit = value;
                    OnPropertyChanged(() => Unit);
                }
            }
        }

        public DeviceTreeItemViewModel DeviceTreeItemViewModel { get; set; }

        public Guid RecordLab { get; set; }//诊断用

        public DateTime aCQDatetime;//诊断用
        public DateTime ACQDatetime
        {
            get
            {
                return aCQDatetime;
            }
            set
            {
                aCQDatetime = value;
                OnPropertyChanged("ACQDatetime");
            }
        }
        public Guid T_Item_Guid { get; set; }//诊断用
        public float RPM { get; set; }//诊断用

        private AlarmGrade alarmGrade;
        public AlarmGrade AlarmGrade
        {
            get { return alarmGrade; }
            set
            {
                if (alarmGrade != value)
                {
                    alarmGrade = value;
                    OnPropertyChanged(() => AlarmGrade);
                }
            }
        }

        public int ItemType { get; set; }
    }

    public class RunInfo
    {
        public DateTime Time { get; set; }
        public double RunHours { get; set; }
        public double Result { get; set; }
        public Guid RecordLab { get; set; }//诊断用
        public DateTime ACQDatetime { get; set; }//诊断用
        public Guid T_Item_Guid { get; set; }//诊断用
        public float RPM { get; set; }//诊断用
        public string Unit { get; set; }
        public AlarmGrade AlarmGrade { get; set; }
    }
}
