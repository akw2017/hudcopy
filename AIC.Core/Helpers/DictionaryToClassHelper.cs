﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Core.Helpers
{
     public class DictionaryToClassHelper
    {
        /// <summary>  
         /// 字典类型转化为对象  
         /// </summary>  
         /// <param name="dic"></param>  
         /// <returns></returns>  
        public static T DicToObject<T>(Dictionary<string, object> dic) where T : new()
        {
            var md = new T();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            foreach (var d in dic)
            {
                //var filed = textInfo.ToTitleCase(d.Key);
                var filed = d.Key;
                try
                {
                    var value = d.Value;
                    md.GetType().GetProperty(filed).SetValue(md, value);
                }
                catch (Exception e)
                {

                }
            }
            return md;
        }
    }
}
