using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.Core.UserManageModels;
using AIC.QuickDataPage.ViewModels;
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

namespace AIC.QuickDataPage.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class ServerQucikDataView : UserControl, ICloseable
    {
        public ServerQucikDataView()
        {
            InitializeComponent();

            var menu = MenuManageList.GetMenu("menuServerQucikData");
            this.Closer = new CloseableHeader("menuServerQucikData", menu.Name, true, menu.IconPath);
        }
        public CloseableHeader Closer { get; private set; }

        private ServerQucikDataViewModel ViewModel
        {
            get { return DataContext as ServerQucikDataViewModel; }
            set { this.DataContext = value; }
        }

        public void GotoServer(ServerInfo serverinfo)
        {
            if (ViewModel != null)
            { 
                ViewModel.Init(serverinfo);
            }
        }
    }
}