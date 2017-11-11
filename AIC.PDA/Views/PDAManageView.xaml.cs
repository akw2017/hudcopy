
using AIC.PDA.ViewModels;
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

namespace AIC.PDA.Views
{
    /// <summary>
    /// Interaction logic for PDAManageView.xaml
    /// </summary>
    public partial class PDAManageView : UserControl, ICloseable
    {
        internal const string StateSelectionSelected = "Channel";
        internal const string UnStateSelectionSelected = "Normal";
        private string currentstate;

        public PDAManageView()
        {
            InitializeComponent();
            this.Closer = new CloseableHeader("数采器设置", true);
        }
        public CloseableHeader Closer { get; private set; }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (currentstate != UnStateSelectionSelected)
            {
                VisualStateManager.GoToState(this, UnStateSelectionSelected, true);
                currentstate = UnStateSelectionSelected;
            }
        }

        void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentstate != StateSelectionSelected)
            {
                VisualStateManager.GoToState(this, StateSelectionSelected, true);
                currentstate = StateSelectionSelected;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           // (DataContext as PDAManageViewModel).SelectedIEPECard = (DataContext as PDAManageViewModel).Cards.First();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
          //  (DataContext as PDAManageViewModel).SelectedChannel = ((DataContext as PDAManageViewModel).SelectedIEPECard as IEPECard).Channels.First();
        }
    }
}
