using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.PDAPage.Models
{
    public class OrganizationSort : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private int sort_No;
        public int Sort_No
        {
            get { return sort_No; }
            set { sort_No = value; OnPropertyChanged("Sort_No"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
