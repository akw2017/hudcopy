using AIC.Core.Models;
using AIC.Core.UserManageModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AIC.UserPage.Converters
{
    public class GuidNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
            {
                //throw new NotImplementedException();
                return null;
            }

            if (values[0] is Guid)
            {
                Guid Guid = (Guid)values[0];
                IList list = values[1] as IList;
                if (list == null || list.Count == 0)
                {
                    return null;
                }
                List<IAnobject> Category = new List<IAnobject>();
                foreach (var p in list)
                {
                    if (p is IAnobject)
                    {
                        Category.Add(p as IAnobject);
                    }
                }
                if (Category != null && Category.Count != 0)
                {
                    IAnobject SelectedItem = (from p in Category where p.Guid == Guid select p).FirstOrDefault();
                    return SelectedItem;
                }
            }
            //throw new NotImplementedException();
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is IAnobject)
            {
                IAnobject SelectedItem = value as IAnobject;
                Guid Guid = SelectedItem.Guid;
                object[] obj = new object[1];
                obj[0] = Guid;
                return obj;
            }
            throw new NotImplementedException();
        }
    }
}
