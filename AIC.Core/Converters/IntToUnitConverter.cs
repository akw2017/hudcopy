using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AIC.Core
{
    public class IntToUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case 0:
                    return "m/s^2";
                case 1:
                    return "mm/s";
                case 2:
                    return "um";
                case 3:
                    return "°C";
                case 4:
                    return "Pa";
                case 5:
                    return "RMP";
                case 6:
                    return "Unit";
                default:
                    break;
            }
            return Unit.Unit;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "m/s^2":
                    return 0;
                case "mm/s":
                    return 1;
                case "um":
                    return 2;
                case "°C":
                    return 3;
                case "Pa":
                    return 4;
                case "RMP":
                    return 5;
                case "Unit":
                    return 6;
                default:
                    break;
            }
            return 0;
        }
    }
}
