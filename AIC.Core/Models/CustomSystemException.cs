using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Models
{
    public class CustomSystemException
    {
        public int Type { get; set; }
        public int Degree { get; set; }
        public DateTime EventTime { get; set; }
        public string Remark { get; set; }
        public Guid T_Item_Guid { get; set; }
        public int T_Item_Type { get; set; }
    }
}
