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
    //code与selected转换
    public class CodeSelectedItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
            {
                //throw new NotImplementedException();
                return null;
            }

            if (values[0] is int)
            {
                int Code = (int)values[0];
                IList list = values[1] as IList;
                if (list == null || list.Count == 0)
                {
                    return null;
                }
                List<ICategory> Category = new List<ICategory>();
                foreach (var p in list)
                {
                    if (p is ICategory)
                    {
                        Category.Add(p as ICategory);
                    }
                }
                if (Category != null && Category.Count != 0)
                {
                    ICategory SelectedItem = (from p in Category where p.Code == Code select p).FirstOrDefault();
                    return SelectedItem;
                }
            }
            //throw new NotImplementedException();
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is ICategory)
            {
                ICategory SelectedItem = value as ICategory;
                int Code = SelectedItem.Code;
                object[] obj = new object[1];
                obj[0] = Code;
                return obj;
            }
            throw new NotImplementedException();
        }
    }
}
