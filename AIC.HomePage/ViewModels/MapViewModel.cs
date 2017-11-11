using AIC.Core.Models;
using AIC.Core.Events;
using AIC.HomePage.Menus;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIC.Resources.Models;
using AIC.Core.ControlModels;
using AIC.Core.OrganizationModels;

namespace AIC.HomePage.ViewModels
{
    class MapViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly IOrganizationService _organizationService;
        public MapViewModel(ILocalConfiguration localConfiguration, IEventAggregator eventAggregator, IOrganizationService organizationService)
        {
            _localConfiguration = localConfiguration;
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            InitTree();
        }

        #region 管理树
        private void InitTree()
        { 
            OrganizationTreeItems = _organizationService.OrganizationTreeItems;
            //TreeExpanded();
        }

        private void TreeExpanded()
        {
            foreach (var first in OrganizationTreeItems)
            {
                first.IsExpanded = true;
                foreach (var second in first.Children)
                {
                    second.IsExpanded = true;
                    foreach (var third in second.Children)
                    {
                        third.IsExpanded = true;
                    }
                }
            }
        }
        #endregion

        #region 属性与字段
        private ObservableCollection<OrganizationTreeItemViewModel> _organizationTreeItems;
        public ObservableCollection<OrganizationTreeItemViewModel> OrganizationTreeItems
        {
            get { return _organizationTreeItems; }
            set
            {
                _organizationTreeItems = value;
                OnPropertyChanged("OrganizationTreeItems");
            }
        }
        #endregion

    }
}
