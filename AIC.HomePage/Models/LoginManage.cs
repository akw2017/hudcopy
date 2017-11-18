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
        private static LoginWin win;
        private static bool succeed; 
        private static IDatabaseComponent _databaseComponent;

        public delegate void TransferLoginResult(bool result);
        public static event TransferLoginResult LoginChanged;


        public static bool Enter(LoginInfo logininfo)
        {
            succeed = false;
            logininfo.RrrorInformation = "";//清空信息
            if (win == null || win.IsVisible == false)
            {
                win = new LoginWin(logininfo);
                win.Title = (string)Application.Current.Resources["strLogin"];
                win.Parachanged += Win_Parachanged;
                win.Show();
            }
            else
            {
                win.SetLogin(logininfo);
            }
            return succeed;
        }

        private async static void Win_Parachanged(LoginInfo logininfo)
        {
            _databaseComponent = ServiceLocator.Current.GetInstance<IDatabaseComponent>();

            //string pingresult = await _databaseComponent.UserPing(logininfo.ServerInfo.IP);
            //if (pingresult != "OK")
            //{
            //    logininfo.RrrorInformation = pingresult;
            //    win.WaitStop();
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
                        win.WaitStop();
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
                        serverinfo.T_User = null;
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
                        serverinfo.T_User = null;
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

                succeed = true;                
                win.Close();
                LoginChanged(succeed);
                return;
            }

            var role_user = await _databaseComponent.UserLogin(logininfo.ServerInfo.IP, logininfo.UserName, logininfo.Password);
            if (role_user == null)
            {
                logininfo.RrrorInformation = (string)Application.Current.Resources["strUserError"];
                win.WaitStop();
                return;
            }

            foreach (var serverinfo in logininfo.ServerInfoList)
            {
                if (serverinfo.IP == logininfo.ServerInfo.IP)
                {
                    logininfo.ServerInfo = serverinfo;
                    serverinfo.LoginResult = true;
                    serverinfo.T_User = role_user.Item2;
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
                else if (logininfo.HasSecondaryServer == true && serverinfo.IsLogin == true && serverinfo.Permission != "网络未连接")
                {
                    var secondrole_user = await _databaseComponent.UserLogin(serverinfo.IP, logininfo.UserName, logininfo.Password);
                    if (secondrole_user != null)
                    {
                        serverinfo.LoginResult = true;
                        serverinfo.T_User = secondrole_user.Item2;
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

            succeed = true;
            win.Close();
            LoginChanged(succeed);
        }
    }
}
