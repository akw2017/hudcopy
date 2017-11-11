using System;
using System.Globalization;
using System.Windows.Data;
using System.Linq;
using System.Windows;

namespace AIC.PDA.Converters
{
    public class AcronymConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value.ToString();
            if(!string.IsNullOrWhiteSpace(str))
            {
                return str.ToUpper()[0];
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
