using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIC.Menus
{
    public class MenuItemViewModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
        /// </summary>
        /// <param name="parentViewModel">The parent view model.</param>
        public MenuItemViewModel(MenuItemViewModel parentViewModel)
        {
            ParentViewModel = parentViewModel;
            _childMenuItems = new ObservableCollection<MenuItemViewModel>();
        }

        private ObservableCollection<MenuItemViewModel> _childMenuItems;
        /// <summary>
        /// Gets the child menu items.
        /// </summary>
        /// <value>The child menu items.</value>
        public ObservableCollection<MenuItemViewModel> Children
        {
            get
            {
                return _childMenuItems;
            }
        }

        private string _header;
        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
                OnPropertyChanged("Header");
            }
        }
        private string _childlx;
        /// <summary>
        /// Gets or sets the CommandParameter.
        /// </summary>
        /// <value>The CommandParameter.</value>
        public string CommandPara
        {
            get
            {
                return _childlx;
            }
            set
            {
                _childlx = value;
                OnPropertyChanged("Childlx");
            }
        }
        /// <summary>
        /// Gets or sets the Command.
        /// </summary>
        /// <value>The Command.</value>
        private ICommand command;
        public ICommand Command
        {
            get
            {
                return command;
            }
            set
            {
                command = value;
                OnPropertyChanged("Command");
            }
        }

        private string icon;
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

        /// <summary>
        /// Gets or sets the parent view model.
        /// </summary>
        /// <value>The parent view model.</value>
        public MenuItemViewModel ParentViewModel { get; set; }

        public virtual void LoadChildMenuItems()
        {

        }
    }
}
