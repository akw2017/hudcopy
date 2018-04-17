using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public class NaturalFre
    {
        public DivFreType DivFre { get; set; }
        public NaturalFreMode Mode { get; set; }
        //[AllowNull]
        public string Fault { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        //[AllowNull]
        public string Proposal { get; set; }
        //[AllowNull]
        public string Harm { get; set; }
    }
}
