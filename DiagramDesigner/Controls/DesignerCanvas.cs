using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Linq;

namespace DiagramDesigner
{
    public class DesignerCanvas : Canvas
    {
        private ConnectorViewModel partialConnection;
        private Connector sourceConnector;
        private Connector sinkConnector;
        private Point? rubberbandSelectionStartPoint = null;

        public DesignerCanvas()
        {
            this.AllowDrop = true;
        }

        public void BringToFront(UIElement element)
        {
           // this.UpdateZOrder(element, true);
        }

        public void SendToBack(UIElement element)
        {
            this.UpdateZOrder(element, false);
        }

        private void UpdateZOrder(UIElement element, bool bringToFront)
        {
            #region Safety Check

            if (element == null)
                throw new ArgumentNullException("element");

            if (!base.Children.Contains(element))
                throw new ArgumentException("Must be a child element of the Canvas.", "element");

            #endregion // Safety Check

            #region Calculate Z-Indici And Offset

            // Determine the Z-Index for the target UIElement.
            int elementNewZIndex = -1;
            if (bringToFront)
            {
                foreach (UIElement elem in base.Children)
                    if (elem.Visibility != Visibility.Collapsed)
                        ++elementNewZIndex;
            }
            else
            {
                elementNewZIndex = 0;
            }

            // Determine if the other UIElements' Z-Index 
            // should be raised or lowered by one. 
            int offset = (elementNewZIndex == 0) ? +1 : -1;

            int elementCurrentZIndex = Canvas.GetZIndex(element);

            #endregion // Calculate Z-Indici And Offset

            #region Update Z-Indici

            // Update the Z-Index of every UIElement in the Canvas.
            foreach (UIElement childElement in base.Children)
            {
                if (childElement == element)
                    Canvas.SetZIndex(element, elementNewZIndex);
                else
                {
                    int zIndex = Canvas.GetZIndex(childElement);

                    // Only modify the z-index of an element if it is  
                    // in between the target element's old and new z-index.
                    if (bringToFront && elementCurrentZIndex < zIndex ||
                        !bringToFront && zIndex < elementCurrentZIndex)
                    {
                        Canvas.SetZIndex(childElement, zIndex + offset);
                    }
                }
            }

            #endregion // Update Z-Indici
        }

        public Connector SourceConnector
        {
            get { return sourceConnector; }
            set
            {
                if (sourceConnector != value)
                {
                    sourceConnector = value;
                    FullyCreatedConnectorInfo sourceDataItem = sourceConnector.DataContext as FullyCreatedConnectorInfo;

                    Rect rectangleBounds = sourceConnector.TransformToVisual(this).TransformBounds(new Rect(sourceConnector.RenderSize));
                    Point point = new Point(rectangleBounds.Left + (rectangleBounds.Width / 2),
                                            rectangleBounds.Bottom + (rectangleBounds.Height / 2));
                    partialConnection = new ConnectorViewModel(sourceDataItem, new PartCreatedConnectionInfo(point));
                    sourceDataItem.DataItem.Parent.AddItemCommand.Execute(partialConnection);
                }
            }
        }

        //protected override void OnMouseDown(MouseButtonEventArgs e)
        //{
        //    base.OnMouseDown(e);

        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        //if we are source of event, we are rubberband selecting
        //        if (e.Source == this)
        //        {
        //            // in case that this click is the start for a 
        //            // drag operation we cache the start point
        //            rubberbandSelectionStartPoint = e.GetPosition(this);

        //            //IDiagramViewModel vm = (this.DataContext as IDiagramViewModel);
        //            //if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
        //            //{
        //            //    vm.ClearSelectedItemsCommand.Execute(null);
        //            //}
        //            e.Handled = true;
        //        }
        //    }
        //}

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (sourceConnector != null)
            {
                FullyCreatedConnectorInfo sourceDataItem = sourceConnector.DataContext as FullyCreatedConnectorInfo;
                if (sourceConnector != null && sinkConnector != null && sourceConnector != sinkConnector)
                {
                    FullyCreatedConnectorInfo sinkDataItem = sinkConnector.DataContext as FullyCreatedConnectorInfo;

                    int indexOfLastTempConnection = sinkDataItem.DataItem.Parent.Items.Count - 1;
                    sinkDataItem.DataItem.Parent.RemoveItemCommand.Execute(
                        sinkDataItem.DataItem.Parent.Items[indexOfLastTempConnection]);
                    sinkDataItem.DataItem.Parent.AddItemCommand.Execute(new ConnectorViewModel(sourceDataItem, sinkDataItem));
                    sinkDataItem.DataItem.IsDragConnectionOver = false;
                }
                else
                {
                    //Need to remove last item as we did not finish drawing the path
                    int indexOfLastTempConnection = sourceDataItem.DataItem.Parent.Items.Count - 1;
                    sourceDataItem.DataItem.Parent.RemoveItemCommand.Execute(
                    sourceDataItem.DataItem.Parent.Items[indexOfLastTempConnection]);
                }
            }

            if (HitDesignerItem != null)
            {
                this.HitDesignerItem.IsDragConnectionOver = false;
            }
            sourceConnector = null;
            sinkConnector = null;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if(SourceConnector != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point currentPoint = e.GetPosition(this);
                    partialConnection.SinkConnectorInfo = new PartCreatedConnectionInfo(currentPoint);
                    HitTesting(currentPoint);
                }
            }
            //else
            //{
            //    // if mouse button is not pressed we have no drag operation, ...
            //    if (e.LeftButton != MouseButtonState.Pressed)
            //        rubberbandSelectionStartPoint = null;

            //    // ... but if mouse button is pressed and start
            //    // point value is set we do have one
            //    if (this.rubberbandSelectionStartPoint.HasValue)
            //    {
            //        // create rubberband adorner
            //        AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
            //        if (adornerLayer != null)
            //        {
            //            RubberbandAdorner adorner = new RubberbandAdorner(this, rubberbandSelectionStartPoint);
            //            if (adorner != null)
            //            {
            //                adornerLayer.Add(adorner);
            //            }
            //        }
            //    }
            //}
            e.Handled = true;
        }


        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();

            foreach (UIElement element in this.InternalChildren)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                //measure desired size for each child
                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }
            // add margin 
            //size.Width += 10;
           // size.Height += 10;
            return size;
        }

        private DesignerItemViewModelBase hitDesignerItem;
        private DesignerItemViewModelBase HitDesignerItem
        {
            get { return hitDesignerItem; }
            set
            {
                if (hitDesignerItem != value)
                {
                    if (hitDesignerItem != null)
                        hitDesignerItem.IsDragConnectionOver = false;

                    hitDesignerItem = value;

                    if (hitDesignerItem != null)
                        hitDesignerItem.IsDragConnectionOver = true;
                }
            }
        }

        private void HitTesting(Point hitPoint)
        {
            DependencyObject hitObject = this.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null &&
                    hitObject.GetType() != typeof(DesignerCanvas))
            {
                if (hitObject is Connector)
                {
                    sinkConnector = hitObject as Connector;
                    HitDesignerItem = (sinkConnector.DataContext as FullyCreatedConnectorInfo).DataItem;
                    return;
                }

                else if (hitObject is ContentPresenter && ((FrameworkElement)hitObject).DataContext is DesignerItemViewModelBase)
                {
                    HitDesignerItem = ((FrameworkElement)hitObject).DataContext as DesignerItemViewModelBase;
                    return;
                }
                hitObject = VisualTreeHelper.GetParent(hitObject);
            }
            //sinkConnector = null;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            if (dragObject != null)
            {
                (DataContext as IDiagramViewModel).ClearSelectedItemsCommand.Execute(null);
                Point position = e.GetPosition(this);
                DesignerItemViewModelBase itemBase = (DesignerItemViewModelBase)Activator.CreateInstance(dragObject.ContentType);
                itemBase.Left = Math.Max(0, position.X - itemBase.ItemWidth / 2);
                itemBase.Top = Math.Max(0, position.Y - itemBase.ItemHeight / 2);
                itemBase.IsSelected = true;
                (DataContext as IDiagramViewModel).AddItemCommand.Execute(itemBase);
            }
            e.Handled = true;
        }
    }
}
