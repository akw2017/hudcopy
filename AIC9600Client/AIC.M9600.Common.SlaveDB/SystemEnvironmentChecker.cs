using AIC.M9600.Common.PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.M9600.Common.SlaveDB
{
    public class SystemEnvironmentChecker
    {
        public string OS_Version
        {
            get
            {
                return new Microsoft.VisualBasic.Devices.ComputerInfo().OSFullName;
            }
        }

        public string OS_ServicePack
        {
            get
            {
                return System.Environment.OSVersion.ServicePack;
            }
        }

        public bool OS_Is64Bit
        {
            get
            {
                return System.Environment.Is64BitOperatingSystem;
            }
        }

        public string CLR_Version_String
        {
            get
            {
                return System.Environment.Version.ToString();
            }
        }

        public Version CLR_Version
        {
            get
            {
                return System.Environment.Version;
            }
        }

        public string DB_Version(string connectionString)
        {
            SQLServer db = null;
            try
            {
                db = SQLServer.GetInstance(connectionString);
                return db.ExecuteScalar<string>("select @@version");
            }
            catch { return "数据库配置不正确，获取失败"; }
            finally
            {
                if (db != null) db.CompleteDatabase();
            }
        }
    }
}
