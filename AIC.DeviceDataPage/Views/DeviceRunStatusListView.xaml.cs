using AIC.Core.SignalModels;
using AIC.DeviceDataPage.Models;
using AIC.DeviceDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Axes;
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
    public partial class DeviceRunStatusListView : UserControl, ICloseable
    {
        public DeviceRunStatusListView()
        {
            InitializeComponent();

            this.Closer = new CloseableHeader((string)Application.Current.Resources["menuEquipmentRunStatus"], true);

            DeviceRunStatusListViewModel vm = this.DataContext as DeviceRunStatusListViewModel;
            if (vm != null)
            {
                vm.UpdateChart += UpdateChart;
            }

            CreateChart();

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
            _chart.ViewXY.BarViewOptions.Stacking = BarsStacking.Stack;
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

        private void UpdateChart(List<DeviceRunInfo> deviceList)
        {
            _chart.BeginUpdate();
            _chart.ViewXY.BarSeries.Clear();
            _chart.ViewXY.XAxes[0].CustomTicks.Clear();

            int pointsCount = deviceList.Count;
            double max = deviceList.Select(p => p.RunHours).Max();
            double min = 0 - deviceList.Select(p => p.StopHours).Max();
            int with = (int)((_chart.ActualWidth - 100) / (pointsCount + 1) - 20);
            _chart.ViewXY.YAxes[0].SetRange(min, max);
            _chart.ViewXY.XAxes[0].SetRange(0.0, (double)(pointsCount + 1));
            // Add a bar series for each point, with a different color.
            for (int i = 0; i < 2; i++)
            {
                BarSeriesValue[] data = new BarSeriesValue[pointsCount];
                for (int j = 0; j < pointsCount; j++)
                {
                    if (i == 0)
                    {
                        data[j].Value = deviceList[j].RunHours;
                        data[j].Location = j + 1;
                        //Set label text
                        data[j].Text = deviceList[j].RunHours.ToString("0");
                        data[j].Tag = deviceList[j].RunHours.ToString("0");
                    }
                    else if (i == 1)
                    {
                        data[j].Value = 0 - deviceList[j].StopHours;
                        data[j].Location = j + 1;
                        //Set label text                       
                        data[j].Text = deviceList[j].StopHours.ToString("0");
                        data[j].Tag = deviceList[j].StopHours.ToString("0");
                    }                  

                    if (i == 0)
                    {
                        // Let's use custom axis ticks to display days of the month.
                        CustomAxisTick tick = new CustomAxisTick(_chart.ViewXY.XAxes[0]);
                        tick.AxisValue = (double)(j + 1);

                        string name = deviceList[j].DeviceTreeItemViewModel.Name;
                        int HanNumlenght = GetHanNumFromString(name);
                        tick.LabelText = name.Substring(0, HanNumlenght) + "\r\n" + name.Substring(HanNumlenght);
                        tick.Length = 0;                        
                        _chart.ViewXY.XAxes[0].CustomTicks.Add(tick);
                    }
                }
                BarSeries bs = new BarSeries(_chart.ViewXY, _chart.ViewXY.XAxes[0], _chart.ViewXY.YAxes[0]);
                //Set series title 
                if (i == 0)
                {
                    bs.Title.Text = "运行";
                }
                else if (i == 1)
                {
                    bs.Title.Text = "停止";
                }
                //Set series fill 
                if (i == 0)
                {
                    bs.Fill.Color = Color.FromArgb(255, 0x00, 0xff, 0x00);//绿色
                }
                else if (i == 1)
                {
                    bs.Fill.Color = Color.FromArgb(255, 0xff, 0x00, 0x00);//红色
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
        }

        private void gridChart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_chart.ViewXY.BarSeries.Count == 0)
            {
                return;
            }
            _chart.BeginUpdate();
            int pointsCount = _chart.ViewXY.BarSeries[0].Values.Count();
            int with = (int)((_chart.ActualWidth - 100) / (pointsCount + 1) - 20);
            _chart.ViewXY.BarSeries.ForEach(p => p.BarThickness = with);
            _chart.EndUpdate();
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
