using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace AIC.Cloud.DataReplayer.Views
{
    /// <summary>
    /// Interaction logic for SnapshotView.xaml
    /// </summary>
    public partial class SnapshotView : UserControl
    {
        private byte[] imageBuffer;
        public SnapshotView()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "jpeg|*.jpeg";
            if ((bool)dialog.ShowDialog())
            {
                FileStream fileStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write);
                // Writes a block of bytes to this stream using data from a byte array.
                fileStream.Write(imageBuffer, 0, imageBuffer.Length);
                // close file stream
                fileStream.Close();
            }
        }

        public void SetImageStream(byte[] bytes)
        {
            ImageBox.Source = ByteArrayToBitmapImage(bytes);
            imageBuffer = bytes;
        }


        public BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;

            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }

            return bmp;
        }
    }
}
