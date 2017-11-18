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
using AIC.Core;

namespace AIC.UserPage.ViewModels
{
    class UserSetViewModel : BindableBase
    {        
        private readonly IDatabaseComponent _databaseComponent;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoginUserService _loginUserService;
        public UserSetViewModel(IDatabaseComponent databaseComponent, IEventAggregator eventAggregator, ILoginUserService loginUserService)
        {
            _databaseComponent = databaseComponent;
            _eventAggregator = eventAggregator;
            _loginUserService = loginUserService;

            //先从数据库里取数据

            T_User = _databaseComponent.GetUserData();
            T_Role = _databaseComponent.GetRoleData();
            T_Menu = _databaseComponent.GetMenuData();
            T_OrganizationPrivilege = _databaseComponent.GetOrganizationPrivilegeData();
            T_User.ForEach(p => 
            {
                p.T_Role_Name = (from role in T_Role where role.Guid == p.T_Role_Guid select role.Name).FirstOrDefault();
                p.T_Menu_Name = (from menu in T_Menu where menu.Guid == p.T_Menu_Guid select menu.Name).FirstOrDefault();
                p.T_OrganizationPrivilege_Name = (from organizationPrivilege in T_OrganizationPrivilege where organizationPrivilege.Guid == p.T_OrganizationPrivilege_Guid select organizationPrivilege.Name).FirstOrDefault();
            });

            InitPager();
            _view = new ListCollectionView(T_UserLast);
            _view.GroupDescriptions.Add(new PropertyGroupDescription("Name"));//对视图进行分组
        }

        private readonly ICollectionView _view;
        public ICollectionView T_UserView { get { return _view; } }

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

        private List<T1_Role> T_Role;

        private List<T1_Menu> T_Menu;

        private List<T1_OrganizationPrivilege> T_OrganizationPrivilege;

        private List<T1_User> T_User;//原始       

        private List<T1_User> T_UserMid;//查询后

        private List<T1_User> T_UserLast;//分页后  

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
                return this.addCommand ?? (this.addCommand = new DelegateCommand<object>(value => this.Add(value)));
            }
        }

        private DelegateCommand<object> editCommand;
        public DelegateCommand<object> EditCommand
        {
            get
            {
                return this.editCommand ?? (this.editCommand = new DelegateCommand<object>(value => this.Edit(value)));
            }
        }

        private DelegateCommand<object> deleteCommand;
        public DelegateCommand<object> DeleteCommand
        {
            get
            {
                return this.deleteCommand ?? (this.deleteCommand = new DelegateCommand<object>(value => this.Delete(value)));
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

        private void InitPager()
        {
            PageSizeList = new List<int>();
            PageSizeList.Add(20);
            PageSizeList.Add(50);
            PageSizeList.Add(100);
            PageSize = PageSizeList[0];
            T_UserMid = (from p in T_User where p.Name.Contains(SearchName) || SearchName == "" select p).ToList();
            TotalItems = T_UserMid.Count;
            T_UserLast = new List<T1_User>();
            for (int i = 0; i < T_UserMid.Count; i++)
            {
                if (i >= 0 && i < PageSize)
                {
                    T_UserLast.Add(T_UserMid[i]);
                }
            }
        }

        private void Query()
        {
            T_UserMid = (from p in T_User where p.Name.Contains(SearchName) || SearchName == "" select p).ToList();
            TotalItems = T_UserMid.Count;
            T_UserLast.Clear();
            for (int i = 0; i < T_UserMid.Count; i++)
            {
                if (i >= (CurrentPage - 1) * PageSize && i < CurrentPage * PageSize)
                {
                    T_UserLast.Add(T_UserMid[i]);
                }
            }
            _view.Refresh();
        }

        private void Add(object value)
        {
            T1_User user = new T1_User();
            ShowDialog(user, T_Role, T_Menu, T_OrganizationPrivilege,  ModifyStatus.Add);            
        }      

        private void Edit(object value)
        {
            T1_User user = value as T1_User;
            if (user != null)
            {
                ShowDialog(user, T_Role, T_Menu, T_OrganizationPrivilege, ModifyStatus.Edit);               
            }
        }

        private void Delete(object value)
        {
            T1_User user = value as T1_User;
            if (user != null)
            {
                ShowDialog(user, T_Role, T_Menu, T_OrganizationPrivilege, ModifyStatus.Delete);                
            }
        }

        //仅仅是为了兼容网页模式ShowDialog不生效
        private bool winshow = false;
        private void ShowDialog(T1_User user, List<T1_Role> role, List<T1_Menu> menu, List<T1_OrganizationPrivilege> organization, ModifyStatus status)
        {
            if (winshow == true)
            {
                return;
            }
            
            try
            {
                winshow = true;
                UserSetWin win = new UserSetWin(user, role, menu, organization, status);
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
            }           
            finally
            {
                winshow = false;
            }
        }

        private async void Win_Parachanged(T1_User user, string oldname, string oldpwd, ModifyStatus mode)
        {
            //long max = T_User.Count;
            //if (T_User.Count > 0)
            //{
            //    max = (from p in T_User select p.id).Max();
            //}
            try
            {
                Status = ViewModelStatus.Querying;
                switch (mode)
                {
                    case ModifyStatus.Add:
                        {
                            if (T_User.Contains(user, EqualityHelper<T1_User>.CreateComparer(p => p.Name)))//重名鉴定
                            {
#if XBAP
                                MessageBox.Show("用户名称重名，添加失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                Xceed.Wpf.Toolkit.MessageBox.Show("用户名称重名，添加失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                                return;
                            }
                            else
                            {
                                //user.id = ++max;
                                user.T_Role_Name = (from role in T_Role where role.Guid == user.T_Role_Guid select role.Name).FirstOrDefault();
                                user.T_Menu_Name = (from menu in T_Menu where menu.Guid == user.T_Menu_Guid select menu.Name).FirstOrDefault();
                                user.T_OrganizationPrivilege_Name = (from organization in T_OrganizationPrivilege where organization.Guid == user.T_OrganizationPrivilege_Guid select organization.Name).FirstOrDefault();
                                user.Password = MyEncrypt.EncryptDES(user.Password);
                                if (await _databaseComponent.Add<T_User>(_databaseComponent.MainServerIp, user) == true)
                                {
                                    //T_User.Add(user);//在DatabaseComponent添加
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
                            if (T_User.Contains(user, EqualityHelper<T1_User>.CreateComparer(p => p.Name)))//重名鉴定
                            {
                                if (user.Name != oldname)
                                {
#if XBAP
                                    MessageBox.Show("用户名称重名，修改失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                    Xceed.Wpf.Toolkit.MessageBox.Show("用户名称重名，修改失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                                    return;
                                }
                            }

                            for (int i = 0; i < T_User.Count; i++)
                            {
                                if (T_User[i].id == user.id)//找到修改项
                                {
                                    user.T_Role_Name = (from role in T_Role where role.Guid == user.T_Role_Guid select role.Name).FirstOrDefault();
                                    user.T_Menu_Name = (from menu in T_Menu where menu.Guid == user.T_Menu_Guid select menu.Name).FirstOrDefault();
                                    user.T_OrganizationPrivilege_Name = (from organization in T_OrganizationPrivilege where organization.Guid == user.T_OrganizationPrivilege_Guid select organization.Name).FirstOrDefault();
                                    if (user.Password != oldpwd)
                                    {
                                        user.Password = MyEncrypt.EncryptDES(user.Password);
                                    }

                                    if (await _databaseComponent.Modify<T_User>(_databaseComponent.MainServerIp, null, user) == true)
                                    {
                                        T_User[i] = user;
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
                            for (int i = 0; i < T_User.Count; i++)
                            {
                                if (T_User[i].id == user.id)//找到修改项
                                {
                                    user.T_Role_Name = (from role in T_Role where role.Guid == user.T_Role_Guid select role.Name).FirstOrDefault();
                                    user.T_Menu_Name = (from menu in T_Menu where menu.Guid == user.T_Menu_Guid select menu.Name).FirstOrDefault();
                                    user.T_OrganizationPrivilege_Name = (from organization in T_OrganizationPrivilege where organization.Guid == user.T_OrganizationPrivilege_Guid select organization.Name).FirstOrDefault();
                                    if (await _databaseComponent.Delete<T_User>(_databaseComponent.MainServerIp, user.id) == true)
                                    {
                                        //T_User.RemoveAt(i);//在DatabaseComponent删除
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

                _loginUserService.AddOperateRecord(OperateType.UserManage);

                T_UserMid = (from p in T_User where p.Name.Contains(SearchName) || SearchName == "" select p).ToList();
                TotalItems = T_UserMid.Count;
                T_UserLast.Clear();
                for (int i = 0; i < T_UserMid.Count; i++)
                {
                    if (i >= (CurrentPage - 1) * PageSize && i < CurrentPage * PageSize)
                    {
                        T_UserLast.Add(T_UserMid[i]);
                    }
                }
                _view.Refresh();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("修改用户", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private void CurrentPageChanged(object value)
        {
            if (T_User == null)
            {
                return;
            }
           
            RoutedPropertyChangedEventArgs<PageNavigationEventArgs> args = ((ExCommandParameter)value).EventArgs as RoutedPropertyChangedEventArgs<PageNavigationEventArgs>;
            int pageSize = args.NewValue.PageSize;
            int currentPage = args.NewValue.CurrectPage;
            T_UserLast.Clear();
            for (int i = 0; i < T_UserMid.Count; i++)
            {
                if (i >= (currentPage - 1) * pageSize && i < currentPage * pageSize)
                {
                    T_UserLast.Add(T_UserMid[i]);
                }
            }
            _view.Refresh();
        }
    }
}
