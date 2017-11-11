//using NullGuard;

using AIC.CoreType;

namespace AIC.Domain
{
    public class NaturalFre
    {
        public DivFreType DivFre { get; set; }
        public NaturalFreMode Mode { get; set; }
        //[AllowNull]
        public string Fault { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        //[AllowNull]
        public string Proposal { get; set; }
        //[AllowNull]
        public string Harm { get; set; }
    }
}
