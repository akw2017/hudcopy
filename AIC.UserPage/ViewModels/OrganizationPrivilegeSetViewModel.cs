using AIC.Core.Events;
using AIC.Core.ExCommand;
using AIC.Core.Helpers;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.Core.Servers;
using AIC.Core.UserManageModels;
using AIC.CoreType;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.Resources.Models;
using AIC.ServiceInterface;
using AIC.UserPage.Models;
using AIC.UserPage.Views;
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

namespace AIC.UserPage.ViewModels
{
    class OrganizationPrivilegeSetViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly IOrganizationService _organizationService;
        private readonly ILoginUserService _loginUserService;
        private readonly ILocalConfiguration _localConfiguration;

        public OrganizationPrivilegeSetViewModel(IDatabaseComponent databaseComponent, IEventAggregator eventAggregator, IOrganizationService organizationService, ILoginUserService loginUserService, ILocalConfiguration localConfiguration)
        {
            _databaseComponent = databaseComponent;
            _eventAggregator = eventAggregator;
            _organizationService = organizationService;
            _loginUserService = loginUserService;
            _localConfiguration = localConfiguration;

            ServerIPCategory = _databaseComponent.GetServerIPCategory();
            ServerIP = _databaseComponent.MainServerIp;

            InitPager(ServerIP);

            _view = new ListCollectionView(MyOrganizationPrivilegeLast);
            _view.GroupDescriptions.Add(new PropertyGroupDescription("Name"));//对视图进行分组           
        }

        private readonly ICollectionView _view;
        public ICollectionView MyOrganizationPrivilegeView { get { return _view; } }

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

        private List<T1_OrganizationPrivilege> T_OrganizationPrivilege;

        private List<T1_Organization> T_Organization;

        private List<MyOrganizationPrivilege> MyOrganizationPrivilege;//原始       

        private List<IGrouping<string, MyOrganizationPrivilege>> MyOrganizationPrivilegeMid;//查询后

        private List<MyOrganizationPrivilege> MyOrganizationPrivilegeLast;//分页后  

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
            if (_localConfiguration.ServerInfoList.Where(p => p.IP == ServerIP).Where(p => p.Permission.Contains("admin") || p.Permission.Contains("管理员")).Count() > 0)
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
            T_OrganizationPrivilege = _databaseComponent.GetOrganizationPrivilegeData(ip);
            T_Organization = _databaseComponent.GetOrganizationData(ip);

            MyOrganizationPrivilege = new List<MyOrganizationPrivilege>();
            var grouporganization = (from p in T_OrganizationPrivilege group p by p.Name into s select s).ToList();
            foreach (var organization in grouporganization)
            {
                MyOrganizationPrivilege myorganization = new MyOrganizationPrivilege();
                myorganization.Name = organization.FirstOrDefault().Name;
                myorganization.OrganizationTreeItems = new List<OrganizationTreeItemViewModel>();
                OrganizationProcess.GetOrganizationWithIsChecked(myorganization.OrganizationTreeItems, organization.ToList(), T_Organization);
                MyOrganizationPrivilege.Add(myorganization);
            }

            if (PageSizeList == null)
            {
                PageSizeList = new List<int>();
                PageSizeList.Add(20);
                PageSizeList.Add(50);
                PageSizeList.Add(100);
                PageSize = PageSizeList[0];
            }

            MyOrganizationPrivilegeMid = (from p in MyOrganizationPrivilege where p.Name.Contains(SearchName) || SearchName == "" group p by p.Name into s select s).ToList();
            TotalItems = MyOrganizationPrivilegeMid.Count;
            if (MyOrganizationPrivilegeLast == null)
            {
                MyOrganizationPrivilegeLast = new List<MyOrganizationPrivilege>();
            }
            else
            {
                MyOrganizationPrivilegeLast.Clear();
            }

            if (CanOperate(null) == true)
            {
                for (int i = 0; i < MyOrganizationPrivilegeMid.Count; i++)
                {
                    if (i >= 0 && i < PageSize)
                    {
                        MyOrganizationPrivilegeLast.AddRange(MyOrganizationPrivilegeMid[i]);
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
            MyOrganizationPrivilegeMid = (from p in MyOrganizationPrivilege where p.Name.Contains(SearchName) || SearchName == "" group p by p.Name into s select s).ToList();
            TotalItems = MyOrganizationPrivilegeMid.Count;
            MyOrganizationPrivilegeLast.Clear();
            for (int i = 0; i < MyOrganizationPrivilegeMid.Count; i++)
            {
                if (i >= (CurrentPage - 1) * PageSize && i < CurrentPage * PageSize)
                {
                    MyOrganizationPrivilegeLast.AddRange(MyOrganizationPrivilegeMid[i]);
                }
            }
            _view.Refresh();
        }

        private void Add(object value)
        {
            List<T1_OrganizationPrivilege> organizationPrivilege = new List<T1_OrganizationPrivilege>();
            long max = T_OrganizationPrivilege.Count;
            if (T_OrganizationPrivilege.Count > 0)
            {
                max = (from p in T_OrganizationPrivilege select p.id).Max();
            }            
            string name = "新建组织" + (max + 1).ToString();
            ShowDialog(organizationPrivilege, T_Organization, name, ModifyStatus.Add);           
        }

        private void Edit(object value)
        {
            MyOrganizationPrivilege organizationPrivilege = value as MyOrganizationPrivilege;
            if (organizationPrivilege != null)
            {
                string name = organizationPrivilege.Name;
                ShowDialog((from p in T_OrganizationPrivilege where p.Name == name select p).ToList(), T_Organization, name, ModifyStatus.Edit);
            }
        }

        private void Delete(object value)
        {
            MyOrganizationPrivilege organizationPrivilege = value as MyOrganizationPrivilege;
            if (organizationPrivilege != null)
            {
                string name = organizationPrivilege.Name;
                ShowDialog((from p in T_OrganizationPrivilege where p.Name == name select p).ToList(), T_Organization, name, ModifyStatus.Delete);
            }
        }

        //仅仅是为了兼容网页模式ShowDialog不生效
        private bool winshow = false;
        private void ShowDialog(List<T1_OrganizationPrivilege> organizationPrivilege, List<T1_Organization> organization, string name, ModifyStatus status)
        {
            if (winshow == true)
            {
                return;
            }
            try
            {
                winshow = true;
                OrganizationPrivilegeSetWin win = new OrganizationPrivilegeSetWin(organizationPrivilege, organization, name, status);
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
            }
            finally
            {
                winshow = false;
            }
        }

        private async void Win_Parachanged(IList<T1_OrganizationPrivilege> organizationPrivilege, string oldname, string newname, ModifyStatus mode)
        {
            if (organizationPrivilege == null || organizationPrivilege.Count == 0)
            {
                return;
            }
            try
            {
                Status = ViewModelStatus.Querying;
                //long max = T_OrganizationPrivilege.Count;
                //if (T_OrganizationPrivilege.Count > 0)
                //{
                //    max = (from p in T_OrganizationPrivilege select p.id).Max();
                //}
                switch (mode)
                {
                    case ModifyStatus.Add:
                        {
                            //organizationPrivilege中一定有数据，且name一致。
                            //if (T_OrganizationPrivilege.Contains(organizationPrivilege[0], EqualityHelper<T1_OrganizationPrivilege>.CreateComparer(p => p.Name)))//重名鉴定
                            if (T_OrganizationPrivilege.Where(p => p.Name == newname).Count() > 0)
                            {
#if XBAP
                                MessageBox.Show("组织名称重名，添加失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                Xceed.Wpf.Toolkit.MessageBox.Show("组织名称重名，添加失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            }
                            else
                            {
                                var tempadd = new List<T1_OrganizationPrivilege>();
                                foreach (var organization in organizationPrivilege)
                                {
                                    //organization.id = ++max;                                
                                    tempadd.Add(organization);
                                }                               
                                if (await _databaseComponent.Add<T_OrganizationPrivilege>(ServerIP, tempadd.Select(p => p as T_OrganizationPrivilege).ToList()) != null)
                                {
                                    //T_OrganizationPrivilege.AddRange(tempadd);//在DatabaseComponent添加
                                    MyOrganizationPrivilege myorganization = new MyOrganizationPrivilege();
                                    myorganization.Name = newname;
                                    myorganization.OrganizationTreeItems = new List<OrganizationTreeItemViewModel>();
                                    OrganizationProcess.GetOrganizationWithIsChecked(myorganization.OrganizationTreeItems, organizationPrivilege, T_Organization);
                                    MyOrganizationPrivilege.Add(myorganization);
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
                            //if (T_OrganizationPrivilege.Contains(organizationPrivilege[0], EqualityHelper<T1_OrganizationPrivilege>.CreateComparer(p => p.Name)))//重名鉴定
                            if (T_OrganizationPrivilege.Where(p => p.Name == newname).Count() > 0)
                            {
                                if (oldname != newname)
                                {
#if XBAP
                                    MessageBox.Show("组织名称重名，修改失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                    Xceed.Wpf.Toolkit.MessageBox.Show("组织名称重名，修改失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                                    return;
                                }
                            }

                            var tempadd = new List<T1_OrganizationPrivilege>();
                            var tempedit = new List<T1_OrganizationPrivilege>();
                            var tempdelete = new List<T1_OrganizationPrivilege>();
                            foreach (var organization in organizationPrivilege)
                            {
                                if (organization.Guid == new Guid())//删除
                                {
                                    var old_organization = (from p in T_OrganizationPrivilege where p.id == organization.id select p).FirstOrDefault();
                                    if (old_organization != null)
                                    {
                                        tempdelete.Add(old_organization);
                                    }
                                }
                                else if (organization.id == 0)//新增
                                {
                                    //organization.id = ++max;                              
                                    tempadd.Add(organization);
                                }
                                else if (oldname != newname)//修改
                                {
                                    var old_organization = (from p in T_OrganizationPrivilege where p.id == organization.id select p).FirstOrDefault();
                                    if (old_organization != null)
                                    {
                                        old_organization.Name = organization.Name;
                                        tempedit.Add(old_organization);
                                    }
                                }
                            }

                            Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
                            addDic.Add("T_OrganizationPrivilege", tempadd.Select(p => p as object).ToList());
                            Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                            editDic.Add("T_OrganizationPrivilege", new Tuple<ICollection<string>, ICollection<object>>(null, tempedit.Select(p => p as object).ToList()));
                            Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();
                            deleteDic.Add("T_OrganizationPrivilege", new Tuple<string, ICollection<object>>("id", tempdelete.Select(p => p.id as object).ToList()));
                            if (await _databaseComponent.Complex(ServerIP, addDic, editDic, deleteDic))
                            {
                                //T_OrganizationPrivilege.AddRange(tempadd);//在DatabaseComponent添加
                                //tempdelete.ForEach(p => T_OrganizationPrivilege.Remove(p));//在DatabaseComponent删除
                                MyOrganizationPrivilege myorganization = (from p in MyOrganizationPrivilege where p.Name == oldname select p).FirstOrDefault();
                                if (myorganization != null)
                                {
                                    var neworganizationPrivilege = (from p in T_OrganizationPrivilege where p.Name == newname select p).ToList();
                                    myorganization.Name = newname;
                                    myorganization.OrganizationTreeItems = new List<OrganizationTreeItemViewModel>();
                                    OrganizationProcess.GetOrganizationWithIsChecked(myorganization.OrganizationTreeItems, neworganizationPrivilege, T_Organization);
                                }
                            }
                            else
                            {
                                tempedit.ForEach(p => p.Name = oldname);//失败回滚
#if XBAP
                                MessageBox.Show("服务器错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            }

                            //if (await _databaseComponent.Add<T_OrganizationPrivilege>(ServerIP, tempadd.Select(p => p as T_OrganizationPrivilege).ToList()) == true)
                            //{
                            //    T_OrganizationPrivilege.AddRange(tempadd);
                            //}
                            //if (await _databaseComponent.Modify<T_OrganizationPrivilege>(ServerIP, new string[] { "Name" }, tempedit.Select(p => p as T_OrganizationPrivilege).ToList()) == true)
                            //{
                            //    //tempedit.ForEach(p => p.Name = organizationPrivilege[0].Name);
                            //}
                            //else
                            //{
                            //    tempedit.ForEach(p => p.Name = oldname);//失败回滚
                            //}
                            //if (await _databaseComponent.Delete<T_OrganizationPrivilege>(ServerIP, tempdelete.Select(p => p.id as object).ToList()) == true)
                            //{
                            //    tempdelete.ForEach(p => T_OrganizationPrivilege.Remove(p));
                            //}                          

                            break;
                        }
                    case ModifyStatus.Delete:
                        {
                            var tempdelete = new List<T1_OrganizationPrivilege>();
                            for (int i = T_OrganizationPrivilege.Count - 1; i >= 0; i--)
                            {
                                if (T_OrganizationPrivilege[i].Name == newname)
                                {
                                    tempdelete.Add(T_OrganizationPrivilege[i]);
                                }
                            }
                           
                            if (await _databaseComponent.Delete<T_OrganizationPrivilege>(ServerIP, tempdelete.Select(p => p.id as object).ToList()) == true)
                            {
                                //tempdelete.ForEach(p => T_OrganizationPrivilege.Remove(p));//在DatabaseComponent删除
                                MyOrganizationPrivilege myorganization = (from p in MyOrganizationPrivilege where p.Name == oldname select p).FirstOrDefault();
                                if (myorganization != null)
                                {
                                    MyOrganizationPrivilege.Remove(myorganization);
                                }
                            }
                            else
                            {
#if XBAP
                                MessageBox.Show("服务器错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                Xceed.Wpf.Toolkit.MessageBox.Show("服务器错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            }

                            break;//还未进行重名鉴定
                        }
                    default: return;
                }

                //_localConfiguration.WriteServerInfo(ServerInfoList);   
                //MyOrganizationPrivilege.Clear();
                //var grouporganization = (from p in T_OrganizationPrivilege group p by p.Name into s select s).ToList();
                //foreach (var organization in grouporganization)
                //{
                //    MyOrganizationPrivilege myorganization = new MyOrganizationPrivilege();
                //    myorganization.Name = organization.FirstOrDefault().Name;
                //    myorganization.OrganizationTreeItems = new List<OrganizationTreeItemViewModel>();
                //    OrganizationProcess.GetOrganizationWithIsChecked(myorganization.OrganizationTreeItems, organization.ToList(), T_Organization);
                //    MyOrganizationPrivilege.Add(myorganization);
                //}

                _loginUserService.AddOperateRecord(ServerIP, UserOperateType.OrganizationManage);

                MyOrganizationPrivilegeMid = (from p in MyOrganizationPrivilege where p.Name.Contains(SearchName) || SearchName == "" group p by p.Name into s select s).ToList();
                TotalItems = MyOrganizationPrivilegeMid.Count;
                MyOrganizationPrivilegeLast.Clear();
                for (int i = 0; i < MyOrganizationPrivilegeMid.Count; i++)
                {
                    if (i >= (CurrentPage - 1) * PageSize && i < CurrentPage * PageSize)
                    {
                        MyOrganizationPrivilegeLast.AddRange(MyOrganizationPrivilegeMid[i]);
                    }
                }
                _view.Refresh();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("修改组织机构", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private void CurrentPageChanged(object value)
        {
            if (MyOrganizationPrivilege == null)
            {
                return;
            }

            RoutedPropertyChangedEventArgs<PageNavigationEventArgs> args = ((ExCommandParameter)value).EventArgs as RoutedPropertyChangedEventArgs<PageNavigationEventArgs>;
            int pageSize = args.NewValue.PageSize;
            int currentPage = args.NewValue.CurrectPage;
            MyOrganizationPrivilegeLast.Clear();
            for (int i = 0; i < MyOrganizationPrivilegeMid.Count; i++)
            {
                if (i >= (currentPage - 1) * pageSize && i < currentPage * pageSize)
                {
                    MyOrganizationPrivilegeLast.AddRange(MyOrganizationPrivilegeMid[i]);
                }
            }
            _view.Refresh();
        }
    }
}
