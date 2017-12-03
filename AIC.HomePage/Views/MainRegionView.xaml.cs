using AIC.Core;
using AIC.Core.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AIC.HomePage.Views
{
    /// <summary>
    /// Interaction logic for MainRegionView.xaml
    /// </summary>
    public partial class MainRegionView : UserControl
    {     

        private readonly IEventAggregator _eventAggregator;
        public MainRegionView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
        }

        private void Menu_MouseEnter(object sender, MouseEventArgs e)
        {
            _eventAggregator.GetEvent<HideHtmlEvent>().Publish(0);
        }

        private void Menu_MouseLeave(object sender, MouseEventArgs e)
        {
            _eventAggregator.GetEvent<HideHtmlEvent>().Publish(1);
        }

        private void ScreenShot_Click(object sender, RoutedEventArgs e)
        {
            string direcory = LocalSetting.ScreenShotDir;
            if (!Directory.Exists(@direcory))
            {
                Directory.CreateDirectory(@direcory);
            }
            string filename = direcory + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            //保存到特定路径
            FileStream fs = new FileStream(@filename, FileMode.Create);
            //对象转换成位图
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)this.usercontrol.ActualWidth + 80, (int)this.usercontrol.ActualHeight + 40, 100, 100, PixelFormats.Pbgra32);
            bmp.Render(this.usercontrol);
            //对象的集合编码转成图像流
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            //保存到路径中
            encoder.Save(fs);
            //释放资源
            fs.Close();
            fs.Dispose();
        }
    }
}
