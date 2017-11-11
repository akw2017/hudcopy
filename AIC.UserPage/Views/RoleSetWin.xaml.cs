using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.UserManageModels;
using AIC.Resources.Models;
using MahApps.Metro.Controls;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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
    public partial class RoleSetWin : MetroWindow
    {
        public delegate void TransferParaData(T1_Role info, string oldName, ModifyStatus mode);
        public event TransferParaData Parachanged;

        public RoleSetWin(T1_Role role, ModifyStatus mode)
        {
            InitializeComponent();
            oldName = role.Name;

            Mode = mode;
            switch (mode)
            {
                case ModifyStatus.Add:
                    {
                        this.Title = "添加用户";
                        T_Role = role;
                        T_Role.Guid = Guid.NewGuid();
                        break;
                    }
                case ModifyStatus.Edit:
                    {
                        this.Title = "修改用户";
                        T_Role = role.ShallowClone();                       
                        break;
                    }
                case ModifyStatus.Delete:
                    {
                        this.Title = "删除用户";
                        T_Role = role.ShallowClone();                       
                        txtName.IsEnabled = false;
                        //txtCode.IsEnabled = false;
                        txtSortNo.IsEnabled = false;
                        chkIsAdmin.IsEnabled = false;
                        chkIsSuperAdmin.IsEnabled = false;                                       
                        break;
                    }              
            }
          
            this.DataContext = T_Role;
        }      

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {           
            Parachanged(T_Role, oldName, Mode);
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        T1_Role T_Role { get; set; }       
        ModifyStatus Mode { get; set; }

        private string oldName;
    }
}
