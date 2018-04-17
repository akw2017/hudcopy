using AIC.Core;
using AIC.Core.Models;
using AIC.Core.Servers;
using AIC.Core.Events;
using AIC.HomePage.Models;
using AIC.HomePage.Views;
using AIC.ServiceInterface;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AIC.Core.UserManageModels;
using AIC.Resources.Models;
using System.Threading.Tasks;
using AIC.Core.Helpers;
using AIC.M9600.Common.SlaveDB.Generated;

namespace AIC.HomePage.ViewModels
{
    class LoginViewModel : BindableBase
    {  
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly ILoginUserService _loginUserService;
        private readonly IUserManageService _userManageService;
        private readonly IDatabaseComponent _databaseComponent;

        private static Uri mapView = new Uri("MapView", UriKind.Relative);
        public LoginViewModel(ILocalConfiguration localConfiguration, IEventAggregator eventAggregator, IRegionManager regionManager, ILoginUserService loginUserService, IUserManageService userManageService, IDatabaseComponent databaseComponent)
        {
            _localConfiguration = localConfiguration;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _loginUserService = loginUserService;
            _userManageService = userManageService;
            _databaseComponent = databaseComponent;

            ServerInfoList = _localConfiguration.ServerInfoList;
            LoginInfo = _loginUserService.DefaultLoginServer();
        }

        #region 属性与字段
        public ObservableCollection<ServerInfo> serverInfoList;
        public ObservableCollection<ServerInfo> ServerInfoList
        {
            get { return serverInfoList; }
            set
            {
                serverInfoList = value;
                OnPropertyChanged("ServerInfoList");
            }
        }

        public LoginInfo loginInfo;
        public LoginInfo LoginInfo
        {
            get
            {
                return loginInfo;
            }
            set
            {
                loginInfo = value;
                OnPropertyChanged("LoginInfo");
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

        public string waitinfo = "用户登录中...";
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

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return this.loginCommand ?? (this.loginCommand = new DelegateCommand<object>(para => this.Login(para)));
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return this.cancelCommand ?? (this.cancelCommand = new DelegateCommand<object>(para => this.Cancel(para)));
            }
        }

        private ICommand selectedServerChangedComamnd;
        public ICommand SelectedServerChangedComamnd
        {
            get
            {
                return this.selectedServerChangedComamnd ?? (this.selectedServerChangedComamnd = new DelegateCommand<object>(para => this.SelectedServerChanged(para)));
            }
        }
        #endregion

        #region 登录

        private void SelectedServerChanged(object para)
        {
            ServerInfo serverinfo = para as ServerInfo;
            if (serverinfo != null)
            {
                LoginInfo = _loginUserService.InitLoginServer(serverinfo);
            }
        }

        private async void Login(object para)
        {
            if (LoginInfo == null)
            {
                return;
            }
            try
            {     
                Status = ViewModelStatus.Querying;
                Dictionary<string, Task<string>> lttask = new Dictionary<string, Task<string>>();
                foreach (var serverinfo in ServerInfoList.Distinct(EqualityHelper<ServerInfo>.CreateComparer(p => p.IP)))
                {
                    if (serverinfo.IP == LoginInfo.ServerInfo.IP)
                    {
                        lttask.Add(serverinfo.IP, _databaseComponent.UserPing(serverinfo.IP));
                    }
                    else if (LoginInfo.HasSecondaryServer == true && serverinfo.IsLogin == true)
                    {
                        lttask.Add(serverinfo.IP, _databaseComponent.UserPing(serverinfo.IP));
                    }
                    serverinfo.LoginResult = false;
                    serverinfo.Permission = "";
                }
                await Task.WhenAll(lttask.Values.ToArray());

                foreach (var task in lttask)
                {
                    if (task.Value.Result != "OK")
                    {
                        if (LoginInfo.ServerInfo.IP == task.Key)
                        {
                            LoginInfo.RrrorInformation = task.Value.Result;
                            return;
                        }

                        _localConfiguration.ServerInfoList.Where(p => p.IP == task.Key).ToList().ForEach(p => { p.LoginResult = false; p.Permission = "网络未连接"; });
                    }
                }

                //超级密码登陆
                if ((LoginInfo.UserName == "superadmin" && LoginInfo.Password == "superadmin"))
                {
                    foreach (var serverinfo in _localConfiguration.ServerInfoList)
                    {
                        if (serverinfo.IP == LoginInfo.ServerInfo.IP)
                        {
                            serverinfo.LoginResult = true;
                            serverinfo.Permission = "超级管理员";
                            serverinfo.T_User = null;
                            if (serverinfo.IsSaveUserName == true)
                            {
                                serverinfo.UserName = LoginInfo.UserName;
                            }
                            else
                            {
                                serverinfo.UserName = "";
                            }
                            if (serverinfo.IsSaveUserPwd == true)
                            {
                                serverinfo.UserPwd = LoginInfo.Password;
                            }
                            else
                            {
                                serverinfo.UserPwd = "";
                            }
                        }
                        //副服务器登陆
                        else if (LoginInfo.HasSecondaryServer == true && serverinfo.IsLogin == true && serverinfo.Permission != "网络未连接")
                        {
                            serverinfo.LoginResult = true;
                            serverinfo.Permission = "超级管理员";
                            serverinfo.T_User = null;
                            if (serverinfo.IsSaveUserName == true)
                            {
                                serverinfo.UserName = LoginInfo.UserName;
                            }
                            else
                            {
                                serverinfo.UserName = "";
                            }
                            if (serverinfo.IsSaveUserPwd == true)
                            {
                                serverinfo.UserPwd = LoginInfo.Password;
                            }
                            else
                            {
                                serverinfo.UserPwd = "";
                            }
                        }
                    }

                    LoginSuccess();
                    return;
                }

                var role_user = await _databaseComponent.UserLogin(LoginInfo.ServerInfo.IP, LoginInfo.UserName, LoginInfo.Password);
                if (role_user == null)
                {
                    LoginInfo.RrrorInformation = (string)Application.Current.Resources["strUserError"];
                    return;
                }

                foreach (var serverinfo in _localConfiguration.ServerInfoList)
                {
                    if (serverinfo.IP == LoginInfo.ServerInfo.IP)
                    {
                        LoginInfo.ServerInfo = serverinfo;
                        serverinfo.LoginResult = true;
                        serverinfo.T_User = role_user.Item2;
                        if (serverinfo.IsSaveUserName == true)
                        {
                            serverinfo.UserName = LoginInfo.UserName;
                        }
                        else
                        {
                            serverinfo.UserName = "";
                        }
                        if (serverinfo.IsSaveUserPwd == true)
                        {
                            serverinfo.UserPwd = LoginInfo.Password;
                        }
                        else
                        {
                            serverinfo.UserPwd = "";
                        }
                        if (role_user.Item1.Is_SuperAdmin)
                        {
                            serverinfo.Permission = "超级管理员";
                        }
                        else if (role_user.Item1.Is_Admin)
                        {
                            serverinfo.Permission = "管理员";
                        }
                        else
                        {
                            serverinfo.Permission = "操作员";
                        }
                    }
                    //副服务器登陆
                    else if (LoginInfo.HasSecondaryServer == true && serverinfo.IsLogin == true && serverinfo.Permission != "网络未连接")
                    {
                        var secondrole_user = await _databaseComponent.UserLogin(serverinfo.IP, LoginInfo.UserName, LoginInfo.Password);
                        if (secondrole_user != null)
                        {
                            serverinfo.LoginResult = true;
                            serverinfo.T_User = secondrole_user.Item2;
                            if (serverinfo.IsSaveUserName == true)
                            {
                                serverinfo.UserName = LoginInfo.UserName;
                            }
                            else
                            {
                                serverinfo.UserName = "";
                            }
                            if (serverinfo.IsSaveUserPwd == true)
                            {
                                serverinfo.UserPwd = LoginInfo.Password;
                            }
                            else
                            {
                                serverinfo.UserPwd = "";
                            }
                            if (secondrole_user.Item1.Is_SuperAdmin)
                            {
                                serverinfo.Permission = "超级管理员";
                            }
                            else if (secondrole_user.Item1.Is_Admin)
                            {
                                serverinfo.Permission = "管理员";
                            }
                            else
                            {
                                serverinfo.Permission = "操作员";
                            }
                        }
                    }
                }
                LoginSuccess();
            }
            catch (Exception ex)
            {
                _eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("登录异常", ex));
            }
            finally
            {
                Status = ViewModelStatus.None;
            }
        }  

        private void Cancel(object para)
        {

        }

        private async void LoginSuccess()
        {            
            await _loginUserService.SetUserLogin();
            _eventAggregator.GetEvent<LoginEvent>().Publish(_loginUserService.LoginInfo);
            //把登录配置保存到本地
            _localConfiguration.WriteServerInfo(_localConfiguration.ServerInfoList);
            await Task.Delay(TimeSpan.FromSeconds(1));
            Status = ViewModelStatus.None;
            //加载后续数据   
            await _loginUserService.LazyLoading();
        }
        #endregion
    }
}
