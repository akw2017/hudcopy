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
using AIC.M9600.Common.MasterDB.Generated;
using AIC.CoreType;

namespace AIC.UserPage.ViewModels
{
    class RoleSetViewModel : BindableBase
    {        
        private readonly IDatabaseComponent _databaseComponent;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoginUserService _loginUserService;
        public RoleSetViewModel(IDatabaseComponent databaseComponent, IEventAggregator eventAggregator, ILoginUserService loginUserService)
        {
            _databaseComponent = databaseComponent;
            _eventAggregator = eventAggregator;
            _loginUserService = loginUserService;

            ServerIPCategory = _databaseComponent.GetServerIPCategory();
            ServerIP = _databaseComponent.GetMainServerIp();
           
            InitPager(ServerIP);
            _view = new ListCollectionView(T_RoleLast);
            //_view.Filter = (object item) =>
            //{
            //    return true;
            //};
            _view.GroupDescriptions.Add(new PropertyGroupDescription("Name"));//对视图进行分组
        }

        private readonly ICollectionView _view;
        public ICollectionView T_RoleView { get { return _view; } }

        private string _serverIP;
        public string ServerIP
        {
            get { return _serverIP; }
            set
            {
                if (_serverIP != value)
                {
                    _serverIP = value;
                    InitPager(_serverIP);
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

        private List<T1_Role> T_Role;//原始       

        private List<IGrouping<string, T1_Role>> T_RoleMid;//查询后

        private List<T1_Role> T_RoleLast;//分页后  

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

        private DelegateCommand<object> queryCommand;
        public DelegateCommand<object> QueryCommand
        {
            get
            {
                return this.queryCommand ?? (this.queryCommand = new DelegateCommand<object>(value => this.Query(value), value => CanOperate(value)));
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
            if (_loginUserService.LoginInfo.ServerInfoList.Where(p => p.IP == ServerIP).Where(p => p.Permission.Contains("admin") || p.Permission.Contains("管理员")).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitPager(string ip)
        {
            T_Role = _databaseComponent.GetRoleData(ip);

            if (PageSizeList == null)
            {
                PageSizeList = new List<int>();
                PageSizeList.Add(20);
                PageSizeList.Add(50);
                PageSizeList.Add(100);
                PageSize = PageSizeList[0];
            }

            T_RoleMid = (from p in T_Role where p.Name.Contains(SearchName) || SearchName == "" orderby p.Sort_No group p by p.Name into s select s).ToList();
            TotalItems = T_RoleMid.Count;
            if (T_RoleLast == null)
            {
                T_RoleLast = new List<T1_Role>();
            }
            else
            {
                T_RoleLast.Clear();
            }

            if (CanOperate(null) == true)
            {
                for (int i = 0; i < T_RoleMid.Count; i++)
                {
                    if (i >= 0 && i < PageSize)
                    {
                        T_RoleLast.AddRange(T_RoleMid[i]);
                    }
                }
            }

            if (_view != null)
            {
                _view.Refresh();
            }

            QueryCommand.RaiseCanExecuteChanged();
            AddCommand.RaiseCanExecuteChanged();
            EditCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
        }

        private void Query(object value)
        {
            T_RoleMid = (from p in T_Role where p.Name.Contains(SearchName) || SearchName == "" group p by p.Name into s select s).ToList();
            TotalItems = T_RoleMid.Count;
            T_RoleLast.Clear();
            for (int i = 0; i < T_RoleMid.Count; i++)
            {
                if (i >= (CurrentPage - 1) * PageSize && i < CurrentPage * PageSize)
                {
                    T_RoleLast.AddRange(T_RoleMid[i]);
                }
            }
            _view.Refresh();
        }

        private void Add(object value)
        {
            T1_Role role = new T1_Role();
            ShowDialog(role, ModifyStatus.Add);
        }      

        private void Edit(object value)
        {
            T1_Role role = value as T1_Role;
            if (role != null)
            {
                ShowDialog(role, ModifyStatus.Edit);                
            }
        }

        private void Delete(object value)
        {
            T1_Role role = value as T1_Role;
            if (role != null)
            {
                ShowDialog(role, ModifyStatus.Delete);               
            }
        }

        //仅仅是为了兼容网页模式ShowDialog不生效
        private bool winshow = false;
        private void ShowDialog(T1_Role role, ModifyStatus status)
        {
            if (winshow == true)
            {
                return;
            }
            try
            {
                winshow = true;
                RoleSetWin win = new RoleSetWin(role, status);
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
                winshow = false;
            }
            finally
            {
                winshow = false;
            }
        }

        private async void Win_Parachanged(T1_Role role, string oldname, ModifyStatus mode)
        {
            //long max = T_Role.Count;
            //if (T_Role.Count > 0)
            //{
            //    max = (from p in T_Role select p.id).Max();
            //}
            try
            {
                Status = ViewModelStatus.Querying;
                switch (mode)
                {
                    case ModifyStatus.Add:
                        {
                            if (T_Role.Contains(role, EqualityHelper<T1_Role>.CreateComparer(p => p.Name)))//重名鉴定
                            {
#if XBAP
                                MessageBox.Show("角色名称重名，添加失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                Xceed.Wpf.Toolkit.MessageBox.Show("角色名称重名，添加失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            }
                            else
                            {
                                //role.id = max + 1;                                
                                if (await _databaseComponent.Add<T_Role>(ServerIP, role) == true)
                                {
                                    //T_Role.Add(role);//在DatabaseComponent添加
                                }
                                else
                                {
#if XBAP
                                    MessageBox.Show("服务器错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                    Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                                }                               
                            }
                            break;
                        }
                    case ModifyStatus.Edit:
                        {
                            if (T_Role.Contains(role, EqualityHelper<T1_Role>.CreateComparer(p => p.Name)))//重名鉴定
                            {
                                if (role.Name != oldname)
                                {
#if XBAP
                                    MessageBox.Show("角色名称重名，修改失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                    Xceed.Wpf.Toolkit.MessageBox.Show("角色名称重名，修改失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                                    return;
                                }
                            }
                            for (int i = 0; i < T_Role.Count; i++)
                            {
                                if (T_Role[i].id == role.id)//找到修改项
                                {                                   
                                    if (await _databaseComponent.Modify<T_Role>(ServerIP, null, role) == true)
                                    {
                                        T_Role[i] = role;
                                    }
                                    else
                                    {
#if XBAP
                                        MessageBox.Show("服务器错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                        Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                                    }                                   
                                    break;
                                }
                            }
                            break;
                        }
                    case ModifyStatus.Delete:
                        {
                            for (int i = 0; i < T_Role.Count; i++)
                            {
                                if (T_Role[i].id == role.id)//找到修改项
                                {                                   
                                    if (await _databaseComponent.Delete<T_Role>(ServerIP, role.id) == true)
                                    {
                                        //T_Role.RemoveAt(i);//在DatabaseComponent删除
                                    }
                                    else
                                    {
#if XBAP
                                        MessageBox.Show("服务器错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                        Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                                    }                                    
                                    break;
                                }
                            }
                            break;//还未进行重名鉴定
                        }
                    default: return;
                }

                _loginUserService.AddOperateRecord(OperateType.RoleManage);

                T_RoleMid = (from p in T_Role where p.Name.Contains(SearchName) || SearchName == "" group p by p.Name into s select s).ToList();
                TotalItems = T_RoleMid.Count;
                T_RoleLast.Clear();
                for (int i = 0; i < T_RoleMid.Count; i++)
                {
                    if (i >= (CurrentPage - 1) * PageSize && i < CurrentPage * PageSize)
                    {
                        T_RoleLast.AddRange(T_RoleMid[i]);
                    }
                }
                _view.Refresh();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("修改角色", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private void CurrentPageChanged(object value)
        {
            if (T_Role == null)
            {
                return;
            }
           
            RoutedPropertyChangedEventArgs<PageNavigationEventArgs> args = ((ExCommandParameter)value).EventArgs as RoutedPropertyChangedEventArgs<PageNavigationEventArgs>;
            int pageSize = args.NewValue.PageSize;
            int currentPage = args.NewValue.CurrectPage;
            T_RoleLast.Clear();
            for (int i = 0; i < T_RoleMid.Count; i++)
            {
                if (i >= (currentPage - 1) * pageSize && i < currentPage * pageSize)
                {
                    T_RoleLast.AddRange(T_RoleMid[i]);
                }
            }
            _view.Refresh();
        }
    }
}
