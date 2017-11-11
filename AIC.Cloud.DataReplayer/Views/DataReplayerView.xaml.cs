using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;

using System.Configuration;
using AIC.Cloud.DataReplayer.ViewModels;

namespace AIC.Cloud.DataReplayer.Views
{
    /// <summary>
    /// Interaction logic for DataReplayerView.xaml
    /// </summary>
    [Export("DataReplayerView")]
    public partial class DataReplayerView : UserControl
    {
        public DataReplayerView()
        {
            InitializeComponent();
            Loaded += DataReplayerView_Loaded;
        }

        private void DataReplayerView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= DataReplayerView_Loaded;
            int count = ViewModel.HistoricalDatas.Where(o => o.IsVisible == true).Count();
            if (count == 1 || count == 2)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth - 8;
                ViewModel.ItemHeight = (verticalScrollViewer.ActualHeight - 5) / count;
            }
            else if (count == 3 || count == 4)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth / 2 - 6;
                ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / 2 - 2;
            }
            else if (count == 5 || count == 6)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth / 3 - 4;
                ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / 2 - 2;
            }
            else if (count > 6)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth / 3 - 4.5;
                ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / Math.Ceiling((double)count / 3) - 2;
            }
        }

        [Import]
        DataReplayerViewModel ViewModel
        {
            get { return DataContext as DataReplayerViewModel; }
            set { this.DataContext = value; }
        }

        private void HorizontalAlignButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ItemWidth = verticalScrollViewer.ActualWidth / ViewModel.HistoricalDatas.Where(o => o.IsVisible == true).Count() - 3.5;
            ViewModel.ItemHeight = verticalScrollViewer.ActualHeight - 5;
        }

        private void VerticalAlignButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ItemWidth = verticalScrollViewer.ActualWidth - 15;
            ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / ViewModel.HistoricalDatas.Where(o => o.IsVisible == true).Count() - 1.5;
        }

        private void AllAlignButtonClick(object sender, RoutedEventArgs e)
        {
            int count = ViewModel.HistoricalDatas.Where(o => o.IsVisible == true).Count();
            if (count == 1 || count == 2)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth - 8;
                ViewModel.ItemHeight = (verticalScrollViewer.ActualHeight - 5) / count;
            }
            else if (count == 3 || count == 4)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth/2 - 6;
                ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / 2- 2;
            }
            else if (count == 5 || count == 6)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth / 3 - 4;
                ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / 2 - 2;
            }
            else if (count > 6)
            {
                ViewModel.ItemWidth = verticalScrollViewer.ActualWidth / 3 - 4.5;
                ViewModel.ItemHeight = verticalScrollViewer.ActualHeight / Math.Ceiling((double)count / 3) - 2;
            }
        }

        private void FilterDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()).ToString(); 
        }
    }
}
