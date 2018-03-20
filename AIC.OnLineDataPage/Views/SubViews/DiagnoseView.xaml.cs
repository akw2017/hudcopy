using AIC.Core.SignalModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIC.OnLineDataPage.Views.SubViews
{
    /// <summary>
    /// DiagnoseView.xaml 的交互逻辑
    /// </summary>
    public partial class DiagnoseView : ChartViewBase
    {
        public DiagnoseView()
        {
            InitializeComponent();
        }

        protected override void UpdateChart(object args)
        {

            if (ViewModel == null || !(ViewModel.Signal is BaseWaveSignal))
            {
                return;
            }
            (ViewModel.Signal as BaseWaveSignal).IsDiagnostic = true;

        }
    }
}
