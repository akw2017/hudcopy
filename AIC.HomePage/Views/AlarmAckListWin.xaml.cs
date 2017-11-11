using AIC.Core.DataModels;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.M9600.Common.SlaveDB.Generated;
using MahApps.Metro.Controls;
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

namespace AIC.HomePage.Views
{
    /// <summary>
    /// Interaction logic for EddyCurrentDisplacementSlotWin.xaml
    /// </summary>
    public partial class AlarmAckListWin : MetroWindow
    {
        public AlarmAckListWin(ObservableCollection<BaseAlarmSignal> sglist)
        {
            InitializeComponent();
            this.DataContext = sglist;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

           
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
