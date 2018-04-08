 //using NullGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.Models
{
    public class GearClass : IMach
    {
        //[AllowNull]
        public string Name { get; set; }
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
