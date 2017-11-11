using Microsoft.Practices.Unity;
using Prism.Unity;
using AIC.Views;
using System.Windows;
using Prism.Modularity;
using AIC.PDA;
using AIC.TreeService;
using AIC.PDAService;
using AIC.OnLineMonitor;
using AIC.SignalService;
using AIC.ServiceInterface;
using AIC.AkkaSystem;
using AIC.Database;
using AIC.SignalCache;
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
using MahApps.Metro.Controls.Dialogs;
using AIC.HomePage;
using AIC.RTDataService;

namespace AIC
{
    class Bootstrapper : UnityBootstrapper
    {
        private readonly CallbackLogger callbackLogger = new CallbackLogger();
        private List<Exception> exceptionList = new List<Exception>();

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {

            base.InitializeShell();

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
                            MessageBox.Show(sb.ToString().Trim());
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
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IUnitOfWork, UnitOfWork>(new ContainerControlledLifetimeManager());            
            Container.RegisterType<ITreeService, TreeService.TreeService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IPDAService, PDAService.PDAService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRTDataService, RTDataService.RTDataService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISignalCache, SignalCache.SignalCache>(new ContainerControlledLifetimeManager());
            this.RegisterTypeIfMissing(typeof(IModuleTracker), typeof(ModuleTracker), true);
            this.Container.RegisterInstance<CallbackLogger>(this.callbackLogger);
            Container.RegisterType<IRegionNavigationContentLoader, ScopedRegionNavigationContentLoader>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IDialogCoordinator, DialogCoordinator>(new ContainerControlledLifetimeManager());
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

            //catalog.AddModule(typeof(DatabaseModule));
            //catalog.AddModule(typeof(SignalCacheModule));
            //catalog.AddModule(typeof(ActorSystemModule)); 
            //catalog.AddModule(typeof(PDAServiceModule));
            //catalog.AddModule(typeof(TreeServiceModule));
            //catalog.AddModule(typeof(SignalCacheModule));
            //catalog.AddModule(typeof(RTDataServiceModule));
            //catalog.AddModule(typeof(OnLineMonitorModule));
            //catalog.AddModule(typeof(PDAModule));
            catalog.AddModule(typeof(HomePageModule));
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
