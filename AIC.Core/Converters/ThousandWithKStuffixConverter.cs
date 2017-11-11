using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace AIC.Core
{
    public class ThousandWithKStuffixConverter : IValueConverter
    {
        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    return ((bool)value == true) ? 0 : 1;   
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    return ((int)value == 0) ? true : false;
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = (double)value;

            if (result >= 100000000)
                return (result / 1000000).ToString("#,0MHz");

            else if (result >= 10000000)
                return (result / 1000000).ToString("0.#") + "MHz";

            else if (result >= 100000)
                return (result / 1000).ToString("#,0KHz");

            else if (result >= 1000)
            {
                var item = (result / 1000).ToString("0.#") + "KHz";
                return item;
            }
            else
            {
                return result.ToString("0.#") + "Hz";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string result = ((string)value).Replace("Hz", "");
            if (result.Contains("K"))
            {
                string temp = result.Replace("K", "");
                int asdasd = int.Parse(temp) * 1000;
                return asdasd;
            }
            else {
                int asdasd = int.Parse(result);
                return asdasd;
            }
        }
    }

}
