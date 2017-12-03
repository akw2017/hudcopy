using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AIC.Core
{
    public class AlarmLimitToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string alarmlimit = value as string;
                switch (alarmlimit)
                {
                    case "无效": return "I";
                    case "正常": 
                    case "高正常": return "N";
                    case "预警": 
                    case "高预警": return "PH";
                    case "警告": 
                    case "高警告": return "H";
                    case "危险": 
                    case "高危险": return "HH";
                    case "低正常": return "LN";
                    case "低预警": return "PL";
                    case "低警告": return "L";
                    case "低危险": return "LL";
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
