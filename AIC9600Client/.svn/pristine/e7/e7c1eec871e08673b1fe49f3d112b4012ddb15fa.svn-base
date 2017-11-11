/* Author : zhengyangyong */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.DTO.Web
{
    public class WebResponse<T>
    {
        /// <summary>
        /// 请求是否OK
        /// </summary>
        public bool IsOK { get; set; }
        /// <summary>
        /// 错误类型
        /// </summary>
        public string ErrorType { get; set; }
        /// <summary>
        /// IsOK = false时错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 仅Query类返回
        /// </summary>
        public T ResponseItem { get; set; }

        public static WebResponse<T> Success(T responseItem)
        {
            return new WebResponse<T>() { IsOK = true, ResponseItem = responseItem };
        }

        public static WebResponse<T> Failed(string errorType,string errorMessage)
        {
            return new WebResponse<T>() { IsOK = false,ErrorType = errorType, ErrorMessage = errorMessage };
        }
    }

    public class WebResponse
    {
        /// <summary>
        /// 请求是否OK
        /// </summary>
        public bool IsOK { get; set; }
        /// <summary>
        /// 错误类型
        /// </summary>
        public string ErrorType { get; set; }
        /// <summary>
        /// IsOK = false时错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        public static WebResponse Success()
        {
            return new WebResponse() { IsOK = true };
        }

        public static WebResponse Failed(string errorType, string message)
        {
            return new WebResponse() { IsOK = false, ErrorType = errorType, ErrorMessage = message };
        }
    }
}
