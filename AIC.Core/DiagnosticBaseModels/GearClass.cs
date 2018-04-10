 //using NullGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  AIC.Core.DiagnosticBaseModels
{
    public class GearClass : IMach
    {
        public int ID { get; set; } = -1;//新增为-1
        public string Name { get; set; }
        public Guid BeltID { get; set; }
        public int TeethNumber { get; set; }

        //public Gear Clone()
        //{
        //    return new Gear()
        //    {
        //        Name = this.Name,
        //        TeethNumber = this.TeethNumber,
        //    };
        //}

        //IMach IMach.Clone()
        //{
        //    return Clone();
        //}
    }
}
