using AIC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace AIC.PDA.Behaviors
{
    public class TextConflictBehavior //: Behavior<TextBox>
    {
        //protected override void OnAttached()
        //{

        //    base.OnAttached();
        //    AssociatedObject.TextChanged += AssociatedObject_TextChanged;
  
        //}

        //protected override void OnDetaching()
        //{
        //    AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
        //}

        //private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        //{
            
        //    var source = GetItemSource(this);
        //    var items = source as IEnumerable<TreeViewItemModel>;
        //    if (items == null) return;

            

        //    var treeItemModel = items.Where(o => o == AssociatedObject.DataContext).FirstOrDefault();
        //    if (treeItemModel == null) return;

        //    var nameText = GetName(this);
        //    if(string.IsNullOrEmpty(nameText))
        //    {

        //    }

        //    if (treeItemModel is TestPointTreeModel)
        //    {
        //        var newText = treeItemModel.Name.Value + ((TestPointTreeModel)treeItemModel).MSSN.Value;
        //        if (items.OfType<TestPointTreeModel>().Where(o => o.Name.Value + o.MSSN.Value == newText).Count() > 1)
        //        {
        //            MessageBox.Show("命名重复;" + "'" + newText + "'" + "已被使用");
        //            //var oldText = e.OldValue.ToString();
        //            //treeItemModel.Name.Value = oldText;
        //        }
        //    }
        //    else if (treeItemModel is EquipmentTreeModel)
        //    {
        //        var newText = treeItemModel.Name.Value + ((EquipmentTreeModel)treeItemModel).MSSN.Value;
        //        if (items.OfType<EquipmentTreeModel>().Where(o => o.Name.Value + o.MSSN.Value == newText).Count() > 1)
        //        {
        //            if(string.IsNullOrEmpty(newText))
        //            {

        //            }
        //           // MessageBox.Show("命名重复;" + "'" + newText + "'" + "已被使用");
        //            //var oldText = e.OldValue.ToString();
        //            //treeItemModel.Name.Value = oldText;
        //        }
        //    }
        //    else
        //    {
        //        var newText = treeItemModel.Name.Value;
        //        if (items.Where(o => o.Name.Value == newText).Count() > 1)
        //        {
        //            MessageBox.Show("命名重复;" + "'" + newText + "'" + "已被使用");
        //            //var oldText = e.OldValue.ToString();
        //            //treeItemModel.Name.Value = oldText;
        //        }
        //    }
        //}

        public static object GetItemSource(DependencyObject obj)
        {
            return (object)obj.GetValue(ItemSourceProperty);
        }

        public static void SetItemSource(DependencyObject obj, object value)
        {
            obj.SetValue(ItemSourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.RegisterAttached("ItemSource", typeof(object), typeof(TextConflictBehavior), new PropertyMetadata(null,OnItemsSourceChanged));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = GetItemSource(d);
            if(source!=null)
            {

            }
        }

        public static string GetName(DependencyObject obj)
        {
            return (string)obj.GetValue(NameProperty);
        }

        public static void SetName(DependencyObject obj, string value)
        {
            obj.SetValue(NameProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.RegisterAttached("Name", typeof(string), typeof(TextConflictBehavior), new PropertyMetadata(string.Empty,OnNameChanged));

        private static void OnNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = d as TextBox;
            var items = GetItemSource(textBox) as IEnumerable<TreeViewItemModel>;
            if (items == null) return;

            var treeItemModel = items.Where(o => o == textBox.DataContext).FirstOrDefault();
            if (treeItemModel == null) return;


            if(treeItemModel is TestPointTreeModel)
            {
                var newText = treeItemModel.Name.Value + ((TestPointTreeModel)treeItemModel).MSSN.Value;
                if (items.OfType<TestPointTreeModel>().Where(o => o.Name.Value+o.MSSN.Value == newText).Count() > 1)
                {
                    MessageBox.Show("命名重复;" + "'" + newText + "'" + "已被使用");
                    var oldText = e.OldValue.ToString();
                    treeItemModel.Name.Value = oldText;
                }
            }
            else if (treeItemModel is EquipmentTreeModel)
            {
                var newText = treeItemModel.Name.Value + ((EquipmentTreeModel)treeItemModel).MSSN.Value;
                if (items.OfType<EquipmentTreeModel>().Where(o => o.Name.Value + o.MSSN.Value == newText).Count() > 1)
                {
                    MessageBox.Show("命名重复;" + "'" + newText + "'" + "已被使用");
                    var oldText = e.OldValue.ToString();
                    treeItemModel.Name.Value = oldText;
                }
            }
            else 
            {
                var newText = treeItemModel.Name.Value;
                if (items.Where(o => o.Name.Value == newText).Count() > 1)
                {
                    MessageBox.Show("命名重复;" + "'" + newText + "'" + "已被使用");
                    var oldText = e.OldValue.ToString();
                    treeItemModel.Name.Value = oldText; 
                }
            }
        }
    }
}
