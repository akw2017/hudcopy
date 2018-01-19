using AICAir.ViewModels;
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

namespace AICAir.Views
{
    /// <summary>
    /// Interaction logic for ShellPage.xaml
    /// </summary>
    public partial class ShellPage : Page
    {
        public ShellPage()
        {
            InitializeComponent();
            Bootstrapper.ShellPage = this;//XBAP.1       
            this.DataContext = new ShellViewModel();     
        }      

        private void MainContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
