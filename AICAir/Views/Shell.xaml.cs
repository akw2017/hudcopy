using AIC.Core;
using AIC.ServiceInterface;
using AICAir.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.Windows.Forms;

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
            InitNotifyIcon();
            _loginUserService = loginUserService;
        }
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_loginUserService.LoginInfo.LoginStatus == false)
            {
                System.Windows.Application.Current.Shutdown();
            }
#if XBAP
            MessageBoxResult result = System.Windows.MessageBox.Show((string)System.Windows.Application.Current.Resources["strExitSystem"], (string)System.Windows.Application.Current.Resources["strExit"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#else
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show((string)System.Windows.Application.Current.Resources["strExitSystem"], (string)System.Windows.Application.Current.Resources["strExit"], MessageBoxButton.OKCancel, MessageBoxImage.Question);
#endif
            if (result != MessageBoxResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }

        }

        NotifyIcon notifyIcon;
        public void InitNotifyIcon()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.BalloonTipText = "双击打开";
            this.notifyIcon.Text = LocalSetting.Title;
            this.notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            this.notifyIcon.Visible = false;
            this.notifyIcon.MouseDoubleClick += OnNotifyIconDoubleClick;
            //this.notifyIcon.MouseClick += OnNotifyIconMouseClick;           

            System.Windows.Forms.ContextMenuStrip contextMenu = new System.Windows.Forms.ContextMenuStrip();
            System.Windows.Forms.ToolStripMenuItem openitem = new System.Windows.Forms.ToolStripMenuItem();
            openitem.Click += OnNotifyIconDoubleClick;
            openitem.Text = "显示";
            System.Windows.Forms.ToolStripMenuItem closeitem = new System.Windows.Forms.ToolStripMenuItem();
            closeitem.Text = "退出";
            closeitem.Click += OnNotifyIconCloseClick;
            contextMenu.Items.Add(openitem);
            contextMenu.Items.Add(closeitem);
            this.notifyIcon.ContextMenuStrip = contextMenu;
        }

        private void OnNotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            this.notifyIcon.ShowBalloonTip(500);
        }

        private void OnNotifyIconDoubleClick(object sender, System.EventArgs e)
        {
            this.ShowInTaskbar = true;          
            this.notifyIcon.Visible = false;
            this.WindowState = WindowState.Maximized;
            this.Activate();
        }

        private void OnNotifyIconCloseClick(object sender, System.EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ShowNotifyIcon_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            this.ShowInTaskbar = false;
            this.notifyIcon.Visible = true;
           
        }
    }
}
