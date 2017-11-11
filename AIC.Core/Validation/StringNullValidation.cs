using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Validation
{
    //字符串非空检查
    public class StringNullValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var str = value as string;
           
            if (str != null && str.Length > 0)
            {
                return true;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return "用户名不能为空, 请输入用户名！！！";
        }
    }
}
