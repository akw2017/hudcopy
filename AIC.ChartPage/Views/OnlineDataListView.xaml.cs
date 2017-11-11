using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.CloseTabControl;

namespace AIC.ChartPage.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class OnlineDataListView : UserControl//, ICloseable
    {
        private readonly IEventAggregator _eventAggregator;
        public MapView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            DoSomething ds = new DoSomething();
            this.webBrowser.ObjectForScripting = ds;
            //this.webBrowser.Navigate(new Uri(System.Environment.CurrentDirectory + @"/MyData/Htmls/BMapOffline.html", UriKind.RelativeOrAbsolute));//获取根目录的html文件  
            this.webBrowser.Navigate(new Uri(LocalAddress.MapHtmlUri));//获取根目录的html文件  
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ServerMarkEvent>().Subscribe(AddMarker);
            _eventAggregator.GetEvent<HideHtmlEvent>().Subscribe(HideHtml);
            _eventAggregator.GetEvent<RenameTreeItemEvent>().Subscribe(RenameEvent);

            //this.Closer = new CloseableHeader("首页", false);
            readDataTimer.Tick += new EventHandler(timeCycle);
            readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();

        //public CloseableHeader Closer { get; private set; }
        IList<ServerInfo> ServerInfo;
        private void AddMarker(IList<ServerInfo> serverInfo)
        {
            ServerInfo = serverInfo;
            readDataTimer.Start();
        }

        private void timeCycle(object sender, EventArgs e)
        {
            webBrowser.InvokeScript("removeOverlay", null);
            foreach (var server in ServerInfo)
            {
                if (server.LoginResult == true)
                {
                    object[] objs = new object[4] {
                    server.Longitude,
                    server.Latitude,
                    0,
                    server.Factory};
                    webBrowser.InvokeScript("addMarker", objs);
                }
            }
            readDataTimer.Stop();
        }

        private void HideHtml(int para)
        {
            if (para == 0)
            {
                //隐藏并用Image替代WebBrowser
                imageResource.Source = WebScreenshot.BrowserSnapShot(webBrowser);
                webBrowser.Visibility = Visibility.Hidden;
            }
            else
            {
                //恢复WebBrowser
                imageResource.Source = null;
                webBrowser.Visibility = Visibility.Visible;
            }
        }
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //恢复WebBrowser
            imageResource.Source = null;
            webBrowser.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            //隐藏并用Image替代WebBrowser
            imageResource.Source = WebScreenshot.BrowserSnapShot(webBrowser);
            webBrowser.Visibility = Visibility.Hidden;
        }


        private void RenameEvent()
        {
            ReNameTreeViewItem_Click(null, null);
        }

        TextBox tempTextBox;
        TextBlock tempTextBlock;
        TreeViewItem item;
        private string oldText;

        //对当前TreeViewItem进行重命名
        private void ReNameTreeViewItem_Click(object sender, RoutedEventArgs e)
        {
            //获取在TreeView.ItemTemplate中定义的TextBox控件
            tempTextBox = FindVisualChild<TextBox>(item as DependencyObject);
            //设置该TextBox的Visibility 属性为Visible
            tempTextBox.Visibility = Visibility.Visible;
            tempTextBox.Focus();
            oldText = tempTextBox.Text;

            tempTextBlock = FindVisualChild<TextBlock>(item as DependencyObject);
            tempTextBlock.Visibility = Visibility.Collapsed;
        }

        //获取当前TreeView的TreeViewItem
        public TreeViewItem GetParentObjectEx<TreeViewItem>(DependencyObject obj) where TreeViewItem : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if (parent is TreeViewItem)
                {
                    return (TreeViewItem)parent;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

        //获取ItemTemplate内部的各种控件
        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        //当TextBox失去焦点时发生此事件
        private void renametextbox_LostFous(object sender, RoutedEventArgs e)
        {
            tempTextBox.Visibility = Visibility.Collapsed;
            tempTextBlock.Visibility = Visibility.Visible;
        }

        private void TreeView_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //此处item定义的是一个类的成员变量，是一个TreeViewItem类型
            item = GetParentObjectEx<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (item != null)
            {
                //使当前节点获得焦点
                item.Focus();

                //系统不再处理该操作
                //e.Handled = true;
            }
            e.Handled = true;//防抖动
        }

        private void renametextbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tempTextBox.Visibility = Visibility.Collapsed;
                tempTextBlock.Visibility = Visibility.Visible;
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                tempTextBox.Visibility = Visibility.Collapsed;
                tempTextBlock.Visibility = Visibility.Visible;
                tempTextBox.Text = oldText;
                e.Handled = true;
            }

        }

        private void TreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _lastMouseDown = e.GetPosition(treeview);
                mousedown = true;
            }
        }

        private void TreeView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mousedown = false;
        }

        private void TreeView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (mousedown == false)//只有左键按下才能拖拽
                {
                    return;
                }
                Point currentPosition = e.GetPosition(treeview);

                // Note: This should be based on some accessibility number and not just 2 pixels
                if ((Math.Abs(currentPosition.X - _lastMouseDown.X) > 4.0) ||
                    (Math.Abs(currentPosition.Y - _lastMouseDown.Y) > 4.0))
                {
                    TreeItemViewModel selectedItem = (TreeItemViewModel)treeview.SelectedItem;
                    if ((selectedItem != null) && (selectedItem.Parent != null))
                    {
                        TreeViewItem container = GetContainerFromStuff(selectedItem);
                        if (container != null)
                        {
                            DragDropEffects finalDropEffect = DragDrop.DoDragDrop(container, selectedItem, DragDropEffects.Move);
                            if ((finalDropEffect == DragDropEffects.Move) && (_targetNode != null))
                            {
                                // A Move drop was accepted
                                if (_targetNode != selectedItem)//避免删除自己
                                {
                                    selectedItem.Parent.Children.Remove(selectedItem);
                                    _targetNode.Children.Add(selectedItem);
                                }
                                _targetNode = null;
                            }
                        }
                    }
                }
            }
        }

        private TreeViewItem GetContainerFromStuff(TreeItemViewModel stuff)
        {
            Stack<TreeItemViewModel> _stack = new Stack<TreeItemViewModel>();
            _stack.Push(stuff);
            TreeItemViewModel parent = stuff.Parent;

            while (parent != null)
            {
                _stack.Push(parent);
                parent = parent.Parent;
            }

            ItemsControl container = treeview;
            while ((_stack.Count > 0) && (container != null))
            {
                TreeItemViewModel top = _stack.Pop();
                container = (ItemsControl)container.ItemContainerGenerator.ContainerFromItem(top);
            }

            return container as TreeViewItem;
        }

        private TreeViewItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item.
            TreeViewItem container = element as TreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;
            }

            return container;
        }

        private void TheTreeView_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;


            // Verify that this is a valid drop and then store the drop target
            TreeViewItem container = GetNearestContainer(e.OriginalSource as UIElement);
            if (container != null)
            {
                TreeItemViewModel sourceNode = (TreeItemViewModel)e.Data.GetData(typeof(TreeItemViewModel));
                TreeItemViewModel targetNode = (TreeItemViewModel)container.Header;
                if ((sourceNode != null) && (targetNode != null))
                {
                    //if (!targetNode.MoreStuff.Contains(sourceNode))
                    {
                        _targetNode = targetNode;
                        e.Effects = DragDropEffects.Move;
                    }
                }
            }
        }

        private void TheTreeView_CheckDropTarget(object sender, DragEventArgs e)
        {
            if (!IsValidDropTarget(e.OriginalSource as UIElement))
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private bool IsValidDropTarget(UIElement target)
        {
            if (target != null)
            {
                TreeViewItem container = GetNearestContainer(target);

                // Ensure that the target is one of the root items
                //return ((container != null) && (((TreeItemViewModel)container.Header).Parent == null));
                return container != null;
            }

            return false;
        }

        private Point _lastMouseDown;
        private TreeItemViewModel _targetNode;
        private bool mousedown = false;
    }

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class DoSomething
    {
        public static string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public void ClickEvent(string str)
        {
            this.Name = str;
            MessageBox.Show("Welcome " + str);
        }
    }

    class WebScreenshot
    {
        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

        /// <summary>
        /// 对一个WebBrowser进行截图
        /// </summary>
        /// <param name="targetBrowser">我这里用的是Forms的WebBrowser，如果是wpf的，请自己改成Controls并调整参数</param>
        /// <returns></returns>
        public static ImageSource BrowserSnapShot(WebBrowser targetBrowser)
        {
            // 获取宽高
            int screenWidth = (int)targetBrowser.ActualWidth;
            int screenHeight = (int)targetBrowser.ActualHeight;

            IntPtr myIntptr = targetBrowser.Handle;
            int hwndInt = myIntptr.ToInt32();
            IntPtr hwnd = myIntptr;
            //创建图形
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bm);
            IntPtr hdc = g.GetHdc();

            //调用api 把hwnd的内容用图形绘制到hdc 如果你有代码洁癖 可以不使用api 使用g.CopyFromScreen，请自行研究
            bool result = PrintWindow(hwnd, hdc, 0);
            g.ReleaseHdc(hdc);
            g.Flush();


            if (result == true) //成功 转换并返回ImageSource
            {
                ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
                MemoryStream stream = new MemoryStream();
                bm.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return (ImageSource)imageSourceConverter.ConvertFrom(stream);
            }
            return null;
        }
    }
}
