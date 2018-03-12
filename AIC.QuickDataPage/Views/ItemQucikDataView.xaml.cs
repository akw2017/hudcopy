using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.Core.SignalModels;
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
    public partial class ItemQucikDataView : DisposableUserControl, ICloseable
    {
        public ItemQucikDataView()
        {
            InitializeComponent();

            var menu = MenuManageList.GetMenu("menuItemQucikData");
            this.Closer = new CloseableHeader("menuItemQucikData", menu.Name, true, menu.IconPath);

            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }
        public CloseableHeader Closer { get; private set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ItemQucikDataViewModel vm = this.DataContext as ItemQucikDataViewModel;
                if (vm != null)
                {
                    vm.RemoveProcessorTrend(vm.FirstItemTreeItem.BaseAlarmSignal);
                }
            }
        }

        public void GotoItem(BaseAlarmSignal sg)
        {
            ItemQucikDataViewModel vm = this.DataContext as ItemQucikDataViewModel;
            if (vm != null)
            {
                vm.GotoCommand.Execute(sg);
            }
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
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