using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AIC.PDAPage.Converters
{
    //空字符串的texblock宽度设定
    public class StringWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            if (str != null)
            {               
                return (str.Length - 1) * 8 + 3;               
            }
            string para = parameter as string;
            if (para == "1")
            {
                return 1;
            }
            else
            {
                return 0;
            }      
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
