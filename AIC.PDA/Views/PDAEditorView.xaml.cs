using AIC.PDA.ViewModels;
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
    /// Interaction logic for PDAEditorView.xaml
    /// </summary>
    public partial class PDAEditorView : UserControl
    {
        public PDAEditorView()
        {
            InitializeComponent();
            Loaded += PDAEditorView_Loaded;
        }

        private void PDAEditorView_Loaded(object sender, RoutedEventArgs e)
        {
            //  ((PDAEditorViewModel)DataContext).ShowAsCard = true;
            Loaded -= PDAEditorView_Loaded;
            VisualStateManager.GoToState(this, "PDAState", false);
        }

        private void PDAVisual_Loaded(object sender, RoutedEventArgs e)
        {
            //PDAVisual.Loaded -= PDAVisual_Loaded;
            //VisualStateManager.GoToState(this, "CardList",false);
        }
    }
}
