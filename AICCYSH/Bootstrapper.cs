using Microsoft.Practices.Unity;
using Prism.Unity;
using AICCYSH.Views;
using System.Windows;
using Prism.Modularity;
using AIC.PDA;
using AIC.OnLineDataPage;
using AIC.ServiceInterface;
using System;
using Prism.Events;
using System.Threading;
using ModuleTracking;
using System.Text;
using System.Collections.Generic;
using System.Windows.Threading;
using Prism.Logging;
using Prism.Regions;
using AIC.Core;
using AIC.HomePage;
using MahApps.Metro.Controls.Dialogs;
using AICCYSH.ModuleTracker;
using System.Windows.Navigation;
using AIC.LocalConfiguration;
using AIC.HardwareService;
using AIC.DatabaseService;
using AIC.DatabaseService.Models;
using AIC.UserPage;
using AIC.CYSHPage;

namespace AICCYSH
{
    class Bootstrapper : UnityBootstrapper
    {
        private readonly CallbackLogger callbackLogger = new CallbackLogger();
        private List<Exception> exceptionList = new List<Exception>();
        public static DependencyObject ShellPage { private get; set; }//XBAP.6
        protected override DependencyObject CreateShell()
        {
#if XBAP//XBAP.4
            NavigationWindow parent = ((ShellPage)ShellPage).Parent as NavigationWindow;
            parent.ShowsNavigationUI = false;
            RegionManager.SetRegionName(((ShellPage)ShellPage).MainRegionContent, "MainRegionRegion");  
            return ShellPage;
#else
            return this.Container.TryResolve<Shell>();
#endif
        }

        protected override void InitializeShell()
        {

            base.InitializeShell();
#if XBAP//XBAP.5

#else
            /*
            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<ModuleInitializeExceptionEvent>().Subscribe(e => exceptionList.Add(e));
            Application.Current.Dispatcher.BeginInvoke(
                (Action)(() =>
                {          
                    eventAggregator.GetEvent<CloseSplashEvent>().Publish(null);
                    if (exceptionList.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in exceptionList)
                        {
                            sb.AppendLine(item.ToString());
                            Xceed.Wpf.Toolkit.MessageBox.Show(sb.ToString().Trim());
                        }
                    }
                }));

            //  var WaitForCreation = new AutoResetEvent(false);
            var thread = new Thread(new ThreadStart(() =>
            {
                var spalsh = Container.Resolve<SplashView>();
                spalsh.Show();
                eventAggregator.GetEvent<CloseSplashEvent>().Subscribe
                (
                    e =>
                    {
                        spalsh.Dispatcher.BeginInvoke((Action)spalsh.Close);
                        Application.Current.MainWindow.Show();
                    },
                    ThreadOption.PublisherThread, true);

                //  WaitForCreation.Set();

                Dispatcher.Run();
            }));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            */
            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
#endif
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.RegisterType<ILocalConfiguration, LocalConfiguration>(new ContainerControlledLifetimeManager());//配置文件
            this.Container.RegisterType<IDatabaseComponent, DatabaseComponent>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IHardwareService, HardwareService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IOrganizationService, OrganizationService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<ISignalProcess, SignalProcess>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IConvertToDataBaseFormat, ConvertToDataBaseFormat>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IConvertFromDataBaseFormat, ConvertFromDataBaseFormat>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<ILoginUserService, LoginUserService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IUserManageService, UserManageService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<ICardProcess, CardProcess>(new ContainerControlledLifetimeManager());               
            this.RegisterTypeIfMissing(typeof(IModuleTracker), typeof(ModuleTracker.ModuleTracker), true);
            this.Container.RegisterInstance<CallbackLogger>(this.callbackLogger);
            this.Container.RegisterType<IRegionNavigationContentLoader, ScopedRegionNavigationContentLoader>(new ContainerControlledLifetimeManager());
            //this.Container.RegisterType<IDialogCoordinator, DialogCoordinator>(new ContainerControlledLifetimeManager());
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            IRegionBehaviorFactory behaviors = base.ConfigureDefaultRegionBehaviors();
            behaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
            return behaviors;
        }

        //注意排放顺序为初始化顺序
        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;

            catalog.AddModule(typeof(LocalConfigurationModule));//配置文件初始化
            catalog.AddModule(typeof(DatabaseServiceModule));//读取硬件信息            
            //catalog.AddModule(typeof(DatabaseModule));//wcf数据
            //catalog.AddModule(typeof(ActorSystemModule));
            //catalog.AddModule(typeof(PDAServiceModule));
            //catalog.AddModule(typeof(TreeServiceModule));
            //catalog.AddModule(typeof(SignalCacheModule));
            //catalog.AddModule(typeof(RTDataServiceModule));
            //catalog.AddModule(typeof(OnLineDataPageModule));
            //catalog.AddModule(typeof(PDAModule));
            catalog.AddModule(typeof(CYSHPageModule));
            catalog.AddModule(typeof(UserPageModule));
            catalog.AddModule(typeof(SignalProcessModule));
        }

        protected override ILoggerFacade CreateLogger()
        {
            // Because the Shell is displayed after most of the interesting boostrapper work has been performed,
            // this quickstart uses a special logger class to hold on to early log entries and display them 
            // after the UI is visible.
            return this.callbackLogger;
        }
    }

    public class CloseSplashEvent : PubSubEvent<object>
    {
    }
}
