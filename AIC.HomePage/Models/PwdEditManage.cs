using AIC.Core;
using AIC.Core.Models;
using AIC.HomePage.Views;
using AIC.M9600.Common.MasterDB.Generated;
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
    public static class PwdEditManage
    {
        private static PwdEditWin win;
        private static bool succeed;
        private static IDatabaseComponent _databaseComponent;

        //仅仅是为了兼容网页模式ShowDialog不生效
        private static bool winshow = false;

        public static bool Enter(LoginInfo logininfo)
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
                win = new PwdEditWin(logininfo);
                win.Title = (string)Application.Current.Resources["menuEditPwd"];
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
                return succeed;
            }
            finally
            {
                winshow = false;
            }
        }
        private static async void Win_Parachanged(LoginInfo logininfo, string pwd)
        {
            _databaseComponent = ServiceLocator.Current.GetInstance<IDatabaseComponent>();
            logininfo.Password = pwd;
            string encryptedpwd = MyEncrypt.EncryptDES(pwd);
            var servers = logininfo.ServerInfoList.Where(p => p.LoginResult == true);

            Dictionary<string, Task<bool>> lttask = new Dictionary<string, Task<bool>>();
            foreach (var serverinfo in servers)
            {
                if (serverinfo.T_User == null)
                {
                    continue;
                }
                serverinfo.T_User.Password = encryptedpwd;
                lttask.Add(serverinfo.IP, _databaseComponent.Modify<T_User>(serverinfo.IP, new string[] { "Password" }, serverinfo.T_User));
            }
            await Task.WhenAll(lttask.Values.ToArray());

            logininfo.RrrorInformation = "服务器";
            succeed = true;
            foreach (var task in lttask)
            {
                if (task.Value.Result == false)
                {
                    logininfo.RrrorInformation +=  task.Key + ";";
                    succeed = false;
                }
            }
            logininfo.RrrorInformation += "密码修改失败！！！";          
            win.WaitStop();

            if (succeed == true)
            {
                win.Close();
            }
        }
    }
}
