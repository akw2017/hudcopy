using AIC.Core;
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

namespace AIC.PDA.Views
{
    /// <summary>
    /// Interaction logic for ChannelTreeView.xaml
    /// </summary>
    public partial class ChannelTreeView : UserControl
    {
        public ChannelTreeView()
        {
            InitializeComponent();
        }

        private void RenameTreeViewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            testPointTreeView.BeginEdit();
        }
    }
}
