using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace AIC.Core
{
    public class ContentControlDragDropBehavior : Behavior<ContentControl>
{
        private bool _isMouseDown;
        private object _data;
        private Point _dragStartPosition;
        private bool _isDragging;
        private DragAdorner _dragAdorner;
        private InsertAdorner _insertAdorner;
        private const int DRAG_WAIT_COUNTER_LIMIT = 10;

        public ContentControlDragDropBehavior()
        {
            IsEnable = true;
            _isMouseDown = false;
            _isDragging = false;
        }

        protected override void OnAttached()
        {
            
            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(itemsControl_PreviewMouseLeftButtonDown);
            this.AssociatedObject.PreviewMouseMove += new MouseEventHandler(itemsControl_PreviewMouseMove);
            this.AssociatedObject.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(itemsControl_PreviewMouseLeftButtonUp);
            this.AssociatedObject.PreviewDrop += new DragEventHandler(itemsControl_PreviewDrop);
            this.AssociatedObject.PreviewQueryContinueDrag += new QueryContinueDragEventHandler(itemsControl_PreviewQueryContinueDrag);
            this.AssociatedObject.PreviewDragEnter += new DragEventHandler(itemsControl_PreviewDragEnter);
            this.AssociatedObject.PreviewDragOver += new DragEventHandler(itemsControl_PreviewDragOver);
            this.AssociatedObject.DragLeave += new DragEventHandler(itemsControl_DragLeave);
        }

        #region Properties

        public bool IsEnable { get; set; }
        public Type ItemType { get; set; }

        public DataTemplate DataTemplate { get; set; }

        public DataTemplateSelector DataTemplateSelector { get; set; }

        #endregion

        #region Button Events

        void itemsControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ResetState();
        }

        void itemsControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (IsEnable && _isMouseDown)
            {
                ContentControl contentControl = (ContentControl)sender;
                Point currentPosition = e.GetPosition(contentControl);
                if ((_isDragging == false) && (Math.Abs(currentPosition.X - _dragStartPosition.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(currentPosition.Y - _dragStartPosition.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    DragStarted(contentControl);
                }
            }
        }

        void itemsControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContentControl contentControl = (ContentControl)sender;
            Point p = e.GetPosition(contentControl);
            _data = contentControl.Content;// ContentControlHelp.GetDataObjectFromItemsControl(contentControl, p);
            if (_data != null)
            {
                _isMouseDown = true;
                _dragStartPosition = p;
            }
        }

        #endregion

        #region Drag Events

        void itemsControl_DragLeave(object sender, DragEventArgs e)
        {
            DetachAdorners();
            e.Handled = true;
        }

        void itemsControl_PreviewDragOver(object sender, DragEventArgs e)
        {
            ContentControl contentControl = (ContentControl)sender;
            if (e.Data.GetDataPresent(ItemType))
            {
                UpdateDragAdorner(e.GetPosition(contentControl));
            }
            e.Handled = true;
        }

        void itemsControl_PreviewDragEnter(object sender, DragEventArgs e)
        {
            ContentControl contentControl = (ContentControl)sender;
            if (e.Data.GetDataPresent(ItemType))
            {
                object data = e.Data.GetData(ItemType);
                InitializeDragAdorner(contentControl, data, e.GetPosition(contentControl));
                //InitializeInsertAdorner(contentControl, e);
                
            }
            e.Handled = true;
        }

        void itemsControl_PreviewQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (e.EscapePressed)
            {
                e.Action = DragAction.Cancel;
                ResetState();
                DetachAdorners();
                e.Handled = true;
            }
        }

        void itemsControl_PreviewDrop(object sender, DragEventArgs e)
        {
            ContentControl contentControl = (ContentControl)sender;
            DetachAdorners();
            e.Handled = true;
            if (e.Data.GetDataPresent(ItemType))
            {
                object itemToAdd = e.Data.GetData(ItemType);
                e.Effects = ((e.KeyStates & DragDropKeyStates.ControlKey) != 0) ?
                            DragDropEffects.Copy : DragDropEffects.Move;
                contentControl.Content = itemToAdd;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private bool selfDrop = false;

        #endregion

        #region Private Methods

        private void DragStarted(ContentControl contentControl)
        {

            if (_data.GetType() != ItemType && !_data.GetType().IsSubclassOf(ItemType))
            {
                return;
            }
            _isDragging = true;
            DataObject dObject = new DataObject(ItemType, _data);
            dObject.SetData("DropTarget", "ContentControlDragDropBehavior");
            DragDropEffects e = DragDrop.DoDragDrop(contentControl, dObject, DragDropEffects.Copy | DragDropEffects.Move);
            if ((e & DragDropEffects.Move) != 0)
            {
                var dragSource = dObject.GetData("DropTarget");
                if (dragSource == null || dragSource.ToString() != "ContentControlDragDropBehavior")
                {
                    contentControl.Content = null;
                }
            }
            ResetState();
        }

        private int FindInsertionIndex(ItemsControl itemsControl, DragEventArgs e)
        {
            UIElement dropTargetContainer = Helper.GetItemContainerFromPoint(itemsControl, e.GetPosition(itemsControl));
            if (dropTargetContainer != null)
            {
                int index = itemsControl.ItemContainerGenerator.IndexFromContainer(dropTargetContainer);
                if (Helper.IsPointInTopHalf(itemsControl, e))
                    return index;
                else
                    return index + 1;
            }
            return itemsControl.Items.Count;

        }

        private void ResetState()
        {
            _isMouseDown = false;
            _isDragging = false;
            _data = null;
        }

        private void InitializeDragAdorner(ContentControl itemsControl, object dragData, Point startPosition)
        {
            if (this.DataTemplate != null||this.DataTemplateSelector!=null)
            {
                if (_dragAdorner == null)
                {
                    var adornerLayer = AdornerLayer.GetAdornerLayer(itemsControl);
                    _dragAdorner = new DragAdorner(dragData, DataTemplate, DataTemplateSelector, itemsControl, adornerLayer);
                    _dragAdorner.UpdatePosition(startPosition.X, startPosition.Y);
                }
            }
        }

        private void UpdateDragAdorner(Point currentPosition)
        {
            if (_dragAdorner != null)
            {
                _dragAdorner.UpdatePosition(currentPosition.X, currentPosition.Y);
            }
        }

        private void InitializeInsertAdorner(ItemsControl itemsControl, DragEventArgs e)
        {
            if (_insertAdorner == null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(itemsControl);
                UIElement itemContainer = Helper.GetItemContainerFromPoint(itemsControl, e.GetPosition(itemsControl));
                if (itemContainer != null)
                {
                    bool isPointInTopHalf = Helper.IsPointInTopHalf(itemsControl, e);
                    bool isItemsControlOrientationHorizontal = Helper.IsItemControlOrientationHorizontal(itemsControl);
                    _insertAdorner = new InsertAdorner(isPointInTopHalf, isItemsControlOrientationHorizontal, itemContainer, adornerLayer);
                }
            }
        }

        private void DetachAdorners()
        {
            if (_insertAdorner != null)
            {
                _insertAdorner.Destroy();
                _insertAdorner = null;
            }
            if (_dragAdorner != null)
            {
                _dragAdorner.Destroy();
                _dragAdorner = null;
            }
        }

        #endregion
    }
}
