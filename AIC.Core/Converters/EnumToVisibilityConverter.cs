using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace AIC.Core
{
    /// <summary>
    /// Value converter that converts a boolean value to and from Visibility enumeration values.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(Visibility))]
    public class EnumToVisibilityConverter : IValueConverter
    {
        public bool Reverse { get; set; }

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
             if (parameter == null)
            {
                return DependencyProperty.UnsetValue;
            }
             if (Enum.IsDefined(parameter.GetType(), parameter) == false)
             {
                 return DependencyProperty.UnsetValue;
             }
            if (Enum.IsDefined(value.GetType(), value) == false)
            {
                return DependencyProperty.UnsetValue;
            }
            if (!Reverse)
            {
                return parameter.Equals(value) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return parameter.Equals(value) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
