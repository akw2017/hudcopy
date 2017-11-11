using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DiagramDesigner
{
    public class DragThumb : Thumb
    {
        static DragThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DragThumb), new FrameworkPropertyMetadata(typeof(DragThumb)));
        }

        public DragThumb()
        {
            base.PreviewMouseLeftButtonDown += DragThumb_PreviewMouseLeftButtonDown;
            base.DragDelta += new DragDeltaEventHandler(DragThumb_DragDelta);
        }

        void DragThumb_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (BringToFront)
            {
                var parent = sender as DependencyObject;
                var element = sender as DependencyObject;

                while (element != null && !(element is DesignerCanvas))
                {
                    element = VisualTreeHelper.GetParent(element);
                }

                DesignerCanvas designer = element as DesignerCanvas;
                if (designer != null)
                {
                    while (parent != null && !designer.Children.Contains((UIElement)parent))
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                    }
                    designer.BringToFront((UIElement)parent);
                }
            }
        }

        private void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var element = sender as DependencyObject;
            while (element != null && !(element is DesignerCanvas))
            {
                element = VisualTreeHelper.GetParent(element);
            }

            DesignerCanvas designer = element as DesignerCanvas;
            DesignerItemViewModelBase designerItem = this.DataContext as DesignerItemViewModelBase;

            if (designerItem != null && designerItem.IsSelected && designer != null)
            {

                double minLeft = double.MaxValue;
                double minTop = double.MaxValue;

                // we only move DesignerItems
                var designerItems = designerItem.SelectedItems;

                foreach (DesignerItemViewModelBase item in designerItems.OfType<DesignerItemViewModelBase>())
                {
                    double left = item.Left;
                    double top = item.Top;
                    minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
                    minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

                    double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
                    double deltaVertical = Math.Max(-minTop, e.VerticalChange);
                    item.Left += deltaHorizontal;
                    item.Top += deltaVertical;
                }
                designer.InvalidateMeasure();
                e.Handled = true;
            }

            //else
            //{
            //    ImageViewModel imageVM = this.DataContext as ImageViewModel;
            //    if (imageVM != null && imageVM.IsSelected && designer != null)
            //    {
            //        double minLeft = double.MaxValue;
            //        double minTop = double.MaxValue;

            //        double left = imageVM.Left;
            //        double top = imageVM.Top;
            //        minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
            //        minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

            //        double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
            //        double deltaVertical = Math.Max(-minTop, e.VerticalChange);
            //        imageVM.Left += deltaHorizontal;
            //        imageVM.Top += deltaVertical;

            //        designer.InvalidateMeasure();
            //        e.Handled = true;
            //    }
            //}
        }

        public static readonly DependencyProperty BringToFrontProperty = DependencyProperty.Register("BringToFront", typeof(bool), typeof(DragThumb), new PropertyMetadata(true));
        public bool BringToFront
        {
            get { return (bool)GetValue(BringToFrontProperty); }
            set { SetValue(BringToFrontProperty, value); }
        }

        //  public static readonly 

        //  public bool BringToFront { get; set; }
    }
}
