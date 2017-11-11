using AIC.ServiceInterface;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace AIC.TreeService
{
    public class TreeServiceModule : IModule
    {
        private readonly ITreeService _treeService;

        public TreeServiceModule(ITreeService treeService)
        {
            _treeService = treeService;
        }

        public void Initialize()
        {
            try
            {
                _treeService.Initialize();
            }
            catch { }
        }
    }
}