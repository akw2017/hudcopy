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
            servertxt.Text = LocalAddress.ServerXmlDir;
            maptxt.Text = LocalAddress.MapHtmlUri;           
            devicetxt.Text = LocalAddress.LayoutPath;
            giftxt.Text = LocalAddress.GifDir;
            pdaporttxt.Text = LocalAddress.PDAPort.ToString();
            serverporttxt.Text = LocalAddress.ServerPort.ToString();
            screentxt.Text = LocalAddress.ScreenShotDir.ToString();
            timetxt.Text = LocalAddress.UpdateTime.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LocalAddress.ServerXmlDir = servertxt.Text;
                LocalAddress.MapHtmlUri = maptxt.Text;              
                LocalAddress.LayoutPath = devicetxt.Text;
                LocalAddress.GifDir = giftxt.Text;
                LocalAddress.PDAPort = Convert.ToInt32(pdaporttxt.Text);
                LocalAddress.ServerPort = Convert.ToInt32(serverporttxt.Text);
                LocalAddress.ScreenShotDir = screentxt.Text;
                LocalAddress.UpdateTime = Convert.ToInt32(timetxt.Text);

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
