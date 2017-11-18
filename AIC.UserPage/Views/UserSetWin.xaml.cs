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
    public partial class UserSetWin : MetroWindow
    {
        public delegate void TransferParaData(T1_User info, string oldName, string oldpwd, ModifyStatus mode);
        public event TransferParaData Parachanged;

        public UserSetWin(T1_User user, List<T1_Role> role, List<T1_Menu> menu, List<T1_OrganizationPrivilege> organization, ModifyStatus mode)
        {
            InitializeComponent();
            oldName = user.Name;
            oldPwd = user.Password;

            Mode = mode;
            switch (mode)
            {
                case ModifyStatus.Add:
                    {
                        this.Title = "添加用户";
                        T_User = user;
                        T_User.Guid = Guid.NewGuid();
                        break;
                    }
                case ModifyStatus.Edit:
                    {
                        this.Title = "修改用户";
                        T_User = user.ShallowClone();                       
                        break;
                    }
                case ModifyStatus.Delete:
                    {
                        this.Title = "删除用户";
                        T_User = user.ShallowClone();                       
                        txtName.IsEnabled = false;
                        //txtCode.IsEnabled = false;
                        txtAliasName.IsEnabled = false;
                        txtPassword.IsEnabled = false;
                        cboRole.IsEnabled = false;
                        txtPersonTelephone.IsEnabled = false;
                        txtOfficeTelephone.IsEnabled = false;
                        txtEmail.IsEnabled = false;
                        txtContact.IsEnabled = false;                       
                        break;
                    }              
            }

            var submenu = (from p in menu group p by p.Name into s select s.FirstOrDefault()).ToList();
            var suborganization = (from p in organization group p by p.Name into s select s.FirstOrDefault()).ToList();

            T_UserAndRole = new T1_UserAddition() { T_User = T_User, T_Role = role, T_Menu = submenu, T_OrganizationPrivilege = suborganization};
            this.DataContext = T_UserAndRole;
        }      

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {          
            Parachanged(T_User, oldName, oldPwd, Mode);
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        T1_User T_User { get; set; }
        T1_UserAddition T_UserAndRole { get; set; }
        ModifyStatus Mode { get; set; }

        private string oldName;
        private string oldPwd;
    }
}
