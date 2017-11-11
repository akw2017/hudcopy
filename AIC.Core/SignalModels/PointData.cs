using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class PointData
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public int AlarmGrade { get; set; }
        public PointData(DateTime dateTime, double value, string unit, int alarmGrade)
        {
            DateTime = dateTime;
            Value = value;
            Unit = unit;
            AlarmGrade = alarmGrade;
        }
    }
}
