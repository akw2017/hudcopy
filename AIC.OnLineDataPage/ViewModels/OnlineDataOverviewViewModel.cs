using AIC.Core.Models;
using AIC.Core.Events;
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
using AIC.Core.ControlModels;
using AIC.Core.OrganizationModels;
using AIC.ServiceInterface;
using AIC.Core;
using AIC.OnLineDataPage.Models;
using System.Windows.Input;
using AIC.CoreType;
using System.Windows.Controls;
using AIC.Domain;
using AIC.Core.SignalModels;
using System.Windows;
using Wpf.PageNavigationControl;
using AIC.Core.ExCommand;
using System.ComponentModel;
using System.Windows.Data;

namespace AIC.OnLineDataPage.ViewModels
{
    class OnlineDataOverviewViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ICardProcess _cardProcess;
        private readonly ISignalProcess _signalProcess;
        public OnlineDataOverviewViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ICardProcess cardProcess, ISignalProcess signalProcess)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _cardProcess = cardProcess;
            _signalProcess = signalProcess;          

            InitTree();
            InitItem(OrganizationTreeItems);

            _view = new ListCollectionView(SignalMonitors);
            _view.Filter = (object item) =>
            {
                if (OrganizationNames == null) return true;
                var itemPl = (SignalListViewModel)item;
                if (OrganizationNames.Contains(itemPl.Device.FullName) && itemPl.Device.ServerIP == SelectedOrganization.ServerIP)
                {
                    if (IsInvalidSignal == false && IsHighNormalSignal == false && IsHighPreAlertSignal == false && IsHighAlertSignal == false && IsHighDangerSignal == false
                    && IsLowDangerSignal == false && IsLowAlertSignal == false && IsLowPreAlertSignal == false && IsLowNormalSignal == false)
                    {
                        return true;
                    }
                    if (itemPl.Device.Alarm == AlarmGrade.Invalid && IsInvalidSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.Device.Alarm == AlarmGrade.HighNormal && IsHighNormalSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.Device.Alarm == AlarmGrade.HighPreAlert && IsHighPreAlertSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.Device.Alarm == AlarmGrade.HighAlert && IsHighAlertSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.Device.Alarm == AlarmGrade.HighDanger && IsHighDangerSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.Device.Alarm == AlarmGrade.LowDanger && IsHighDangerSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.Device.Alarm == AlarmGrade.LowAlert && IsHighAlertSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.Device.Alarm == AlarmGrade.LowPreAlert && IsHighPreAlertSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.Device.Alarm == AlarmGrade.LowNormal && IsHighNormalSignal == true)
                    {
                        return true;
                    }

                    return false;
                }
                return false;
            };
           
            //_signalList.SortDescriptions.Add(new SortDescription("DeviceName", ListSortDirection.Ascending));
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

        private readonly ICollectionView _view;
        public ICollectionView SignalList { get { return _view; } }

        private FastObservableCollection<SignalListViewModel> signalMonitors = new FastObservableCollection<SignalListViewModel>();
        public FastObservableCollection<SignalListViewModel> SignalMonitors
        {
            get { return signalMonitors; }
            set
            {
                signalMonitors = value;
                OnPropertyChanged("SignalMonitors");
            }
        }

        private SignalListViewModel selectedSignalMonitor;
        public SignalListViewModel SelectedSignalMonitor
        {
            get { return selectedSignalMonitor; }
            set
            {
                if (selectedSignalMonitor != value)
                {
                    if (selectedSignalMonitor != null)
                    {
                        selectedSignalMonitor.IsSelected = false;
                    }
                    selectedSignalMonitor = value;
                    if (selectedSignalMonitor != null)
                    {
                        selectedSignalMonitor.IsSelected = true;                       
                    }
                    OnPropertyChanged("SelectedSignalMonitor");
                }
            }
        }

        //private string searchName;
        //public string OrganizationNames
        //{
        //    get { return searchName; }
        //    set
        //    {
        //        searchName = value;
        //        OnPropertyChanged("OrganizationNames");
        //    }
        //}

        private double itemWidth =300;
        public double ItemWidth
        {
            get { return itemWidth; }
            set
            {
                if (itemWidth != value)
                {
                    itemWidth = value;
                    OnPropertyChanged("ItemWidth");
                    foreach (var item in SignalMonitors)
                    {
                        item.ItemWidth = value;
                    }
                }
            }
        }

        private double itemHeight = 125;
        public double ItemHeight
        {
            get { return itemHeight; }
            set
            {
                if (itemHeight != value)
                {
                    itemHeight = value;
                    OnPropertyChanged("ItemHeight");
                    foreach (var item in SignalMonitors)
                    {
                        item.ItemHeight = value;
                    }
                }
            }
        }        

        private Orientation orientation = Orientation.Horizontal;
        public Orientation Orientation
        {
            get { return orientation; }
            set
            {
                if (orientation != value)
                {
                    orientation = value;
                    OnPropertyChanged("Orientation");
                    if (value == Orientation.Horizontal)
                    {
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    }
                    if (value == Orientation.Vertical)
                    {
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    }
                }
            }
        }

        private ScrollBarVisibility verticalScrollBarVisibility = ScrollBarVisibility.Auto;
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return verticalScrollBarVisibility; }
            set
            {
                if (verticalScrollBarVisibility != value)
                {
                    verticalScrollBarVisibility = value;
                    OnPropertyChanged("VerticalScrollBarVisibility");
                }
            }
        }

        private ScrollBarVisibility horizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return horizontalScrollBarVisibility; }
            set
            {
                if (horizontalScrollBarVisibility != value)
                {
                    horizontalScrollBarVisibility = value;
                    OnPropertyChanged("HorizontalScrollBarVisibility");
                }
            }
        }

        private bool isComposing = false;
        public bool IsComposing
        {
            get { return isComposing; }
            set
            {
                if (isComposing != value)
                {
                    isComposing = value;
                    OnPropertyChanged("IsComposing");
                }
            }
        }

        private bool isInvalidSignal;
        public bool IsInvalidSignal
        {
            get { return isInvalidSignal; }
            set
            {
                if (isInvalidSignal != value)
                {
                    isInvalidSignal = value;
                    OnPropertyChanged(() => IsInvalidSignal);
                    _view.Refresh();
                }
            }
        }

        private bool isHighNormalSignal;
        public bool IsHighNormalSignal
        {
            get { return isHighNormalSignal; }
            set
            {
                if (isHighNormalSignal != value)
                {
                    isHighNormalSignal = value;
                    OnPropertyChanged(() => IsHighNormalSignal);
                    _view.Refresh();
                }
            }
        }

        private bool isHighPreAlertSignal;
        public bool IsHighPreAlertSignal
        {
            get { return isHighPreAlertSignal; }
            set
            {
                if (isHighPreAlertSignal != value)
                {
                    isHighPreAlertSignal = value;
                    OnPropertyChanged(() => IsHighPreAlertSignal);
                    _view.Refresh();
                }
            }
        }

        private bool isHighAlertSignal;
        public bool IsHighAlertSignal
        {
            get { return isHighAlertSignal; }
            set
            {
                if (isHighAlertSignal != value)
                {
                    isHighAlertSignal = value;
                    OnPropertyChanged(() => IsHighAlertSignal);
                    _view.Refresh();
                }
            }
        }

        private bool isHighDangerSignal;
        public bool IsHighDangerSignal
        {
            get { return isHighDangerSignal; }
            set
            {
                if (isHighDangerSignal != value)
                {
                    isHighDangerSignal = value;
                    OnPropertyChanged(() => IsHighDangerSignal);
                    _view.Refresh();
                }
            }
        }

        private bool isLowDangerSignal;
        public bool IsLowDangerSignal
        {
            get { return isLowDangerSignal; }
            set
            {
                if (isLowDangerSignal != value)
                {
                    isLowDangerSignal = value;
                    OnPropertyChanged(() => IsLowDangerSignal);
                    _view.Refresh();
                }
            }
        }

        private bool isLowAlertSignal;
        public bool IsLowAlertSignal
        {
            get { return isLowAlertSignal; }
            set
            {
                if (isLowAlertSignal != value)
                {
                    isLowAlertSignal = value;
                    OnPropertyChanged(() => IsLowAlertSignal);
                    _view.Refresh();
                }
            }
        }

        private bool isLowPreAlertSignal;
        public bool IsLowPreAlertSignal
        {
            get { return isLowPreAlertSignal; }
            set
            {
                if (isLowPreAlertSignal != value)
                {
                    isLowPreAlertSignal = value;
                    OnPropertyChanged(() => IsLowPreAlertSignal);
                    _view.Refresh();
                }
            }
        }

        private bool isLowNormalSignal;
        public bool IsLowNormalSignal
        {
            get { return isLowNormalSignal; }
            set
            {
                if (isLowNormalSignal != value)
                {
                    isLowNormalSignal = value;
                    OnPropertyChanged(() => IsLowNormalSignal);
                    _view.Refresh();
                }
            }
        }
        #endregion

        #region Command   
        private ICommand queryCommand;
        public ICommand QueryCommand
        {
            get
            {
                return this.queryCommand ?? (this.queryCommand = new DelegateCommand<object>(para => this.Query(para)));
            }
        }

        private ICommand groupCommand;
        public ICommand GroupCommand
        {
            get
            {
                return this.groupCommand ?? (this.groupCommand = new DelegateCommand<object>(para => this.Group(para)));
            }
        }

        private ICommand unGroupCommand;
        public ICommand UnGroupCommand
        {
            get
            {
                return this.unGroupCommand ?? (this.unGroupCommand = new DelegateCommand<object>(para => this.UnGroup(para)));
            }
        }

        private ICommand selectedTreeChangedComamnd;
        public ICommand SelectedTreeChangedComamnd
        {
            get
            {
                return this.selectedTreeChangedComamnd ?? (this.selectedTreeChangedComamnd = new DelegateCommand<object>(para => this.SelectedTreeChanged(para)));
            }
        }
        #endregion

        #region 添加、删除显示

        private void InitItem(IList<OrganizationTreeItemViewModel> organization)
        {           
            var devices = _cardProcess.GetDevices(organization);
            foreach (var device in devices)
            {
                SignalListViewModel viewModel = new SignalListViewModel(device);
                viewModel.ItemWidth = itemWidth;
                viewModel.ItemHeight = itemHeight;
                SignalMonitors.Add(viewModel);
            }

        }

        private void Query(object para)
        {
            _view.Refresh();
        }

        private void Group(object para)
        {
            _view.GroupDescriptions.Clear();
            _view.GroupDescriptions.Add(new PropertyGroupDescription("DeviceName"));//对视图进行分组
        }

        private void UnGroup(object para)
        {
            _view.GroupDescriptions.Clear();
            _view.GroupDescriptions.Add(new PropertyGroupDescription("NullName"));//对视图进行分组
        }
        #endregion

        #region       
        private List<string> OrganizationNames;
        private OrganizationTreeItemViewModel SelectedOrganization;
        public void SelectedTreeChanged(object para)
        {
            if (para is OrganizationTreeItemViewModel)
            {               
                OrganizationNames = _cardProcess.GetDevices(para as OrganizationTreeItemViewModel).Select(p => p.FullName).ToList();
                SelectedOrganization = para as OrganizationTreeItemViewModel;
                _view.Refresh();
            }
        }
        #endregion
    }
}
