﻿using AIC.Core.Models;
using MahApps.Metro.Controls;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIC.CYSHPage.Views
{
    /// <summary>
    /// Interaction logic for LoginWin.xaml
    /// </summary>
    public partial class LoginWin : MetroWindow
    {
        public delegate void TransferParaData(LoginInfo logininfo);
        public event TransferParaData Parachanged;

        public LoginInfo LoginInfo;
        public LoginWin(LoginInfo logininfo)
        {
            LoginInfo = logininfo;
            InitializeComponent();
            this.DataContext = LoginInfo;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (LoginInfo.Error == "")
            {
                waitring.Visibility = Visibility.Visible;
                Parachanged(LoginInfo);              
            }
            else
            {
                ;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {           
            this.Close();         
        }

        public void WaitStop()
        {
            waitring.Visibility = Visibility.Collapsed;
        }


    }
}
