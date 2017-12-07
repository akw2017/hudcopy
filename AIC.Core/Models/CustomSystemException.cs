using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Models
{
    public class CustomSystemException
    {
        public long id { get; set; }
        public int Type { get; set; }
        public int Degree { get; set; }

        public CustomSystemDegree CustomSystemDegree
        {
            get
            {
                return (CustomSystemDegree)Degree;
            }
        }

        public CustomSystemType CustomSystemType
        {
            get
            {
                return (CustomSystemType)Type;
            }
        }
        public DateTime EventTime { get; set; }
        public string Remarks { get; set; }
        public Guid T_Item_Guid { get; set; }
        public int T_Item_Type { get; set; }
    }
}
