using AIC.Core;
using AIC.Core.Models;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Forms;
using System.Configuration;
using System;
using AIC.Core.Events;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace AIC.HomePage.Views
{
    /// <summary>
    /// Interaction logic for LoginWin.xaml
    /// </summary>
    public partial class HistoryQuickPlayWin : MetroWindow
    {        
        public HistoryQuickPlayWin()
        {            
            InitializeComponent();
            IsHistoryMode.IsChecked = LocalSetting.IsHistoryMode;
            IsHistoryRrackingMode.IsChecked = LocalSetting.IsHistoryRrackingMode;
            HistoryModeStartTime.Value = LocalSetting.HistoryModeStartTime;
            HistoryModeEndTime.Value = LocalSetting.HistoryModeEndTime;
            HistoryModeSpeedUpRatio.Text = LocalSetting.HistoryModeSpeedUpRatio.ToString();
            HistoryModeDataInterval.Text = LocalSetting.HistoryModeDataInterval.ToString();
            HistoryModeDBCallInterval.Text = LocalSetting.HistoryModeDBCallInterval.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToSingle(HistoryModeSpeedUpRatio.Text) < 1 || Convert.ToSingle(HistoryModeDataInterval.Text) < 1 || Convert.ToSingle(HistoryModeDBCallInterval.Text) < 10)
                {
                    return;
                }
                LocalSetting.IsHistoryMode = IsHistoryMode.IsChecked.Value;
                LocalSetting.IsHistoryRrackingMode = IsHistoryRrackingMode.IsChecked.Value;
                LocalSetting.HistoryModeStartTime = HistoryModeStartTime.Value.Value;
                LocalSetting.HistoryModeEndTime = HistoryModeEndTime.Value.Value;
                LocalSetting.HistoryModeSpeedUpRatio = Convert.ToSingle(HistoryModeSpeedUpRatio.Text);
                LocalSetting.HistoryModeDataInterval = Convert.ToSingle(HistoryModeDataInterval.Text);
                LocalSetting.HistoryModeDBCallInterval = Convert.ToSingle(HistoryModeDBCallInterval.Text);

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
                appSettings.Settings["IsHistoryMode"].Value = IsHistoryMode.IsChecked.ToString();
                appSettings.Settings["IsHistoryRrackingMode"].Value = IsHistoryRrackingMode.IsChecked.ToString();
                appSettings.Settings["HistoryModeStartTime"].Value = HistoryModeStartTime.Value.Value.ToString("yyyy-MM-dd HH:mm:ss");
                appSettings.Settings["HistoryModeEndTime"].Value = HistoryModeEndTime.Value.Value.ToString("yyyy-MM-dd HH:mm:ss");
                appSettings.Settings["HistoryModeSpeedUpRatio"].Value = HistoryModeSpeedUpRatio.Text;
                appSettings.Settings["HistoryModeDataInterval"].Value = HistoryModeDataInterval.Text;
                appSettings.Settings["HistoryModeDBCallInterval"].Value = HistoryModeDBCallInterval.Text;

                config.Save();
                ConfigurationManager.RefreshSection("appSettings");
                this.Close();
            }
            catch(Exception ex)            
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("设置错误", ex));
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);

        }
    }
}
