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
using AIC.MatlabMath;
using AIC.OnLineDataPage.Models;

namespace AIC.OnLineDataPage.ViewModels
{
    class OnlineDataListViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;       
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;      

        public OnlineDataListViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess)
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
            _view.Filter = (object item) =>
            {
                if (OrganizationNames == null || OrganizationNames.Count == 0) return false;
                var itemPl = (BaseAlarmSignal)item;
                if (itemPl == null) return false;               
                if (OrganizationNames.Contains(itemPl.OrganizationDeviceName) && itemPl.ServerIP == SelectedOrganization.ServerIP)             
                {
                    if (IsInvalidSignal == false && IsHighNormalSignal == false && IsHighPreAlertSignal == false && IsHighAlertSignal == false && IsHighDangerSignal == false
                    && IsLowDangerSignal == false && IsLowAlertSignal == false && IsLowPreAlertSignal == false && IsLowNormalSignal == false && IsUnConnectSignal == false)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.Invalid && IsInvalidSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.HighNormal && IsHighNormalSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.HighPreAlert && IsHighPreAlertSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.HighAlert && IsHighAlertSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.HighDanger && IsHighDangerSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.LowDanger && IsHighDangerSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.LowAlert && IsHighAlertSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.LowPreAlert && IsHighPreAlertSignal == true)
                    {
                        return true;
                    }
                    if (itemPl.AlarmGrade == AlarmGrade.LowNormal && IsHighNormalSignal == true)
                    {
                        return true;
                    }
                    if(itemPl.AlarmGrade == AlarmGrade.DisConnect && IsUnConnectSignal == true)
                    {
                        return true;
                    }

                    return false;
                }
                return false;
            };
            _view.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationDeviceName"));//对视图进行分组
            InitTree();

            readDataTimer.Tick += new EventHandler(timeCycle);
            readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            readDataTimer.Start();
        }
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

        private string diagnosticInfo;
        public string DiagnosticInfo
        {
            get { return diagnosticInfo; }
            set
            {
                if (diagnosticInfo != value)
                {
                    diagnosticInfo = value;
                    OnPropertyChanged(() => DiagnosticInfo);
                }
            }
        }

        private string diagnosticAdvice;
        public string DiagnosticAdvice
        {
            get { return diagnosticAdvice; }
            set
            {
                if (diagnosticAdvice != value)
                {
                    diagnosticAdvice = value;
                    OnPropertyChanged(() => DiagnosticAdvice);
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

        private bool isUnConnectSignal;
        public bool IsUnConnectSignal
        {
            get { return isUnConnectSignal; }
            set
            {
                if (isUnConnectSignal != value)
                {
                    isUnConnectSignal = value;
                    OnPropertyChanged(() => IsUnConnectSignal);
                    _view.Refresh();
                }
            }
        }

        private int normalCount;
        public int NormalCount
        {
            get { return normalCount; }
            set
            {
                if (normalCount != value)
                {
                    normalCount = value;
                    this.OnPropertyChanged(() => this.NormalCount);
                }
            }
        }
        private int preAlertCount;
        public int PreAlertCount
        {
            get { return preAlertCount; }
            set
            {
                if (preAlertCount != value)
                {
                    preAlertCount = value;
                    this.OnPropertyChanged(() => this.PreAlertCount);
                }
            }
        }
        private int alertCount;
        public int AlertCount
        {
            get { return alertCount; }
            set
            {
                if (alertCount != value)
                {
                    alertCount = value;
                    this.OnPropertyChanged(() => this.AlertCount);
                }
            }
        }
        private int dangerCount;
        public int DangerCount
        {
            get { return dangerCount; }
            set
            {
                if (dangerCount != value)
                {
                    dangerCount = value;
                    this.OnPropertyChanged(() => this.DangerCount);
                }
            }
        }

        private int abnormalCount;
        public int AbnormalCount
        {
            get { return abnormalCount; }
            set
            {
                if (abnormalCount != value)
                {
                    abnormalCount = value;
                    this.OnPropertyChanged(() => this.AbnormalCount);
                }
            }
        }

        private int unConnectCount;
        public int UnConnectCount
        {
            get { return unConnectCount; }
            set
            {
                if (unConnectCount != value)
                {
                    unConnectCount = value;
                    this.OnPropertyChanged(() => this.UnConnectCount);
                }
            }
        }

        #endregion

        #region 命令
        private ICommand selectedTreeChangedComamnd;
        public ICommand SelectedTreeChangedComamnd
        {
            get
            {
                return this.selectedTreeChangedComamnd ?? (this.selectedTreeChangedComamnd = new DelegateCommand<object>(para => this.SelectedTreeChanged(para)));
            }
        }

        public ICommand selectedDataGridChangedComamnd;
        public ICommand SelectedDataGridChangedComamnd
        {
            get
            {
                return this.selectedDataGridChangedComamnd ?? (this.selectedDataGridChangedComamnd = new DelegateCommand<object>(para => this.SelectedDataGridChanged(para)));
            }
        }

        public ICommand groupViewCommand;
        public ICommand GroupViewCommand
        {
            get
            {
                return this.groupViewCommand ?? (this.groupViewCommand = new DelegateCommand(() => this.GroupView()));
            }
        }

        public ICommand unGroupViewCommand;
        public ICommand UnGroupViewCommand
        {
            get
            {
                return this.unGroupViewCommand ?? (this.unGroupViewCommand = new DelegateCommand(() => this.UnGroupView()));
            }
        }
        #endregion

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

        private List<string> OrganizationNames;
        private OrganizationTreeItemViewModel SelectedOrganization;
        public void SelectedTreeChanged(object para)
        {           
            if (para is DeviceTreeItemViewModel)
            {  
                OrganizationNames = _cardProcess.GetDevices(para as OrganizationTreeItemViewModel).Select(p => p.FullName).ToList();
                SelectedOrganization = para as OrganizationTreeItemViewModel;
               _view.Refresh();

                if (SelectedOrganization.Children != null)
                {
                    var childs = SelectedOrganization.Children.OfType<ItemTreeItemViewModel>().Where(p => p.T_Item != null && p.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo && p.BaseAlarmSignal != null);
                    var abnormal = childs.Where(p => p.Alarm != AlarmGrade.Invalid && p.Alarm != AlarmGrade.HighNormal && p.Alarm != AlarmGrade.LowNormal && p.Alarm != AlarmGrade.DisConnect).OrderByDescending(p => (int)p.Alarm & 0xff).ThenByDescending(p => p.BaseAlarmSignal.Result).FirstOrDefault();
                    if (abnormal != null && abnormal.BaseAlarmSignal is BaseWaveSignal)
                    {
                        var sg = abnormal.BaseAlarmSignal as BaseWaveSignal;
                        DiagnosticInfoClass.GetDiagnosticInfo(sg);
                        DiagnosticInfo = sg.OrganizationDeviceName + "-诊断信息:" + sg.DiagnosticInfo;
                        DiagnosticAdvice = sg.DiagnosticAdvice;
                    }
                    else
                    {
                        DiagnosticInfo = null;
                        DiagnosticAdvice = null;
                    }
                }
            }
            else if (para is OrganizationTreeItemViewModel)
            {
                SelectedOrganization = para as OrganizationTreeItemViewModel;
                if ((para as OrganizationTreeItemViewModel).Children != null && (para as OrganizationTreeItemViewModel).Children.Count > 0 && (para as OrganizationTreeItemViewModel).Children[0] is DeviceTreeItemViewModel)
                {                 
                    OrganizationNames = _cardProcess.GetDevices(para as OrganizationTreeItemViewModel).Select(p => p.FullName).ToList();
                    _view.Refresh();
                }
                else
                {
                    OrganizationNames = new List<string>();
                    _view.Refresh();
                }
            }          
          
        }

        private void SelectedDataGridChanged(object para)
        {
            var sg = para as BaseWaveSignal;
            if (sg != null)
            {
                DiagnosticInfoClass.GetDiagnosticInfo(sg);
                DiagnosticInfo = sg.OrganizationDeviceName + "-诊断信息:" + sg.DiagnosticInfo;
                DiagnosticAdvice = sg.DiagnosticAdvice;
            }
            else
            {
                DiagnosticInfo = "不是振动数据";
                DiagnosticAdvice = null;
            }
        }

        private void GroupView()
        {
            _view.GroupDescriptions.Clear();
            _view.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationDeviceName"));//对视图进行分组
        }

        private void UnGroupView()
        {
            _view.GroupDescriptions.Clear();
            _view.GroupDescriptions.Add(new PropertyGroupDescription("NullName"));//对视图进行分组
        }

        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();      

        private void timeCycle(object sender, EventArgs e)
        {
            NormalCount = 0;
            PreAlertCount = 0;
            AlertCount = 0;
            DangerCount = 0;
            AbnormalCount = 0;
            UnConnectCount = 0;
            if (OrganizationNames == null || SelectedOrganization == null)
            {
                return;
            }
            foreach (var sg in signals)
            {
                if (OrganizationNames.Contains(sg.OrganizationDeviceName) && sg.ServerIP == SelectedOrganization.ServerIP)
                {
                    if (sg != null)
                    {
                        switch (sg.DelayAlarmGrade)
                        {
                            case AlarmGrade.HighNormal:
                            case AlarmGrade.LowNormal:
                                NormalCount++; break;
                            case AlarmGrade.HighPreAlert:
                            case AlarmGrade.LowPreAlert:
                                PreAlertCount++; break;
                            case AlarmGrade.HighAlert:
                            case AlarmGrade.LowAlert:
                                AlertCount++; break;
                            case AlarmGrade.HighDanger:
                            case AlarmGrade.LowDanger:
                                DangerCount++; break;
                            case AlarmGrade.Abnormal:
                                AbnormalCount++; break;
                            case AlarmGrade.DisConnect:
                                UnConnectCount++; break;
                        }
                    }                   
                } 
            }         

        }
    }
}
