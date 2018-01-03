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
    /// Interaction logic for MainControlCardWin.xaml
    /// </summary>
    public partial class MainControlCardWin : MetroWindow
    {
        public delegate void TransferParaData(ICard i_card);
        public event TransferParaData Parachanged;

        MainControlCard Card = new MainControlCard();
        public MainControlCardWin(ICard i_card)
        {           
            InitializeComponent();
            //Card = ClassCopyHelper.DeepCopy<MainControlCard>(card as MainControlCard);
            CardCopyHelper.MainControlCardLeftCopyToRight(i_card as MainControlCard, Card);

            //Card = ClassCopyHelper.Mapper<MainControlCard, MainControlCard>(card as MainControlCard);
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
            if (Card.Error == "")
            {
                Parachanged(Card);
            }
            //this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();         
        }

       
    }
}
