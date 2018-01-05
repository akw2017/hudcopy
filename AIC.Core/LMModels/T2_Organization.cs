using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T2_Organization : T1_Organization
    {
        private string structure;
        public string Structure//结构
        {
            get
            {
                return structure;
            }
            set
            {
                structure = value;
                NotifyPropertyChange("Structure");
            }
        }

        private string operate;
        public string Operate//操作，增，删，改
        {
            get
            {
                return operate;
            }
            set
            {
                operate = value;
                NotifyPropertyChange("Operate");
            }
        }

        private string hint;
        public string Hint//提示
        {
            get
            {
                return hint;
            }
            set
            {
                hint = value;
                NotifyPropertyChange("Hint");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
