using AIC.Core.HardwareModels;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.DiagnosePage.Models;
using AIC.DiagnosePage.TestDatas;
using AIC.DiagnosePage.ViewModels;
using AIC.PDAPage.Models;
using MahApps.Metro.Controls;
using Prism.Commands;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIC.DiagnosePage.Views
{
    /// <summary>
    /// Interaction logic for MainControlCardWin.xaml
    /// </summary>
    public partial class EditDeviceComponentsWin : MetroWindow
    {
        public EditDeviceComponentsWin()
        {           
            InitializeComponent();
        }

        private EditDeviceComponentsViewModel ViewModel
        {
            get { return mydevice.DataContext as EditDeviceComponentsViewModel; }
            set { mydevice.DataContext = value; }
        }

        public void GotoDevice(DeviceDiagnosisModel device)
        {
            if (ViewModel != null)
            {
                ViewModel.Init(device);
            }
        }
    }
}
