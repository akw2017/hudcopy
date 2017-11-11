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
    public class CodeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                int code = (int)value;
                switch (code)
                {
                    case 0: return new SolidColorBrush(Color.FromRgb(0xd7, 0x13, 0x45));//红色
                    case 1: return new SolidColorBrush(Color.FromRgb(0xf4, 0x79, 0x20));//橙色
                    case 2: return new SolidColorBrush(Color.FromRgb(0xff, 0xd4, 0x00));//黄色
                    case 3: return new SolidColorBrush(Color.FromRgb(0x7c, 0xfc, 0x00));//绿色
                    case 4: return new SolidColorBrush(Color.FromRgb(0x7c, 0xfc, 0x00));//绿色
                    case 5: return new SolidColorBrush(Color.FromRgb(0x7c, 0xfc, 0x00));//绿色
                    case 6: return new SolidColorBrush(Color.FromRgb(0xff, 0xd4, 0x00));//黄色
                    case 7: return new SolidColorBrush(Color.FromRgb(0xf4, 0x79, 0x20));//橙色
                    case 8: return new SolidColorBrush(Color.FromRgb(0xd7, 0x13, 0x45));//红色
                }

            }
            return new SolidColorBrush(Color.FromRgb(0x00, 0x00, 0x00));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
