using AIC.Core.Events;
using AIC.Core.ExCommand;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.Servers;
using AIC.HomePage.Views;
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

namespace AIC.HomePage.ViewModels
{
    class ServerSetViewModel : BindableBase
    {        
        private readonly IEventAggregator _eventAggregator;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly ILoginUserService _loginUserService;
        public ServerSetViewModel(ILocalConfiguration localConfiguration, IEventAggregator eventAggregator, ILoginUserService loginUserService)
        {
            _localConfiguration = localConfiguration;
            _loginUserService = loginUserService;
            _eventAggregator = eventAggregator;         
               
            ServerInfoList = _localConfiguration.ServerInfoList;
            //_view = new ListCollectionView(ServerInfo);
            //_view.Filter = (object item) =>
            //{
            //    if (SearchName == null || SearchName == "") return true;
            //    var itemPl = (ServerInfo)item;
            //    if (itemPl.Name.Contains(SearchName))
            //    {
            //        return true;
            //    }
            //    return false;
            //};
            InitPager();
        }        

        //private readonly ICollectionView _view;
        //public ICollectionView ServerInfoView { get { return _view; } }

        private string searchName;
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

        private ObservableCollection<ServerInfo> ServerInfoList;      

        private ObservableCollection<ServerInfo> ServerInfoView;       

        private ObservableCollection<ServerInfo> serverInfoShow;
        public ObservableCollection<ServerInfo> ServerInfoShow
        {
            get
            {
                return serverInfoShow;
            }
            set
            {
                serverInfoShow = value;
                OnPropertyChanged("ServerInfoShow");
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

        private DelegateCommand<object> addCommand;
        public DelegateCommand<object> AddCommand
        {
            get
            {
                return this.addCommand ?? (this.addCommand = new DelegateCommand<object>(value => this.Add(value), value => CanOperate(value)));
            }
        }

        private DelegateCommand<object> editCommand;
        public DelegateCommand<object> EditCommand
        {
            get
            {
                return this.editCommand ?? (this.editCommand = new DelegateCommand<object>(value => this.Edit(value), value => CanOperate(value)));
            }
        }

        private DelegateCommand<object> deleteCommand;
        public DelegateCommand<object> DeleteCommand
        {
            get
            {
                return this.deleteCommand ?? (this.deleteCommand = new DelegateCommand<object>(value => this.Delete(value), value => CanOperate(value)));
            }
        }

        private DelegateCommand<object> defaultCommand;
        public DelegateCommand<object> DefaultCommand
        {
            get
            {
                return this.defaultCommand ?? (this.defaultCommand = new DelegateCommand<object>(value => this.Default(value), value => CanOperate(value)));
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


        private bool CanOperate(object para)
        {
            if (_loginUserService.LoginInfo.ServerInfo.Permission.Contains("admin") || _loginUserService.LoginInfo.ServerInfo.Permission.Contains("管理员"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitPager()
        {
            ServerInfoView = ServerInfoList;
            PageSizeList = new List<int>();
            PageSizeList.Add(20);
            PageSizeList.Add(50);
            PageSizeList.Add(100);
            PageSize = PageSizeList[0];
            TotalItems = ServerInfoView.Count;
            ServerInfoShow = new ObservableCollection<ServerInfo>(ServerInfoView.Skip(0).Take(PageSize));
        }

        private void Query()
        {
            //_view.Refresh();
            ServerInfoView = new ObservableCollection<ServerInfo>(from p in ServerInfoList where p.Name.Contains(SearchName) || SearchName == "" select p);
            TotalItems = ServerInfoView.Count;
            ServerInfoShow = new ObservableCollection<ServerInfo>(ServerInfoView.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
        }

        private void Add(object value)
        {
            ServerInfo info = new ServerInfo();           
            ServerSetWin win = new ServerSetWin(info, ModifyStatus.Add);
            win.Parachanged += Win_Parachanged;
            win.ShowDialog();           
        }      

        private void Edit(object value)
        {
            ServerInfo info = value as ServerInfo;
            if (info != null)
            {
                ServerSetWin win = new ServerSetWin(info, ModifyStatus.Edit);
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
            }
        }

        private void Delete(object value)
        {
            ServerInfo info = value as ServerInfo;
            if (info != null)
            {
                ServerSetWin win = new ServerSetWin(info, ModifyStatus.Delete);
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
            }
        }

        private void Default(object value)
        {
            ServerInfo info = value as ServerInfo;
            if (info != null)
            {
                ServerSetWin win = new ServerSetWin(info, ModifyStatus.Default);
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
            }
        }
        private void Win_Parachanged(ServerInfo info, ModifyStatus mode)
        {
            try
            {
                switch (mode)
                {
                    case ModifyStatus.Add:
                        {
                            if (!ServerInfoList.Contains(info, EqualityHelper<ServerInfo>.CreateComparer(p => p.IP)))//重名鉴定
                            //if (ServerInfoList.Where(p => p.IP == info.IP).Count() == 0)
                            {
                                var max = (from p in ServerInfoList select p.ID).Max();
                                info.ID = max + 1;
                                info.LoginResult = false;
                                info.Permission = "无权限";
                                ServerInfoList.Add(info);
                            }
                            else
                            {
#if XBAP
                                MessageBox.Show("服务器IP地址重名，添加失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                Xceed.Wpf.Toolkit.MessageBox.Show("服务器IP地址重名，添加失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            }
                            break;
                        }
                    case ModifyStatus.Edit:
                        {
                            for (int i = 0; i < ServerInfoList.Count; i++)
                            {
                                if (ServerInfoList[i].ID == info.ID)//找到修改项
                                {
                                    ServerInfoList[i] = info;
                                    break;
                                }
                            }
                            break;
                        }
                    case ModifyStatus.Delete:
                        {
                            int id = 0;
                            for (int i = 0; i < ServerInfoList.Count; i++)
                            {
                                if (ServerInfoList[i].ID == info.ID)//找到修改项
                                {
                                    id = ServerInfoList[i].ID;
                                    ServerInfoList.RemoveAt(i);
                                    break;
                                }
                            }
                            for (int i = 0; i < ServerInfoList.Count; i++)
                            {
                                if (ServerInfoList[i].ID > id)//修正ID
                                {
                                    ServerInfoList[i].ID--;
                                }
                            }
                            break;//还未进行重名鉴定
                        }
                    case ModifyStatus.Default:
                        {
                            for (int i = 0; i < ServerInfoList.Count; i++)
                            {
                                if (ServerInfoList[i].ID == info.ID)//找到修改项
                                {
                                    ServerInfoList[i] = info;
                                    break;
                                }
                            }
                            break;
                        }
                    default: return;
                }

                _localConfiguration.WriteServerInfo(ServerInfoList);

                _eventAggregator.GetEvent<ServerChangedEvent>().Publish(ServerInfoList);
                //_view.Refresh();
                ServerInfoView = new ObservableCollection<ServerInfo>(from p in ServerInfoList where p.Name.Contains(SearchName) || SearchName == "" select p);
                TotalItems = ServerInfoView.Count;
                ServerInfoShow = new ObservableCollection<ServerInfo>(ServerInfoView.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("服务器设置", ex));
            }
        }

        private void CurrentPageChanged(object value)
        {
            if (ServerInfoList == null)
            {
                return;
            }
            //PageNavigation pager = (PageNavigation)value;
            RoutedPropertyChangedEventArgs<PageNavigationEventArgs> args = ((ExCommandParameter)value).EventArgs as RoutedPropertyChangedEventArgs<PageNavigationEventArgs>;
            int pageSize = args.NewValue.PageSize;
            int currentPage = args.NewValue.CurrectPage;
            ServerInfoShow = new ObservableCollection<ServerInfo>(ServerInfoView.Skip((currentPage - 1) * pageSize).Take(pageSize));
        }
    }
}
