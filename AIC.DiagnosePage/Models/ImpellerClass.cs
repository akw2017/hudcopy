 //using NullGuard;

namespace AIC.DiagnosePage.Models
{
    public class ImpellerClass : IMach
    {
        //[AllowNull]
        public string Name { get; set; }
        //叶片数
        public int NumberOfBlades { get; set; }

        //public Impeller Clone()
        //{
        //    return new Impeller()
        //    {
        //        Name = this.Name,
        //        NumberOfBlades = this.NumberOfBlades,
        //    };
        //}

        //IMach IMach.Clone()
        //{
        //    return Clone();
        //}
    }
}
