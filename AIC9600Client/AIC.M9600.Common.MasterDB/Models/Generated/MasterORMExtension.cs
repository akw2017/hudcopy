/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.MasterDB.Generated
{
    public partial class MasterORM
    {
        public MasterORM(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
            CommonConstruct();
        }
    }
}
