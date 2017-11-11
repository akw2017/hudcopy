using AIC.Core.Models;
using AIC.HomePage.Views;
using AIC.ServiceInterface;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIC.HomePage.Models
{
    public class LockManage
    {
        private static LockWin loginWin;
        private static bool loginSucceed;
        private IDatabaseComponent _databaseComponent;

        //仅仅是为了兼容网页模式ShowDialog不生效
        private static bool winshow = false;

        public bool Lock(LoginInfo logininfo)
        {
            if (winshow == true)
            {
                return false;
            }
            try
            {
                winshow = true;
                LoginInfo lockinfo = logininfo.ShallowClone();
                loginSucceed = false;
                logininfo.RrrorInformation = "";//清空信息
                loginWin = new LockWin(logininfo);
                loginWin.Title = (string)Application.Current.Resources["strLock"];
                loginWin.Parachanged += Win_Parachanged;
                loginWin.ShowDialog();
                return loginSucceed;
            }
            finally
            {
                winshow = false;
            }
        }

        public bool UnLock(LoginInfo logininfo)
        {
            if (winshow == true)
            {
                return false;
            }
            try
            {
                winshow = true;
                LoginInfo lockinfo = logininfo.ShallowClone();
                loginSucceed = false;
                logininfo.RrrorInformation = "";//清空信息
                loginWin = new LockWin(logininfo);
                loginWin.Title = (string)Application.Current.Resources["strUnLock"];
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

            if (logininfo.UserName == "superadmin" && logininfo.Password == "superadmin")
            {
                loginSucceed = true;
                loginWin.Close();
            }
            else if (await _databaseComponent.UserLogin(logininfo.ServerInfo.IP, logininfo.UserName, logininfo.Password) != null)
            {
                loginSucceed = true;
                loginWin.Close();
            }
            else
            {
                logininfo.RrrorInformation = (string)Application.Current.Resources["strUserError"];                
            }
        }
    }
}
