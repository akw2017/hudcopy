using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIC.Menus
{
    class ChildMenuViewModel : MenuItemViewModel
    {
        private ChildItem _child;
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"></see> class.
        /// </summary>
        /// <param name="parentViewModel">The parent view model.</param>
        public ChildMenuViewModel(SecondMenuViewModel parentViewModel, ChildItem child)
            : base(parentViewModel)
        {
            _child = child;
        }
        public string ChildName
        {
            get
            {
                return _child.ChildName;
            }
            set
            {
                _child.ChildName = value;
                OnPropertyChanged("ChildName");
            }
        }
        public string ChildCommandPara
        {
            get
            {
                return _child.CommandPara;
            }
            set
            {
                _child.CommandPara = value;
                OnPropertyChanged("ChildCommandPara");
            }
        }
        public ICommand ChildCommand
        {
            get
            {
                return _child.Command;
            }
            set
            {
                _child.Command = value;
                OnPropertyChanged("ChildCommand");
            }
        }
    }
}
