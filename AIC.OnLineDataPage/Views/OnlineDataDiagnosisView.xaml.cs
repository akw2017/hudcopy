using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
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

namespace AIC.OnLineDataPage.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class OnlineDataDiagnosisView : UserControl, ICloseable
    {
        public OnlineDataDiagnosisView()
        {
            InitializeComponent();

            this.Closer = new CloseableHeader((string)Application.Current.Resources["menuOnlineDataDiagnosis"], true);
        }
        public CloseableHeader Closer { get; private set; }
    }
}