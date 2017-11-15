using AIC.OnLineDataPage.Models;
using System.Windows;
using System.Windows.Controls;

namespace AIC.OnLineDataPage.Models
{
    public class ValueTypeTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ImageValueDesigner)
            {
                return ImageTemplate;
            }
            return DefaultTemplate;
        }

        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate ImageTemplate { get; set; }
        
    }
}
