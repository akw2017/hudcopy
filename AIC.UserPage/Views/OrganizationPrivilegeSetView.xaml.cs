using AIC.Core.UserManageModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.CloseTabControl;

namespace AIC.UserPage.Views
{
    /// <summary>
    /// Interaction logic for ServerSetView.xaml
    /// </summary>
    public partial class OrganizationPrivilegeSetView : UserControl, ICloseable
    {
        public OrganizationPrivilegeSetView()
        {
            InitializeComponent();

            var menu = MenuManageList.GetMenu("menuOrganizationManage");
            this.Closer = new CloseableHeader("menuOrganizationManage", menu.Name, true, menu.IconPath);
        }
        public CloseableHeader Closer { get; private set; }

        private void chkbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk != null)
            {
                FrameworkElement el = datagrid.Columns.Last().GetCellContent(datagrid.SelectedItem);
                DataGridRow row = DataGridRow.GetRowContainingElement(el.Parent as FrameworkElement);
                row.DetailsVisibility = chk.IsChecked == false ? Visibility.Collapsed : Visibility.Visible;
                //if (chk.Name == "chkbox")
                //{
                //    datagrid.ClearDetailsVisibilityForItem(row);
                //    if (chk.IsChecked == true)
                //    {
                //        datagrid.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Visible;
                //    }
                //    else
                //    {
                //        datagrid.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
                //    }                   
                //}
                //else
                //{

                //row.DetailsVisibility = chk.IsChecked == false ? Visibility.Collapsed : Visibility.Visible;
                //datagrid.ClearDetailsVisibilityForItem(row);
                              
            }
        }
    }
}
