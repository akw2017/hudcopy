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
using System.Threading;
using AIC.Resources.Models;
using System.Windows;
using AIC.M9600.Client.DataProvider;
using AIC.Core;

namespace AIC.OnLineDataPage.ViewModels
{
    class OnlineDataStatisticsViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ISignalProcess _signalProcess;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ILoginUserService _loginUserService;
        public delegate void UpdatePie3D(IList<int> countlist);
        public event UpdatePie3D UpdateChart;

        public OnlineDataStatisticsViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ISignalProcess signalProcess, ICardProcess cardProcess, IDatabaseComponent databaseComponent, ILoginUserService loginUserService)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _signalProcess = signalProcess;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;
            _loginUserService = loginUserService;

            _signalProcess.SignalsAdded += _signalCache_SignalAdded;
            _signalProcess.SignalsRemoved += _signalCache_SignalRemoved;

            foreach (var sg in _signalProcess.Signals.OfType<BaseAlarmSignal>().ToArray())
            {
                signals.Add(sg);
            }

            selectedsignals = signals;
            CustomSystemException = _loginUserService.CustomSystemException;
            if (CustomSystemException.Count > 0)
            {
                AlarmEventTitle = "报警事件(" + CustomSystemException.FirstOrDefault().EventTime.ToString("yyyy-MM-dd") + "),最新" + CustomSystemException.Count() + "条";
            }

            _view = new ListCollectionView(signals);
            _view.Filter = (object item) =>
            {
                var itemPl = (BaseAlarmSignal)item;
                if (itemPl == null) return false;
                if (selectedsignals.Contains(itemPl))
                {
                    if(itemPl.DelayAlarmGrade == AlarmGrade.HighNormal || itemPl.DelayAlarmGrade == AlarmGrade.LowNormal)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            };
            _view.GroupDescriptions.Add(new PropertyGroupDescription("DelayAlarmGrade"));//对视图进行分组
            _view.SortDescriptions.Add(new SortDescription("DelayAlarmGrade", ListSortDirection.Descending));
            InitTree();

            _eventAggregator.GetEvent<CustomSystemEvent>().Subscribe(CustomSystemHappenEvent, ThreadOption.UIThread);//<!--昌邑石化-->
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
        private ObservableCollection<CustomSystemException> customSystemException;
        public ObservableCollection<CustomSystemException> CustomSystemException
        {
            get { return customSystemException; }
            set
            {
                customSystemException = value;
                OnPropertyChanged("CustomSystemException");
            }
        }

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

        public string alarmEventTitle = "报警事件";
        public string AlarmEventTitle
        {
            get
            {
                return alarmEventTitle;
            }
            set
            {
                alarmEventTitle = value;
                OnPropertyChanged("AlarmEventTitle");
            }
        }

        private AlarmGrade firstAlarmGrade;
        public AlarmGrade FirstAlarmGrade
        {
            get { return firstAlarmGrade; }
            set
            {
                if (firstAlarmGrade != value)
                {
                    firstAlarmGrade = value;
                    OnPropertyChanged(() => FirstAlarmGrade);
                }
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

        private AlarmGrade? SliceAlarmGrade;
        private IEnumerable<BaseAlarmSignal> selectedsignals;
        public void SelectedTreeChanged(object para)
        {
            if (para is OrganizationTreeItemViewModel)
            {
                selectedsignals = _cardProcess.GetItems(para as OrganizationTreeItemViewModel).Select(p => p.BaseAlarmSignal);
                Refresh();
            }
        }

        private void SelectedDataGridChanged(object para)
        {
            ;
        }

        private void CustomSystemHappenEvent(CustomSystemException ex)
        {
            AlarmEventTitle = "报警事件(" + ex.EventTime.ToString("yyyy-MM-dd")+ "),最新" + CustomSystemException.Count() + "条";
            Refresh();
        }

        private void Refresh()
        {
            if (selectedsignals == null)
            {
                return;
            }
            
            _view.Refresh();           
            int NormalCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighNormal || o.DelayAlarmGrade == AlarmGrade.LowNormal)).Count();
            int PreAlertCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighPreAlarm || o.DelayAlarmGrade == AlarmGrade.LowPreAlarm)).Count();
            int AlertCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighAlarm || o.DelayAlarmGrade == AlarmGrade.LowAlarm)).Count();
            int DangerCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.HighDanger || o.DelayAlarmGrade == AlarmGrade.LowDanger)).Count();
            int AbnormalCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.Abnormal)).Count();
            int UnConnectCount = selectedsignals.Where(o => (o.DelayAlarmGrade == AlarmGrade.DisConnect)).Count();
            if (UpdateChart != null)
            {
                UpdateChart(new List<int> { NormalCount, PreAlertCount, AlertCount, DangerCount, AbnormalCount, UnConnectCount });
            }
        }

        public void SliceClick(AlarmGrade alarmgrade)
        {
            List<AlarmGrade> grades = new List<AlarmGrade>();
            foreach (AlarmGrade grade in Enum.GetValues(typeof(AlarmGrade)))
            {
                if ((alarmgrade & grade) == grade)
                {
                    if (!grades.Contains(grade))
                    {
                        grades.Add(grade);
                    }
                }
            }
            FirstAlarmGrade = selectedsignals.Where(p => grades.Contains(p.DelayAlarmGrade)).FirstOrDefault().DelayAlarmGrade;
            //_view.Refresh();
        }
    }
}
