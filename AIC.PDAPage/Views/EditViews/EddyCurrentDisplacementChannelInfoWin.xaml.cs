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
    public partial class EddyCurrentDisplacementChannelInfoWin : MetroWindow
    {
        public delegate void TransferParaData(IChannel channel);
        public event TransferParaData Parachanged;

        EddyCurrentDisplacementChannelInfo Channel = new EddyCurrentDisplacementChannelInfo();
        public EddyCurrentDisplacementChannelInfoWin(IChannel channel)
        {
            InitializeComponent();
            //Channel = ClassCopyHelper.DeepCopy<EddyCurrentDisplacementChannelInfo>(channel as EddyCurrentDisplacementChannelInfo);
            CardCopyHelper.ChannelLeftCopyToRight(channel, Channel);
            this.DataContext = Channel;

            btnOK.IsEnabled = false;
            Channel.PropertyChanged += Channel_PropertyChanged;
            if (Channel.AlarmStrategy != null)
            {
                if (Channel.AlarmStrategy.Absolute != null)
                {
                    if (Channel.AlarmStrategy.Absolute.Category != null)
                    {
                        Channel.AlarmStrategy.Absolute.Category.ForEach(p => p.PropertyChanged += Channel_PropertyChanged);
                    }
                    if (Channel.AlarmStrategy.Absolute.Para != null)
                    {
                        Channel.AlarmStrategy.Absolute.Para.ForEach(p => p.PropertyChanged += Channel_PropertyChanged);
                    }
                    if (Channel.AlarmStrategy.Absolute.Mode != null)
                    {
                        Channel.AlarmStrategy.Absolute.Mode.ForEach(p => p.PropertyChanged += Channel_PropertyChanged);
                    }
                }
                if (Channel.AlarmStrategy.Comparative != null)
                {
                    if (Channel.AlarmStrategy.Comparative.Para != null)
                    {
                        Channel.AlarmStrategy.Comparative.Para.ForEach(p => p.PropertyChanged += Channel_PropertyChanged);
                    }
                }
            }
            this.Closed += MetroWindow_Closed;
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Channel.PropertyChanged -= Channel_PropertyChanged;
        }

        private void Channel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            btnOK.IsEnabled = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Channel.Error == "")
            {
                Parachanged(Channel);
            }
            //this.Close();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
