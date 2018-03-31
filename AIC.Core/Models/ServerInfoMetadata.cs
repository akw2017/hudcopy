using AIC.Core.Helpers;
using AIC.Core.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Models
{ 
    public partial class ServerInfo : IDataErrorInfo
    {
        class ServerInfoMetadata
        {
            [IPValidation]
            public string IP { get; set; }

            [Range(0, 65535, ErrorMessage = "请输入0-65535之间的整数。")]
            public int Port { get; set; }            
        }

        public string Error
        {
            get
            {
                string error = null;
                PropertyInfo[] propertys = this.GetType().GetProperties();
                foreach (PropertyInfo pinfo in propertys)
                {
                    //循环遍历属性
                    if (pinfo.CanRead && pinfo.CanWrite)
                    {
                        error = this.ValidateProperty<ServerInfoMetadata>(pinfo.Name);
                        if (error != null && error.Length > 0)
                        {
                            break;
                        }
                    }
                }
                return error;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return this.ValidateProperty<ServerInfoMetadata>(columnName);
            }
        }
    }
}
