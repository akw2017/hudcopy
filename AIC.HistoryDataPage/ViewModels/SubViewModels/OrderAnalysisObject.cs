using Arction.Wpf.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.HistoryDataPage.ViewModels
{
    public class OrderAnalysisObject
    {
        public SurfacePoint[,] SurfacePointArray { get; set; }
        public double MaxYValue { get; set; }
        public double MaxXValue { get; set; }
        public double MaxZValue { get; set; }
        public ChannelToken Token { get; set; }
    }
}
