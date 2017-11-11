using AIC.ServiceInterface;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace AIC.LocalConfiguration
{
    public class SignalProcessModule : IModule
    {
        private readonly ISignalProcess _signalProcess;

        public SignalProcessModule(ISignalProcess signalProcess)
        {
            _signalProcess = signalProcess;
        }

        public void Initialize()
        {
            try
            {
                _signalProcess.Initialize();
            }
            catch(Exception ex )
            {
                throw ex;
            }
        }
    }
}