using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AIC.Core.Helpers
{
    public class WebScreenshotHelp
    {
        #region import dll
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(string driver, string device, IntPtr res1, IntPtr res2);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth,
            int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        public enum TernaryRasterOperations
        {
            SRCCOPY = 0x00CC0020, /* dest = source*/
            SRCPAINT = 0x00EE0086, /* dest = source OR dest*/
            SRCAND = 0x008800C6, /* dest = source AND dest*/
            SRCINVERT = 0x00660046, /* dest = source XOR dest*/
            SRCERASE = 0x00440328, /* dest = source AND (NOT dest )*/
            NOTSRCCOPY = 0x00330008, /* dest = (NOT source)*/
            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
            MERGECOPY = 0x00C000CA, /* dest = (source AND pattern)*/
            MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest*/
            PATCOPY = 0x00F00021, /* dest = pattern*/
            PATPAINT = 0x00FB0A09, /* dest = DPSnoo*/
            PATINVERT = 0x005A0049, /* dest = pattern XOR dest*/
            DSTINVERT = 0x00550009, /* dest = (NOT dest)*/
            BLACKNESS = 0x00000042, /* dest = BLACK*/
            WHITENESS = 0x00FF0062, /* dest = WHITE*/
        };

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(
         IntPtr hwnd
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(
         IntPtr hdc,        // handle to DC
         int nWidth,     // width of bitmap, in pixels
         int nHeight     // height of bitmap, in pixels
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(
         IntPtr hdc // handle to DC
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(
         IntPtr hdc,          // handle to DC
         IntPtr hgdiobj   // handle to object
         );

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(
         IntPtr hwnd,               // Window to copy,Handle to the window that will be copied. 
         IntPtr hdcBlt,             // HDC to print into,Handle to the device context. 
         UInt32 nFlags              // Optional flags,Specifies the drawing options. It can be one of the following values. 
         );

        [DllImport("gdi32.dll")]
        public static extern int DeleteDC(
         IntPtr hdc          // handle to DC
         );
        #endregion

        /// <summary>
        /// 对一个WebBrowser进行截图
        /// </summary>
        /// <param name="targetBrowser">我这里用的是Forms的WebBrowser，如果是wpf的，请自己改成Controls并调整参数</param>
        /// <returns></returns>
        public static ImageSource BrowserSnapShot(System.Windows.Controls.WebBrowser targetBrowser)
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

        public static Bitmap FullScreenSnapShot(string path)
        {
            try
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
                rect.Width = Screen.PrimaryScreen.Bounds.Width;
                rect.Height = Screen.PrimaryScreen.Bounds.Height;

                IntPtr dcTmp = CreateDC("DISPLAY", "DISPLAY", (IntPtr)null, (IntPtr)null);
                Graphics gScreen = Graphics.FromHdc(dcTmp);
                Bitmap image = new Bitmap((int)(rect.Width), (int)(rect.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics gImage = Graphics.FromImage(image);
                IntPtr dcImage = gImage.GetHdc();
                IntPtr dcScreen = gScreen.GetHdc();
                BitBlt(dcImage, 0, 0, (int)(rect.Width), (int)(rect.Height), dcScreen, (int)(rect.Left), (int)(rect.Top), TernaryRasterOperations.SRCCOPY);
                gScreen.ReleaseHdc(dcScreen);
                gImage.ReleaseHdc(dcImage);

                //save image
                if (path != null)
                {
                    image.Save(path);
                }
                return image;
            }
            catch { }
            return null;
        }

        public static Bitmap WindowSnapShot(Window window, string path)
        {
            IntPtr mywindows = new WindowInteropHelper(window).Handle;

            try
            {
                IntPtr hscrdc = GetWindowDC(mywindows);
                IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, Convert.ToInt32(window.Width), Convert.ToInt32(window.Height));
                IntPtr hmemdc = CreateCompatibleDC(hscrdc);
                SelectObject(hmemdc, hbitmap);
                PrintWindow(mywindows, hmemdc, 0);
                Bitmap image = Bitmap.FromHbitmap(hbitmap);
                DeleteDC(hscrdc);//删除用过的对象
                DeleteDC(hmemdc);//删除用过的对象

                //save image
                if (path != null)
                {
                    image.Save(path);
                }
                return image;

            }
            catch { }
            return null;
        }       

        public static Bitmap ControlSnapShot(System.Windows.Controls.Control control, string path)
        {
            try
            {
                Bitmap image = new Bitmap(getImageFormControl(control));
                //save image
                if (path != null)
                {
                    image.Save(path);
                }
                return image;
            }
            catch { }
            return null;
        }

        private static Stream getImageFormControl(System.Windows.Controls.Control control)
        {
            MemoryStream ms = null;

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext context = drawingVisual.RenderOpen())
            {
                VisualBrush brush = new VisualBrush(control) { Stretch = Stretch.None };
                context.DrawRectangle(brush, null, new Rect(0, 0, control.ActualWidth, control.ActualHeight));
                context.Close();
            }

            //dpi可以自己设定   // 获取dpi方法：PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)control.ActualWidth, (int)control.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(drawingVisual);

            PngBitmapEncoder encode = new PngBitmapEncoder();
            encode.Frames.Add(BitmapFrame.Create(bitmap));
            ms = new MemoryStream();
            encode.Save(ms);

            return ms;
        }
    }
}
