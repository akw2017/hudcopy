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

namespace AIC.Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(i);
            }
            listbox.ItemsSource = list;
        }

        List<int> list = new List<int>();

        int index = 0;
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(index);
            index++;
        }
    }
}
