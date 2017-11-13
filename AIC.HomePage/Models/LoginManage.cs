using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.UserManageModels;
using AIC.HomePage.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using AIC.ServiceInterface;

namespace AIC.HomePage.Models
{
    public static class LoginManage
    {
        private static LoginWin loginWin;
        private static bool loginSucceed; 
        private static IDatabaseComponent _databaseComponent;

        public delegate void TransferLoginResult(bool loginresult);
        public static event TransferLoginResult LoginChanged;


        public static bool Login(LoginInfo logininfo)
        {
            loginSucceed = false;
            logininfo.RrrorInformation = "";//清空信息
            if (loginWin == null || loginWin.IsVisible == false)
            {
                loginWin = new LoginWin(logininfo);
                loginWin.Title = (string)Application.Current.Resources["strLogin"];
                loginWin.Parachanged += Win_Parachanged;
                loginWin.Show();
            }
            else
            {
                loginWin.SetLogin(logininfo);
            }
            return loginSucceed;
        }

        private async static void Win_Parachanged(LoginInfo logininfo)
        {
            _databaseComponent = ServiceLocator.Current.GetInstance<IDatabaseComponent>();

            //string pingresult = await _databaseComponent.UserPing(logininfo.ServerInfo.IP);
            //if (pingresult != "OK")
            //{
            //    logininfo.RrrorInformation = pingresult;
            //    loginWin.WaitStop();
            //    return;
            //}

            Dictionary<string, Task<string>> lttask = new Dictionary<string, Task<string>>();
            foreach (var serverinfo in logininfo.ServerInfoList)
            {
                if (serverinfo.IP == logininfo.ServerInfo.IP)
                {
                    lttask.Add(serverinfo.IP, _databaseComponent.UserPing(serverinfo.IP));
                }
                else if (logininfo.HasSecondaryServer == true && serverinfo.IsLogin == true)
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
                    if (logininfo.ServerInfo.IP == task.Key)
                    {
                        logininfo.RrrorInformation = task.Value.Result;
                        loginWin.WaitStop();
                        return;
                    }

                    logininfo.ServerInfoList.Where(p => p.IP == task.Key).ToList().ForEach(p => { p.LoginResult = false; p.Permission = "网络未连接"; });
                }
            }

            //超级密码登陆
            if ((logininfo.UserName == "superadmin" && logininfo.Password == "superadmin"))
            {
                foreach (var serverinfo in logininfo.ServerInfoList)
                {
                    if (serverinfo.IP == logininfo.ServerInfo.IP)
                    {
                        serverinfo.LoginResult = true;
                        serverinfo.Permission = "超级管理员";
                        if (serverinfo.IsSaveUserName == true)
                        {
                            serverinfo.UserName = logininfo.UserName;
                        }
                        else
                        {
                            serverinfo.UserName = "";
                        }
                        if (serverinfo.IsSaveUserPwd == true)
                        {
                            serverinfo.UserPwd = logininfo.Password;
                        }
                        else
                        {
                            serverinfo.UserPwd = "";
                        }
                    }
                    //副服务器登陆
                    else if (logininfo.HasSecondaryServer == true && serverinfo.IsLogin == true && serverinfo.Permission != "网络未连接")
                    {
                        serverinfo.LoginResult = true;
                        serverinfo.Permission = "超级管理员";
                        if (serverinfo.IsSaveUserName == true)
                        {
                            serverinfo.UserName = logininfo.UserName;
                        }
                        else
                        {
                            serverinfo.UserName = "";
                        }
                        if (serverinfo.IsSaveUserPwd == true)
                        {
                            serverinfo.UserPwd = logininfo.Password;
                        }
                        else
                        {
                            serverinfo.UserPwd = "";
                        }
                    }
                }

                loginSucceed = true;                
                loginWin.Close();
                LoginChanged(loginSucceed);
                return;
            }

            var role = await _databaseComponent.UserLogin(logininfo.ServerInfo.IP, logininfo.UserName, logininfo.Password);
            if (role == null)
            {
                logininfo.RrrorInformation = (string)Application.Current.Resources["strUserError"];
                loginWin.WaitStop();
                return;
            }

            foreach (var serverinfo in logininfo.ServerInfoList)
            {
                if (serverinfo.IP == logininfo.ServerInfo.IP)
                {
                    logininfo.ServerInfo = serverinfo;
                    serverinfo.LoginResult = true;
                    if (serverinfo.IsSaveUserName == true)
                    {
                        serverinfo.UserName = logininfo.UserName;
                    }
                    else
                    {
                        serverinfo.UserName = "";
                    }
                    if (serverinfo.IsSaveUserPwd == true)
                    {
                        serverinfo.UserPwd = logininfo.Password;
                    }
                    else
                    {
                        serverinfo.UserPwd = "";
                    }
                    if (role.Is_SuperAdmin)
                    {
                        serverinfo.Permission = "超级管理员";
                    }
                    else if (role.Is_Admin)
                    {
                        serverinfo.Permission = "管理员";
                    }
                    else
                    {
                        serverinfo.Permission = "操作员";
                    }
                }
                //副服务器登陆
                else if (logininfo.HasSecondaryServer == true && serverinfo.IsLogin == true && serverinfo.Permission != "网络未连接")
                {
                    var secondrole = await _databaseComponent.UserLogin(serverinfo.IP, logininfo.UserName, logininfo.Password);
                    if (secondrole != null)
                    {
                        serverinfo.LoginResult = true;
                        if (serverinfo.IsSaveUserName == true)
                        {
                            serverinfo.UserName = logininfo.UserName;
                        }
                        else
                        {
                            serverinfo.UserName = "";
                        }
                        if (serverinfo.IsSaveUserPwd == true)
                        {
                            serverinfo.UserPwd = logininfo.Password;
                        }
                        else
                        {
                            serverinfo.UserPwd = "";
                        }
                        if (secondrole.Is_SuperAdmin)
                        {
                            serverinfo.Permission = "超级管理员";
                        }
                        else if (secondrole.Is_Admin)
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

            loginSucceed = true;
            loginWin.Close();
            LoginChanged(loginSucceed);
        }
    }
}
