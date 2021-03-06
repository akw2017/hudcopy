﻿using AIC.Core.OrganizationModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.UserManageModels
{
    public class MyOrganizationPrivilege 
    {
        public string Name { get; set; }       
        public List<OrganizationTreeItemViewModel> OrganizationTreeItems { get; set; }
    }
}
