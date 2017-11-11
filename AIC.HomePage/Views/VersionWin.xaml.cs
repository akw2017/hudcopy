using AIC.Core;
using AIC.Core.Models;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Forms;
using System.Configuration;
using System;
using AIC.Core.Events;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace AIC.HomePage.Views
{
    /// <summary>
    /// Interaction logic for LoginWin.xaml
    /// </summary>
    public partial class VersionWin : MetroWindow
    {
        public VersionWin()
        {
            InitializeComponent();
            versiontxt.Text = LocalAddress.Version;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
      
}
