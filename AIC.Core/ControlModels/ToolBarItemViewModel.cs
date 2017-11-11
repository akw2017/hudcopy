using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AIC.Core.ControlModels
{
    public class ToolBarItemViewModel : BindableBase
    {
        private string text;

        private string icon;  

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public string Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }

        private Visibility visibility;
        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public ICommand Command { get; set; }
       
    }
    public class SeparatorItemViewModel : ToolBarItemViewModel
    {
        public SeparatorItemViewModel() : base()
        {

        }
    }
}
