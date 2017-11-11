using AIC.CoreType;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AIC.PDA.Controls
{
    public class InSignalCategoryHelper : DependencyObject
    {
        public static readonly DependencyProperty InSignalCategoryProperty =
            DependencyProperty.RegisterAttached("InSignalCategory", typeof(InSignalCategory), typeof(InSignalCategoryHelper), new PropertyMetadata(InSignalCategory.Acceleration, OnInSignalCategoryChanged));

        private static void OnInSignalCategoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as EnumItemsControl;
            if (control != null)
            { 
                if (control.Source == typeof(Integration))
                {
                    var inSignalCategory = GetInSignalCategory(control);
                    var _enum = Enum.GetValues(control.Source).OfType<Integration>().ToList();
                    if (inSignalCategory == InSignalCategory.Acceleration || inSignalCategory == InSignalCategory.Cach)
                    {
                        control.ItemsSource = _enum;
                    }
                    else if (inSignalCategory == InSignalCategory.Speed)
                    {
                        _enum.RemoveAt(_enum.Count - 1);
                        control.ItemsSource = _enum;
                    }
                    else if(inSignalCategory == InSignalCategory.Displacement)
                    {
                        _enum.RemoveAt(_enum.Count - 1);
                        _enum.RemoveAt(_enum.Count - 1);
                        control.ItemsSource = _enum;
                    }
                }

            }
        }

        public static InSignalCategory GetInSignalCategory(DependencyObject obj)
        {
            return (InSignalCategory)obj.GetValue(InSignalCategoryProperty);
        }

        public static void SetInSignalCategory(DependencyObject obj, InSignalCategory value)
        {
            obj.SetValue(InSignalCategoryProperty, value);
        }
    }
}
