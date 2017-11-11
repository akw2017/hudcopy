using AIC.Core;
using AIC.Core.Models;
using MahApps.Metro.Controls;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
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
    public partial class AboutWin : MetroWindow
    {
        public AboutWin()
        {
            InitializeComponent();
            this.DataContext = this;//new AboutWinViewModel();
            txtVersion.Text = GetEdition();
        }

        private ICommand settingCommand;
        public ICommand SettingCommand
        {
            get
            {
                return this.settingCommand ?? (this.settingCommand = new DelegateCommand(() => this.Setting()));
            }
        }

        private void Setting()
        {
            DefaultSettingWin win = new DefaultSettingWin();
            win.ShowDialog();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 获取当前系统的版本号
        /// </summary>
        /// <returns></returns>
        private static string GetEdition()
        {
            //string str = "程序集版本：" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //try
            //{
            //    str += "部署版本：" + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            //}
            //catch { }
            string str = LocalAddress.Version;
            return str;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            System.Diagnostics.Process.Start(link.NavigateUri.AbsoluteUri);
        }
    }
}
