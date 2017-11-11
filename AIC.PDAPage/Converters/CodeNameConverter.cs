using AIC.Core.Models;
using AIC.PDAPage.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AIC.PDAPage.Converters
{
    public class CodeNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 1)
            {
                throw new NotImplementedException();
            }

            int Code = 0;
            if (values[0] is int || values[0] is bool)//参数0为Code，即目录数组[Code,Name]的索引
            {
                Code = System.Convert.ToInt32(values[0]);
            }              

            List<ICategory> Category = new List<ICategory>();

            for (int i = 1; i < values.Length; i++)//参数1,2,3,4...Category，即目录数组[Code,Name]的目录
            {
                //目录单条
                ICategory category = values[i] as ICategory;
                if (category != null)
                {
                    Category.Add(category);
                    continue;
                }
                //目录数组
                IList list = values[i] as IList;
                if (list != null)
                {
                    foreach (var p in list)
                    {
                        if (p is ICategory)
                        {
                            Category.Add(p as ICategory);
                        }
                    }
                }
            }

            //目录参数
            string para = parameter as string;//将目录放在参数里
            if (para != null)
            {
                string[] sArray = para.Split('|');
                for (int i = 0; i < sArray.Length; i++)
                {
                    ICategory category = new BaseCategory() { Code = i, Name = sArray[i] };
                    Category.Add(category);
                }
            }          

            if (Category != null && Category.Count != 0)//返回名字
            {
                string name = (from p in Category where p.Code == Code select p.Name).FirstOrDefault();
                return name;
            }

            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
