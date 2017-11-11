using AIC.Server.Storage.Contract;
using Arction.WPF.LightningChartUltimate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    public class RPM3DSpectrumObject
    {
       // public SurfacePoint[] SurfacePoints { get; set; }
       // public Dictionary<VInfoTableContract, float[]> DataSource { get; set; }

        public Dictionary<Tuple<double,double>, double[]> DataSource { get; set; }
        public double RangeMinX { get; set; }
        public double RangeMaxX { get; set; }
        public double RangeMinY { get; set; }
        public double RangeMaxY { get; set; }
        public double RangeMinZ { get; set; }
        public double RangeMaxZ { get; set; }
        public ChannelToken Token { get; set; }
    }
}
