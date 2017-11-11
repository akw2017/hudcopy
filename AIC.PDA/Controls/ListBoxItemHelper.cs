using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AIC.PDA.Controls
{
    public class ListBoxItemHelper: DependencyObject
    {

        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.RegisterAttached("ItemWidth", typeof(double), typeof(ListBoxItemHelper), new FrameworkPropertyMetadata(10.0, OnItemWidthChanged));

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.RegisterAttached("ItemHeight", typeof(double), typeof(ListBoxItemHelper), new FrameworkPropertyMetadata(10.0, OnItemHeightChanged));


        private static void OnItemWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ListBox;
            if (control != null)
            {
                if (control.IsLoaded)
                {
                    ArrangeItemWidth(control);
                }
                else
                {
                    RoutedEventHandler handler = null;
                    handler = delegate (object sender, RoutedEventArgs re)
                    {
                        control.Loaded -= handler;
                        ArrangeItemWidth(control);
                    };
                    control.Loaded += handler;
                }
            }
        }

        private static void OnItemHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ListBox;
            if (control != null)
            {
                if (control.IsLoaded)
                {
                    ArrangeItemHeight(control);
                }
                else
                {
                    RoutedEventHandler handler = null;
                    handler = delegate (object sender, RoutedEventArgs re)
                    {
                        control.Loaded -= handler;
                        ArrangeItemHeight(control);
                    };
                    control.Loaded += handler;
                }
            }
        }

        private static void ArrangeItemWidth(ListBox lb)
        {
            foreach (var item in lb.Items)
            {
                var listBoxItem = lb.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                if (listBoxItem != null)
                {
                    listBoxItem.Width = GetItemWidth(lb);
                }
            }
        }

        private static void ArrangeItemHeight(ListBox lb)
        {
            foreach (var item in lb.Items)
            {
                var listBoxItem = lb.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                if (listBoxItem != null)
                {
                    listBoxItem.Height = GetItemHeight(lb);
                }
            }
        }

        public static double GetItemWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(ItemWidthProperty);
        }

        public static void SetItemWidth(DependencyObject obj, double value)
        {
            obj.SetValue(ItemWidthProperty, value);
        }

        public static double GetItemHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(ItemHeightProperty);
        }

        public static void SetItemHeight(DependencyObject obj, double value)
        {
            obj.SetValue(ItemHeightProperty, value);
        }
    }
}
