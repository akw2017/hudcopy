using Arction.WPF.LightningChartUltimate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    public class OrderAnalysisObject
    {
        public SurfacePoint[,] SurfacePointArray { get; set; }
       // public IntensityPoint[] IntensityPoints { get; set; }
        //public IntensityPoint[,] IntensityPoints { get; set; }
        //public double FrequencyMultiple { get; set; }
        //public double Amplitude { get; set; }
        //public double RPM { get; set; }
        public double MaxYValue { get; set; }
        public double MaxXValue { get; set; }
        public double MaxZValue { get; set; }
        public ChannelToken Token { get; set; }
    }
}
