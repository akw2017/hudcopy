using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AIC.PDA.Controls
{
    public class ItemIndicator : Decorator
    {
        private ListBox _itemsControl;


        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(ItemIndicator));

        public static readonly DependencyProperty ItemRectProperty =
            DependencyProperty.Register("ItemRect", typeof(Rect), typeof(ItemIndicator), new FrameworkPropertyMetadata(new Rect(), FrameworkPropertyMetadataOptions.AffectsRender));

        private Rect _prevRect;

        public Rect ItemRect
        {
            get { return (Rect)GetValue(ItemRectProperty); }
            set { SetValue(ItemRectProperty, value); }
        }

        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        protected override void OnInitialized(EventArgs e)
        {
            _itemsControl = TemplatedParent as ListBox;
            if(_itemsControl!=null)
            {
                _itemsControl.Loaded += _itemsControl_Loaded;
            }
        }

        private void _itemsControl_Loaded(object sender, RoutedEventArgs e)
        {
            _itemsControl.Loaded -= _itemsControl_Loaded;
            _itemsControl.SelectionChanged += _itemsControl_SelectionChanged;
            _prevRect = GetItemRect();
            SetValue(ItemRectProperty, _prevRect);
        }

        private void _itemsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Storyboard sb = new Storyboard();
            Rect newRect = GetItemRect();
            RectAnimation anim = new RectAnimation(_prevRect, newRect, new Duration(TimeSpan.FromMilliseconds(250)));
            Storyboard.SetTargetProperty(anim, new PropertyPath("ItemRect"));
            sb.FillBehavior = FillBehavior.Stop;
            sb.Children.Add(anim);
            sb.Begin(this);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            _prevRect = GetItemRect();
            SetValue(ItemRectProperty, _prevRect);

            return arrangeSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (_itemsControl == null)
            {
                dc.DrawText(new FormattedText("This panel can only be present inside the ControlTemplate for a ListBox", CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 14, Brushes.Red), new Point());
                return;
            }
            Rect rect = (Rect)GetValue(ItemRectProperty);
            dc.DrawRectangle(null, new Pen(BorderBrush, 1), rect);
        }

        private Rect GetItemRect()
        {
            ListBoxItem item = _itemsControl.ItemContainerGenerator.ContainerFromItem(_itemsControl.SelectedItem) as ListBoxItem;
            if (item != null)
            {
                var point = item.TranslatePoint(new Point(), _itemsControl);
                Rect itemRect = new Rect(new Point(point.X + item.Padding.Left, point.Y + item.Padding.Top), new Size(item.ActualWidth - item.Padding.Left - item.Padding.Right, item.ActualHeight - item.Padding.Top - item.Padding.Bottom));
                return itemRect;
            }
            else
            {
                return new Rect();
            }         
        }
    }
}
