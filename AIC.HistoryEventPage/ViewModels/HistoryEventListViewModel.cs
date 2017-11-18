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

namespace AIC.HistoryEventPage.ViewModels
{
    class HistoryEventListViewModel : BindableBase
    {        
        private readonly ILoginUserService _loginUserService;
        public HistoryEventListViewModel(ILoginUserService loginUserService)
        {
            _loginUserService = loginUserService;

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

        private DelegateCommand queryCommand;
        public DelegateCommand QueryCommand
        {
            get
            {
                return this.queryCommand ?? (this.queryCommand = new DelegateCommand(() => this.Query()));
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

        private void Query()
        {
            _view.Refresh();
        }

     
    }
}
