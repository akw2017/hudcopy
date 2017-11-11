/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.SlaveDB.Generated
{
    public partial class SlaveORM
    {
        public SlaveORM(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
            CommonConstruct();
        }
    }
}
