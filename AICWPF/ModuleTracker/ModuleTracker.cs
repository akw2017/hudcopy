using AICWPF.Properties;
using ModuleTracking;
using Prism.Logging;
using Prism.Modularity;
using System;
using System.Globalization;

namespace AICWPF.ModuleTracker
{
    public class ModuleTracker : IModuleTracker
    {
        private readonly ModuleTrackingState moduleDatabaseTrackingState;
        private readonly ModuleTrackingState moduleSignalCacheTrackingState;
        private readonly ModuleTrackingState modulePDAServiceTrackingState;
        private readonly ModuleTrackingState moduleTreeServiceTrackingState;
        private readonly ModuleTrackingState moduleRTDataServiceTrackingState;
        private readonly ModuleTrackingState modulePDATrackingState;
        private readonly ModuleTrackingState moduleOnLineDataPageTrackingState;


        private ILoggerFacade logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTracker"/> class.
        /// </summary>
        public ModuleTracker(ILoggerFacade logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            this.logger = logger;

            // These states are defined specifically for the desktop version of the quickstart.
            this.moduleSignalCacheTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleSignalCache,
                ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                ExpectedInitializationMode = InitializationMode.WhenAvailable,
                ExpectedDownloadTiming = DownloadTiming.InBackground,
            };

            this.modulePDAServiceTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModulePDAService,
                ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                ExpectedInitializationMode = InitializationMode.OnDemand,
                ExpectedDownloadTiming = DownloadTiming.WithApplication,
                //ConfiguredDependencies = WellKnownModuleNames.ModuleService,
            };

            this.moduleTreeServiceTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleTreeService,
                ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                ExpectedInitializationMode = InitializationMode.OnDemand,
                ExpectedDownloadTiming = DownloadTiming.WithApplication,
                ConfiguredDependencies = WellKnownModuleNames.ModulePDAService,
            };

            this.moduleRTDataServiceTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleTreeService,
                ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                ExpectedInitializationMode = InitializationMode.OnDemand,
                ExpectedDownloadTiming = DownloadTiming.WithApplication,
               // ConfiguredDependencies = WellKnownModuleNames.ModuleTreeService,
            };

            this.modulePDATrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModulePDA,
                ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                ExpectedInitializationMode = InitializationMode.OnDemand,
                ExpectedDownloadTiming = DownloadTiming.WithApplication,
                ConfiguredDependencies = WellKnownModuleNames.ModulePDAService,
            };


            this.moduleOnLineDataPageTrackingState = new ModuleTrackingState
            {
                ModuleName = WellKnownModuleNames.ModuleOnLineDataPage,
                ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                ExpectedInitializationMode = InitializationMode.OnDemand,
                ExpectedDownloadTiming = DownloadTiming.WithApplication,
                ConfiguredDependencies = WellKnownModuleNames.ModuleTreeService,
            };
        }

        public ModuleTrackingState ModuleDatabaseTrackingState => moduleDatabaseTrackingState;
        public ModuleTrackingState ModuleSignalCacheTrackingState => moduleSignalCacheTrackingState;
        public ModuleTrackingState ModulePDAServiceTrackingState => modulePDAServiceTrackingState;
        public ModuleTrackingState ModuleTreeServiceTrackingState => moduleTreeServiceTrackingState;
        public ModuleTrackingState ModuleRTDataServiceTrackingState => moduleRTDataServiceTrackingState;
        public ModuleTrackingState ModulePDATrackingState => modulePDATrackingState;
        public ModuleTrackingState ModuleOnLineDataPageTrackingState => moduleOnLineDataPageTrackingState;
        


        /// <summary>
        /// Records the module is loading.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        /// <param name="bytesReceived">The number of bytes downloaded.</param>
        /// <param name="totalBytesToReceive">The total number of bytes received.</param>
        public void RecordModuleDownloading(string moduleName, long bytesReceived, long totalBytesToReceive)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.BytesReceived = bytesReceived;
                moduleTrackingState.TotalBytesToReceive = totalBytesToReceive;

                if (bytesReceived < totalBytesToReceive)
                {
                    moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Downloading;
                }
                else
                {
                    moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Downloaded;
                }
            }

            this.logger.Log(string.Format(CultureInfo.CurrentCulture, Resources.ModuleIsLoadingProgress, moduleName, bytesReceived, totalBytesToReceive), Category.Debug, Priority.Low);
        }

        /// <summary>
        /// Records the module has been constructed.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        public void RecordModuleConstructed(string moduleName)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Constructed;
            }

            this.logger.Log(string.Format(CultureInfo.CurrentCulture, Resources.ModuleConstructed, moduleName), Category.Debug, Priority.Low);
        }

        /// <summary>
        /// Records the module has been initialized.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        public void RecordModuleInitialized(string moduleName)
        {
            ModuleTrackingState moduleTrackingState = this.GetModuleTrackingState(moduleName);
            if (moduleTrackingState != null)
            {
                moduleTrackingState.ModuleInitializationStatus = ModuleInitializationStatus.Initialized;
            }


            this.logger.Log(string.Format(CultureInfo.CurrentCulture, Resources.ModuleIsInitialized, moduleName), Category.Debug, Priority.Low);
        }

        /// <summary>
        /// Records the module is loaded.
        /// </summary>
        /// <param name="moduleName">The <see cref="WellKnownModuleNames">well-known name</see> of the module.</param>
        public void RecordModuleLoaded(string moduleName)
        {
            this.logger.Log(string.Format(CultureInfo.CurrentCulture, Resources.ModuleLoaded, moduleName), Category.Debug, Priority.Low);
        }

        // A helper to make updating specific property instances by name easier.
        private ModuleTrackingState GetModuleTrackingState(string moduleName)
        {
            switch (moduleName)
            {
                case WellKnownModuleNames.ModuleDatabase:
                    return this.ModuleDatabaseTrackingState;
                case WellKnownModuleNames.ModuleSignalCache:
                    return this.ModuleSignalCacheTrackingState;
                case WellKnownModuleNames.ModulePDAService:
                    return this.ModulePDAServiceTrackingState;
                case WellKnownModuleNames.ModuleRTDataService:
                    return this.ModuleRTDataServiceTrackingState;
                case WellKnownModuleNames.ModuleTreeService:
                    return this.ModuleTreeServiceTrackingState;
                case WellKnownModuleNames.ModulePDA:
                    return this.ModulePDATrackingState;
                case WellKnownModuleNames.ModuleOnLineDataPage:
                    return this.ModuleOnLineDataPageTrackingState;
                default:
                    return null;
            }
        }
    }
}
