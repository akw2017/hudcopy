using AIC.ServiceInterface;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace AIC.PDAService
{
    public class PDAServiceModule : IModule
    {
        private readonly IPDAService _pdaService;

        public PDAServiceModule(IPDAService pdaService)
        {
            _pdaService = pdaService;
        }

        public void Initialize()
        {
            _pdaService.Initialize();
        }
    }
}