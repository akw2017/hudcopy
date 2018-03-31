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
    /// Interaction logic for EddyCurrentDisplacementSlotWin.xaml
    /// </summary>
    public partial class EddyCurrentKeyPhaseSlotWin : MetroWindow
    {
        public delegate void TransferParaData(ISlot slot);
        public event TransferParaData Parachanged;

        EddyCurrentKeyPhaseSlot Slot = new EddyCurrentKeyPhaseSlot();
        public EddyCurrentKeyPhaseSlotWin(ISlot slot)
        {
            InitializeComponent();
            //Slot = ClassCopyHelper.DeepCopy<EddyCurrentKeyPhaseSlot>(slot as EddyCurrentKeyPhaseSlot);
            CardCopyHelper.SlotLeftCopyToRight(slot, Slot);
            this.DataContext = Slot;

            btnOK.IsEnabled = false;
            Slot.PropertyChanged += Slot_PropertyChanged;
            this.Closed += MetroWindow_Closed;
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Slot.PropertyChanged -= Slot_PropertyChanged;
        }

        private void Slot_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            btnOK.IsEnabled = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Slot.Error == "")
            {
                Parachanged(Slot);
            }
            //this.Close();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
