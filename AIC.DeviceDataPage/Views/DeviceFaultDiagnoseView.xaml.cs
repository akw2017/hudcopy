using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.Core.UserManageModels;
using AIC.DeviceDataPage.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.CloseTabControl;

namespace AIC.DeviceDataPage.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class DeviceFaultDiagnoseView : DisposableUserControl, ICloseable
    {
        public DeviceFaultDiagnoseView()
        {
            InitializeComponent();

            var menu = MenuManageList.GetMenu("menuDeviceFaultDiagnose");
            this.Closer = new CloseableHeader("menuDeviceFaultDiagnose", menu.Name, true, menu.IconPath);

            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }
        public CloseableHeader Closer { get; private set; }

        private DeviceFaultDiagnoseViewModel ViewModel
        {
            get { return DataContext as DeviceFaultDiagnoseViewModel; }
            set { this.DataContext = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ViewModel.TimeDomainOnLineVM.Close();
                ViewModel.FrequencyDomainOnLineVM.Close();
            }
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Window_Loaded;
            //testListBox.SizeChanged += testListBox_SizeChanged;
            //获取GridSplitterr的cotrolTemplate中的按钮btn，必须在Loaded之后才能获取到
            Button btnGrdSplitter = gsSplitterr.Template.FindName("btnExpend", gsSplitterr) as Button;
            if (btnGrdSplitter != null)
                btnGrdSplitter.Click += new RoutedEventHandler(btnGrdSplitter_Click);
        }

        GridLength m_WidthCache;
        void btnGrdSplitter_Click(object sender, RoutedEventArgs e)
        {
            GridLength temp = grdWorkbench.ColumnDefinitions[0].Width;
            GridLength zero = new GridLength(0);
            if (!temp.Equals(zero))
            {
                //折叠
                m_WidthCache = grdWorkbench.ColumnDefinitions[0].Width;
                grdWorkbench.ColumnDefinitions[0].Width = new GridLength(0);
            }
            else
            {
                //恢复
                grdWorkbench.ColumnDefinitions[0].Width = m_WidthCache;
            }
        }
    }
}