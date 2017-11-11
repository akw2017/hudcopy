using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AIC.UserPage.Converters
{
   
    public class IntStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                if ((int)value == 0)
                {
                    return (string)Application.Current.Resources["menu"];
                }
                if ((int)value == 1)
                {
                    return (string)Application.Current.Resources["button"];
                }
            }
            else if (value is string)
            {
                if ((string)value == "0")
                {
                    return (string)Application.Current.Resources["menu"];
                }
                if ((string)value == "1")
                {
                    return (string)Application.Current.Resources["button"];
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
