
using AIC.Domain;
using System.Windows;
using System.Windows.Controls;

namespace AIC.PDA.ViewModels
{
    public class CardParamDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement elemnt = container as FrameworkElement;
            if (item is IEPECardModel)
            {
                if (IEPECardDataTemplate != null)
                {
                    return IEPECardDataTemplate;
                }
            }
            else if (item is AnalogInCardModel)
            {
                if (AnalogInCardDataTemplate != null)
                {
                    return AnalogInCardDataTemplate;
                }
            }
            return base.SelectTemplate(item, container);
        }

        public DataTemplate IEPECardDataTemplate { get; set; }
        public DataTemplate AnalogInCardDataTemplate { get; set; }
    }
}
