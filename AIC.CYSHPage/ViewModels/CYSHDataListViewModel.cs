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
using AIC.Core.SignalModels;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;
using AIC.CoreType;

namespace AIC.CYSHPage.ViewModels
{
    class CYSHDataListViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;       
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;

        public CYSHDataListViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess)
        {           
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;

            _signalProcess.SignalsAdded += _signalCache_SignalAdded;
            _signalProcess.SignalsRemoved += _signalCache_SignalRemoved;

            foreach (var sg in _signalProcess.Signals.OfType<BaseAlarmSignal>().ToArray())
            {
                signals.Add(sg);
            }

            _view = new ListCollectionView(signals);
            //_view.Filter = (object item) =>
            //{
            //    if (SearchName == null || SearchName == "") return false;
            //    var itemPl = (BaseAlarmSignal)item;
            //    if (itemPl == null) return false;
            //    if (itemPl.DeviceDisplayName.Contains(SearchName))
            //    {
            //        if (IsInvalidSignal == false && IsHighNormalSignal == false && IsHighPreAlertSignal == false && IsHighAlertSignal == false && IsHighDangerSignal == false
            //        && IsLowDangerSignal == false && IsLowAlertSignal == false && IsLowPreAlertSignal == false && IsLowNormalSignal == false)
            //        {
            //            return true;
            //        }
            //        if (itemPl.AlarmGrade == AlarmGrade.Invalid && IsInvalidSignal == true)
            //        {
            //            return true;
            //        }
            //        if (itemPl.AlarmGrade == AlarmGrade.HighNormal && IsHighNormalSignal == true)
            //        {
            //            return true;
            //        }
            //        if (itemPl.AlarmGrade == AlarmGrade.HighPreAlert && IsHighPreAlertSignal == true)
            //        {
            //            return true;
            //        }
            //        if (itemPl.AlarmGrade == AlarmGrade.HighAlert && IsHighAlertSignal == true)
            //        {
            //            return true;
            //        }
            //        if (itemPl.AlarmGrade == AlarmGrade.HighDanger && IsHighDangerSignal == true)
            //        {
            //            return true;
            //        }
            //        if (itemPl.AlarmGrade == AlarmGrade.LowDanger && IsLowDangerSignal == true)
            //        {
            //            return true;
            //        }
            //        if (itemPl.AlarmGrade == AlarmGrade.LowAlert && IsLowAlertSignal == true)
            //        {
            //            return true;
            //        }
            //        if (itemPl.AlarmGrade == AlarmGrade.LowPreAlert && IsLowPreAlertSignal == true)
            //        {
            //            return true;
            //        }
            //        if (itemPl.AlarmGrade == AlarmGrade.LowNormal && IsLowNormalSignal == true)
            //        {
            //            return true;
            //        }

            //        return false;
            //    }
            //    return false;
            //};
            //_view.GroupDescriptions.Add(new PropertyGroupDescription("DeviceDisplayName"));//对视图进行分组
            InitTree();
        }

        #region 信号增减
        private void _signalCache_SignalAdded(BaseAlarmSignal sg)
        {
            if (!(signals.Contains(sg)))
            {
                signals.Add(sg);
            }
        }

        private void _signalCache_SignalRemoved(BaseAlarmSignal sg)
        {
            if (signals.Contains(sg))
            {
                signals.Remove(sg);
            }
        }
        #endregion

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

        private readonly ICollectionView _view;
        public ICollectionView SignalsView { get { return _view; } }

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

        private ObservableCollection<BaseAlarmSignal> signals = new ObservableCollection<BaseAlarmSignal>();
        public IEnumerable<BaseAlarmSignal> Signals { get { return signals; } }

        private BaseAlarmSignal selectedSignal;
        public BaseAlarmSignal SelectedSignal
        {
            get { return selectedSignal; }
            set
            {
                if (selectedSignal != value)
                {
                    selectedSignal = value;
                    OnPropertyChanged(() => SelectedSignal);
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

        private ICommand selectedTreeChangedComamnd;
        public ICommand SelectedTreeChangedComamnd
        {
            get
            {
                return this.selectedTreeChangedComamnd ?? (this.selectedTreeChangedComamnd = new DelegateCommand<object>(para => this.SelectedTreeChanged(para)));
            }
        }

        private string SearchName;
        public void SelectedTreeChanged(object para)
        {
            if (para is DeviceTreeItemViewModel)
            {
                SearchName = _cardProcess.GetOrganizationName(para as OrganizationTreeItemViewModel);               
                _view.Refresh();
            }
            else if (para is OrganizationTreeItemViewModel)
            {
                if ((para as OrganizationTreeItemViewModel).Children != null && (para as OrganizationTreeItemViewModel).Children.Count > 0 && (para as OrganizationTreeItemViewModel).Children[0] is DeviceTreeItemViewModel)
                {
                    SearchName = _cardProcess.GetOrganizationName(para as OrganizationTreeItemViewModel);
                    _view.Refresh();
                }
            }          
          
        }
    }
}
