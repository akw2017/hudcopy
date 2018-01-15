using AIC.Core.Events;
using AIC.Core.OrganizationModels;
using AIC.PDAPage.ViewModels;
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

namespace AIC.PDAPage.Views
{
    /// <summary>
    /// Interaction logic for PDASystemManageView.xaml
    /// </summary>
    public partial class ImportDBDataView : UserControl, ICloseable
    {
        private readonly IEventAggregator _eventAggregator;
        public ImportDBDataView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            
            this.Closer = new CloseableHeader((string)Application.Current.Resources["menuImportDBData"], true);

            // 这里是从后台工作线程触发，更新UI是跨线程操作，需要用Dispatcher.Invoke这种形式
            ((ImportDBDataViewModel)DataContext).NewMessageArrived += (o, args) =>
                Dispatcher.BeginInvoke(new Action(() => txtAllMessage.ScrollToEnd()), null);
        }
        public CloseableHeader Closer { get; private set; }

       
    }
   
}
