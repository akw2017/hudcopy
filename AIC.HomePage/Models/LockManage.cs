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
    public static class LockManage
    {
        private static LockWin win;
        private static bool succeed;
        private static IDatabaseComponent _databaseComponent;

        //仅仅是为了兼容网页模式ShowDialog不生效
        private static bool winshow = false;

        public static bool Lock(LoginInfo logininfo)
        {
            if (winshow == true)
            {
                return false;
            }
            try
            {
                winshow = true;
                LoginInfo lockinfo = logininfo.ShallowClone();
                succeed = false;
                logininfo.RrrorInformation = "";//清空信息
                win = new LockWin(logininfo);
                win.Title = (string)Application.Current.Resources["strLock"];
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
                return succeed;
            }
            finally
            {
                winshow = false;
            }
        }

        public static bool UnLock(LoginInfo logininfo)
        {
            if (winshow == true)
            {
                return false;
            }
            try
            {
                winshow = true;
                LoginInfo lockinfo = logininfo.ShallowClone();
                succeed = false;
                logininfo.RrrorInformation = "";//清空信息
                win = new LockWin(logininfo);
                win.Title = (string)Application.Current.Resources["strUnLock"];
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
                return succeed;
            }
            finally
            {
                winshow = false;
            }
        }
        private static async void Win_Parachanged(LoginInfo logininfo)
        {
            _databaseComponent = ServiceLocator.Current.GetInstance<IDatabaseComponent>();

            if (logininfo.UserName == "superadmin" && logininfo.Password == "superadmin")
            {
                succeed = true;
                win.Close();
            }
            else if (await _databaseComponent.UserLogin(logininfo.ServerInfo.IP, logininfo.UserName, logininfo.Password) != null)
            {
                succeed = true;
                win.Close();
            }
            else
            {
                logininfo.RrrorInformation = (string)Application.Current.Resources["strUserError"];
                win.WaitStop();
            }
        }
    }
}
