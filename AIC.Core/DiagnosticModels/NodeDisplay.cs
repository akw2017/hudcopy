using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticModels
{
    public struct NodeDisplay
    {     
        public int Index { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double InputValue { get; set; }
        public double OutputValue { get; set; }       
    }

    public struct LineDisplay
    {
        public int[] Index { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double LineValue { get; set; }
    }
}
