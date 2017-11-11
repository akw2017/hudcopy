using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIC.ColorThemes
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AIC.ColorThemes"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AIC.ColorThemes;assembly=AIC.ColorThemes"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class TitleControl : HeaderedContentControl
    {
        static TitleControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleControl), new FrameworkPropertyMetadata(typeof(TitleControl)));
        }

        public TitleControl()
        {

        }



        public double TitleWidth
        {
            get { return (double)GetValue(TitleWidthProperty); }
            set { SetValue(TitleWidthProperty, value); }
        }


        public static double GetTitleWidthProperty(DependencyObject obj)
        {
            return (double)obj.GetValue(TitleWidthProperty);
        }

        public static void SetTitleWidthProperty(DependencyObject obj, double value)
        {
            obj.SetValue(TitleWidthProperty, value);
        }


        public static readonly DependencyProperty TitleWidthProperty = TitleControlElement.TitleWidthProperty.AddOwner(typeof(TitleControl));



        //public double TitleWidth
        //{
        //    get { return (double)GetValue(TitleWidthProperty); }
        //    set { SetValue(TitleWidthProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for TitleWidth.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TitleWidthProperty =
        //    DependencyProperty.Register("TitleWidth", typeof(double), typeof(TitleControl), new PropertyMetadata(20.0));


    }
}
