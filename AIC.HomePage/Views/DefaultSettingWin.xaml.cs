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
    public partial class DefaultSettingWin : MetroWindow
    {        
        public DefaultSettingWin()
        {            
            InitializeComponent();
            servertxt.Text = LocalSetting.ServerXmlDir;
            maptxt.Text = LocalSetting.MapHtmlUri;           
            devicetxt.Text = LocalSetting.LayoutPath;
            giftxt.Text = LocalSetting.GifDir;
            pdaporttxt.Text = LocalSetting.PDAPort.ToString();
            serverporttxt.Text = LocalSetting.ServerPort.ToString();
            screentxt.Text = LocalSetting.ScreenShotDir.ToString();
            timetxt.Text = LocalSetting.UpdateTime.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LocalSetting.ServerXmlDir = servertxt.Text;
                LocalSetting.MapHtmlUri = maptxt.Text;              
                LocalSetting.LayoutPath = devicetxt.Text;
                LocalSetting.GifDir = giftxt.Text;
                LocalSetting.PDAPort = Convert.ToInt32(pdaporttxt.Text);
                LocalSetting.ServerPort = Convert.ToInt32(serverporttxt.Text);
                LocalSetting.ScreenShotDir = screentxt.Text;
                LocalSetting.UpdateTime = Convert.ToInt32(timetxt.Text);

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
                appSettings.Settings["ServerXmlDir"].Value = servertxt.Text;
                appSettings.Settings["MapHtmlUri"].Value = maptxt.Text;               
                appSettings.Settings["LayoutPath"].Value = devicetxt.Text;
                appSettings.Settings["GifDir"].Value = giftxt.Text;
                appSettings.Settings["PDAPort"].Value = pdaporttxt.Text;
                appSettings.Settings["ServerPort"].Value = serverporttxt.Text;
                appSettings.Settings["ScreenShotDir"].Value = screentxt.Text;
                appSettings.Settings["UpdateTime"].Value = timetxt.Text;

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

        private void serverbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog odlg = new OpenFileDialog();
            if (odlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                servertxt.Text = odlg.FileName;
            }
        }

        private void mapbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog odlg = new OpenFileDialog();
            if (odlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                maptxt.Text = odlg.FileName;
            }
        }     

        private void devicebtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog odlg = new OpenFileDialog();
            if (odlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                devicetxt.Text = odlg.FileName;
            }
        }

        private void gifbtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();        

            if (m_Dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                giftxt.Text = m_Dialog.SelectedPath;
            }
        }

        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);

        }

        private void screenbtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();

            if (m_Dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                screentxt.Text = m_Dialog.SelectedPath;
            }
        }
    }
}
