using AIC.Core.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIC.HomePage.Views
{
    /// <summary>
    /// Interaction logic for LoginWin.xaml
    /// </summary>
    public partial class PwdEditWin : MetroWindow
    {
        public delegate void TransferParaData(LoginInfo logininfo, string pwd);
        public event TransferParaData Parachanged;

        public LoginInfo LoginInfo;
        public PwdEditWin(LoginInfo logininfo)
        {            
            InitializeComponent();
            LoginInfo = logininfo;
            if (logininfo.ServerInfo.T_User == null)
            {
                logininfo.RrrorInformation = "特殊用户无法修改密码！！！";
                btnOK.IsEnabled = false;
            }
            this.DataContext = LoginInfo;
        }

        public void SetLogin(LoginInfo logininfo)
        {
            this.DataContext = null;
            this.DataContext = LoginInfo;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtPwdold.Password == LoginInfo.Password && txtPwdnew1.Password == txtPwdnew2.Password)
            {               
                waitring.Visibility = Visibility.Visible;
                btnClose.IsEnabled = false;
                Parachanged(LoginInfo, txtPwdnew2.Password);              
            }
            else
            {
                LoginInfo.RrrorInformation = "请确定密码栏没有错误提示！！！";
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {           
            this.Close();         
        }

        public void WaitStop()
        {
            waitring.Visibility = Visibility.Collapsed;
            btnClose.IsEnabled = true;
        }

        private void txtPwdold_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPwdold.Password != LoginInfo.Password)
            {
                errold.Visibility = Visibility.Visible;
            }
            else
            {
                errold.Visibility = Visibility.Collapsed;
            }
        }

        private void txtPwdnew2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPwdnew1.Password != txtPwdnew2.Password)
            {
                errnew.Visibility = Visibility.Visible;
            }
            else
            {
                errnew.Visibility = Visibility.Collapsed;
            }
        }
    }
}
