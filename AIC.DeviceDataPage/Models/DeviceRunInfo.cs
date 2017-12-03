using AIC.Core.OrganizationModels;
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
        private DateTime startTime { get; set; }
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

        private DateTime endTime { get; set; }
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

        public DeviceTreeItemViewModel DeviceTreeItemViewModel { get; set; }
}
}
