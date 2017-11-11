using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HistoryDataPage.ViewModels
{
    public class RPM3DSpectrumObject
    {
        public Dictionary<Tuple<double, double>, double[]> DataSource { get; set; }
        public double RangeMinX { get; set; }
        public double RangeMaxX { get; set; }
        public double RangeMinY { get; set; }
        public double RangeMaxY { get; set; }
        public double RangeMinZ { get; set; }
        public double RangeMaxZ { get; set; }
        public ChannelToken Token { get; set; }
    }
}
