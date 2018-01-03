using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.PDAPage.Models;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace AIC.PDAPage.Views
{
    /// <summary>
    /// Interaction logic for WireMatchingCardWin.xaml
    /// </summary>
    public partial class WireMatchingCardWin : MetroWindow
    {
        public delegate void TransferParaData(ICard i_card);
        public event TransferParaData Parachanged;

        WireMatchingCard Card = new WireMatchingCard();
        public WireMatchingCardWin(ICard i_card)
        {
            InitializeComponent();

            //Card = ClassCopyHelper.DeepCopy<WireMatchingCard>(card as WireMatchingCard);
            CardCopyHelper.WireMatchingCardLeftCopyToRight(i_card as WireMatchingCard, Card);
            this.DataContext = Card;

            btnOK.IsEnabled = false;
            Card.PropertyChanged += Card_PropertyChanged;
            this.Closed += MetroWindow_Closed;
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Card.PropertyChanged -= Card_PropertyChanged;
        }

        private void Card_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            btnOK.IsEnabled = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {            
            Parachanged(Card);
            //this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
