using AICAir.ModuleTracker;
using ModuleTracking;
using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Globalization;

namespace AICAir.ViewModels
{
    public class SplashViewModel : BindableBase
    {
        private IModuleTracker moduleTracker;
        private IModuleManager moduleManager;
        private CallbackLogger _logger;
        private readonly IEventAggregator _eventAggregator;
        public SplashViewModel(
            IModuleManager moduleManager, 
            IModuleTracker moduleTracker, 
            CallbackLogger logger, 
            IEventAggregator eventAggregator)
        {
           
            if (moduleManager == null)
            {
                throw new ArgumentNullException("moduleManager");
            }

            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            this.moduleManager = moduleManager;
            this.moduleTracker = moduleTracker;
            this._eventAggregator = eventAggregator;


            //_eventAggregator.GetEvent<RaisedExceptionEvent>().Subscribe(RaisedException);

            moduleManager.LoadModuleCompleted += ModuleManager_LoadModuleCompleted;
            _logger = logger;
            _logger.Callback = Log;
            //_logger.Callback = async (m, c, p) => await Log(m, c, p);
        }

        //public void Initialize()
        //{
        //    try
        //    {
        //        _bearingService.Initialize();
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.MessageBox.Show(ex.Message);
        //    }   
        //}

        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            if(e.Error!=null)
            {
                TraceText = TraceText + e.Error.Message;              
            }
        }

        public void Log(string message, Category category, Priority priority)
        {
            TraceText = string.Format(CultureInfo.CurrentUICulture, "[{0}][{1}] {2}\r\n", category, priority, message);
        }

        //public async Task Log(string message, Category category, Priority priority)
        //{
        //    await Task.Delay(1000);
        //    TraceText = TraceText + "\r\n" + string.Format(CultureInfo.CurrentUICulture, "[{0}][{1}] {2}\r\n", category, priority, message);
        //}

        private string traceText="Initialize.......";
        public string TraceText
        {
            get { return traceText; }
            set { SetProperty(ref traceText, value); }
        }
    }
}
