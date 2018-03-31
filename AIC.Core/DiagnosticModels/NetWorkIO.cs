using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticModels
{
    public class NetWorkIO : BindableBase
    {
        public double[] Input { get; set; }
        public double[] Output { get; set; }

        private string diagnosticResult;
        public string DiagnosticResult
        {
            get { return diagnosticResult; }
            set
            {
                diagnosticResult = value;
                OnPropertyChanged("DiagnosticResult");
            }
        }
    }
}
