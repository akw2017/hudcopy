using AIC.Core.Events;
using AIC.Core.ExCommand;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.Servers;
using AIC.Core.UserManageModels;
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
using AIC.Core.LMModels;
using AIC.CoreType;
using AIC.HistoryDataPage.Views;
using AIC.Core.HardwareModels;

namespace AIC.HistoryDataPage.ViewModels
{
    class HistoryEventListViewModel : BindableBase
    {        
        private readonly ILoginUserService _loginUserService;
        private readonly IOrganizationService _organizationService;
        private readonly IDatabaseComponent _databaseComponent;
        public HistoryEventListViewModel(IDatabaseComponent databaseComponent, ILoginUserService loginUserService, IOrganizationService organizationService)
        {
            _loginUserService = loginUserService;
            _organizationService = organizationService;
            _databaseComponent = databaseComponent;

            customSystemException = _loginUserService.CustomSystemException;

            ServerIPCategory = _databaseComponent.GetServerIPCategory();
            ServerIP = _databaseComponent.MainServerIp;

            SearchStartTime = DateTime.Now.AddDays(-1);
            SearchEndTime = DateTime.Now;
        }

        #region 字段与属性
        private string _serverIP;
        public string ServerIP
        {
            get { return _serverIP; }
            set
            {
                if (_serverIP != value)
                {
                    _serverIP = value;
                    OnPropertyChanged("ServerIP");
                }
            }
        }

        private List<string> _serverIPCategory;
        public List<string> ServerIPCategory
        {
            get { return _serverIPCategory; }
            set
            {
                _serverIPCategory = value;
                OnPropertyChanged("ServerIPCategory");
            }
        }
        private ObservableCollection<T1_SystemEvent> customSystemException;
        public IEnumerable<T1_SystemEvent> CustomSystemException { get { return customSystemException; } }

        private ObservableCollection<T1_SystemEvent> historyCustomSystemException;
        public ObservableCollection<T1_SystemEvent> HistoryCustomSystemException
        {
            get
            {
                return historyCustomSystemException;
            }
            set
            {
                historyCustomSystemException = value;
                OnPropertyChanged("HistoryCustomSystemException");
            }
        }

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

        private bool isRealTime = true;
        public bool IsRealTime
        {
            get
            {
                return isRealTime;
            }
            set
            {
                if (value != isRealTime)
                {                   
                    isRealTime = value;
                    OnPropertyChanged("IsRealTime");
                }
            }
        }

        private CustomSystemType customSystemType = CustomSystemType.Alarm;
        public CustomSystemType CustomSystemType
        {
            get
            {
                return customSystemType;
            }
            set
            {
                customSystemType = value;
                OnPropertyChanged("CustomSystemType");
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
        #endregion

        #region 命令
        private DelegateCommand queryCommand;
        public DelegateCommand QueryCommand
        {
            get
            {
                return this.queryCommand ?? (this.queryCommand = new DelegateCommand(() => this.Query()));
            }
        }

        private DelegateCommand<object> mouseDoubleClickCommand;
        public DelegateCommand<object> MouseDoubleClickCommand
        {
            get
            {
                return this.mouseDoubleClickCommand ?? (this.mouseDoubleClickCommand = new DelegateCommand<object>(para => this.MouseDoubleClick(para)));
            }
        }
        #endregion

        private async void Query()
        {
            var result =  await _loginUserService.GetSystemEvent(ServerIP, SearchStartTime, SearchEndTime, SearchName, CustomSystemType);
            HistoryCustomSystemException = new ObservableCollection<T1_SystemEvent>(result);
        }

        private void MouseDoubleClick(object para)
        {
            T1_SystemEvent ex = para as T1_SystemEvent;
            if (ex != null && ex.Type == (int)CustomSystemType.Alarm)
            {
               
                var itemTree =  _organizationService.ItemTreeItems.Where(p => p.T_Item.Guid == ex.T_Item_Guid).FirstOrDefault();
                //var itemTree = _organizationService.ItemTreeItems.Where(p => p.T_Item.Guid == new Guid("a0659d1e-2b1e-41ba-abe1-3d42caf678d9")).FirstOrDefault();
                if (itemTree != null)
                {
                    int height;
                    switch ((ChannelType)ex.T_Item_Type)
                    {
                        case ChannelType.WirelessScalarChannelInfo: height = 260; break;
                        case ChannelType.WirelessVibrationChannelInfo: height = 660; break;
                        default: height = 260; break;
                    }
                    HistoryEventAlarmTrendView win = new HistoryEventAlarmTrendView(itemTree.Name, height);
                    //win.ViewModel.AddData(itemTree, DateTime.Parse("10/20/2017 12:15:12"), DateTime.Parse("10/20/2017 13:15:12"));
                    win.AddData(itemTree, ex.EventTime.AddHours(-1), ex.EventTime.AddHours(1));
                    win.Show();
                }
            }
        }

    }
}
