using AIC.Core.Models;
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

namespace AIC.HomePage.Views
{
    /// <summary>
    /// Interaction logic for ServerSetWin.xaml
    /// </summary>
    public partial class ServerSetWin : MetroWindow
    {
        public delegate void TransferParaData(ServerInfo info, ModifyStatus mode);
        public event TransferParaData Parachanged;

        public ServerSetWin(ServerInfo info, ModifyStatus mode)
        {
            InitializeComponent();           
            
            Mode = mode;
            switch (mode)
            {
                case ModifyStatus.Add:
                    {
                        this.Title = "添加服务器";
                        ServerInfo = info;                       
                        break;
                    }
                case ModifyStatus.Edit:
                    {
                        this.Title = "修改服务器";
                        ServerInfo = info.ShallowClone();                       
                        break;
                    }
                case ModifyStatus.Delete:
                    {
                        this.Title = "删除服务器";
                        ServerInfo = info.ShallowClone();                       
                        txtName.IsEnabled = false;
                        txtIP.IsEnabled = false;
                        //txtPort.IsEnabled = false;
                        txtName.IsEnabled = false;
                        txtFactory.IsEnabled = false;
                        txtLongitude.IsEnabled = false;
                        txtLatitude.IsEnabled = false;
                        chkIsLogin.IsEnabled = false;
                        chkIsCloud.IsEnabled = false;
                        break;
                    }
                case ModifyStatus.Default:
                    {
                        this.Title = "设置默认服务器";
                        ServerInfo = info.ShallowClone();
                        ServerInfo.IsLogin = true;
                        txtName.IsEnabled = false;
                        txtIP.IsEnabled = false;
                        //txtPort.IsEnabled = false;
                        txtName.IsEnabled = false;
                        txtFactory.IsEnabled = false;
                        txtLongitude.IsEnabled = false;
                        txtLatitude.IsEnabled = false;
                        chkIsCloud.IsEnabled = false;
                        break;
                    }
                case ModifyStatus.Cloud:
                    {
                        this.Title = "设置为云服务器";                        
                        ServerInfo = info.ShallowClone();
                        ServerInfo.IsCloud = true;
                        txtName.IsEnabled = false;
                        txtIP.IsEnabled = false;
                        //txtPort.IsEnabled = false;
                        txtName.IsEnabled = false;
                        txtFactory.IsEnabled = false;
                        txtLongitude.IsEnabled = false;
                        txtLatitude.IsEnabled = false;
                        chkIsLogin.IsEnabled = false;
                        break;
                    }
            }
            this.DataContext = ServerInfo;
        }      

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
                   
            Parachanged(ServerInfo, Mode);
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        ServerInfo ServerInfo { get; set; }
        ModifyStatus Mode { get; set; }

        //ServerInfo = new ServerInfo()
        //{
        //    ID = info.ID,
        //    Name = info.Name,
        //    IP = info.Name,
        //    Port = info.Port,
        //    Factory = info.Factory,
        //    Longitude = info.Longitude,
        //    Latitude = info.Latitude,
        //    IsLogin = info.IsLogin,
        //    LoginResult = info.LoginResult,
        //    Permission = info.Permission,
        //};
    }
}
