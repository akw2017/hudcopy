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

namespace AIC.HistoryDataPage.ViewModels
{
    class HistoryEventListViewModel : BindableBase
    {        
        private readonly ILoginUserService _loginUserService;
        private readonly IOrganizationService _organizationService;
        public HistoryEventListViewModel(ILoginUserService loginUserService, IOrganizationService organizationService)
        {
            _loginUserService = loginUserService;
            _organizationService = organizationService;

            customSystemException = _loginUserService.CustomSystemException;

            SearchStartTime = DateTime.Now.AddDays(-1);
            SearchEndTime = DateTime.Now;

            _view = new ListCollectionView(customSystemException);
            _view.Filter = (object item) =>
            {
                var p = customSystemException;
                if (IsRealTime == true) return true;
                var itemPl = (CustomSystemException)item;
                if (itemPl == null) return false;
                if (SearchName == null || SearchName == "")
                {                   
                    if (itemPl.Type == (int)CustomSystemType && itemPl.EventTime < SearchEndTime && itemPl.EventTime > SearchStartTime)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (itemPl.Remarks.Contains(SearchName) && itemPl.Type == (int)CustomSystemType && itemPl.EventTime < SearchEndTime && itemPl.EventTime > SearchStartTime)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            };
        }

        #region 字段与属性
        private readonly ICollectionView _view;
        public ICollectionView CustomSystemExceptionView { get { return _view; } }

        private ObservableCollection<CustomSystemException> customSystemException;
        public IEnumerable<CustomSystemException> CustomSystemException { get { return customSystemException; } }

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
                    _view.Refresh();
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

        private void Query()
        {
            _view.Refresh();
        }

        private void MouseDoubleClick(object para)
        {
            CustomSystemException ex = para as CustomSystemException;
            if (ex != null && ex.Type == (int)CustomSystemType.Alarm)
            {
               
                var itemTree =  _organizationService.GetItems().Where(p => p.T_Item.Guid == ex.T_Item_Guid).FirstOrDefault();
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
