/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.DTO
{
    public class SlotDiagnosticData
    {
        public double Result { get; set; }

        public int AlarmGrade { get; set; }

        public string Unit { get; set; }

        public DateTime ACQDateTime { get; set; }

        public Guid? RecordLab { get; set; }

        public double? RPM { get; set; }
    }
}
