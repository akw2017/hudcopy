 //using NullGuard;
using System;

namespace AIC.Core.DiagnosticBaseModels
{
    public class BeltClass : IMach
    {
        public int ID { get; set; } = -1;//新增为-1
        public string Name { get; set; }
        public Guid BeltID { get; set; }
        //皮带轮直径
        public double PulleyDiameter { get; set; }
        //皮带长度
        public double BeltLength { get; set; }

        public double Frequency => BeltLength != 0 ? (Math.PI * PulleyDiameter) / BeltLength : 0;
    }
}
