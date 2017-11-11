using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AIC.OnLineDataPage.Controls
{
    [TemplatePart(Name = "PART_DragThumb", Type = typeof(Thumb))]
    public class ResizableItem : ContentControl
    {
        private Thumb _dragThumb;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _dragThumb = GetTemplateChild("PART_DragThumb") as Thumb;
            _dragThumb.DragDelta += _dragThumb_DragDelta;
        }

        void _dragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (double.IsNaN(this.Height))
            {
                this.Height = this.ActualHeight;
            }
            if (this.Height + e.VerticalChange > this.MinHeight)
            {
                this.Height += e.VerticalChange;
            }
        }
    }
}
