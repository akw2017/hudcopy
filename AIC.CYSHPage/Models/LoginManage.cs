using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.UserManageModels;
using AIC.CYSHPage.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using AIC.ServiceInterface;

namespace AIC.CYSHPage.Models
{
    public class LoginManage
    {
        private LoginWin loginWin;
        private bool loginSucceed; 
        private IDatabaseComponent _databaseComponent;

        //仅仅是为了兼容网页模式ShowDialog不生效
        private static bool winshow = false;

        public bool Login(LoginInfo logininfo)
        {
            if (winshow == true)
            {
                return false;
            }
            try
            {
                winshow = true;
                loginSucceed = false;
                logininfo.RrrorInformation = "";//清空信息
                loginWin = new LoginWin(logininfo);
                loginWin.Title = (string)Application.Current.Resources["strLogin"];
                loginWin.Parachanged += Win_Parachanged;
                loginWin.ShowDialog();
                return loginSucceed;               
            }
            finally
            {
                winshow = false;
            }
        }

        private async void Win_Parachanged(LoginInfo logininfo)
        {            
            _databaseComponent = ServiceLocator.Current.GetInstance<IDatabaseComponent>();

            string pingresult = await _databaseComponent.UserPing(logininfo.ServerInfo.IP);
            if (pingresult != "OK")
            {
                logininfo.RrrorInformation = pingresult;
                loginWin.WaitStop();
                return;
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
                    }
                    //副服务器登陆
                    else if (logininfo.HasSecondaryServer == true && serverinfo.IsLogin == true)
                    {
                        serverinfo.LoginResult = true;
                        serverinfo.Permission = "超级管理员";
                    }
                }

                loginSucceed = true;
                loginWin.Close();
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
                else if (logininfo.HasSecondaryServer == true && serverinfo.IsLogin == true)
                {
                    var secondrole = await _databaseComponent.UserLogin(serverinfo.IP, logininfo.UserName, logininfo.Password);
                    if (secondrole != null)
                    {
                        serverinfo.LoginResult = true;
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
            return;            
        }
    }
}
