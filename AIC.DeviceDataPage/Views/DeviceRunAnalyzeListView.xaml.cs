using AIC.Core;
using AIC.Core.Helpers;
using AIC.Core.SignalModels;
using AIC.Core.UserManageModels;
using AIC.DeviceDataPage.Models;
using AIC.DeviceDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.EventMarkers;
using Arction.Wpf.Charting.SeriesXY;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Wpf.CloseTabControl;

namespace AIC.DeviceDataPage.Views
{
    /// <summary>
    /// Interaction logic for ServerSetView.xaml
    /// </summary>
    public partial class DeviceRunAnalyzeListView : DisposableUserControl, ICloseable
    {
        public DeviceRunAnalyzeListView()
        {
            InitializeComponent();

            var menu = MenuManageList.GetMenu("menuDeviceRunAnalyze");
            this.Closer = new CloseableHeader("menuDeviceRunAnalyze", menu.Name, true, menu.IconPath);
            this.Loaded += Window_Loaded;

            if (ViewModel != null)
            {
                ViewModel.DeviceRunAnalyzePicGenerated += ViewModel_DeviceRunAnalyzePicGenerated;
            }
        }

        private void ViewModel_DeviceRunAnalyzePicGenerated(DeviceRunAnalyzeDataInfo deviceRunAnalyze)
        {
            ScreenShot(deviceRunAnalyze);
        }

        public CloseableHeader Closer { get; private set; }

        private DeviceRunAnalyzeListViewModel ViewModel
        {
            get { return DataContext as DeviceRunAnalyzeListViewModel; }
            set { this.DataContext = value; }
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Window_Loaded;
            //获取GridSplitterr的cotrolTemplate中的按钮btn，必须在Loaded之后才能获取到
            Button btnGrdSplitter = gsSplitterr.Template.FindName("btnExpend", gsSplitterr) as Button;
            if (btnGrdSplitter != null)
                btnGrdSplitter.Click += new RoutedEventHandler(btnGrdSplitter_Click);
        }

        GridLength m_WidthCache;
        void btnGrdSplitter_Click(object sender, RoutedEventArgs e)
        {
            GridLength temp = grdWorkbench.ColumnDefinitions[0].Width;
            GridLength zero = new GridLength(0);
            if (!temp.Equals(zero))
            {
                //折叠
                m_WidthCache = grdWorkbench.ColumnDefinitions[0].Width;
                grdWorkbench.ColumnDefinitions[0].Width = new GridLength(0);
            }
            else
            {
                //恢复
                grdWorkbench.ColumnDefinitions[0].Width = m_WidthCache;
            }
        }

        //定义一个函数，返回字符串中的连续汉字个数
        public static int GetHanNumFromString(string str)
        {
            int count = 0;
            Regex regex = new Regex(@"^[\u4E00-\u9FA5]{0,}$");
            for (int i = 0; i < str.Length; i++)
            {
                if (regex.IsMatch(str[i].ToString()))
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count;
        }

        private void ScreenShot(DeviceRunAnalyzeDataInfo deviceRunAnalyze)
        {
            string filename = System.AppDomain.CurrentDomain.BaseDirectory + "\\DeviceRunAnalyzeRunTime.png";
            if (File.Exists(filename)) //判断文件是否存在
            {
                File.Delete(filename);
            }
            //保存到特定路径
            FileStream fs = new FileStream(@filename, FileMode.Create);
            //对象转换成位图
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)this.runControl.ActualWidth + 80, (int)this.runControl.ActualHeight + 40, 100, 100, PixelFormats.Pbgra32);
            bmp.Render(this.runControl);
            //对象的集合编码转成图像流
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            //保存到路径中
            encoder.Save(fs);
            //释放资源
            fs.Close();
            fs.Dispose();
            //deviceRunAnalyze.RunTimePicPath = filename;          

            BitmapImage image = new BitmapImage();
            using (FileStream fileStream = File.OpenRead(filename))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;//图像缓存到内存中，不会占用文件，没有被引用时会被自动回收。
                image.StreamSource = fileStream;
                image.EndInit();
            }
            deviceRunAnalyze.RunTimeImage = image;


            filename = System.AppDomain.CurrentDomain.BaseDirectory + "\\DeviceRunAnalyzeMaxValue.png";
            if (File.Exists(filename)) //判断文件是否存在
            {
                File.Delete(filename);
            }
            //保存到特定路径
            fs = new FileStream(@filename, FileMode.Create);
            //对象转换成位图
            bmp = new RenderTargetBitmap((int)this.maxControl.ActualWidth + 80, (int)this.maxControl.ActualHeight + 40, 100, 100, PixelFormats.Pbgra32);
            bmp.Render(this.maxControl);
            //对象的集合编码转成图像流
            encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            //保存到路径中
            encoder.Save(fs);
            //释放资源
            fs.Close();
            fs.Dispose();
            //deviceRunAnalyze.MaxValuePicPath = filename;

            image = new BitmapImage();
            using (FileStream fileStream = File.OpenRead(filename))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;//图像缓存到内存中，不会占用文件，没有被引用时会被自动回收。
                image.StreamSource = fileStream;
                image.EndInit();
            }
            deviceRunAnalyze.MaxValueImage = image;
        }        
    }
}
