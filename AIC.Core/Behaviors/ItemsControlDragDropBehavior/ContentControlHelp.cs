using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AIC.Core
{
    public class ContentControlHelp
    {
        public static object GetDataObjectFromItemsControl(ContentControl contentControl, Point p)
        {
            UIElement element = contentControl.InputHitTest(p) as UIElement;
            while (element != null)
            {
                if (element == contentControl)
                    return null;

                object data = contentControl.Content;//.ItemContainerGenerator.ItemFromContainer(element);
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
                else
                {
                    element = VisualTreeHelper.GetParent(element) as UIElement;
                }
            }
            return null;
        }
    }
}
