using AIC.ServiceInterface;
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
        private readonly ILoginUserService _loginUserService;
        public Shell(ILoginUserService loginUserService)
        {
            InitializeComponent();
            _loginUserService = loginUserService;
        }
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_loginUserService.LoginInfo.LoginStatus == false)
            {
                Application.Current.Shutdown();
            }
#if XBAP
            MessageBoxResult result = MessageBox.Show((string)Application.Current.Resources["strExitSystem"], (string)Application.Current.Resources["strExit"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)Application.Current.Resources["strExitSystem"], (string)Application.Current.Resources["strExit"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
            if (result != MessageBoxResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }

        }

    }
}
