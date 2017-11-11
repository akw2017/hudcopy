using AIC.Core;
using AIC.Core.Models;
using AIC.Core.Servers;
using AIC.Core.Events;
using AIC.CYSHPage.Models;
using AIC.CYSHPage.Views;
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

namespace AIC.CYSHPage.ViewModels
{
    class LoginViewModel : BindableBase
    {  
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly ILocalConfiguration _localConfiguration;
        private readonly ILoginUserService _loginUserService;
        private readonly IUserManageService _userManageService;

        private static Uri mapView = new Uri("MapView", UriKind.Relative);
        public LoginViewModel(ILocalConfiguration localConfiguration, IEventAggregator eventAggregator, IRegionManager regionManager, ILoginUserService loginUserService, IUserManageService userManageService)
        {
            _localConfiguration = localConfiguration;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _loginUserService = loginUserService;
            _userManageService = userManageService;

            var server = _localConfiguration.ServerInfoList;
            ServerInfo = new ObservableCollection<ServerInfo>(server);
            _eventAggregator.GetEvent<ServerChangedEvent>().Subscribe(UpdataInfo);
        }

        #region 属性与字段
        private ObservableCollection<ServerInfo> _serverInfo;
        public ObservableCollection<ServerInfo> ServerInfo
        {
            get
            {
                return _serverInfo;
            }
            set
            {
                _serverInfo = value;
                OnPropertyChanged("InfoList");
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

        public string WaitInfo { get { return "用户登录中"; } set { }}        

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return this.loginCommand ?? (this.loginCommand = new DelegateCommand<object>(para => this.Login(para)));
            }
        }
        #endregion

        private void UpdataInfo(IList<ServerInfo> serverinfo)//服务器更新
        {
            ServerInfo = new ObservableCollection<ServerInfo>(serverinfo);
        }

        #region 登录
        private async void Login(object para)
        {
            ServerInfo serverinfo = para as ServerInfo;
            if (serverinfo == null)
            {
                return;
            }
            
            try
            {
                if (new LoginManage().Login(_loginUserService.InitLoginServer(serverinfo)))
                {
                    Status = ViewModelStatus.Querying;
                    //把登录配置保存到本地
                    _localConfiguration.WriteServerInfo(ServerInfo);
                    await _loginUserService.SetUserLogin();
                    _eventAggregator.GetEvent<LoginEvent>().Publish(_loginUserService.LoginInfo);
                   
                    //加载数据   
                }
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
        #endregion

  


    }
}
