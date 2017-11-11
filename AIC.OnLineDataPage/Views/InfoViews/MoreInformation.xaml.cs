using AIC.Core.SignalModels;
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

namespace AIC.OnLineDataPage.Views
{
    /// <summary>
    /// Interaction logic for MoreInformation.xaml
    /// </summary>
    public partial class MoreInformation : UserControl
    {
        public MoreInformation()
        {
            InitializeComponent();
        }

        //仅仅是为了兼容网页模式ShowDialog不生效
        private bool winshow = false;
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (winshow == true)
            {
                return;
            }
            try
            {
                winshow = true;

                var sg = this.DataContext;
                if (sg is WirelessVibrationChannelSignal)
                {
                    WirelessVibrationInfoWin win = new WirelessVibrationInfoWin(sg as WirelessVibrationChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is WirelessScalarChannelSignal)
                {
                    WirelessScalarInfoWin win = new WirelessScalarInfoWin(sg as WirelessScalarChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is IEPEChannelSignal)
                {
                    IEPEInfoWin win = new IEPEInfoWin(sg as IEPEChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is EddyCurrentDisplacementChannelSignal)
                {
                    EddyCurrentDisplacementInfoWin win = new EddyCurrentDisplacementInfoWin(sg as EddyCurrentDisplacementChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is EddyCurrentKeyPhaseChannelSignal)
                {
                    EddyCurrentKeyPhaseInfoWin win = new EddyCurrentKeyPhaseInfoWin(sg as EddyCurrentKeyPhaseChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is EddyCurrentTachometerChannelSignal)
                {
                    EddyCurrentTachometerInfoWin win = new EddyCurrentTachometerInfoWin(sg as EddyCurrentTachometerChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is DigitTachometerChannelSignal)
                {
                    DigitTachometerInfoWin win = new DigitTachometerInfoWin(sg as DigitTachometerChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is AnalogRransducerInChannelSignal)
                {
                    AnalogRransducerInInfoWin win = new AnalogRransducerInInfoWin(sg as AnalogRransducerInChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is AnalogRransducerOutChannelSignal)
                {
                    AnalogRransducerOutInfoWin win = new AnalogRransducerOutInfoWin(sg as AnalogRransducerOutChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is DigitRransducerInChannelSignal)
                {
                    DigitRransducerInInfoWin win = new DigitRransducerInInfoWin(sg as DigitRransducerInChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is DigitRransducerOutChannelSignal)
                {
                    DigitRransducerOutInfoWin win = new DigitRransducerOutInfoWin(sg as DigitRransducerOutChannelSignal);
                    win.ShowDialog();
                }
                else if (sg is RelayChannelSignal)
                {
                    RelayInfoWin win = new RelayInfoWin(sg as RelayChannelSignal);
                    win.ShowDialog();
                }
            }
            finally
            {
                winshow = false;
            }
        }
    }
}
