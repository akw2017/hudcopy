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
    /// Interaction logic for OnLineTileView.xaml
    /// </summary>
    public partial class OnlineDataTileView : UserControl, ICloseable
    {
        public OnlineDataTileView()
        {
            InitializeComponent();
            this.Closer = new CloseableHeader((string)Application.Current.Resources["menuOnlineDataTile"], true);
            this.Loaded += new RoutedEventHandler(Window_Loaded);
            //CommandManager.AddPreviewExecutedHandler(listview, new ExecutedRoutedEventHandler(OnScorllCommandForListView));
        }

        public CloseableHeader Closer { get; private set; }

        ///// <summary>
        ///// Handle the OnScorllCommand event for RichTextBox
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void OnScorllCommandForListView(object sender, ExecutedRoutedEventArgs e)
        //{
        //    RoutedCommand command = (RoutedCommand)e.Command;            
        //    if (command.Name.Equals("LineDown", StringComparison.CurrentCultureIgnoreCase))
        //    {
        //        // Do some thing
        //    }
        //    else if (command.Name.Equals("ScrollToVerticalOffset", StringComparison.CurrentCultureIgnoreCase))
        //    {
        //        // Do some thing
        //    }
        //}     

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
