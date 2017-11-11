using AICAir.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;

namespace AICAir.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        public Shell()
        {
            InitializeComponent();            
        }
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
#if XBAP
            MessageBoxResult result = MessageBox.Show((string)Application.Current.Resources["strExitSystem"], (string)Application.Current.Resources["strExit"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["strExitSystem"], (string)Application.Current.Resources["strExit"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
            if (result != MessageBoxResult.OK)
            {
                e.Cancel = true;
            }

        }

    }
}
