 //using NullGuard;
using System;

namespace AIC.DiagnosePage.Models
{
    public class BeltClass : IMach
    {
        public string Name { get; set; }
        //皮带轮直径
        public double PulleyDiameter { get; set; }
        //皮带长度
        public double BeltLength { get; set; }

        public double Frequency => BeltLength != 0 ? (Math.PI * PulleyDiameter) / BeltLength : 0;
    }
}
