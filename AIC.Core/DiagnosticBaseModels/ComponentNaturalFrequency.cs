using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticBaseModels
{
    public class ComponentNaturalFrequency
    {
        public ComponentNaturalFrequency(string name, double frequency)
        {
            Name = name;
            Frequency = frequency;
        }
        public string Name { get; set; }
        public double Frequency { get; set; }
    }
}
