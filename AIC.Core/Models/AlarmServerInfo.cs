using AIC.Core.SignalModels;
using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Models
{
    public class AlarmServerInfo
    {
        public BaseAlarmSignal BaseAlarmSignal { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public int AlarmGrade { get; set; }//1正常，2预警，3报警，4危险
        public double AlarmRate { get; set; }
        public int DangerNumber { get; set; }
        public int AlarmNumber { get; set; }
        public int PreAlarmNumber { get; set; }
        public List<Tuple<DateTime, bool>> DateRunning { get; set; }
    }
}
