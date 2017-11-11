using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIC.ColorThemes
{
    public class TitleControlElement
    {


        public static double GetTitleWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(TitleWidthProperty);
        }

        public static void SetTitleWidth(DependencyObject obj, double value)
        {
            obj.SetValue(TitleWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for TitleWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleWidthProperty = DependencyProperty.RegisterAttached("TitleWidth", typeof(double), typeof(TitleControlElement), new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender | FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));

       // public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(TextElement), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender | FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));

    }
}
