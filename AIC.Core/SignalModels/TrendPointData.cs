using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.SignalModels
{
    public class TrendPointData
    {
        public DateTime ACQDateTime { get; set; }
        public double Result { get; set; }
        public string Unit { get; set; }
        public int AlarmGrade { get; set; }

        public Guid RecordLab { get; set; }
        public bool IsValidWave { get; set; }
        public TrendPointData(DateTime acqdateTime, double result, string unit, int alarmGrade)
        {
            ACQDateTime = acqdateTime;
            Result = result;
            Unit = unit;
            AlarmGrade = alarmGrade;
        }
    }
}
