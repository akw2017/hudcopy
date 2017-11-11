using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Linq;
using System.Collections.Generic;

namespace DiagramDesigner
{
    public class ResizeThumb : Thumb
    {
        public ResizeThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
        }
        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var element = sender as DependencyObject;
            while (element != null && !(element is ContentPresenter))
            {
                element = VisualTreeHelper.GetParent(element);
            }

            ContentPresenter ItemContainer =  element as ContentPresenter;

            if (ItemContainer != null)
            {
                double deltaVertical, deltaHorizontal;

                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        deltaVertical = Math.Min(-e.VerticalChange, ItemContainer.ActualHeight - ItemContainer.MinHeight);
                        ItemContainer.Height -= deltaVertical;
                        break;
                    case VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.VerticalChange, ItemContainer.ActualHeight - ItemContainer.MinHeight);
                        Canvas.SetTop(ItemContainer, Canvas.GetTop(ItemContainer) + deltaVertical);
                        ItemContainer.Height -= deltaVertical;
                        break;
                    default:
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange, ItemContainer.ActualWidth - ItemContainer.MinWidth);
                        Canvas.SetLeft(ItemContainer, Canvas.GetLeft(ItemContainer) + deltaHorizontal);
                        ItemContainer.Width -= deltaHorizontal;
                        break;
                    case HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange, ItemContainer.ActualWidth - ItemContainer.MinWidth);
                        ItemContainer.Width -= deltaHorizontal;
                        break;
                    default:
                        break;
                }
            }

            e.Handled = true;
        }
    }
}
