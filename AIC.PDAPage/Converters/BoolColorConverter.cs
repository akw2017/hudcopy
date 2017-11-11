using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AIC.PDAPage.Converters
{
    //不使能，属性颜色变灰
    public class BoolColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value)
                {
                    return new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0xff));
                }
            }
            return new SolidColorBrush(Color.FromRgb(0x77, 0x78, 0x7b));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
