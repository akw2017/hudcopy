using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiagramDesigner
{
    /// <summary>
    /// Interaction logic for DiagramControl.xaml
    /// </summary>
    public partial class DiagramControl : UserControl
    {
        public DiagramControl()
        {
            InitializeComponent();

        }


        public static DataTemplateSelector GetItemTemplateSelector(DependencyObject obj)
        {
            return (DataTemplateSelector)obj.GetValue(ItemTemplateSelectorProperty);
        }

        public static void SetItemTemplateSelector(DependencyObject obj, DataTemplateSelector value)
        {
            obj.SetValue(ItemTemplateSelectorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ItemTemplateSelector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateSelectorProperty =
            DependencyProperty.RegisterAttached("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(DiagramControl), new PropertyMetadata(null));







        public static object GetSelectedItem(DependencyObject obj)
        {
            return (object)obj.GetValue(SelectedItemProperty);
        }

        public static void SetSelectedItem(DependencyObject obj, object value)
        {
            obj.SetValue(SelectedItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(DiagramControl), new PropertyMetadata(null));



    }
}
