using AIC.Core.Events;
using AIC.Core.OrganizationModels;
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
    public partial class ExportDBDataView : UserControl, ICloseable
    {
        private readonly IEventAggregator _eventAggregator;
        public ExportDBDataView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            
            this.Closer = new CloseableHeader("menuExportDBData", (string)Application.Current.Resources["menuExportDBData"], true);
        }
        public CloseableHeader Closer { get; private set; }

       
    }
   
}
