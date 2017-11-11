using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIC.Menus
{
    public class RootMenu
    {
        public string RootName { get; set; }
        //public ICommand Command { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public RootMenu(string rootName)
        {
            RootName = rootName;
            MenuItems = new List<MenuItem>();
        }
    }
    public class MenuItem
    {
        public string MenuItemName { get; set; }
        public string CommandPara { get; set; }
        public ICommand Command { get; set; }
        public string Icon { get; set; }
        public List<ChildItem> Childs { get; set; }
        public MenuItem(string menuName, string para, ICommand command, string icon)
        {
            MenuItemName = menuName;
            CommandPara = para;
            Command = command;
            Icon = icon;
            Childs = new List<ChildItem>();
        }
        public MenuItem(string menuName, ICommand command, string icon) : this(menuName, menuName, command, icon) { }
       
        public MenuItem(string menuName, string para, ICommand command) : this(menuName, para, command, "") { }

        public MenuItem(string menuName, ICommand command) : this(menuName, menuName, command, "") { }

        public MenuItem(string menuName) 
        {
            MenuItemName = menuName;
            Childs = new List<ChildItem>();
        }
        public MenuItem() { }

    }
    public class ChildItem
    {

        public string ChildName { get; set; }
        public string CommandPara { get; set; }
        public ICommand Command { get; set; }
        public string Icon { get; set; }
        public MenuItem MenuItem { get; set; }
        public ChildItem(MenuItem menuClass, string childName, string para, ICommand command, string icon)
        {
            MenuItem = menuClass;
            ChildName = childName;
            CommandPara = para;
            Command = command;
            Icon = icon;
        }
        public ChildItem(MenuItem menuClass, string childName, ICommand command, string icon) : this(menuClass, childName, childName, command, icon) { }

        public ChildItem(string childName)
        {
            ChildName = childName;
        }
        public ChildItem() { }

    }
    public class SeparatorViewModel : MenuItemViewModel
    {
        public SeparatorViewModel(MenuItemViewModel parentViewModel)
            : base(parentViewModel)
        {

        }
    }
}
