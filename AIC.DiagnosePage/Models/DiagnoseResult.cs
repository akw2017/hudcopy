using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DiagnosePage.Models
{
    public class DiagnoseResult : BindableBase
    {
        private ObservableCollection<DiagnoseFault> descriptionCollection = new ObservableCollection<DiagnoseFault>();
        private string error;
        private string warning;
      

        public IEnumerable<DiagnoseFault> description { get { return descriptionCollection; } }
        public string Error
        {
            get { return error; }
            set { this.SetProperty(ref error, value); }
        }
        public string Warning
        {
            get { return warning; }
            set { this.SetProperty(ref warning, value); }
        }

        private string descriptionString;
        public string DescriptionString
        {
            get { return descriptionString; }
            set { this.SetProperty(ref descriptionString, value); }
        }

        public void SetDiagnosticResult(string name)
        {
            Name = name;
            if (Error != null && Error != "")
            {
                DescriptionString = "错误" + Error;
            }
            else
            {
                DescriptionString = null;
            }
            if (Warning != null && Warning != "")
            {
                DescriptionString += "警告" + Warning;
            }
            if (descriptionCollection.Count != 0)
            {              
                DescriptionString += "结论" + string.Join("\r\n", descriptionCollection.Select(p => p.Result));
            }
            if (DescriptionString == null)
            {
                Result = "正常";
            }
            else
            {
                Result = "异常";
            }
        }

        private string name;
        [JsonIgnore]
        public string Name
        {
            get { return name; }
            set { this.SetProperty(ref name, value);  }
        }
        private string result;
        [JsonIgnore]
        public string Result
        {
            get { return result; }
            set { this.SetProperty(ref result, value); }
        }

    }

    public class DiagnoseFault : BindableBase
    {
        private int code;
        private string fault;
        private string harm;
        private string proposal;

        public int Code
        {
            get { return code; }
            set { this.SetProperty(ref code, value); }
        }
        public string Fault
        {
            get { return fault; }
            set { this.SetProperty(ref fault, value); }
        }
        public string Harm
        {
            get { return harm; }
            set { this.SetProperty(ref harm, value); }
        }
        public string Proposal
        {
            get { return proposal; }
            set { this.SetProperty(ref proposal, value); }
        }

        public string Result
        {
            get { return Code.ToString() + "." + Fault + Harm + Proposal; }
        }
    }
}
