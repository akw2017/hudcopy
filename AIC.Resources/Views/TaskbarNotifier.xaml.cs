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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AIC.Resources.Views
{
    /// <summary>
    /// TaskbarNotifier.xaml 的交互逻辑
    /// </summary>
    public partial class TaskbarNotifier : Window
    {
        public delegate void TransferClosed(double heightoffset);
        public event TransferClosed ThisClosed;

        DispatcherTimer timer;
        public double EndTop { get; set; }
        private double heightoffset;
        public TaskbarNotifier(string title, string content, double heightoffset = 0)
        {
            InitializeComponent();
            titleLb.Text = title;
            contentTxt.Text = content;
            this.heightoffset = heightoffset;
            this.Left = SystemParameters.WorkArea.Width - this.Width;
            this.EndTop = SystemParameters.WorkArea.Height - this.Height - heightoffset;
            this.Top = SystemParameters.WorkArea.Height - this.Height - heightoffset;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Image_MouseLeftButtonDown(null, null);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsEnabled = false;

            mGrid.OpacityMask = this.Resources["ClosedBrush"] as LinearGradientBrush;
            Storyboard std = this.Resources["ClosedStoryboard"] as Storyboard;
            std.Completed += delegate 
            {
                if (ThisClosed != null)
                {
                    this.ThisClosed(heightoffset);
                }
                this.Close();
            };

            std.Begin();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}
