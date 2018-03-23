using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.Core.UserManageModels;
using AIC.CoreType;
using AIC.OnLineDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.SeriesXY;
using Arction.Wpf.Charting.Views.ViewPie3D;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    public partial class OnlineDataStatisticsView : DisposableUserControl, ICloseable
    {
        public OnlineDataStatisticsView()
        {
            InitializeComponent();

            var menu = MenuManageList.GetMenu("menuOnlineDataStatistics");
            this.Closer = new CloseableHeader("menuOnlineDataStatistics", menu.Name, true, menu.IconPath);
           
            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }

        public CloseableHeader Closer { get; private set; }        

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Window_Loaded;
            //获取GridSplitterr的cotrolTemplate中的按钮btn，必须在Loaded之后才能获取到
            Button btnGrdSplitter = gsSplitterr.Template.FindName("btnExpend", gsSplitterr) as Button;
            if (btnGrdSplitter != null)
                btnGrdSplitter.Click += new RoutedEventHandler(btnGrdSplitter_Click);

            table.PreviewMouseWheel += table_PreviewMouseWheel;

            Timer timer = new Timer()
            {
                Interval = 100,
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.scrollvw.IsMouseOver != true)
            {
                this.scrollvw.Dispatcher.BeginInvoke(new changedelegate(ChangedOffset));
            }           
        }
        public delegate void changedelegate();
        public void ChangedOffset()
        {
            if (scrollvw.VerticalOffset + scrollvw.ViewportHeight == scrollvw.ExtentHeight)
            {
                scrollvw.ScrollToTop();
            }
            else
            {
                this.scrollvw.ScrollToVerticalOffset(scrollvw.VerticalOffset + 1);
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

        private void table_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseWheelEventArgs h = e;
            if (h != null)
            {
                h.Handled = true;
            }
        }
    }
}