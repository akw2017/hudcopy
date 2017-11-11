//using NullGuard;
using AIC.CoreType;
using System;
using System.Collections.Generic;

namespace AIC.Domain
{
    public class Shaft : IMach
    {
        public Shaft()
        {
            RPMCoeff = 1;
            DefaultRPM = 6000;
            DeltaRPM = 100;
        }
        public int ID { get; set; }
        public Guid ShaftID { get; set; }
        //[AllowNull]
        public string Name { get; set; }
        //是否为滑动轴承
        public bool IsSlidingBearing { get; set; }
        //转速差
        public double DeltaRPM { get; set; }
        //默认转速
        public double DefaultRPM { get; set; }
        //转速系数，默认值为1
        public double RPMCoeff { get; set; }
        //[AllowNull]
        public List<IMachComponent> MachComponents { get; set; }
        //[AllowNull]
        public List<NegationDivFreStrategy> NegationDivFreStrategies { get; set; }
        //[AllowNull]
        public List<NaturalFre> NaturalFres { get; set; }
        //[AllowNull]
        public List<DivFreThresholdProportion> DivFreThresholdProportiones { get; set; }
        public FilterType FilterType { get; set; }
        public bool BindRPMForFilter { get; set; }
        //[AllowNull]
        public BandPassFilter BandPassFilter { get; set; }
        //[AllowNull]
        public HighPassFilter HighPassFilter { get; set; }
        //[AllowNull]
        public LowPassFilter LowPassFilter { get; set; }

    }
}
