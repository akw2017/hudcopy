using AIC.Core.LMModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIC.Core.UserManageModels
{
    public class MyMenu : BindableBase
    {
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

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public string Name
        {
            get { return T_Menu.ShowText; }
        }

        public int Number
        {
            get { return T_Menu.InternalNumber; }
        }

        public string IconPath { get; set; }

        public T1_Menu T_Menu { get; set; }
        public MyMenu(int number, string text, string path = "/AIC.Resources;component/Images/application.png")
        {
            T_Menu = new T1_Menu();
            T_Menu.InternalNumber = number;
            T_Menu.ShowText = text;
            IconPath = path;
            Visibility = Visibility.Collapsed;
        }
    }
}
