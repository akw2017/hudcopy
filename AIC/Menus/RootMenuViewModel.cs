using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIC.Menus
{
    public class RootMenuViewModel : MenuItemViewModel
    {
        private RootMenu _rootMenu;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"></see> class.
        /// </summary>
        /// <param name="parentViewModel">The parent view model.</param>
        public RootMenuViewModel(RootMenu rootMenu)
            : base(null)
        {
            _rootMenu = rootMenu;
            base.Header = RootName;
            //base.Command = RootCommand;
            if (_rootMenu.MenuItems != null)
                LoadChildMenuItems();
        }
        public string RootName
        {
            get
            {
                return _rootMenu.RootName;
            }
            set
            {
                _rootMenu.RootName = value;
                OnPropertyChanged("RootName");
            }
        }
        //public ICommand RootCommand
        //{
        //    get
        //    {
        //        return _rootMenu.Command;
        //    }
        //    set
        //    {
        //        _rootMenu.Command = value;
        //        OnPropertyChanged("RootCommand");
        //    }
        //}
        /// <summary>
        /// Loads the child menu items.
        /// </summary>
        public override void LoadChildMenuItems()
        {
            _rootMenu.MenuItems.ForEach
                (
                        MenuItem =>
                        {
                            if (MenuItem.MenuItemName == string.Empty || MenuItem.MenuItemName == "Separator")
                            {
                                SeparatorViewModel separatorViewModel = new SeparatorViewModel(this);
                                Children.Add(separatorViewModel);
                            }
                            else if (MenuItem.MenuItemName != null)
                            {
                                SecondMenuViewModel childMenuViewModel = new SecondMenuViewModel(this, MenuItem);
                                childMenuViewModel.Header = MenuItem.MenuItemName;
                                childMenuViewModel.CommandPara = MenuItem.CommandPara;
                                childMenuViewModel.Command = MenuItem.Command;
                                childMenuViewModel.Icon = MenuItem.Icon;
                                Children.Add(childMenuViewModel);
                            }
                        }
                );
        }
    }
}
