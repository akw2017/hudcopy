using AIC.Core.Events;
using AIC.Core.ExCommand;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.Servers;
using AIC.Core.UserManageModels;
using AIC.UserPage.Views;
using AIC.Resources.Models;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;
using Wpf.PageNavigationControl;
using AIC.Core.LMModels;
using AIC.CoreType;

namespace AIC.UserPage.ViewModels
{
    class OperateRecordViewModel : BindableBase
    {        
        private readonly IUserManageService _userManageService;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoginUserService _loginUserService;
        public OperateRecordViewModel(IUserManageService userManageService, IEventAggregator eventAggregator, ILoginUserService loginUserService)
        {
            _userManageService = userManageService;
            _eventAggregator = eventAggregator;
            _loginUserService = loginUserService;

            SearchStartTime = DateTime.Now.AddDays(-1);
            SearchEndTime = DateTime.Now;
            //先从数据库里取数据
            //T_OperateRecord = _userManageService.T_OperateRecord["192.168.0.210"];//测试数据 
            T_OperateRecord = new List<T1_OperateRecord>();

            InitPager();
           // _view = new ListCollectionView(T_OperateRecordLast);
            //_view.GroupDescriptions.Add(new PropertyGroupDescription("Name"));//对视图进行分组

            
        }

        //private readonly ICollectionView _view;
        //public ICollectionView T_OperateRecordView { get { return _view; } }


        private string searchName = "";
        public string SearchName
        {
            get
            {
                return searchName;
            }
            set
            {
                searchName = value;
                OnPropertyChanged("SearchName");
            }
        }

        private int pageSize;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
                OnPropertyChanged("PageSize");
            }
        }

        private List<int> pageSizeList;
        public List<int> PageSizeList
        {
            get
            {
                return pageSizeList;
            }
            set
            {
                pageSizeList = value;
                OnPropertyChanged("PageSizeList");
            }
        }

        private int totalItems;
        public int TotalItems
        {
            get
            {
                return totalItems;
            }
            set
            {
                totalItems = value;
                OnPropertyChanged("TotalItems");
            }
        }

        private int currentPage;
        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }      

        private List<T1_OperateRecord> T_OperateRecord;//原始,改为查询数据          

        //public List<LM_OperateRecord> T_OperateRecordLast { get; set; }//分页后  

        private List<T1_OperateRecord> t_OperateRecordLast;
        public List<T1_OperateRecord> T_OperateRecordLast
        {
            get
            {
                return t_OperateRecordLast;
            }
            set
            {
                t_OperateRecordLast = value;
                OnPropertyChanged("T_OperateRecordLast");
            }
        }

        private DelegateCommand queryCommand;
        public DelegateCommand QueryCommand
        {
            get
            {
                return this.queryCommand ?? (this.queryCommand = new DelegateCommand(() => this.Query()));
            }
        }      

        private DelegateCommand<object> currentPageChangedComamnd;
        public DelegateCommand<object> CurrentPageChangedComamnd
        {
            get
            {
                return this.currentPageChangedComamnd ?? (this.currentPageChangedComamnd = new DelegateCommand<object>(value => this.CurrentPageChanged(value)));
            }
        }

        private OperateType operateType;
        public OperateType OperateType
        {
            get
            {
                return operateType;
            }
            set
            {
                operateType = value;
                OnPropertyChanged("OperateType");
            }
        }

        private DateTime searchStartTime;
        public DateTime SearchStartTime
        {
            get
            {
                return searchStartTime;
            }
            set
            {
                searchStartTime = value;
                OnPropertyChanged("SearchStartTime");
            }
        }
        private DateTime searchEndTime;
        public DateTime SearchEndTime
        {
            get
            {
                return searchEndTime;
            }
            set
            {
                searchEndTime = value;
                OnPropertyChanged("SearchEndTime");
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

        public string WaitInfo { get { return "数据处理中"; } set { } }

        private void InitPager()
        {
            PageSizeList = new List<int>();
            PageSizeList.Add(20);
            PageSizeList.Add(50);
            PageSizeList.Add(100);
            PageSize = PageSizeList[0];
            
            TotalItems = T_OperateRecord.Count;
            T_OperateRecordLast = new List<T1_OperateRecord>();           
        }

        private async void Query()
        {
            try
            {
                Status = ViewModelStatus.Querying;
                if (SearchStartTime > SearchEndTime)
                {
                    SearchStartTime = SearchEndTime.AddDays(-1);
                }

                T_OperateRecord = await _loginUserService.GetOperateRecord(SearchStartTime, SearchEndTime, SearchName, OperateType);
                TotalItems = T_OperateRecord.Count;
                T_OperateRecordLast.Clear();
                T_OperateRecordLast = T_OperateRecord.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                //_view.Refresh();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("查询记录", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private void CurrentPageChanged(object value)
        {
            if (T_OperateRecord == null)
            {
                return;
            }
           
            RoutedPropertyChangedEventArgs<PageNavigationEventArgs> args = ((ExCommandParameter)value).EventArgs as RoutedPropertyChangedEventArgs<PageNavigationEventArgs>;
            int pageSize = args.NewValue.PageSize;
            int currentPage = args.NewValue.CurrectPage;
            T_OperateRecordLast.Clear();
            T_OperateRecordLast = T_OperateRecord.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            //_view.Refresh();
        }
    }
}
