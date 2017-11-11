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

//ע����������һ��Ҫ���ϣ�ָ��log4netʹ��.config�ļ�����ȡ������Ϣ
//�����WinForm���ٶ�����ΪMyDemo.exe������Ҫһ��MyDemo.exe.config�ļ���
//�����WebForm�����web.config�ж�ȡ�����Ϣ
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
            //��¼���ش��� 
            log.Fatal(e.Exception);
            e.Handled = true;//ʹ����һ�д����������ʱ�����쳣�������ˣ�������ΪUnhandledException�׳��ˡ�            
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //��¼���ش���                  
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