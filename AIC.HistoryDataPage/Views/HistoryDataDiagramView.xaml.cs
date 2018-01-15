using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.HistoryDataPage.ViewModels;
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

namespace AIC.HistoryDataPage.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class HistoryDataDiagramView : UserControl, ICloseable
    {
        public HistoryDataDiagramView(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            this.Closer = new CloseableHeader((string)Application.Current.Resources["menuHistoryDataDiagram"], true);
            Loaded += HistoryDataDiagramView_Loaded;

        }
        public CloseableHeader Closer { get; private set; }

        private void HistoryDataDiagramView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= HistoryDataDiagramView_Loaded;
            VerticalAlignButtonClick(null, null);
            //获取GridSplitterr的cotrolTemplate中的按钮btn，必须在Loaded之后才能获取到
            Button btnGrdSplitter = gsSplitterr.Template.FindName("btnExpend", gsSplitterr) as Button;
            if (btnGrdSplitter != null)
                btnGrdSplitter.Click += new RoutedEventHandler(btnGrdSplitter_Click);
        }

        HistoryDataDiagramViewModel ViewModel
        {
            get { return DataContext as HistoryDataDiagramViewModel; }
            set { this.DataContext = value; }
        }

        private void HorizontalAlignButtonClick(object sender, RoutedEventArgs e)
        {
            int count = ViewModel.HistoricalDatas.Where(o => o.IsVisible == true).Count();
            int subcount = (ViewModel.ShowTimeDomain ? 1 : 0) + (ViewModel.ShowFrequencyDomain ? 1 : 0) + (ViewModel.ShowPowerSpectrum ? 1 : 0) + (ViewModel.ShowPowerSpectrumDensity ? 1 : 0);
            if (ViewModel.IsMulticursor == true)
            {
                count = count - subcount;
            }
           
            ViewModel.ItemWidth = verticalScrollViewer.ActualWidth / count - 20;
            ViewModel.ItemHeight = verticalScrollViewer.ActualHeight - 5;
        }

        private void VerticalAlignButtonClick(object sender, RoutedEventArgs e)
        {
            int count = ViewModel.HistoricalDatas.Where(o => o.IsVisible == true).Count();
            int subcount = (ViewModel.ShowTimeDomain ? 1 : 0) + (ViewModel.ShowFrequencyDomain ? 1 : 0) + (ViewModel.ShowPowerSpectrum ? 1 : 0) + (ViewModel.ShowPowerSpectrumDensity ? 1 : 0);
            if (ViewModel.IsMulticursor == true)
            {
                count = count - subcount;
            }

            ViewModel.ItemWidth = verticalScrollViewer.ActualWidth - 60;
            ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / count - 5;
        }

        private void AllAlignButtonClick(object sender, RoutedEventArgs e)
        {
            int count = ViewModel.HistoricalDatas.Where(o => o.IsVisible == true).Count();
            int subcount = (ViewModel.ShowTimeDomain ? 1 : 0) + (ViewModel.ShowFrequencyDomain ? 1 : 0) + (ViewModel.ShowPowerSpectrum ? 1 : 0) + (ViewModel.ShowPowerSpectrumDensity ? 1 : 0);
            if (ViewModel.IsMulticursor == true)
            {
                count = count - subcount;               
            }
            if (count == 1 || count == 2)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth - 60;
                ViewModel.ItemHeight = (verticalScrollViewer.ActualHeight - 5) / count;
            }
            else if (count == 3 || count == 4)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth / 2 - 40;
                ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / 2 - 5;
            }
            else if (count == 5 || count == 6)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth / 3 - 20;
                ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / 2 - 5;
            }
            else if (count > 6)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth / 3 - 10;
                ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / Math.Ceiling((double)count / 3) - 5;
            }
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