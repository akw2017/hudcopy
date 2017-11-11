using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIC.Menus
{
    public class SecondMenuViewModel : MenuItemViewModel
    {
        private MenuItem _menuClass;
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"></see> class.
        /// </summary>
        /// <param name="parentViewModel">The parent view model.</param>
        public SecondMenuViewModel(RootMenuViewModel parentViewModel, MenuItem menuClass)
            : base(parentViewModel)
        {
            _menuClass = menuClass;
            if (_menuClass.Childs != null)
                LoadChildMenuItems();
        }

        //private string _menuName;
        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        /// <value>The name of the team.</value>
        public string MenuName
        {
            get
            {
                return _menuClass.MenuItemName;
            }
            set
            {
                _menuClass.MenuItemName = value;
                OnPropertyChanged("MenuName");
            }
        }
        public ICommand ItemCommand
        {
            get
            {
                return _menuClass.Command;
            }
            set
            {
                _menuClass.Command = value;
                OnPropertyChanged("ItemCommand");
            }
        }


        public override void LoadChildMenuItems()
        {
            _menuClass.Childs.ForEach(child =>
            {
                if (child.ChildName == string.Empty || child.ChildName == "Separator")
                {
                    SeparatorViewModel separatorViewModel = new SeparatorViewModel(this);
                    Children.Add(separatorViewModel);
                }
                else
                {
                    ChildMenuViewModel childViewModel = new ChildMenuViewModel(this, child);
                    childViewModel.Header = child.ChildName;
                    childViewModel.CommandPara = child.CommandPara;
                    childViewModel.Command = child.Command;
                    childViewModel.Icon = child.Icon;
                    Children.Add(childViewModel);
                }
            });
        }
    }
}
