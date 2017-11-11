using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AIC.PDA.Controls
{
    //[StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(Card))]
    public class DataBoard : ListBox
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (this.Items != null)
            {
                //                if (this.Items.Count < 100)
                //                {
                //                    int index = this.ItemContainerGenerator.IndexFromContainer(element);
                //                    var lb = (UIElement)element;

                //                    TimeSpan waitTime = TimeSpan.FromMilliseconds(index * (500.0 / this.Items.Count));

                //                    lb.Opacity = 0.0;
                //                    DoubleAnimation anm = new DoubleAnimation();
                //                    anm.From = 0;
                //                    anm.To = 1;
                //                    anm.Duration = TimeSpan.FromMilliseconds(250);
                //                    anm.BeginTime = waitTime;

                //                    Storyboard storyda = new Storyboard();
                //                    storyda.Children.Add(anm);
                //                    Storyboard.SetTarget(storyda, lb);
                //#if NETFX_CORE
                //                    Storyboard.SetTargetProperty(storyda, "Opacity");
                //#else
                //                    Storyboard.SetTargetProperty(storyda, new PropertyPath(OpacityProperty));
                //#endif
                //                    storyda.Begin();
                //                }

                int index = this.ItemContainerGenerator.IndexFromContainer(element);
                var lb = (ListBoxItem)element;

                //var transform = lb.TransformToVisual((Visual)this.Parent).Transform(new Point());
                //Rect listboxItemBounds = VisualTreeHelper.GetDescendantBounds(lb);
                //listboxItemBounds.Offset(transform.X, transform.Y);

                //Point cursorPos = lb.TranslatePoint(new Point(100.0, 0.0), lb);
                //if(cursorPos.X>0)
                //{

                //}

                Point pointTransformToVisual = lb.TransformToVisual((Visual)this.Parent).Transform(new Point());

                Rect boundsRect = VisualTreeHelper.GetDescendantBounds(lb);
                if(boundsRect!=Rect.Empty)
                {
                    boundsRect.Offset(pointTransformToVisual.X, pointTransformToVisual.Y);
                }
             
            }

            base.PrepareContainerForItemOverride(element, item);
        }

        //protected override DependencyObject GetContainerForItemOverride()
        //{
        //    return new Card();
        //    // You can throw an exception here
        //}

        //protected override bool IsItemItsOwnContainerOverride(object item)
        //{
        //    return item is Card;
        //}
    }
}
