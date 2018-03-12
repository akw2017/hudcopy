using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.CoreType;
using AIC.HomePage.ViewModels;
using AIC.OnLineDataPage.Views;
using AIC.ServiceInterface;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Annotations;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.EventMarkers;
using Arction.Wpf.Charting.SeriesPolar;
using Arction.Wpf.Charting.Titles;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class HomeMapView : UserControl, ICloseable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly ILoginUserService _loginUserService;
        private readonly ILocalConfiguration _localConfiguration;
        private string dir = System.AppDomain.CurrentDomain.BaseDirectory + "MyData\\Htmls\\BMapOffline.html";

        public HomeMapView(ILocalConfiguration localConfiguration, IEventAggregator eventAggregator, IRegionManager regionManager, ILoginUserService loginUserService)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _loginUserService = loginUserService;
            _localConfiguration = localConfiguration;


            #region            
            DoSomething ds = new DoSomething();
            this.webBrowser.ObjectForScripting = ds;
            ds.TransferClicked += Ds_TransferClicked;
            //this.webBrowser.Navigate(new Uri(System.Environment.CurrentDirectory + @"/MyData/Htmls/BMapOffline.html", UriKind.RelativeOrAbsolute));//获取根目录的html文件  
            this.webBrowser.LoadCompleted += WebBrowser_LoadCompleted;

            string serverhtml = "http://" + _loginUserService.LoginInfo.ServerInfo.IP + ":38080/BMapOffline.html";

            if (GetPage(@serverhtml))
            {
                this.webBrowser.Navigate(new Uri(@serverhtml));//获取根目录的html文件  
            }
            else
            {
                //if (!File.Exists(@LocalSetting.MapHtmlUri))//昌邑石化
                if (!File.Exists(dir))
                {
                    this.webBrowser.NavigateToString(ConvertExtendedASCII("地图文件不存在，请设置地图文件路径！！！"));
                }
                else
                {
                    //this.webBrowser.Navigate(new Uri(@LocalSetting.MapHtmlUri));//获取根目录的html文件  //昌邑石化
                    this.webBrowser.Navigate(new Uri(@dir));//获取根目录的html文件 
                }
            }
            #endregion

            _eventAggregator.GetEvent<HideHtmlEvent>().Subscribe(HideHtml);

            this.Closer = new CloseableHeader("tabFirst", (string)Application.Current.Resources["tabFirst"], false);


            HomeMapViewModel vm = this.DataContext as HomeMapViewModel;
            if (vm != null)
            {
                vm.ShowMapAlarmChanged += ShowMapAlarmChanged;
                vm.ShowMapServerChanged += ShowMapServerChanged;
            }
        }

        public CloseableHeader Closer { get; private set; }

        #region 地图数据加载
        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            ShowMapAlarmChanged();

            webBrowser.InvokeScript("MoveToPoint", new object[2] { _loginUserService.LoginInfo.ServerInfo.Longitude, _loginUserService.LoginInfo.ServerInfo.Latitude });
        }

        private void ShowMapAlarmChanged()
        {
            webBrowser.InvokeScript("removeOverlay", null);
            foreach (var server in _localConfiguration.LoginServerInfoList)
            {
                string name = (server.Name == null || server.Name == "") ? server.IP : server.Name;
                string number = (server.AlarmCount > 99) ? "99+" : server.AlarmCount.ToString();
                switch (server.AlarmGrade)
                {
                    case AlarmGrade.Invalid: 
                    case AlarmGrade.HighNormal:
                    case AlarmGrade.LowNormal:
                        {
                            webBrowser.InvokeScript("addMarker", new object[5] { server.Longitude, server.Latitude, name, "green", number });
                            break;
                        }
                    case AlarmGrade.HighPreAlarm:
                    case AlarmGrade.LowPreAlarm:
                        {
                            webBrowser.InvokeScript("addMarker", new object[5] { server.Longitude, server.Latitude, name, "yellow", number });
                            break;
                        }
                    case AlarmGrade.HighAlarm:
                    case AlarmGrade.LowAlarm:
                        {
                            webBrowser.InvokeScript("addMarker", new object[5] { server.Longitude, server.Latitude, name, "orange", number });
                            break;
                        }
                    case AlarmGrade.HighDanger:
                    case AlarmGrade.LowDanger:
                        {
                            webBrowser.InvokeScript("addMarker", new object[5] { server.Longitude, server.Latitude, name, "red", number });
                            break;
                        }
                    case AlarmGrade.DisConnect:
                        {
                            webBrowser.InvokeScript("addMarker", new object[5] { server.Longitude, server.Latitude, name, "darkred", number });
                            break;
                        }
                    default:
                        {
                            webBrowser.InvokeScript("addMarker", new object[5] { server.Longitude, server.Latitude, name, "green", "" });
                            break;
                        }
                }

            }
        }

        private void ShowMapServerChanged()
        {
            Move_Click(null, null);
        }
        #endregion

        #region 测试使用
        private void Move_Click(object sender, RoutedEventArgs e)
        {
            object[] objs = new object[2] {
                double.Parse(this.jin.Text),
                double.Parse(this.wei.Text)};
            webBrowser.InvokeScript("MoveToPoint", objs);
        }
        private void Mark_Click(object sender, RoutedEventArgs e)
        {
            object[] objs = new object[5] {
                double.Parse(this.jin.Text),
                double.Parse(this.wei.Text),
                "123",
                "red",
                "10"};
            webBrowser.InvokeScript("addMarker", objs);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.InvokeScript("removeOverlay", null);
        }

        private void AddMarks_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.InvokeScript("addMarkerClusterer", null);
        }        
        #endregion

        #region 辅助网页打开程序

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

        /// <summary>
        /// 判断是否中文
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public bool IsChinese(char Text)
        {

            if ((int)Text > 127)
                return true;

            return false;
        }

        //测试是否连通
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
                Console.WriteLine("\r\nWebException Raised. The following error occured : {0}", e.Status);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nThe following Exception was raised : {0}", e.Message);
            }

            return ok;
        }
        #endregion

        #region
        private void Ds_TransferClicked(string str)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                HomeMapViewModel vm = this.DataContext as HomeMapViewModel;
                if (vm != null)
                {
                    vm.GotoCommand.Execute(str);
                }                
            }));
        }


        //public CloseableHeader Closer { get; private set; }
        //IList<ServerInfo> ServerInfo;
        //private void AddMarker(IList<ServerInfo> serverInfo)
        //{
        //    ServerInfo = serverInfo;
        //}

        #endregion

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
            if (TransferClicked != null)
            {
                TransferClicked(str);
            }
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
