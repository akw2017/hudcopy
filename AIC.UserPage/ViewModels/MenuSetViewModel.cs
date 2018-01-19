using AIC.Core.Events;
using AIC.Core.ExCommand;
using AIC.Core.Helpers;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.Servers;
using AIC.Core.UserManageModels;
using AIC.CoreType;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.Resources.Models;
using AIC.ServiceInterface;
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
    class MenuSetViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDatabaseComponent _databaseComponent;
        private readonly ILoginUserService _loginUserService;
        public MenuSetViewModel(IDatabaseComponent databaseComponent, IEventAggregator eventAggregator, ILoginUserService loginUserService)
        {
            _databaseComponent = databaseComponent;
            _eventAggregator = eventAggregator;
            _loginUserService = loginUserService;

            ServerIPCategory = _databaseComponent.GetServerIPCategory();
            ServerIP = _databaseComponent.MainServerIp;
           
            InitPager(ServerIP);

            _view = new ListCollectionView(T_MenuLast);
            _view.Filter = (object item) =>
            {
                return true;
                //if (SearchName == null || SearchName == "") return true;
                //var itemPl = (T_Menu)item;
                //if (itemPl.Name.Contains(SearchName))
                //{
                //    return true;
                //}
                //return false;
            };
            _view.GroupDescriptions.Add(new PropertyGroupDescription("Name"));//对视图进行分组

            
        }

        private readonly ICollectionView _view;
        public ICollectionView T_MenuView { get { return _view; } }

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

        private List<T1_Menu> T_Menu;//原始

        private List<IGrouping<string,T1_Menu>> T_MenuMid;//查询后

        private List<T1_Menu> T_MenuLast;//分页后

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
                return this.currentPageChangedComamnd ?? (this.currentPageChangedComamnd = new DelegateCommand<object>(value => this.CurrentPageChanged(value), value => CanOperate(value)));
            }
        }
       
        private bool CanOperate(object para)
        {
            if( _loginUserService.LoginInfo.ServerInfoList.Where(p => p.IP == ServerIP).Where(p => p.Permission.Contains("admin") || p.Permission.Contains("管理员")).Count() > 0)
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
            T_Menu = _databaseComponent.GetMenuData(ip);

            if (PageSizeList == null)
            {
                PageSizeList = new List<int>();
                PageSizeList.Add(20);
                PageSizeList.Add(50);
                PageSizeList.Add(100);
                PageSize = PageSizeList[0];
            }

            T_MenuMid = (from p in T_Menu where p.Name.Contains(SearchName) || SearchName == "" group p by p.Name into s select s).ToList();
            TotalItems = T_MenuMid.Count;
            if (T_MenuLast == null)
            {
                T_MenuLast = new List<T1_Menu>();
            }
            else
            {
                T_MenuLast.Clear();
            }

            if (CanOperate(null) == true)
            {
                for (int i = 0; i < T_MenuMid.Count; i++)
                {
                    if (i >= 0 && i < PageSize)
                    {
                        T_MenuLast.AddRange(T_MenuMid[i]);
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
            T_MenuMid = (from p in T_Menu where p.Name.Contains(SearchName) || SearchName == "" group p by p.Name into s select s).ToList();
            TotalItems = T_MenuMid.Count;
            T_MenuLast.Clear();
            for (int i = 0; i < T_MenuMid.Count; i++)
            {
                if (i >= (CurrentPage - 1) * PageSize && i < CurrentPage * PageSize)
                {
                    T_MenuLast.AddRange(T_MenuMid[i]);
                }
            }
            _view.Refresh();
        }        

        private void Add(object value)
        {     
            List<T1_Menu> menu = new List<T1_Menu>();
            long max = T_Menu.Count;
            if (T_Menu.Count > 0)
            {
                max = (from p in T_Menu select p.id).Max();
            }
            string name = "新建菜单" + (max + 1).ToString();
            ShowDialog(menu, name, ModifyStatus.Add);
        }

        private void Edit(object value)
        {
            T1_Menu menu = value as T1_Menu;
            if (menu != null)
            {
                string name = menu.Name;        
                ShowDialog((from p in T_Menu where p.Name == name select p).ToList(), name, ModifyStatus.Edit);
            }
        }

        private void Delete(object value)
        {
            T1_Menu menu = value as T1_Menu;
            if (menu != null)
            {
                string name = menu.Name;               
                ShowDialog((from p in T_Menu where p.Name == name select p).ToList(), name, ModifyStatus.Delete);
            }
        }

        //仅仅是为了兼容网页模式ShowDialog不生效
        private bool winshow = false;
        private void ShowDialog(List<T1_Menu> menu, string name, ModifyStatus status)
        {
            if (winshow == true)
            {
                return;
            }
            try
            {
                winshow = true;
                MenuSetWin win = new MenuSetWin(menu, name, status);
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
            }
            finally
            {
                winshow = false;
            }
        }

        private async void Win_Parachanged(IList<MyMenu> menu, string oldname, string newname, ModifyStatus mode)
        {
            if (menu == null || menu.Count == 0)
            {
                return;
            }
            try
            {
                Status = ViewModelStatus.Querying;
                //long max = T_Menu.Count;
                //if (T_Menu.Count > 0)
                //{
                //    max = (from p in T_Menu select p.id).Max();
                //}
                switch (mode)
                {
                    case ModifyStatus.Add:
                        {
                            //menu中一定有数据，且name一致。
                            //if (T_Menu.Contains(menu[0].T_Menu, EqualityHelper<T1_Menu>.CreateComparer(p => p.Name)))//重名鉴定
                            if (T_Menu.Where(p => p.Name == newname).Count() > 0)//重名鉴定
                            {
#if XBAP
                                MessageBox.Show("菜单名称重名，添加失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                Xceed.Wpf.Toolkit.MessageBox.Show("菜单名称重名，添加失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                            }
                            else
                            {
                                var tempadd = new List<T1_Menu>();
                                foreach (var mymenu in menu)
                                {
                                    if (mymenu.IsChecked == true)
                                    {
                                        var new_menu = mymenu.T_Menu;
                                        //new_menu.id = ++max;
                                        tempadd.Add(new_menu);
                                    }
                                }

                                if (await _databaseComponent.Add<T_Menu>(ServerIP, tempadd.Select(p => p as T_Menu).ToList()) == true)
                                {
                                    //T_Menu.AddRange(tempadd);//在DatabaseComponent添加
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
                            //if (T_Menu.Contains(menu[0].T_Menu, EqualityHelper<T1_Menu>.CreateComparer(p => p.Name)))//重名鉴定
                            if (T_Menu.Where(p => p.Name == newname).Count() > 0)//重名鉴定
                            {
                                if (newname != oldname)
                                {
#if XBAP
                                    MessageBox.Show("菜单名称重名，修改失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                                    Xceed.Wpf.Toolkit.MessageBox.Show("菜单名称重名，修改失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                                    return;
                                }
                            }

                            var tempadd = new List<T1_Menu>();
                            var tempedit = new List<T1_Menu>();
                            var tempdelete = new List<T1_Menu>();

                            foreach (var mymenu in menu)
                            {
                                var new_menu = mymenu.T_Menu;//新增
                                if (!T_Menu.Contains(new_menu, EqualityHelper<T1_Menu>.CreateComparer(p => p.id)))
                                {
                                    if (mymenu.IsChecked == true)
                                    {
                                        //new_menu.id = ++max;
                                        tempadd.Add(new_menu);
                                    }
                                }
                                else
                                {
                                    if (mymenu.IsChecked == true)//判断是否改变名字
                                    {
                                        var old_menu = (from p in T_Menu where p.id == new_menu.id select p).FirstOrDefault();
                                        if (old_menu != null)
                                        {
                                            old_menu.Name = new_menu.Name;
                                            tempedit.Add(old_menu);
                                        }
                                    }
                                    else//删除
                                    {
                                        var old_menu = (from p in T_Menu where p.id == new_menu.id select p).FirstOrDefault();
                                        if (old_menu != null)
                                        {
                                            tempdelete.Add(old_menu);
                                        }
                                    }
                                }
                            }

                            Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
                            addDic.Add("T_Menu", tempadd.Select(p => p as object).ToList());
                            Dictionary<string, Tuple<ICollection<string>, ICollection<object>>> editDic = new Dictionary<string, Tuple<ICollection<string>, ICollection<object>>>();
                            editDic.Add("T_Menu", new Tuple<ICollection<string>, ICollection<object>>(null, tempedit.Select(p => p as object).ToList()));
                            Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();
                            deleteDic.Add("T_Menu", new Tuple<string, ICollection<object>>("id", tempdelete.Select(p => p.id as object).ToList()));
                            if (await _databaseComponent.Complex(ServerIP, addDic, editDic, deleteDic))
                            {
                                //T_Menu.AddRange(tempadd);//在DatabaseComponent添加
                                //tempdelete.ForEach(p => T_Menu.Remove(p));//在DatabaseComponent删除
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
                            //if (await _databaseComponent.Add<T_Menu>(ServerIP, tempadd.Select(p => p as T_Menu).ToList()) == true)
                            //{
                            //    T_Menu.AddRange(tempadd);
                            //}
                            //if (await _databaseComponent.Modify<T_Menu>(ServerIP, new string[] { "Name" }, tempedit.Select(p => p as T_Menu).ToList()) == true)
                            //{
                            //}
                            //else
                            //{
                            //    tempedit.ForEach(p => p.Name = oldname);//失败回滚
                            //}
                            //if (await _databaseComponent.Delete<T_Menu>(ServerIP, tempdelete.Select(p => p.id as object).ToList()) == true)
                            //{
                            //    tempdelete.ForEach(p => T_Menu.Remove(p));
                            //}                       
                            break;
                        }
                    case ModifyStatus.Delete:
                        {
                            var tempdelete = new List<T1_Menu>();
                            for (int i = T_Menu.Count - 1; i >= 0; i--)
                            {
                                if (T_Menu[i].Name == newname)
                                {
                                    //T_Menu.Remove(T_Menu[i]); 
                                    tempdelete.Add(T_Menu[i]);
                                }
                            }

                            if (await _databaseComponent.Delete<T_Menu>(ServerIP, tempdelete.Select(p => p.id as object).ToList()) == true)
                            {
                                //tempdelete.ForEach(p => T_Menu.Remove(p));//在DatabaseComponent删除
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

                _loginUserService.AddOperateRecord(OperateType.MenuManage);

                T_MenuMid = (from p in T_Menu where p.Name.Contains(SearchName) || SearchName == "" group p by p.Name into s select s).ToList();
                TotalItems = T_MenuMid.Count;
                T_MenuLast.Clear();
                for (int i = 0; i < T_MenuMid.Count; i++)
                {
                    if (i >= (CurrentPage - 1) * PageSize && i < CurrentPage * PageSize)
                    {
                        T_MenuLast.AddRange(T_MenuMid[i]);
                    }
                }
                _view.Refresh();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("修改菜单", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }

        private void CurrentPageChanged(object value)
        {
            if (T_Menu == null)
            {
                return;
            }

            RoutedPropertyChangedEventArgs<PageNavigationEventArgs> args = ((ExCommandParameter)value).EventArgs as RoutedPropertyChangedEventArgs<PageNavigationEventArgs>;
            int pageSize = args.NewValue.PageSize;
            int currentPage = args.NewValue.CurrectPage;
            T_MenuLast.Clear();              
            for (int i = 0; i < T_MenuMid.Count; i++)
            {
                if (i >= (currentPage - 1) * pageSize && i < currentPage * pageSize)
                {
                    T_MenuLast.AddRange(T_MenuMid[i]);
                }
            }
            _view.Refresh();
        }
    }
}
