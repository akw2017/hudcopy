/*
 * MathConverter and accompanying samples are copyright (c) 2011 by Ivan Krivyakov
 * ivan [at] ikriv.com
 * They are distributed under the Apache License http://www.apache.org/licenses/LICENSE-2.0.html
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace AIC.Core
{
    public class IndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            dynamic item = values[0];
            dynamic list = values[1];
            return list.IndexOf(item) + 1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}