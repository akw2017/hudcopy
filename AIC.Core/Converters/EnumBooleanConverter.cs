using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows;

namespace AIC.Core
{    
    public class EnumBooleanConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
            {
                return DependencyProperty.UnsetValue;
            }
            if (Enum.IsDefined(value.GetType(), value) == false)
            {
                return DependencyProperty.UnsetValue;
            }
            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isChecked = value as bool?;
            if (isChecked == null)
            {
                return DependencyProperty.UnsetValue;
            }
            string parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;
            if (isChecked == true)
            {
                return Enum.Parse(targetType, parameterString);
            }
            return null;

            //object result;
            //string parameterString = parameter as string;
            //if (parameterString == null)
            //    return DependencyProperty.UnsetValue;
            //result = Enum.Parse(targetType, parameterString);
            //bool isTrue = (bool)value;
            //if (isTrue) {
            //    lastSelectedItem = result;
            //    return result;
            //}
            //else {
            //    return lastSelectedItem;
            //}
        }
        #endregion
    }
}
