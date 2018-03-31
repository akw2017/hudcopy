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
using AIC.OnLineDataPage.ViewModels.SubViewModels;
using Wpf.GridSelected;
using AIC.Resources.Models;

namespace AIC.OnLineDataPage.ViewModels
{
    class OnlineDataTileViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrganizationService _organizationService;
        private readonly ICardProcess _cardProcess;

        public delegate void UpdateListColumnRow(int column, int row);
        public event UpdateListColumnRow UpdateListShow;
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

        private SignalPreProccessType signalPreProccessType;
        public SignalPreProccessType SignalPreProccessType
        {
            get { return signalPreProccessType; }
            set
            {
                if (signalPreProccessType != value)
                {
                    ChangeProcessor(signalPreProccessType, value);
                    signalPreProccessType = value;
                    OnPropertyChanged("SignalPreProccessType");
                }
            }
        }

        private bool isFilter;
        public bool IsFilter
        {
            get { return isFilter; }
            set
            {
                if (isFilter != value)
                {
                    ChangeFilter(isFilter, value);
                    isFilter = value;
                    OnPropertyChanged("IsFilter");
                }
            }
        }

        private BaseAlarmSignal selectedSignal;
        public BaseAlarmSignal SelectedSignal
        {
            get { return selectedSignal; }
            set
            {
                if (selectedSignal != value)
                {
                    selectedSignal = value;
                    OnPropertyChanged(() => this.SelectedSignal);
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
                    if (SignalMonitors.Count != 0)
                    {
                        return;
                    }
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
                    //SelectedSignal = SelectedSignalMonitor.Signal;
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

        private RectangleGridEventArgs select;
        public RectangleGridEventArgs Select
        {
            get { return select; }
            set
            {
                select = value;
                OnPropertyChanged("Select");              
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

        public string waitinfo = "图表添加中";
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

        private List<UIElement> fluidElements = new List<UIElement>();
        public List<UIElement> FluidElements
        {
            get
            {
                return fluidElements;
            }
            set
            {
                fluidElements = value;
                OnPropertyChanged("FluidElements");
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

        private DelegateCommand<object> selectRowColumnChangedComamnd;
        public DelegateCommand<object> SelectRowColumnChangedComamnd
        {
            get
            {
                return this.selectRowColumnChangedComamnd ?? (this.selectRowColumnChangedComamnd = new DelegateCommand<object>(para => this.SelectRowColumnChanged(para)));
            }
        }
        #endregion

        #region 添加、删除显示
        private bool adding = false;
        private void Add(object para)
        {
            try
            {
                Status = ViewModelStatus.Querying;
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

                    SignalTileViewModel viewModel = BuildSignalTileViewModel(signal, CurrentSignalDisplayType, IsFilter, SignalPreProccessType);
                    SignalMonitors.Add(viewModel);
                    //await Task.Delay(500);
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
                        if (!(CurrentSignalDisplayType == SignalDisplayType.RMSTrend || CurrentSignalDisplayType == SignalDisplayType.Value))
                        {
                            CurrentSignalDisplayType = SignalDisplayType.Value;
                        }
                    }

                    SignalTileViewModel viewModel = BuildSignalTileViewModel(signal, CurrentSignalDisplayType, IsFilter, SignalPreProccessType);
                    SignalMonitors.Add(viewModel);
                    //await Task.Delay(500);
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
                            if (!(CurrentSignalDisplayType == SignalDisplayType.RMSTrend || CurrentSignalDisplayType == SignalDisplayType.Value))
                            {
                                continue;
                            }
                        }
                        SignalTileViewModel viewModel = BuildSignalTileViewModel(signal, CurrentSignalDisplayType, IsFilter, SignalPreProccessType);
                        SignalMonitors.Add(viewModel);
                        ///await Task.Delay(500);
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
                            if (!(CurrentSignalDisplayType == SignalDisplayType.RMSTrend || CurrentSignalDisplayType == SignalDisplayType.Value))
                            {
                                continue;
                            }
                        }
                        SignalTileViewModel viewModel = BuildSignalTileViewModel(signal, CurrentSignalDisplayType, IsFilter, SignalPreProccessType);
                        SignalMonitors.Add(viewModel);
                        //await Task.Delay(500);
                    }
                }

                //GetItemsIsUpated(lineCount, startNo, endNo);
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-拼图视图-添加信号", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
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

        private async void ShowFunctions(object args)
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
                Status = ViewModelStatus.Querying;
                foreach (SignalDisplayType displayType in Enum.GetValues(typeof(SignalDisplayType)))
                {
                    if (displayType == SignalDisplayType.AlarmPointTrend
                        || displayType == SignalDisplayType.OffDesignCondition
                        || displayType == SignalDisplayType.SingleDivFre
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
                        if (!(displayType == SignalDisplayType.RMSTrend || displayType == SignalDisplayType.Value))
                        {
                            continue;
                        }
                    }
                    SignalTileViewModel viewModel = BuildSignalTileViewModel(signal, displayType, IsFilter, SignalPreProccessType);
                    SignalMonitors.Add(viewModel);
                    await Task.Delay(500);
                }
              
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-多功能拼图", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private SignalTileViewModel BuildSignalTileViewModel(BaseAlarmSignal sg, SignalDisplayType signalDisplayType, bool isfilter, SignalPreProccessType signalPreType)
        {
            SignalTileViewModel viewModel = new SignalTileViewModel(sg);
            viewModel.ItemWidth = itemWidth;
            viewModel.ItemHeight = itemHeight;
            viewModel.SetDisplayModeAsync(signalDisplayType, isfilter, signalPreType);
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
                        SelectedSignalMonitor.SetDisplayModeAsync(CurrentSignalDisplayType, IsFilter, SignalPreProccessType);
                        if (SelectedSignalMonitor.DataViewModel != null)
                        {
                            //SelectedSignalMonitor.DataViewModel.IsUpdated = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-拼图视图-选取信号", ex));
            }
        }

        private void ChangeProcessor(SignalPreProccessType oldsignalPreType, SignalPreProccessType newsignalPreTyp)
        {
            if (adding == true)
            {
                return;
            }
            try
            {
                if (SelectedSignalMonitor != null && SelectedSignalMonitor.DataViewModel != null)
                {
                    SelectedSignalMonitor.DataViewModel.SignalPreProccessType = newsignalPreTyp;
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-拼图视图-选取信号", ex));
            }
        }

        public void ChangeFilter(bool oldisFilter, bool newisFilter)
        {
            if (adding == true)
            {
                return;
            }
            try
            {
                if (SelectedSignalMonitor != null && SelectedSignalMonitor.DataViewModel != null)
                {
                    SelectedSignalMonitor.DataViewModel.IsFilter = newisFilter;
                }
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-拼图视图-选取信号", ex));
            }
        }

        private void SelectRowColumnChanged(object para)
        {
            RoutedPropertyChangedEventArgs<RectangleGridEventArgs> args = ((ExCommandParameter)para).EventArgs as RoutedPropertyChangedEventArgs<RectangleGridEventArgs>;
            if (args != null)
            {
                if (UpdateListShow != null)
                {
                    UpdateListShow(args.NewValue.Row, args.NewValue.Column);
                }
            }
        }
        #endregion

        #region 页面控制       
        private double listWidth;
        private double listHeight;
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
            var xx = this.FluidElements;
            var sender = ((ExCommandParameter)value).Sender as ListBox;
            var args = ((ExCommandParameter)value).EventArgs as ScrollChangedEventArgs;
            if (args != null)
            {
                var offset = new Size(args.HorizontalOffset, args.VerticalOffset);
                var extent = new Size(args.ExtentWidth, args.ExtentHeight);
                var viewport = new Size(args.ViewportWidth, args.ViewportHeight);

                int firstVisibleItemIndex = 0;
                int lastVisibleItemIndex = 0;
                GetVisibleRange(ref firstVisibleItemIndex, ref lastVisibleItemIndex, extent, offset, viewport);

                CleanUpItems(firstVisibleItemIndex, lastVisibleItemIndex);
            }
        }

        private void CleanUpItems(int firstVisibleItemIndex, int lastVisibleItemIndex)
        {
            for (int i = 0; i < this.FluidElements.Count; i++)
            {
                var viewModel = (this.FluidElements[i] as ListBoxItem).DataContext as SignalTileViewModel;
                if (viewModel != null)
                {
                    if (firstVisibleItemIndex <= i && i <= lastVisibleItemIndex)
                    {
                        viewModel.Load();
                    }
                    else
                    {
                        viewModel.Unload();
                    }
                }
            }
        }

        private void GetVisibleRange(ref int firstVisibleItemIndex, ref int lastVisibleItemIndex, Size extent, Size offset, Size viewport )
        {
            try
            {
                if (this.Orientation == Orientation.Horizontal)
                {
                    int childrenPerRow = CalculateChildrenPerRow(extent);
                    firstVisibleItemIndex = Convert.ToInt32(Math.Floor(offset.Height / this.ItemHeight)) * childrenPerRow;
                    lastVisibleItemIndex = Convert.ToInt32(Math.Ceiling((offset.Height + viewport.Height) / this.ItemHeight)) * childrenPerRow - 1;
                }
                else
                {
                    int childrenPerColumn = CalculateChildrenPerColumn(extent);
                    firstVisibleItemIndex = Convert.ToInt32(Math.Floor(offset.Width / this.ItemWidth)) * childrenPerColumn;
                    lastVisibleItemIndex = Convert.ToInt32(Math.Ceiling((offset.Width + viewport.Width) / this.ItemWidth)) * childrenPerColumn - 1;
                }
                int itemCount = this.FluidElements.Count;
                if (lastVisibleItemIndex >= itemCount)
                {
                    lastVisibleItemIndex = itemCount - 1;
                }
            }
            catch (OverflowException)
            {
                // No idea if we can ignore this
            }
        }

        private int CalculateChildrenPerRow(Size availableSize)
        {
            // Figure out how many children fit on each row
            int childrenPerRow = 0;

            if (availableSize.Width == double.PositiveInfinity)
            {
                childrenPerRow = this.FluidElements.Count;
            }
            else
            {
                try
                {
                    childrenPerRow = Math.Max(1, Convert.ToInt32(Math.Floor(availableSize.Width / this.ItemWidth)));
                }
                catch (OverflowException)
                {
                    // No idea if we can ignore this
                }
            }
            return childrenPerRow;
        }

        private int CalculateChildrenPerColumn(Size availableSize)
        {
            // Figure out how many children fit on each row
            int childrenPerColumn = 0;

            if (availableSize.Height == double.PositiveInfinity)
            {
                childrenPerColumn = this.FluidElements.Count;
            }
            else
            {
                try
                {
                    childrenPerColumn = Math.Max(1, Convert.ToInt32(Math.Floor(availableSize.Height / this.ItemHeight)));
                }
                catch (OverflowException)
                {
                    // No idea if we can ignore this
                }
            }
            return childrenPerColumn;
        }
        #endregion
    }
}