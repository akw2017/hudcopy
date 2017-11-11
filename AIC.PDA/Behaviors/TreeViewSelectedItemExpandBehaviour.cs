using AIC.Domain;
using System.Windows;
using System.Windows.Controls;

namespace AIC.PDA.Behaviors
{
    public class TreeViewSelectedItemExpandBehaviour
    {
        public static object GetTreeViewSelectedItem(DependencyObject obj)
        {
            return (object)obj.GetValue(TreeViewSelectedItemProperty);
        }

        public static void SetTreeViewSelectedItem(DependencyObject obj, object value)
        {
            obj.SetValue(TreeViewSelectedItemProperty, value);
        }

        public static readonly DependencyProperty TreeViewSelectedItemProperty =
            DependencyProperty.RegisterAttached("TreeViewSelectedItem", typeof(object), typeof(TreeViewSelectedItemExpandBehaviour), new PropertyMetadata(new object(), TreeViewSelectedItemChanged));

        static void TreeViewSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TreeView treeView = sender as TreeView;
            if (treeView == null)
            {
                return;
            }
            
            treeView.SelectedItemChanged -= new RoutedPropertyChangedEventHandler<object>(treeView_SelectedItemChanged);
            treeView.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(treeView_SelectedItemChanged);

            var treeViewItemModel = e.NewValue as TreeViewItemModel;
            if (treeViewItemModel != null)
            {
                treeViewItemModel.IsSelected = true;
                treeViewItemModel = treeViewItemModel.Parent;
                while (treeViewItemModel != null)
                {
                    treeViewItemModel.IsExpanded = true;
                    treeViewItemModel = treeViewItemModel.Parent;
                }
            }
        }

        static void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView treeView = sender as TreeView;
            SetTreeViewSelectedItem(treeView, e.NewValue);
        }
    }
}
