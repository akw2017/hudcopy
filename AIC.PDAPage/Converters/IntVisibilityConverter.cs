using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AIC.PDAPage.Converters
{
    //选中项索引转换成可见项
    public class IntVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
           
            if (value is int && parameter is string) //与参数相同则显示
            {
                if(value.ToString() == (string)parameter)
                {
                    return Visibility.Visible;
                }
            }
            else if (value is int) //无参数，则大于等于0显示
            {
                if ((int)value >= 0)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
