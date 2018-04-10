//using NullGuard;

using System;

namespace  AIC.Core.DiagnosticBaseModels
{
    public class ImpellerClass : IMach
    {
        public int ID { get; set; } = -1;//新增为-1
        public string Name { get; set; }
        public Guid ImpellerID { get; set; }
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
