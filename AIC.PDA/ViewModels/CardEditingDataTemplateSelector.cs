
using AIC.Domain;
using System.Windows;
using System.Windows.Controls;

namespace AIC.PDA.ViewModels
{
    public class CardEditingDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement elemnt = container as FrameworkElement;
            if (item is AnalogInCardModel)
            {
                if (AnalogInCardDataTemplate != null)
                {
                    return AnalogInCardDataTemplate;
                }
            }
            else if (item is DigitTachometerCardModel)
            {
                if (DigitTachometerCardDataTemplate != null)
                {
                    return DigitTachometerCardDataTemplate;
                }
            }
            else if (item is EddyCurrentDisplacementCardModel)
            {
                if (EddyCurrentDisplacementCardDataTemplate != null)
                {
                    return EddyCurrentDisplacementCardDataTemplate;
                }
            }
            else if (item is EddyCurrentKeyPhaseCardModel)
            {
                if (EddyCurrentKeyPhaseCardDataTemplate != null)
                {
                    return EddyCurrentKeyPhaseCardDataTemplate;
                }
            }
            else if (item is EddyCurrentTachometerCardModel)
            {
                if (EddyCurrentTachometerCardDataTemplate != null)
                {
                    return EddyCurrentTachometerCardDataTemplate;
                }
            }
            else if (item is IEPECardModel)
            {
                if (IEPECardDataTemplate != null)
                {
                    return IEPECardDataTemplate;
                }
            }
            else if (item is RelayCardModel)
            {
                if (RelayCardDataTemplate != null)
                {
                    return RelayCardDataTemplate;
                }
            }
            return base.SelectTemplate(item, container);
        }

        public DataTemplate AnalogInCardDataTemplate { get; set; }
        public DataTemplate DigitTachometerCardDataTemplate { get; set; }
        public DataTemplate EddyCurrentDisplacementCardDataTemplate { get; set; }
        public DataTemplate EddyCurrentKeyPhaseCardDataTemplate { get; set; }
        public DataTemplate EddyCurrentTachometerCardDataTemplate { get; set; }
        public DataTemplate IEPECardDataTemplate { get; set; }
        public DataTemplate RelayCardDataTemplate { get; set; }
    }
}
