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
using System.Threading;
using AIC.Resources.Models;
using System.Windows;
using AIC.M9600.Client.DataProvider;
using AIC.Core;

namespace AIC.HistoryDataPage.ViewModels
{
    class HistoryDataStatisticsViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;       
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;

        public HistoryDataStatisticsViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, IDatabaseComponent databaseComponent)
        {           
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;

            _signalProcess.SignalsAdded += _signalCache_SignalAdded;
            _signalProcess.SignalsRemoved += _signalCache_SignalRemoved;

            foreach (var sg in _signalProcess.Signals.OfType<BaseWaveSignal>().ToArray())
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
                    return true;
                }
                return false;
            };
            //_view.GroupDescriptions.Add(new PropertyGroupDescription("OrganizationDeviceName"));//对视图进行分组
            _view.SortDescriptions.Add(new SortDescription("DangerCount",ListSortDirection.Descending));
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

        private ViewModelStatus _status = ViewModelStatus.None;
        public ViewModelStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public string waitinfo = "数据查询中";
        public string WaitInfo
        {
            get
            {
                return waitinfo;
            }
            set
            {
                waitinfo = value;
                OnPropertyChanged("WaitInfo");
            }
        }
        #endregion

        #region Command
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

        private DelegateCommand refreshCommand;
        public DelegateCommand RefreshCommand
        {
            get
            {
                return this.refreshCommand ?? (this.refreshCommand = new DelegateCommand(() => this.Refresh()));
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
                Refresh();
            }
            else if (para is OrganizationTreeItemViewModel)
            {
                if ((para as OrganizationTreeItemViewModel).Children != null && (para as OrganizationTreeItemViewModel).Children.Count > 0 && (para as OrganizationTreeItemViewModel).Children[0] is DeviceTreeItemViewModel)
                {                    
                    OrganizationNames = _cardProcess.GetDevices(para as OrganizationTreeItemViewModel).Select(p => p.FullName).ToList();
                    SelectedOrganization = para as OrganizationTreeItemViewModel;                   
                    Refresh();
                }
                else
                {
                    OrganizationNames = new List<string>();                  
                    Refresh();
                }
            }          
          
        }

        private void SelectedDataGridChanged(object para)
        {
            ;      
        }
      
        private async void Refresh()
        {
            _view.Refresh();
            try
            {
                Status = ViewModelStatus.Querying;

                string ip = null;
                HashSet<Guid> guidlist = new HashSet<Guid>();
                List<BaseAlarmSignal> sglist = new List<BaseAlarmSignal>();
                foreach (var item in _view)
                {
                    var sg = item as BaseWaveSignal;
                    if (sg != null)
                    {
                        guidlist.Add(sg.Guid);
                        sglist.Add(sg);
                        if (ip == null)
                        {
                            ip = sg.ServerIP;
                        }
                    }
                }
                if (sglist.Count == 0)
                {
                    return;
                }
               
                var alarmlist = await _databaseComponent.GetStatisticsData(ip, guidlist);
                
                foreach (var alarm in alarmlist)
                {                    
                    if (alarm.Value.Count > 0)
                    {
                        BaseAlarmSignal sg = sglist.Where(p => p.Guid == alarm.Key).FirstOrDefault();
                        if (sg != null)
                        {
                            if (alarm.Value.ContainsKey("PreAlarmCount"))
                            {
                                sg.PreAlarmCount = alarm.Value["PreAlarmCount"];
                            }
                            if (alarm.Value.ContainsKey("AlarmCount"))
                            {
                                sg.AlarmCount = alarm.Value["AlarmCount"];
                            }
                            if (alarm.Value.ContainsKey("DangerCount"))
                            {
                                sg.DangerCount = alarm.Value["DangerCount"];
                            }
                            var first = alarm.Value["FirstUploadTime"];
                            var last = alarm.Value["LastUploadTime"];
                            sg.FirstUploadTime = new DateTime((long)first);
                            sg.LastUploadTime = new DateTime((long)last);
                        }                     
                    }                   
                }

                _eventAggregator.GetEvent<DataStatisticsEvent>().Publish(sglist.OrderByDescending(p => p.DangerCount).ToList());
            }
            catch(Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("报警统计", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }          
    }
}
