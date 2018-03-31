using AIC.Core.HardwareModels;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.PDAPage.Models;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIC.PDAPage.Views
{
    /// <summary>
    /// Interaction logic for DivFreInfoWin.xaml
    /// </summary>
    public partial class DivFreInfoWin : MetroWindow
    {
        public delegate void TransferParaData(DivFreInfo info);
        public event TransferParaData Parachanged;

        DivFreInfo Info = new DivFreInfo();
        public DivFreInfoWin(DivFreInfo info)
        {
            InitializeComponent();
            CardCopyHelper.DivFreInfoLeftCopyToRight(info, Info);    
            this.DataContext = Info;

            btnOK.IsEnabled = false;
            Info.PropertyChanged += Info_PropertyChanged;
            this.Closed += MetroWindow_Closed;
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Info.PropertyChanged -= Info_PropertyChanged;
        }

        private void Info_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            btnOK.IsEnabled = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {           
            Parachanged(Info);
            //this.Close();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
