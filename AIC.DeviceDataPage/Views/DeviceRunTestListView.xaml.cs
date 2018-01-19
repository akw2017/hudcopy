using AIC.Core.SignalModels;
using AIC.DeviceDataPage.Models;
using AIC.DeviceDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.EventMarkers;
using Arction.Wpf.Charting.SeriesXY;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class DeviceRunTestListView : UserControl, ICloseable
    {
        public DeviceRunTestListView()
        {
            InitializeComponent();

            this.Closer = new CloseableHeader("menuDeviceRunAnalyze",(string)Application.Current.Resources["menuDeviceRunAnalyze"], true);

            DeviceRunAnalyzeListViewModel vm = this.DataContext as DeviceRunAnalyzeListViewModel;
            if (vm != null)
            {
                vm.UpdateChart += UpdateChart;
                vm.ShowBarSeriesChanged += ShowBarSeriesChanged;
            }

            CreateChart();
            CreateChartMore();

            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }
        public CloseableHeader Closer { get; private set; }

        private LightningChartUltimate _chart;

        private void CreateChart()
        {
            gridChart.Children.Clear();
            if (_chart != null)
            {
                _chart.Dispose();
                _chart = null;
            }
            // Create a new chart.
            _chart = new LightningChartUltimate();
            _chart.BeginUpdate();
            _chart.Title.Text = "";
            _chart.ViewXY.DropOldSeriesData = true;
            _chart.ViewXY.DropOldEventMarkers = true;

            _chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            _chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            _chart.ChartBackground.GradientFill = GradientFill.Solid;
            _chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            _chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            _chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            //Chart has one Y axis ready to go. Just set the range 
            //_chart.ViewXY.YAxes[0].SetRange(0, 100);
            //_chart.ViewXY.XAxes[0].ScrollMode = XAxisScrollMode.None;
            // Configure x-axis.
            _chart.ViewXY.XAxes[0].AutoFormatLabels = false;
            _chart.ViewXY.XAxes[0].CustomTicksEnabled = true;
            //_chart.ViewXY.XAxes[0].LabelsAngle = 15;
            _chart.ViewXY.XAxes[0].MinorDivTickStyle.Visible = false;
            _chart.ViewXY.XAxes[0].MajorGrid.Pattern = LinePattern.Dash;
            _chart.ViewXY.XAxes[0].MajorGrid.Visible = false;
            _chart.ViewXY.XAxes[0].MouseInteraction = false;
            _chart.ViewXY.XAxes[0].Title.Visible = false;
            _chart.ViewXY.XAxes[0].ValueType = AxisValueType.Number;
            _chart.ViewXY.XAxes[0].ScrollMode = XAxisScrollMode.None;
            _chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);

            //Hide X axis
            //_chart.ViewXY.XAxes[0].Visible = false;
            _chart.ViewXY.YAxes[0].Visible = true;
            _chart.ViewXY.YAxes[0].Title.Visible = false;
            _chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);

            //Arrange bars side-by-side and fit to width of the chart
            _chart.ViewXY.BarViewOptions.Grouping = BarsGrouping.ByLocation;
            _chart.ViewXY.BarViewOptions.Stacking = BarsStacking.None;
            _chart.ViewXY.BarViewOptions.BarSpacing = 5;
            _chart.ViewXY.BarViewOptions.Orientation = BarsOrientation.Vertical;

            //Set the legend box in to the bottom
            _chart.ViewXY.LegendBoxes[0].Position = LegendBoxPositionXY.TopRight;
            _chart.ViewXY.LegendBoxes[0].Offset.SetValues(0, 30);
            _chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;

            //Allow chart rendering
            _chart.EndUpdate();

            gridChart.Children.Add(_chart);
            gridChart.SizeChanged += gridChart_SizeChanged;
        }

        private void UpdateChart(IEnumerable<DeviceRunInfo> deviceList)
        {
            _chart.BeginUpdate();
            _chart.ViewXY.BarSeries.Clear();
            _chart.ViewXY.XAxes[0].CustomTicks.Clear();

            int pointsCount = deviceList.Count();
            double max = deviceList.Select(p => p.MaxResult).Max();
            double min = 0 - deviceList.Select(p => p.MinResult).Min();
            int with = (int)((_chart.ActualWidth - 100) / (pointsCount + 1) - 20) / 2;
            _chart.ViewXY.YAxes[0].SetRange(0, max);
            _chart.ViewXY.XAxes[0].SetRange(0.0, (double)(pointsCount + 1));
            // Add a bar series for each point, with a different color.
            for (int i = 0; i < 2; i++)
            {
                BarSeriesValue[] data = new BarSeriesValue[pointsCount];
                int j = 0;
                foreach (var device in deviceList)
                {
                    if (i == 0)
                    {
                        data[j].Value = device.MaxResult;
                        data[j].Location = j + 1;
                        //Set label text
                        data[j].Text = device.MaxResult.ToString("0.0");
                        data[j].Tag = device.MaxResult.ToString("0.0");
                    }
                    else if (i == 1)
                    {
                        data[j].Value = device.MinResult;
                        data[j].Location = j + 1;
                        //Set label text                       
                        data[j].Text = device.MinResult.ToString("0.0");
                        data[j].Tag = device.MinResult.ToString("0.0");
                    }

                    if (i == 0)
                    {
                        // Let's use custom axis ticks to display days of the month.
                        CustomAxisTick tick = new CustomAxisTick(_chart.ViewXY.XAxes[0]);
                        tick.AxisValue = (double)(j + 1);

                        string name = device.DeviceTreeItemViewModel.Name;
                        int HanNumlenght = GetHanNumFromString(name);
                        tick.LabelText = name.Substring(0, HanNumlenght) + "\r\n" + name.Substring(HanNumlenght);
                        tick.Length = 0;
                        _chart.ViewXY.XAxes[0].CustomTicks.Add(tick);
                    }
                    j++;
                }
                BarSeries bs = new BarSeries(_chart.ViewXY, _chart.ViewXY.XAxes[0], _chart.ViewXY.YAxes[0]);
                //Set series title 
                if (i == 0)
                {
                    bs.Title.Text = "最大值";
                }
                else if (i == 1)
                {
                    bs.Title.Text = "最小值";
                }
                //Set series fill 
                if (i == 0)
                {
                    bs.Fill.Color = Color.FromArgb(255, 0xff, 0x00, 0x00);//绿色
                }
                else if (i == 1)
                {
                    bs.Fill.Color = Color.FromArgb(255, 0x00, 0xff, 0x00); //红色
                }
                bs.Fill.GradientFill = GradientFill.Solid;
                //Set label text style
                bs.LabelStyle.Angle = 0;
                bs.LabelStyle.VerticalAlign = BarsTitleVerticalAlign.BarTop;

                //Assign the value
                bs.Values = data;
                bs.BarThickness = with;

                _chart.ViewXY.BarSeries.Add(bs);
            }
            _chart.EndUpdate();
            UpdateChartMore(deviceList);
        }

        private void gridChart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_chart.ViewXY.BarSeries.Count == 0)
            {
                return;
            }
            _chart.BeginUpdate();
            int pointsCount = _chart.ViewXY.BarSeries[0].Values.Count();
            int with = (int)((_chart.ActualWidth - 100) / (pointsCount + 1) - 20) / 2;
            _chart.ViewXY.BarSeries.ForEach(p => p.BarThickness = with);
            _chart.EndUpdate();
        }

        private LightningChartUltimate m_chart;

        private void CreateChartMore()
        {
            gridChartMore.Children.Clear();
            if (m_chart != null)
            {
                m_chart.Dispose();
                m_chart = null;
            }

            m_chart = new LightningChartUltimate();
            m_chart.BeginUpdate();
            m_chart.Title.Text = "";

            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            //m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.DateTime;
            //m_chart.ViewXY.XAxes[0].AutoFormatLabels = false;
            //m_chart.ViewXY.XAxes[0].LabelsTimeFormat = "yy-MM-dd";
            m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.Number;
            m_chart.ViewXY.XAxes[0].AutoFormatLabels = false;
            m_chart.ViewXY.XAxes[0].CustomTicksEnabled = true;
            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].MajorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);

            m_chart.ViewXY.LegendBoxes[0].Visible = true;
            m_chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.ViewXY.LegendBoxes[0].Fill.Style = RectFillStyle.None;
            m_chart.ViewXY.LegendBoxes[0].Shadow.Visible = false;
            m_chart.ViewXY.LegendBoxes[0].BorderWidth = 0;
            m_chart.ViewXY.LegendBoxes[0].Position = LegendBoxPositionXY.TopRight;
            m_chart.ViewXY.LegendBoxes[0].Offset.SetValues(-80, 5);
            m_chart.ViewXY.LegendBoxes[0].SeriesTitleFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            m_chart.ViewXY.ZoomToFit();
            m_chart.EndUpdate();

            gridChartMore.Children.Add(m_chart);
        }

        private void UpdateChartMore(IEnumerable<DeviceRunInfo> deviceList)
        {
            m_chart.BeginUpdate();

            m_chart.ViewXY.HighLowSeries.Clear();
            m_chart.ViewXY.XAxes[0].CustomTicks.Clear();

            int count = 0;
            foreach (var device in deviceList)
            {              
                var axisY = m_chart.ViewXY.YAxes[0];

                HighLowSeries series = new HighLowSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], axisY);
                series.MouseInteraction = false;
                series.Title.Text = device.DeviceTreeItemViewModel.Name;              
            
                Color color = DefaultColors.SeriesForBlackBackgroundWpf[count];
                series.Fill.Color = color;
                series.LineStyle.Color = color;
                series.LineStyleLow.Color = color;
                series.Title.Color = color;               
                series.LineStyle.AntiAliasing = LineAntialias.Normal;
                series.LineStyle.Width = 1;

                if (device.RunInfo == null || device.RunInfo.Count == 0)
                {
                    continue;
                }

                HighLowSeriesPoint[] points = new HighLowSeriesPoint[device.RunInfo.Count];
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].X = i;//m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(device.RunInfo[i].Time);
                    points[i].YHigh = device.RunInfo[i].MaxResult;
                    points[i].YLow = device.RunInfo[i].MinResult;

                    SeriesEventMarker marker = new SeriesEventMarker(series);
                    marker.XValue = points[i].X;
                    marker.YValue = points[i].YHigh;
                    marker.HorizontalPosition = SeriesEventMarkerHorizontalPosition.AtXValue;
                    marker.Symbol.Width = 5;
                    marker.Symbol.Height = 5;
                    //store values in label text    
                    marker.Label.Text = "最大值" + points[i].YHigh.ToString("f3");
                    marker.Label.HorizontalAlign = AlignmentHorizontal.Center;
                    marker.Label.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9f, System.Drawing.FontStyle.Bold);
                    marker.Label.Shadow.Style = TextShadowStyle.HighContrast;
                    marker.Label.Shadow.ContrastColor = Colors.Black;
                    marker.Label.VerticalAlign = AlignmentVertical.Top;
                    marker.Label.Visible = false;
                    marker.Symbol.GradientFill = GradientFillPoint.Solid;
                    marker.Symbol.Color1 = color;
                    marker.Symbol.Shape = Arction.Wpf.Charting.Shape.Circle;
                    marker.VerticalPosition = SeriesEventMarkerVerticalPosition.AtYValue;
                    marker.MoveByMouse = false;
                    marker.MouseOverOn += new MouseEventHandler(marker_MouseOverOn);
                    marker.MouseOverOff += new MouseEventHandler(marker_MouseOverOff);
                    series.SeriesEventMarkers.Add(marker);

                    SeriesEventMarker marker2 = new SeriesEventMarker(series);
                    marker2.XValue = points[i].X;
                    marker2.YValue = points[i].YLow;
                    marker2.HorizontalPosition = SeriesEventMarkerHorizontalPosition.AtXValue;
                    marker2.Symbol.Width = 5;
                    marker2.Symbol.Height = 5;
                    //store values in label text    
                    marker2.Label.Text = "最小值" + points[i].YLow.ToString("f3");
                    marker2.Label.HorizontalAlign = AlignmentHorizontal.Center;
                    marker2.Label.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9f, System.Drawing.FontStyle.Bold);
                    marker2.Label.Shadow.Style = TextShadowStyle.HighContrast;
                    marker2.Label.Shadow.ContrastColor = Colors.Black;
                    marker2.Label.VerticalAlign = AlignmentVertical.Top;
                    marker2.Label.Visible = false;
                    marker2.Symbol.GradientFill = GradientFillPoint.Solid;
                    marker2.Symbol.Color1 = color;
                    marker2.Symbol.Shape = Arction.Wpf.Charting.Shape.Circle;
                    marker2.VerticalPosition = SeriesEventMarkerVerticalPosition.AtYValue;
                    marker2.MoveByMouse = false;
                    marker2.MouseOverOn += new MouseEventHandler(marker_MouseOverOn);
                    marker2.MouseOverOff += new MouseEventHandler(marker_MouseOverOff);
                    series.SeriesEventMarkers.Add(marker2);

                    CustomAxisTick tick = new CustomAxisTick(m_chart.ViewXY.XAxes[0]);
                    tick.AxisValue = i;
                    tick.LabelText = device.RunInfo[i].Time.ToShortDateString();

                    m_chart.ViewXY.XAxes[0].CustomTicks.Add(tick);
                }
                series.Points = points;
                series.Visible = false;
                series.Tag = device;
                m_chart.ViewXY.HighLowSeries.Add(series);
                count++;
                if (count >= 16)
                {
                    count = 0;
                }
            }

            if (m_chart.ViewXY.HighLowSeries.Count > 0)
            {
                double minX = m_chart.ViewXY.HighLowSeries.SelectMany(o => o.Points).Select(o => o.X).Min();
                double maxX = m_chart.ViewXY.HighLowSeries.SelectMany(o => o.Points).Select(o => o.X).Max();
                m_chart.ViewXY.XAxes[0].SetRange(minX, maxX);

                double minY = m_chart.ViewXY.HighLowSeries.SelectMany(o => o.Points).Select(o => o.YLow).Min();
                double maxY = m_chart.ViewXY.HighLowSeries.SelectMany(o => o.Points).Select(o => o.YHigh).Max();
                m_chart.ViewXY.YAxes[0].SetRange(minY, maxY);
            }

            m_chart.EndUpdate();

        }

        private void ShowBarSeriesChanged(DeviceRunInfo device)
        {
            foreach (var series in m_chart.ViewXY.HighLowSeries)
            {
                if (series.Tag == device)
                {
                    series.Visible = true;
                }
                else
                {
                    series.Visible = false;
                }
            }
        }

        private void marker_MouseOverOff(object sender, MouseEventArgs e)
        {
            ((SeriesEventMarker)sender).Label.Visible = false;
        }
        private void marker_MouseOverOn(object sender, MouseEventArgs e)
        {
            ((SeriesEventMarker)sender).Label.Visible = true;
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
    }
}
