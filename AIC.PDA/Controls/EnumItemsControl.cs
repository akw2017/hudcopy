using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIC.PDA.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:HUD"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:HUD;assembly=HUD"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:EnumItemsControl/>
    ///
    /// </summary>

    //[System.Windows.Markup.ContentProperty("Setters")]
    //[StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(EnumItemContainer))]
    public class EnumItemsControl : ListBox
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(Type), typeof(EnumItemsControl), new PropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(EnumItemsControl), new PropertyMetadata(string.Empty));


        static EnumItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumItemsControl), new FrameworkPropertyMetadata(typeof(EnumItemsControl)));
        }

        public Type Source
        {
            get { return (Type)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (EnumItemsControl)d;
            if (e.NewValue != null)
            {
                var _enum = Enum.GetValues(e.NewValue as Type);
                source.ItemsSource = _enum;
            }
        }
    }
}
