 //using NullGuard;
using System;

namespace AIC.Domain
{
    public class Belt : IMach
    {
        //[AllowNull]
        public string Name { get; set; }
        //皮带轮直径
        public double PulleyDiameter { get; set; }
        //皮带长度
        public double BeltLength { get; set; }

        public double Frequency => BeltLength != 0 ? (Math.PI * PulleyDiameter) / BeltLength : 0;

        //public Belt Clone()
        //{
        //    return new Belt()
        //    {
        //        Name = this.Name,
        //        PulleyDiameter = this.PulleyDiameter,
        //        BeltLength = this.BeltLength,
        //    };
        //}

        //IMach IMach.Clone()
        //{
        //    return Clone();
        //}
    }
}
