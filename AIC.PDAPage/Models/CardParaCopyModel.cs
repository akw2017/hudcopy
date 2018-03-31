using AIC.Core.HardwareModels;
using AIC.Core.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.PDAPage.Models
{
    public class CardParaCopyModel : BindableBase
    {
        public string Name { get; set; }
        public bool CopyChecked { get; set; }
        public int CopyType { get; set; }
        public List<ICategory> Category { get; set; }

        public bool CopyBoolPara { get; set; }
        public bool OldBoolPara { get; set; }

        private bool newBoolPara;
        public bool NewBoolPara
        {
            get
            {
                return newBoolPara;
            }
            set
            {
                newBoolPara = value;
                OnPropertyChanged("NewBoolPara");
            }
        }

        public int CopyIntPara { get; set; }
        public int OldIntPara { get; set; }

        private int newIntPara;
        public int NewIntPara
        {
            get
            {
                return newIntPara;
            }
            set
            {
                newIntPara = value;
                OnPropertyChanged("NewIntPara");
            }
        }

        public string CopyStringPara { get; set; }
        public string OldStringPara { get; set; }

        private string newStringPara;
        public string NewStringPara
        {
            get
            {
                return newStringPara;
            }
            set
            {
                newStringPara = value;
                OnPropertyChanged("NewStringPara");
            }
        }


    }
}
