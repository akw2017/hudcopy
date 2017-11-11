using AIC.ServiceInterface;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService
{
    public class DatabaseServiceModule : IModule
    {
        private readonly IHardwareService _hardwareService;
        private readonly IOrganizationService _organizationService;
        private readonly ILoginUserService _loginUserService;

        public DatabaseServiceModule(IHardwareService hardwareService, IOrganizationService organizationService, ILoginUserService loginUserService)
        {
            _hardwareService = hardwareService;
            _organizationService = organizationService;
            _loginUserService = loginUserService;
        }

        public void Initialize()
        {           
            _hardwareService.Initialize();
            _organizationService.Initialize();
            _loginUserService.Initialize();
        }
    }
}