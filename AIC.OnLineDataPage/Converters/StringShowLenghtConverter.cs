using System;
using System.Globalization;
using System.Windows.Data;

namespace AIC.OnLineDataPage.Converters
{
    public class StringShowLenghtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string str = value as string;
                return str.Substring(0, str.Length > 6 ? 6 : str.Length);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
