using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.OnLineDataPage.Views;
using AIC.ServiceInterface;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.CloseTabControl;

namespace AIC.HomePage.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : UserControl//, ICloseable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly ILoginUserService _loginUserService;
        public MapView(IEventAggregator eventAggregator, IRegionManager regionManager, ILoginUserService loginUserService)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _loginUserService = loginUserService;

            DoSomething ds = new DoSomething();
            this.webBrowser.ObjectForScripting = ds;
            ds.TransferClicked += Ds_TransferClicked;
            //this.webBrowser.Navigate(new Uri(System.Environment.CurrentDirectory + @"/MyData/Htmls/BMapOffline.html", UriKind.RelativeOrAbsolute));//获取根目录的html文件  
            
            string serverhtml = "http://" + _loginUserService.LoginInfo.ServerInfo.IP + ":38080/BMapOffline.html";
           
            if (GetPage(@serverhtml))
            {
                this.webBrowser.Navigate(new Uri(@serverhtml));//获取根目录的html文件  
                readDataTimer.Tick += new EventHandler(timeCycle);
                readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
            }
            else
            {
                if (!File.Exists(@LocalSetting.MapHtmlUri))
                {
                    this.webBrowser.NavigateToString(ConvertExtendedASCII("地图文件不存在，请设置地图文件路径！！！"));
                }
                else
                {
                    this.webBrowser.Navigate(new Uri(@LocalSetting.MapHtmlUri));//获取根目录的html文件  
                    readDataTimer.Tick += new EventHandler(timeCycle);
                    readDataTimer.Interval = new TimeSpan(0, 0, 0, 1);
                }
            }

            _eventAggregator.GetEvent<ServerMarkEvent>().Subscribe(AddMarker);
            _eventAggregator.GetEvent<HideHtmlEvent>().Subscribe(HideHtml);
            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }

        private static string ConvertExtendedASCII(string HTML)
        {
            StringBuilder sb = new StringBuilder();
            char[] s = HTML.ToCharArray();
            foreach (char c in s)
            {
                if (Convert.ToInt32(c) > 127)
                    sb.Append(string.Format("&#{0};", Convert.ToInt32(c)));
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        public static bool GetPage(String url)
        {
            bool ok = false;
            try
            {
                // Creates an HttpWebRequest for the specified URL.
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                // 有些网站会阻止程序访问，需要加入下面这句
                myHttpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
                myHttpWebRequest.Method = "GET";
                // Sends the HttpWebRequest and waits for a response.
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    ok = true;
                }
                // Releases the resources of the response.
                myHttpWebResponse.Close();

            }
            catch (WebException e)
            {
                //Console.WriteLine("\r\nWebException Raised. The following error occured : {0}", e.Status);                
            }
            catch (Exception e)
            {
                //Console.WriteLine("\nThe following Exception was raised : {0}", e.Message);
            }
            
            return ok;
        }

        private void Ds_TransferClicked(string str)
        {
            string viewName = "MenuOnlineDataList";
            IRegion region = this._regionManager.Regions["MainTabRegion"];
            if (region.GetView(viewName) != null)
            {
                region.Activate(region.GetView(viewName));
                return;
            }
            Object viewObj = ServiceLocator.Current.GetInstance<OnlineDataListView>();
            ICloseable view = viewObj as ICloseable;
            if (view != null)
            {
                view.Closer.RequestClose += () => region.Remove(view);
            }
            region.Add(view, viewName);
            region.Activate(view);
        }

        private System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();

        //public CloseableHeader Closer { get; private set; }
        IList<ServerInfo> ServerInfo;
        private void AddMarker(IList<ServerInfo> serverInfo)
        {
            ServerInfo = serverInfo;
            readDataTimer.Start();
        }
        private void timeCycle(object sender, EventArgs e)
        {
            webBrowser.InvokeScript("removeOverlay", null);
            foreach (var server in ServerInfo)
            {
                if (server.LoginResult == true)
                {
                    object[] objs = new object[4] {
                    server.Longitude,
                    server.Latitude,
                    0,
                    server.Factory};
                    webBrowser.InvokeScript("addMarker", objs);
                }
            }
            readDataTimer.Stop();
        }
        private void HideHtml(int para)
        {
            #if XBAP
            if (para == 0)
            {
                //隐藏并用Image替代WebBrowser
                imageResource.Source = WebScreenshot.BrowserSnapShot(webBrowser);
                webBrowser.Visibility = Visibility.Hidden;
            }
            else
            {
                //恢复WebBrowser
                imageResource.Source = null;
                webBrowser.Visibility = Visibility.Visible;
            }
            #endif
        }
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //恢复WebBrowser
            imageResource.Source = null;
            webBrowser.Visibility = Visibility.Visible;
        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            //隐藏并用Image替代WebBrowser
            imageResource.Source = WebScreenshot.BrowserSnapShot(webBrowser);
            webBrowser.Visibility = Visibility.Hidden;
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
    }

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class DoSomething
    {
        public delegate void TransferClick(string str);
        public event TransferClick TransferClicked;

        public static string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public void ClickEvent(string str)
        {
            this.Name = str;
            TransferClicked(str);
            //MessageBox.Show("Welcome " + str);           
        }
    }

    class WebScreenshot
    {
        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

        /// <summary>
        /// 对一个WebBrowser进行截图
        /// </summary>
        /// <param name="targetBrowser">我这里用的是Forms的WebBrowser，如果是wpf的，请自己改成Controls并调整参数</param>
        /// <returns></returns>
        public static ImageSource BrowserSnapShot(WebBrowser targetBrowser)
        {
            try
            {
                // 获取宽高
                int screenWidth = (int)targetBrowser.ActualWidth;
                int screenHeight = (int)targetBrowser.ActualHeight;

                IntPtr myIntptr = targetBrowser.Handle;
                int hwndInt = myIntptr.ToInt32();
                IntPtr hwnd = myIntptr;
                //创建图形
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bm);
                IntPtr hdc = g.GetHdc();

                //调用api 把hwnd的内容用图形绘制到hdc 如果你有代码洁癖 可以不使用api 使用g.CopyFromScreen，请自行研究
                bool result = PrintWindow(hwnd, hdc, 0);
                g.ReleaseHdc(hdc);
                g.Flush();


                if (result == true) //成功 转换并返回ImageSource
                {
                    ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
                    MemoryStream stream = new MemoryStream();
                    bm.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    return (ImageSource)imageSourceConverter.ConvertFrom(stream);
                }
            }
            catch { }
            return null;
        }
    }

}
