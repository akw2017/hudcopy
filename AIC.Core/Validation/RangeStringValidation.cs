using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AIC.Core.Validation
{
    //IP格式检查
    public class RangeStringValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var str = value as string;
           
            if (str == null || IsRangeString(str))
            {
                return true;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return "请输入正确的范围格式XXXX~XXXX！！！";
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsRangeString(string ip)
        {
            return Regex.IsMatch(ip, @"^(([+-]?\d+(\.\d+)?)\~([+-]?\d+(\.\d+)?))?$");
        }      
    }

}