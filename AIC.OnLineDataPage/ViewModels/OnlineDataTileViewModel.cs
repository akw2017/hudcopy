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

namespace AIC.OnLineDataPage.ViewModels
{
    class OnlineDataTileViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ICardProcess _cardProcess;
        public OnlineDataTileViewModel(IEventAggregator eventAggregator, IOrganizationService organizationService, ICardProcess cardProcess)
        {
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _cardProcess = cardProcess;
            InitTree();            
        }

        #region 管理树
        private void InitTree()
        {
            OrganizationTreeItems = _organizationService.OrganizationTreeItems;
            TreeExpanded();
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

        private FastObservableCollection<SignalTileViewModel> signalMonitors = new FastObservableCollection<SignalTileViewModel>();
        public FastObservableCollection<SignalTileViewModel> SignalMonitors
        {
            get { return signalMonitors; }
            set
            {
                signalMonitors = value;
                OnPropertyChanged("SignalMonitors");
            }
        }


        private SignalDisplayType currentSignalDisplayType;
        public SignalDisplayType CurrentSignalDisplayType
        {
            get { return currentSignalDisplayType; }
            set
            {
                if (currentSignalDisplayType != value)
                {
                    currentSignalDisplayType = value;
                    OnPropertyChanged("CurrentSignalDisplayType");
                }
            }
        }    

        private SignalTileViewModel selectedSignalMonitor;
        public SignalTileViewModel SelectedSignalMonitor
        {
            get { return selectedSignalMonitor; }
            set
            {
                if (value == null)
                {
                    return;
                }
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
                        CurrentSignalDisplayType = SelectedSignalMonitor.DisplayMode;
                    }
                    OnPropertyChanged("SelectedSignalMonitor");
                }
            }
        }

        private double itemWidth = 350;
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

        private double itemHeight = 205;
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

        private bool isShowAlarm = false;
        public bool IsShowAlarm
        {
            get { return isShowAlarm; }
            set
            {
                if (isShowAlarm != value)
                {
                    isShowAlarm = value;
                    OnPropertyChanged("IsShowAlarm");
                    foreach (var item in SignalMonitors)
                    {
                        item.IsShowAlarm = value;
                    }
                }
            }
        }
        # endregion

        #region Command
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                return this.addCommand ?? (this.addCommand = new DelegateCommand<object>(para => this.Add(para)));
            }
        }
        private DelegateCommand<object> deleteCommand;
        public DelegateCommand<object> DeleteCommand
        {
            get
            {
                return this.deleteCommand ?? (this.deleteCommand = new DelegateCommand<object>(para => this.Delete(para)));
            }
        }

        private ICommand selectSignalDisplayModeCommand;
        public ICommand SelectSignalDisplayModeCommand
        {
            get
            {
                return this.selectSignalDisplayModeCommand ?? (this.selectSignalDisplayModeCommand = new DelegateCommand<object>(para => this.SelectSignalDisplayMode(para)));
            }
        }

        private DelegateCommand<object> showFunctionsCommand;
        public DelegateCommand<object> ShowFunctionsCommand
        {
            get
            {
                return this.showFunctionsCommand ?? (this.showFunctionsCommand = new DelegateCommand<object>(para => this.ShowFunctions(para)));
            }
        }     

        private DelegateCommand<object> clearAllCommand;
        public DelegateCommand<object> ClearAllCommand
        {
            get
            {
                return this.clearAllCommand ?? (this.clearAllCommand = new DelegateCommand<object>(para => this.ClearAll(para)));
            }
        }      

        private DelegateCommand<object> listBoxLoadedComamnd;
        public DelegateCommand<object> ListBoxLoadedComamnd
        {
            get
            {
                return this.listBoxLoadedComamnd ?? (this.listBoxLoadedComamnd = new DelegateCommand<object>(para => this.ListBoxLoaded(para)));
            }
        }

        private DelegateCommand<object> scrollChangedComamnd;
        public DelegateCommand<object> ScrollChangedComamnd
        {
            get
            {
                return this.scrollChangedComamnd ?? (this.scrollChangedComamnd = new DelegateCommand<object>(para => this.ScrollChanged(para)));
            }
        }

      
        #endregion

        #region 添加、删除显示
        private bool adding = false;
        private void Add(object para)
        {
            try
            {
                OrganizationTreeItemViewModel organization = para as OrganizationTreeItemViewModel;
                adding = true;
                if (organization is DivFreTreeItemViewModel)//分频
                {
                    var signal = (organization.Parent as ItemTreeItemViewModel).BaseAlarmSignal;
                    if (!(signal != null && signal is BaseAlarmSignal))//数据为空时
                    {
                        return;
                    }

                    CurrentSignalDisplayType = SignalDisplayType.MultiDivFre;

                    SignalTileViewModel viewModel = BuildSignalTileViewModel(signal, CurrentSignalDisplayType);
                    SignalMonitors.Add(viewModel);
                }
                else if (organization is ItemTreeItemViewModel)//测点
                {
                    if ((organization as ItemTreeItemViewModel).IsPaired == false)
                    {
                        return;
                    }
                    var signal = (organization as ItemTreeItemViewModel).BaseAlarmSignal;
                    if (!(signal != null && signal is BaseAlarmSignal))//数据为空时
                    {
                        return;
                    }

                    if (!(signal is BaseWaveSignal))
                    {
                        if (!(CurrentSignalDisplayType == SignalDisplayType.AMSTrend || CurrentSignalDisplayType == SignalDisplayType.Value))
                        {
                            CurrentSignalDisplayType = SignalDisplayType.Value;
                        }
                    }

                    SignalTileViewModel viewModel = BuildSignalTileViewModel(signal, CurrentSignalDisplayType);
                    SignalMonitors.Add(viewModel);
                }
                else if (organization is DeviceTreeItemViewModel)//设备
                {
                    foreach (var p in organization.Children)
                    {
                        if ((p as ItemTreeItemViewModel).IsPaired == false)
                        {
                            continue;
                        }
                        var signal = (p as ItemTreeItemViewModel).BaseAlarmSignal;
                        if (!(signal != null && signal is BaseAlarmSignal))//数据为空时
                        {
                            continue;
                        }

                        if (!(signal is BaseWaveSignal))
                        {
                            if (!(CurrentSignalDisplayType == SignalDisplayType.AMSTrend || CurrentSignalDisplayType == SignalDisplayType.Value))
                            {
                                continue;
                            }
                        }
                        SignalTileViewModel viewModel = BuildSignalTileViewModel(signal, CurrentSignalDisplayType);
                        SignalMonitors.Add(viewModel);
                    }
                }
                else if (organization is OrganizationTreeItemViewModel)//组织
                {
                    var items = _cardProcess.GetItems(organization);
                    foreach (var item in items)
                    {
                        if (item.IsPaired == false)
                        {
                            continue;
                        }
                        var signal = item.BaseAlarmSignal;
                        if (!(signal != null && signal is BaseAlarmSignal))//数据为空时
                        {
                            continue;
                        }

                        if (!(signal is BaseWaveSignal))
                        {
                            if (!(CurrentSignalDisplayType == SignalDisplayType.AMSTrend || CurrentSignalDisplayType == SignalDisplayType.Value))
                            {
                                continue;
                            }
                        }
                        SignalTileViewModel viewModel = BuildSignalTileViewModel(signal, CurrentSignalDisplayType);
                        SignalMonitors.Add(viewModel);
                    }
                }

                GetItemsIsUpated(lineCount, startNo, endNo);
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-拼图视图-添加信号", ex));
            }
            finally
            {
                adding = false;
            }
        }

        public void Delete(object para)
        {
            SignalTileViewModel viewModel = para as SignalTileViewModel;
            if (viewModel != null && SignalMonitors.Contains(viewModel))
            {
                viewModel.Close();
                SignalMonitors.Remove(viewModel);
            }           
        }

        private void ClearAll(object args)
        {
            foreach (var viewModel in SignalMonitors)
            {
                viewModel.Close();
            }
            SignalMonitors.Clear();           
        }

        private void ShowFunctions(object args)
        {
            var item = args as ItemTreeItemViewModel;

            if (item == null || item.IsPaired == false)
            {
                return;
            }
            var signal = item.BaseAlarmSignal;
            if (!(signal != null && signal is BaseAlarmSignal))//数据为空时
            {
                return;
            }

            try
            {
                List<SignalTileViewModel> models = new List<SignalTileViewModel>();
                foreach (SignalDisplayType displayType in Enum.GetValues(typeof(SignalDisplayType)))
                {
                    if (displayType == SignalDisplayType.AlarmPointTrend
                        || displayType == SignalDisplayType.OffDesignCondition
                        || displayType == SignalDisplayType.SingleDivFre
                        //|| displayType == SignalDisplayType.Time3DSpectrum
                        || displayType == SignalDisplayType.RPM3D
                        || displayType == SignalDisplayType.OrderAnalysis
                        || displayType == SignalDisplayType.Nyquist
                        || displayType == SignalDisplayType.Ortho
                        || displayType == SignalDisplayType.MultiDivFre
                        || displayType == SignalDisplayType.Bode)
                    {
                        continue;
                    }
                    if (!(signal is BaseWaveSignal))
                    {
                        if (!(displayType == SignalDisplayType.AMSTrend || displayType == SignalDisplayType.Value))
                        {
                            continue;
                        }
                    }
                    SignalTileViewModel viewModel = BuildSignalTileViewModel(signal, displayType);
                    if (viewModel != null)
                    {
                        models.Add(viewModel);
                    }
                }
                if (models.Count > 1)
                {
                    SignalMonitors.AddItems(models);
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-多功能拼图", ex));
            }
        }

        private SignalTileViewModel BuildSignalTileViewModel(BaseAlarmSignal sg, SignalDisplayType signalDisplayType)
        {
            SignalTileViewModel viewModel = new SignalTileViewModel(sg);
            viewModel.ItemWidth = itemWidth;
            viewModel.ItemHeight = itemHeight;
            viewModel.SetDisplayModeAsync(signalDisplayType); 
            viewModel.CloseCommand = DeleteCommand;
            return viewModel;
        }

        private void SelectSignalDisplayMode(object para)
        {
            if (adding == true)
            {
                return;
            }
            try
            {
                if (SelectedSignalMonitor != null && SelectedSignalMonitor.Signal != null)
                {
                    var sg = SelectedSignalMonitor.Signal;
                    if (sg.SupportFunView(CurrentSignalDisplayType))
                    {                       
                        SelectedSignalMonitor.SetDisplayModeAsync(CurrentSignalDisplayType);
                        if (SelectedSignalMonitor.DataViewModel != null)
                        {
                            SelectedSignalMonitor.DataViewModel.IsUpdated = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-拼图视图-选取信号", ex));
            }
        }
     
        #endregion

        #region 页面控制       
        private double listWidth;
        private double listHeight;
        private int lineCount;
        private int startNo;
        private int endNo;
        private void ListBoxLoaded(object value)
        {
            var sender = ((ExCommandParameter)value).Sender as ListBox;
            if (sender != null)
            {
                listWidth = sender.ActualWidth;
                listHeight = sender.ActualHeight;
                ItemHeight = (listHeight - 40) / 4;
                ItemWidth = (listWidth - 60) / 4;
            }
        }

        private void ScrollChanged(object value)
        {
            var sender = ((ExCommandParameter)value).Sender as ListBox;
            var args = ((ExCommandParameter)value).EventArgs as ScrollChangedEventArgs;
            if (args != null)
            {
                var HorizontalOffset = args.HorizontalOffset;
                var VerticalOffset = args.VerticalOffset;
                var ExtentWidth = args.ExtentWidth;
                var ExtentHeight = args.ExtentHeight;
                if (Orientation == Orientation.Vertical)//横向滑动
                {
                    lineCount = (int)(ExtentHeight / ItemHeight);
                    startNo = (int)(HorizontalOffset / ItemWidth);
                    endNo = (int)Math.Ceiling((HorizontalOffset + listWidth) / ItemWidth);                    
                }
                else if (Orientation == Orientation.Horizontal)//竖向滑动
                {
                    lineCount = (int)(ExtentWidth / ItemWidth);
                    startNo = (int)(VerticalOffset / ItemHeight);
                    endNo = (int)Math.Ceiling((VerticalOffset + listHeight) / ItemHeight);                    
                }
                GetItemsIsUpated(lineCount, startNo, endNo);
            } 
        }

        private void GetItemsIsUpated(int linecount, int startno, int endno)
        {
            for (int i = 0; i < SignalMonitors.Count; i++)
            {
                if (SignalMonitors[i].DataViewModel == null)
                {
                    continue;
                }

                if (i >= startno * linecount && i < endno * linecount)
                {  
                    SignalMonitors[i].DataViewModel.IsUpdated = true;
                }
                else
                {
                    SignalMonitors[i].DataViewModel.IsUpdated = true;
                }
            }              
        }
        
        #endregion
    }
}
