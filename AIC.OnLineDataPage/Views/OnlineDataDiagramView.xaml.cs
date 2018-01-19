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

namespace AIC.OnLineDataPage.Views
{
    /// <summary>
    /// Interaction logic for OnLineEquipmentView.xaml
    /// </summary>
    public partial class OnlineDataDiagramView : UserControl, ICloseable
    {
        public OnlineDataDiagramView()
        {
            InitializeComponent();
            this.Closer = new CloseableHeader("menuOnlineDataDiagram", (string)Application.Current.Resources["menuOnlineDataDiagram"], true);
            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }
        public CloseableHeader Closer { get; private set; }

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


        GridLength m_SecondWidthCache = new GridLength(0);

        private void expander_Expanded(object sender, RoutedEventArgs e)
        {
            if (m_SecondWidthCache != new GridLength(0))
            {
                grdWorkbench.ColumnDefinitions[4].Width = m_SecondWidthCache;
            }
        }

        private void expander_Collapsed(object sender, RoutedEventArgs e)
        {
            m_SecondWidthCache = grdWorkbench.ColumnDefinitions[4].Width;
            grdWorkbench.ColumnDefinitions[4].Width = new GridLength(28);
        }
    }
}
