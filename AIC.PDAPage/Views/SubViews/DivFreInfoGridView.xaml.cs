using AIC.Core.Models;
using AIC.PDAPage.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIC.PDAPage.Views.SubViews
{
    /// <summary>
    /// Interaction logic for DivFreInfoGridView.xaml
    /// </summary>
    public partial class DivFreInfoGridView : UserControl
    {
        public DivFreInfoGridView()
        {
            InitializeComponent();
        }

        private void table_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DivFreInfo info = table.SelectedItem as DivFreInfo;
            if (info != null)
            {
                DivFreInfoWin win = new DivFreInfoWin(info);
                win.Parachanged += Win_Parachanged;
                win.ShowDialog();
            }
        }

        private void Win_Parachanged(DivFreInfo info)
        {
            var items = table.ItemsSource as ObservableCollection<DivFreInfo>;
            items[table.SelectedIndex] = info;
            table.ItemsSource = null;
            table.ItemsSource = items;

        }
    }
}
