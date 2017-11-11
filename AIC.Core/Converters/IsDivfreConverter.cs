using AIC.Core.SignalModels;
using System;
using System.Windows.Data;

namespace AIC.Core
{
    public class IsDivfreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {   
            if (value != null)
            {
                if (value is BaseDivfreSignal)
                {
                    return true;
                }
            }
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
