using AIC.Core.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace AIC.DeviceDataPage.Views
{
    /// <summary>
    /// ExportPreviewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExportPreviewWindow : Window
    {
        private ExcelHelper _excelHelper;
        private DataView _dataGridSource;
        public ExportPreviewWindow()
        {
            InitializeComponent();
            _excelHelper = new ExcelHelper();
        }

        public ExportPreviewWindow(DataTable dt) :this()
        {
            _dataGridSource = dt.DefaultView;
            datagrid.ItemsSource = _dataGridSource;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.XLS)|*.xls"; ;
            if ((bool)(saveFileDialog.ShowDialog()))
            {
                string message = _excelHelper.SaveToExcel(saveFileDialog.FileName, _dataGridSource.Table);

#if XBAP
                MessageBox.Show(message ?? "保存成功","保存", MessageBoxButton.OK, MessageBoxImage.Information);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show(message ?? "保存成功", "保存", MessageBoxButton.OK, MessageBoxImage.Information);
#endif
            }
        }
    }
}
