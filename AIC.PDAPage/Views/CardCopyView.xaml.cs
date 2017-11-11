using AIC.Core.Models;
using AIC.PDAPage.Models;
using AIC.PDAPage.ViewModels;
using MahApps.Metro.Controls;
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

namespace AIC.PDAPage.Views
{
    /// <summary>
    /// Interaction logic for MainControlCardCopyWin.xaml
    /// </summary>
    public partial class CardCopyView : MetroWindow
    {
        public delegate void TransferParaData(List<CardParaCopyModel> paras);
        public event TransferParaData Parachanged;        

        public CardCopyView(List<CardParaCopyModel> paras)
        { 
            InitializeComponent();
           
            this.DataContext = new CardCopyViewModel(paras);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var paras = table.ItemsSource as ObservableCollection<CardParaCopyModel>;
            if (paras != null)
            {                
                Parachanged(new List<CardParaCopyModel>(paras));
            }
            //this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
