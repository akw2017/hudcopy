using AIC.Core.HardwareModels;
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
    public partial class WirelessReceiveCardWin : MetroWindow
    {
        public delegate void TransferParaData(ICard i_card);
        public event TransferParaData Parachanged;

        WirelessReceiveCard Card = new WirelessReceiveCard();
        public WirelessReceiveCardWin(ICard i_card)
        {
            InitializeComponent();

            //Card = ClassCopyHelper.DeepCopy<WireMatchingCard>(card as WireMatchingCard);
            CardCopyHelper.WirelessReceiveCardLeftCopyToRight(i_card as WirelessReceiveCard, Card);
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
