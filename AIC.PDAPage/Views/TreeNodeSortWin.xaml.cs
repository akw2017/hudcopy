using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.PDAPage.Models;
using AIC.PDAPage.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIC.PDAPage.Views
{
    /// <summary>
    /// Interaction logic for MainControlCardCopyWin.xaml
    /// </summary>
    public partial class TreeNodeSortWin : MetroWindow
    {
        public delegate void TransferParaData(ObservableCollection<OrganizationSort> organizationSorts);
        public event TransferParaData Parachanged;

        private ObservableCollection<OrganizationSort> _OrganizationSorts = new ObservableCollection<OrganizationSort>();

        public TreeNodeSortWin(ObservableCollection<OrganizationSort> organizationSorts)
        { 
            InitializeComponent();

            _OrganizationSorts = organizationSorts;
            LBoxSort.ItemsSource = _OrganizationSorts;
            _OrganizationSorts.CollectionChanged += ItemSortsOnCollectionChanged;
        }

        private void ItemSortsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                for (int i = e.OldStartingIndex; i < _OrganizationSorts.Count; i++)
                {
                    _OrganizationSorts[i].Sort_No = i;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                for (int i = e.NewStartingIndex; i < _OrganizationSorts.Count; i++)
                {
                    _OrganizationSorts[i].Sort_No = i;
                }
            }
        }


        private void LBoxSort_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(LBoxSort);
            HitTestResult result = VisualTreeHelper.HitTest(LBoxSort, pos);
            if (result == null)
            {
                return;
            }
            var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
            if (listBoxItem == null)// || listBoxItem.Content != LBoxSort.SelectedItem)
            {
                return;
            }
            DataObject dataObj = new DataObject(listBoxItem.Content as OrganizationSort);
            DragDrop.DoDragDrop(LBoxSort, dataObj, DragDropEffects.Move);

        }

        private void LBoxSort_OnDrop(object sender, DragEventArgs e)
        {
            var pos = e.GetPosition(LBoxSort);
            var result = VisualTreeHelper.HitTest(LBoxSort, pos);
            if (result == null)
            {
                return;
            }
            //查找元数据
            var sourceItemSort = e.Data.GetData(typeof(OrganizationSort)) as OrganizationSort;
            if (sourceItemSort == null)
            {
                return;
            }
            //查找目标数据
            var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
            if (listBoxItem == null)
            {
                return;
            }
            var targetItemSort = listBoxItem.Content as OrganizationSort;
            if (ReferenceEquals(targetItemSort, sourceItemSort))
            {
                return;
            }
            _OrganizationSorts.Remove(sourceItemSort);
            _OrganizationSorts.Insert(_OrganizationSorts.IndexOf(targetItemSort) + 1, sourceItemSort);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Parachanged(_OrganizationSorts);
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
   

    internal static class Utils
    {
        //根据子元素查找父元素
        public static T FindVisualParent<T>(DependencyObject obj) where T : class
        {
            while (obj != null)
            {
                if (obj is T)
                    return obj as T;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }
    }

}
