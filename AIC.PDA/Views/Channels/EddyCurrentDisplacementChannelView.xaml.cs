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
    /// Interaction logic for EddyCurrentDisplacementChannelView.xaml
    /// </summary>
    public partial class EddyCurrentDisplacementChannelView : UserControl, ICreateRegionManagerScope
    {
        public EddyCurrentDisplacementChannelView()
        {
            InitializeComponent();
        }

        private void DrawGraphClicked(object sender, RoutedEventArgs e)
        {

        }

        private void ClearFormulaClicked(object sender, RoutedEventArgs e)
        {

        }

        public bool CreateRegionManagerScope
        {
            get { return true; }
        }
    }
}
