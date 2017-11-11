using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    public class DivFreMeanModel
    {
        public string GroupCOName { get; set; }
        public string CorporationName { get; set; }
        public string WorkShopName { get; set; }
        public string DevName { get; set; }
        public string DevSN { get; set; }
        public string Name { get; set; }
        public string MSSN { get; set; }
        public string FreDescription { get; set; }
        public double MeanValue { get; set; }
        public double MaximumValue { get; set; }
    }
}
