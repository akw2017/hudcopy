using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  AIC.Core.DiagnosticBaseModels
{
    public class NegationDivFreStrategy
    {
        public int Code { get; set; }
        public string Fault { get; set; }
        public double RelativeY { get; set; }
        public double RelativeX { get; set; }
        public double RelativeZ { get; set; }
    }
}
