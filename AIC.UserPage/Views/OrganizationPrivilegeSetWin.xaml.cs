using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.Core.UserManageModels;
using AIC.Resources.Models;
using AIC.UserPage.Models;
using MahApps.Metro.Controls;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AIC.UserPage.Views
{
    /// <summary>
    /// Interaction logic for ServerSetWin.xaml
    /// </summary>
    public partial class OrganizationPrivilegeSetWin : MetroWindow
    {
        public delegate void TransferParaData(IList<T1_OrganizationPrivilege> organizationPrivilege, string oldname, string newname, ModifyStatus mode);
        public event TransferParaData Parachanged;

        public OrganizationPrivilegeSetWin(List<T1_OrganizationPrivilege> organizationPrivilege, List<T1_Organization> organization, string name, ModifyStatus mode)
        {
            InitializeComponent();

            T_OrganizationPrivilege = new List<T1_OrganizationPrivilege>();
            if (organizationPrivilege == null || organizationPrivilege.Count == 0)
            {               
                TreeGuid = Guid.NewGuid();              
            }
            else
            {
                organizationPrivilege.ForEach(p=> T_OrganizationPrivilege.Add(p.ShallowClone()));
                TreeGuid = organizationPrivilege[0].Guid;               
            }
            OrganizationTreeItems = new List<OrganizationTreeItemViewModel>();
            OrganizationProcess.GetOrganization(OrganizationTreeItems, organizationPrivilege, organization);
            TreeName = name;
            oldName = name;

            Mode = mode;
            switch (mode)
            {
                case ModifyStatus.Add:
                    {
                        this.Title = "添加菜单";                                              
                        break;
                    }
                case ModifyStatus.Edit:
                    {
                        this.Title = "修改菜单";                       
                        break;
                    }
                case ModifyStatus.Delete:
                    {
                        this.Title = "删除菜单";                       
                        treeview.IsEnabled = false;
                                                         
                        break;
                    }              
            }
            
            this.DataContext = this;
        }      

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            newname = TreeName;
            OrganizationProcess.GetOrganizationCheck(OrganizationTreeItems, T_OrganizationPrivilege, TreeGuid, TreeName);
            T_OrganizationPrivilege.Where(p => p.Name != TreeName).ToList().ForEach(p => p.Guid = new Guid());//删除多余无效的组织机构
            Parachanged(T_OrganizationPrivilege, oldName, newname, Mode);
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<T1_OrganizationPrivilege> T_OrganizationPrivilege { get; set; }
        public List<OrganizationTreeItemViewModel> OrganizationTreeItems { get; set; }
        public string TreeName { get; set; }

        private string oldName;

        private string newname;
        private Guid TreeGuid { get; set; }      
        ModifyStatus Mode { get; set; }                    
      

        /// <summary>
        /// 设置所有子项的选中状态
        /// </summary>
        /// <param name="isChecked"></param>
        public void SetChildrenChecked(bool isChecked)
        {
            foreach (OrganizationTreeItemViewModel child in OrganizationTreeItems)
            {
                child.IsChecked = true;
                child.SetChildrenChecked(true);
            }
        }
        /// <summary>
        /// 选中所有子项菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSelectAllChild_Click(object sender, RoutedEventArgs e)
        {
            if (treeview.SelectedItem != null)
            {
                OrganizationTreeItemViewModel tree = (OrganizationTreeItemViewModel)treeview.SelectedItem;
                tree.IsChecked = true;
                tree.SetChildrenChecked(true);
            }
        }

        /// <summary>
        /// 全部展开菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuExpandAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (OrganizationTreeItemViewModel tree in treeview.ItemsSource)
            {
                tree.IsExpanded = true;
                tree.SetChildrenExpanded(true);
            }
        }

        /// <summary>
        /// 全部折叠菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuUnExpandAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (OrganizationTreeItemViewModel tree in treeview.ItemsSource)
            {
                tree.IsExpanded = false;
                tree.SetChildrenExpanded(false);
            }
        }

        /// <summary>
        /// 全部选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (OrganizationTreeItemViewModel tree in treeview.ItemsSource)
            {
                tree.IsChecked = true;
                tree.SetChildrenChecked(true);
            }
        }

        /// <summary>
        /// 全部取消选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuUnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (OrganizationTreeItemViewModel tree in treeview.ItemsSource)
            {
                tree.IsChecked = false;
                tree.SetChildrenChecked(false);
            }
        }

        /// <summary>
        /// 鼠标右键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (item != null)
            {
                item.Focus();
                e.Handled = true;
            }
        }
        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }
    }
}
