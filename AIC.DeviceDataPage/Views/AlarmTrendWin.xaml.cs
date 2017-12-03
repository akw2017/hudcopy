using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.UserManageModels;
using AIC.Resources.Models;
using MahApps.Metro.Controls;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIC.UserPage.Views
{
    /// <summary>
    /// Interaction logic for ServerSetWin.xaml
    /// </summary>
    public partial class MenuSetWin : MetroWindow
    {
        public delegate void TransferParaData(IList<MyMenu> menu, string oldname, string newname, ModifyStatus mode);
        public event TransferParaData Parachanged;

        public MenuSetWin(List<T1_Menu> menu, string name, ModifyStatus mode)
        {
            InitializeComponent();
            MenuManageList = new MenuManageList();
            if (menu == null || menu.Count == 0)
            {
                MenuGuid = Guid.NewGuid();
            }
            else
            {
                MenuGuid = menu[0].Guid;
            }
            MenuName = name;
            oldname = name;

            Mode = mode;
            switch (mode)
            {
                case ModifyStatus.Add:
                    {
                        this.Title = "添加菜单";                                              
                        break;
                    }
                case ModifyStatus.Edit:
                    {
                        this.Title = "修改菜单";
                        menu.ForEach(p =>
                        {
                            if (MenuManageList.Dictionary.ContainsKey(p.InternalNumber))
                            {
                                MenuManageList.Dictionary[p.InternalNumber].IsChecked = true;
                                MenuManageList.Dictionary[p.InternalNumber].T_Menu.id = p.id;
                            }
                        });
                        break;
                    }
                case ModifyStatus.Delete:
                    {
                        this.Title = "删除菜单";
                        menu.ForEach(p => 
                        {
                            if (MenuManageList.Dictionary.ContainsKey(p.InternalNumber))
                            {
                                MenuManageList.Dictionary[p.InternalNumber].IsChecked = true;
                                MenuManageList.Dictionary[p.InternalNumber].T_Menu.id = p.id;
                            }
                        });
                        datagrid.IsEnabled = false;
                                                         
                        break;
                    }              
            }
            MenuList = new List<MyMenu>(MenuManageList.Dictionary.Values);
            this.DataContext = this;
        }      

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            newname = MenuName;
            foreach (var menu in MenuList)
            {
                menu.T_Menu.Name = MenuName;
                menu.T_Menu.Guid = MenuGuid;
                menu.T_Menu.Type = "1";
            }         
            Parachanged(MenuList, oldname, newname, Mode);
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private MenuManageList MenuManageList { get; set; }    
        public List<MyMenu> MenuList { get; set; }
        public string MenuName { get; set; }

        private string oldname;

        private string newname;

        private Guid MenuGuid;
        ModifyStatus Mode { get; set; }      
    }
}
