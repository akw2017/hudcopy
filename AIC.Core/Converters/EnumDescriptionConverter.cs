using AIC.CoreType;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Data;

namespace AIC.Core
{
    public class EnumDescriptionConverter : IValueConverter
    {
        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDescriptionConverter"/> class.
        /// </summary>
        public EnumDescriptionConverter()
        {
        }

        #endregion

        #region IValueConverter

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if (value is Enum)
                {
                    return GetDescription((Enum)value);
                }
                else
                {
                    return value.ToString();
                }
            }
            else
            {
                return string.Empty;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helpers

        public static string GetDescription(Enum iValue)
        {
            FieldInfo fi = iValue.GetType().GetField(iValue.ToString());

            if (fi != null)
            {
                var attributes = (EnumDescription[])fi.GetCustomAttributes(typeof(EnumDescription), false);

                if (attributes.Length == 0)
                {
                    var descriptions = (DescriptionAttribute[])fi.
                        GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (descriptions.Length > 0)
                    {
                        return descriptions[0].Description;
                    }
                }

                return ((attributes.Length > 0) &&
                            (!String.IsNullOrEmpty(attributes[0].GetDescription())))
                        ? attributes[0].GetDescription()
                        : iValue.ToString();
            }

            return null;
        }

        #endregion
    }
}
