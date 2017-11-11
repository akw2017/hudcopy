using AIC.Cloud.DataReplayer.ViewModels;
using AIC.Cloud.Presentation;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace AIC.Cloud.DataReplayer.Views
{
    /// <summary>
    /// Interaction logic for VideoReplayerView.xaml
    /// </summary>
    public partial class VideoReplayerView : UserControl
    {
        private uint _port = 0;
        private Storyboard _leftPanelExpanded;
        private Storyboard _leftPanelCollapsed;
        private DispatcherTimer timer;
        private bool isPlaying;
        private float position;
        private string selectedVideoFile;

        public VideoReplayerView()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            _leftPanelExpanded = (Storyboard)this.FindResource("LeftPanelExpanded");
            _leftPanelCollapsed = (Storyboard)this.FindResource("LeftPanelCollapsed");

            Storyboard.SetTargetProperty(_leftPanelExpanded, new PropertyPath(GridExtension.ColumnWidthProperty));
            Storyboard.SetTargetProperty(_leftPanelCollapsed, new PropertyPath(GridExtension.ColumnWidthProperty));
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;
            Unloaded += VideoReplayerView_Unloaded;
        }

        void VideoReplayerView_Unloaded(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                btnPlayPause.IsChecked = false;
                ReplaySDK.PlayM4_Pause(_port, !(bool)btnPlayPause.IsChecked);
                timer.Stop();
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            var time = (double)ReplaySDK.PlayM4_GetPlayedTime(_port);
            TBpassedTime.Text = TimeSpan.FromSeconds(time).ToString();
            replayerSlider.Value = time;

            position = ReplaySDK.PlayM4_GetPlayPos(_port);
            if (position == 1)
            {
                isPlaying = false;
                btnPlayPause.IsChecked = false;
                timer.Stop();

                ReplaySDK.PlayM4_Stop(_port);
                ReplaySDK.PlayM4_CloseFile(_port);
                ReplaySDK.PlayM4_FreePort(_port);
                this.pbReplay.Image = null;
                replayerSlider.Value = 0;
                TBTotalTime.Text = TimeSpan.FromSeconds(0).ToString();
                TBpassedTime.Text = TimeSpan.FromSeconds(0).ToString();

                if (!(bool)checkBoxPlayLocalFile.IsChecked)
                {
                    var viewModel = DataContext as DataReplayerViewModel;
                    if (viewModel != null)
                    {
                        if (viewModel.MoveToNextVideo())
                        {
                            btnPlayPause.IsChecked = true;
                            //viewModel.SelectedVideo != null
                            string directory = string.Format(@"c:\{0}", viewModel.SelectedVideo.Directory);
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }
                            string file = string.Format(@"c:\{0}", viewModel.SelectedVideo.FullPath);

                            if (!File.Exists(file))
                            {
                                DownloadAndPlayVideoFile(viewModel.SelectedVideo.Address, file);
                            }
                            else
                            {
                                PlayVideoFile(file);
                            }
                        }
                    }
                }
            }
        }

        private bool _isExpandedLeftPanel = true;
        private void LeftPanelToggleButtonClick(object sender, RoutedEventArgs e)
        {
            if (_isExpandedLeftPanel)
                _leftPanelCollapsed.Begin();
            else
                _leftPanelExpanded.Begin();

            _isExpandedLeftPanel = !_isExpandedLeftPanel;
        }

        private void btnPlayClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isPlaying)
                {
                    ReplaySDK.PlayM4_Pause(_port, !(bool)btnPlayPause.IsChecked);
                    if ((bool)btnPlayPause.IsChecked)
                    {
                        timer.Start();
                    }
                    else
                    {
                        timer.Stop();
                    }
                }
                else
                {
                    if (!(bool)checkBoxPlayLocalFile.IsChecked)
                    {
                        var viewModel = DataContext as DataReplayerViewModel;
                        if (viewModel != null && viewModel.SelectedVideo != null)
                        {
                            string directory = string.Format(@"c:\{0}", viewModel.SelectedVideo.Directory);
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }
                            string file = string.Format(@"c:\{0}", viewModel.SelectedVideo.FullPath);

                            if (!File.Exists(file))
                            {
                                DownloadAndPlayVideoFile(viewModel.SelectedVideo.Address, file);
                            }
                            else
                            {
                                PlayVideoFile(file);
                            }
                        }
                    }
                    else
                    {
                        if (File.Exists(selectedVideoFile))
                        {
                            PlayVideoFile(selectedVideoFile);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("指定的文件不存:{0}", selectedVideoFile));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DownloadAndPlayVideoFile(Uri address, string file)
        {
            WebClient wc = new WebClient();
            wc.DownloadDataCompleted += delegate (object client, DownloadDataCompletedEventArgs we)
            {
                FileStream fileStream = new System.IO.FileStream(file, FileMode.Create, FileAccess.Write);
                // Writes a block of bytes to this stream using data from a byte array.
                fileStream.Write(we.Result, 0, we.Result.Length);
                // close file stream
                fileStream.Close();
                if (ReplaySDK.PlayM4_GetPort(ref _port))
                {
                    ReplaySDK.PlayM4_OpenFile(_port, file);
                    double totalseconds = (double)ReplaySDK.PlayM4_GetFileTime(_port);
                    this.replayerSlider.Maximum = totalseconds;
                    TBTotalTime.Text = TimeSpan.FromSeconds(totalseconds).ToString();
                    isPlaying = ReplaySDK.PlayM4_Play(_port, this.pbReplay.Handle);

                    timer.Start();
                }
            };
            wc.DownloadDataAsync(address);
        }

        private void PlayVideoFile(string file)
        {
            if (ReplaySDK.PlayM4_GetPort(ref _port))
            {
                ReplaySDK.PlayM4_OpenFile(_port, file);
                double totalseconds = (double)ReplaySDK.PlayM4_GetFileTime(_port);
                this.replayerSlider.Maximum = totalseconds;

                TBTotalTime.Text = TimeSpan.FromSeconds(totalseconds).ToString();
                isPlaying = ReplaySDK.PlayM4_Play(_port, this.pbReplay.Handle);

                timer.Start();
            }
        }

        private void btnFastClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ReplaySDK.PlayM4_Fast(_port);
                float s = float.Parse(this.txbReplaySpeed.Text);
                if (s <= 8)
                {
                    this.txbReplaySpeed.Text = (s * 2).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSlowClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ReplaySDK.PlayM4_Slow(_port);
                double s = double.Parse(this.txbReplaySpeed.Text);
                if (s >= (1f / 8f))
                {
                    this.txbReplaySpeed.Text = (s / 2).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnStopClick(object sender, RoutedEventArgs e)
        {
            isPlaying = false;
            btnPlayPause.IsChecked = false;
            ReplaySDK.PlayM4_Stop(_port);
            ReplaySDK.PlayM4_CloseFile(_port);
            ReplaySDK.PlayM4_FreePort(_port);
            replayerSlider.Value = 0;
            this.pbReplay.Image = null;
            TBTotalTime.Text = TimeSpan.FromSeconds(0).ToString();
            TBpassedTime.Text = TimeSpan.FromSeconds(0).ToString();
        }

        private void btnSnaphotClick(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] buffer = new byte[this.pbReplay.Width * this.pbReplay.Height * 3 / 2];
                uint size = 0;
                if (ReplaySDK.PlayM4_GetJPEG(_port, buffer, (uint)buffer.Length, ref size))
                {
                    SnapshotView snapshotView = new SnapshotView();
                    snapshotView.SetImageStream(buffer);
                    // snapshotView.ImageBox.Source = ByteArrayToBitmapImage(buffer);
                    Window window = CreateWindowHostingUserControl(snapshotView);
                    window.Width = 800;
                    window.Height = 600;
                    window.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public Window CreateWindowHostingUserControl(UserControl userControlToHost)
        {
            //Create a border with the initial height and width of the user control.  
            Border borderWithInitialDimensions = new Border();

            borderWithInitialDimensions.Height = userControlToHost.Height;
            borderWithInitialDimensions.Width = userControlToHost.Width;


            //Set the user control's dimensions to double.NaN so that it auto sizes  
            //to fill the window.  
            userControlToHost.Height = double.NaN;
            userControlToHost.Width = double.NaN;


            //Create a grid hosting both the border and the user control.  The   
            //border results in the grid and window (created below) having initial  
            //dimensions.  
            Grid hostGrid = new Grid();

            hostGrid.Children.Add(borderWithInitialDimensions);
            hostGrid.Children.Add(userControlToHost);


            //Create a window that resizes to fit its content with the grid as its   
            //content.  
            Window hostWindow = new Window();

            hostWindow.Content = hostGrid;
            // hostWindow.SizeToContent = SizeToContent.WidthAndHeight;

            return hostWindow;
        }

        private void ButtonOpenFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "mp4|*.mp4|wmv|*.wmv";
            if ((bool)dialog.ShowDialog())
            {
                selectedVideoFile = dialog.FileName;
                txbVideoPath.Text = dialog.FileName;
            }
        }

        private void replayerSlider_DragCompleted_1(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int value = (int)(this.replayerSlider.Value * 1000);
            ReplaySDK.PlayM4_SetPlayedTimeEx(_port, (uint)value);
            timer.Start();
        }


        private void replayerSlider_DragStarted_1(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            timer.Stop();
        }
    }
}
