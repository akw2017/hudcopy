using AIC.Core.DataModels;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.M9600.Common.SlaveDB.Generated;
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

namespace AIC.HomePage.Views
{
    /// <summary>
    /// Interaction logic for EddyCurrentDisplacementSlotWin.xaml
    /// </summary>
    public partial class AlarmAckWin : MetroWindow
    {
        BaseAlarmSignal Sg;
        public AlarmAckWin(BaseAlarmSignal sg)
        {
            InitializeComponent();
            Sg = sg;
            this.DataContext = Sg;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
//#if XBAP
//            MessageBoxResult result = MessageBox.Show("确定要取消报警栏显示？", "取消显示确认", MessageBoxButton.OKCancel, MessageBoxImage.Question);
//#else
//            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("确定要取消报警栏显示？", "取消显示确认", MessageBoxButton.OKCancel, MessageBoxImage.Question);
//#endif
//            if (result == MessageBoxResult.OK)
//            {
                
//            }
            Sg.AlarmAck = true;
            this.Close();

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
