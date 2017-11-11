using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AIC.Core.ControlModels
{

    public class MenuItemViewModel : BindableBase
    {
        public string Icon { get; set; }

        public string header;
        public string Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
                OnPropertyChanged("Header");
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

        public int Level { get; set; }

        public ObservableCollection<MenuItemViewModel> Children { get; set; }

        // ICommand addCommand;
        public ICommand Command { get; set; }
        public void AddChild(MenuItemViewModel child)
        {
            if (!Children.Contains(child))
            {               
                Children.Add(child);
            }
        }

        public MenuItemViewModel()
        {
            Children = new ObservableCollection<MenuItemViewModel>();
            Visibility = Visibility.Collapsed;
        }
        public MenuItemViewModel(string header, string icon, ICommand command)
        {
            Header = header;
            Icon = icon;
            Command = command;
            Children = new ObservableCollection<MenuItemViewModel>();
            Visibility = Visibility.Visible;
        }
        public MenuItemViewModel(string header, string icon, ICommand command, Visibility visibility)
        {
            Header = header;
            Icon = icon;
            Command = command;
            Children = new ObservableCollection<MenuItemViewModel>();
            Visibility = visibility;
        }
        public MenuItemViewModel(string header) : this(header, "", null) { }
    }

    public class SeparatorViewModel : MenuItemViewModel
    {
        public SeparatorViewModel() : base()
        {

        }
    }
}
