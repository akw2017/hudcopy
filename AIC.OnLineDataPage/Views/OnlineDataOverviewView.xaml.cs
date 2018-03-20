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
using Wpf.CloseTabControl;
using AIC.Core.UserManageModels;
using AIC.OnLineDataPage.ViewModels;

namespace AIC.OnLineDataPage.Views
{
    /// <summary>
    /// Interaction logic for OnLineTileView.xaml
    /// </summary>
    public partial class OnlineDataOverviewView : UserControl, ICloseable
    {
        public OnlineDataOverviewView()
        {
            InitializeComponent();

            var menu = MenuManageList.GetMenu("menuOnlineDataOverview");
            this.Closer = new CloseableHeader("menuOnlineDataOverview", menu.Name, true, menu.IconPath);

            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }

        private OnlineDataOverviewViewModel ViewModel
        {
            get { return DataContext as OnlineDataOverviewViewModel; }
            set { this.DataContext = value; }
        }

        public CloseableHeader Closer { get; private set; }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Window_Loaded;
            HorizontalAlignInit();
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

        private void HorizontalAlignInit()
        {
            ViewModel_UpdateListShow(4, 4);
        }

        private void ViewModel_UpdateListShow(int row, int column)
        {
            ViewModel.ItemWidth = (listview.ActualWidth - 40 - 6 * (column + 1)) / column;
            ViewModel.ItemHeight = (listview.ActualHeight - 20 - 6 * (row + 1)) / row;
        } 
    }
}
