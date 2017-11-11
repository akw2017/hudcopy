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
    public partial class OnlineDataOverviewView : UserControl, ICloseable
    {
        public OnlineDataOverviewView()
        {
            InitializeComponent();
            this.Closer = new CloseableHeader((string)Application.Current.Resources["menuOnlineDataOverview"], true);

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
    }
}
