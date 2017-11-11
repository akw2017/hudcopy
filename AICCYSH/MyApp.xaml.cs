 using System;
using System.Windows;
using System.Windows.Navigation;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Windows.Interop;
using Arction.Wpf.Charting;
using AICCYSH.Chart;
using System.Runtime.InteropServices;

//注意下面的语句一定要加上，指定log4net使用.config文件来读取配置信息
//如果是WinForm（假定程序为MyDemo.exe，则需要一个MyDemo.exe.config文件）
//如果是WebForm，则从web.config中读取相关信息
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace AICCYSH
{
    /// <summary>
    /// Interaction logic for MyApp.xaml
    /// </summary>

    public partial class MyApp : Application
    {

        //  Can be used at runtime to determine if running as XBAP
        static public bool IsXBAP
        {
            get
            {
                return BrowserInteropHelper.IsBrowserHosted;
            }
        }

        public MyApp()
        {
            LightningChartUltimate.SetDeploymentKey(LicenseKeyStrings.LightningChartUltimate);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Current.DispatcherUnhandledException += Application_DispatcherUnhandledException;
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //记录严重错误 
            log.Fatal(e.Exception);
            e.Handled = true;//使用这一行代码告诉运行时，该异常被处理了，不再作为UnhandledException抛出了。            
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //记录严重错误                  
            log.Fatal(e.ExceptionObject);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if XBAP//XBAP.2
            Application.Current.StartupUri = new Uri("Views/ShellPage.xaml", UriKind.Relative);//
#else
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
#endif
        }

        protected override void OnLoadCompleted(System.Windows.Navigation.NavigationEventArgs e)
        {
#if XBAP//XBAP.3
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
#else

#endif
        }
    }
}