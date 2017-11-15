using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.PDAPage.Models;
using AIC.PDAPage.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIC.PDAPage.Views
{
    /// <summary>
    /// Interaction logic for MainControlCardCopyWin.xaml
    /// </summary>
    public partial class DeviceEditWin : MetroWindow
    {
        public delegate void TransferParaData(T_Organization organization, string remarks);
        public event TransferParaData Parachanged;

        T_Organization Organization;
        public DeviceEditWin(T_Organization organization)
        { 
            InitializeComponent();
            Organization = organization;
            txtRemarks.Text = organization.Remarks;
        }      

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Parachanged(Organization, txtRemarks.Text);
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }  

}
