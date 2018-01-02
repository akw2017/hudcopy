using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.HistoryDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.SeriesXY;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using Wpf.CloseTabControl;

namespace AIC.HistoryDataPage.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class HistoryDataStatisticsView : UserControl, ICloseable
    {
        public HistoryDataStatisticsView()
        {
            InitializeComponent();

            this.Closer = new CloseableHeader((string)Application.Current.Resources["menuHistoryDataStatistics"], true);
          
            HistoryDataStatisticsViewModel vm = this.DataContext as HistoryDataStatisticsViewModel;
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

        }

        private void UpdateChart(List<BaseAlarmSignal> sglist)
        {
            _chart.BeginUpdate();
            _chart.ViewXY.BarSeries.Clear();
            _chart.ViewXY.XAxes[0].CustomTicks.Clear();

            var devicelist = sglist.GroupBy(p => p.OrganizationDeviceName).ToList(); 
            int pointsCount = devicelist.Count;
            var max = devicelist.Select(p => p.Select(c => c.PreAlarmCount + c.AlarmCount + c.DangerCount).Sum()).Max();// sglist.Max(p => p.AlarmCount + p.PreAlarmCount + p.DangerCount);
            int with = (int)((_chart.ActualWidth - 100) / (pointsCount + 1) - 20) / 3;
            _chart.ViewXY.YAxes[0].SetRange(0, max);
            _chart.ViewXY.XAxes[0].SetRange(0.0, (double)(pointsCount + 1));
            // Add a bar series for each point, with a different color.
            for (int i = 0; i < 3; i++)
            {               
                BarSeriesValue[] data = new BarSeriesValue[pointsCount];              
                for (int j = 0; j < pointsCount; j++)
                {                  
                    if (i == 0)
                    {
                        data[j].Value = devicelist[j].Select(p => p.PreAlarmCount).Sum();
                        data[j].Location = j + 1;
                        //Set label text
                      
                        data[j].Text = data[j].Value.ToString("0");                       
                        data[j].Tag = data[j].Value.ToString("0");
                    }
                    else if (i == 1)
                    {
                        data[j].Value = devicelist[j].Select(p => p.AlarmCount).Sum();
                        data[j].Location = j + 1;
                        //Set label text                       
                        data[j].Text = data[j].Value.ToString("0"); 
                        data[j].Tag = data[j].Value.ToString("0");
                    }
                    else if (i == 2)
                    {
                        data[j].Value = devicelist[j].Select(p => p.DangerCount).Sum();
                        data[j].Location = j + 1;
                        //Set label text                      
                        data[j].Text = data[j].Value.ToString("0");
                        data[j].Tag = data[j].Value.ToString("0");
                    }

                    if (i == 0)
                    {
                        // Let's use custom axis ticks to display days of the month.
                        CustomAxisTick tick = new CustomAxisTick(_chart.ViewXY.XAxes[0]);
                        tick.AxisValue = (double)(j + 1);
                        tick.LabelText = devicelist[j].Select(p => p.DeviceName).FirstOrDefault();
                        tick.Length = 0;
                        _chart.ViewXY.XAxes[0].CustomTicks.Add(tick);
                    }
                }
                BarSeries bs = new BarSeries(_chart.ViewXY, _chart.ViewXY.XAxes[0], _chart.ViewXY.YAxes[0]);
                //Set series title 
                if (i == 0)
                {
                    bs.Title.Text = "预警";
                }
                else if (i == 1)
                {
                    bs.Title.Text = "警告";
                }
                else if (i == 2)
                {
                    bs.Title.Text = "危险";
                }
                //Set series fill 
                //bs.Fill.Color = DefaultColors.SeriesForBlackBackgroundWpf[index];
                //bs.Fill.GradientColor = ChartTools.CalcGradient(DefaultColors.SeriesForBlackBackgroundWpf[index], Colors.Black, 50);
                //bs.BorderColor = ChartTools.CalcGradient(bs.Fill.Color, Colors.Black, 70); 

                if (i == 0)
                {
                    bs.Fill.Color = Color.FromArgb(255, 0xff, 0xd4, 0x00);//黄色
                }
                else if (i == 1)
                {
                    bs.Fill.Color = Color.FromArgb(255, 0xf4, 0x79, 0x20);//橙色
                }
                else if (i == 2)
                {
                    bs.Fill.Color = Color.FromArgb(255, 0xd7, 0x13, 0x45);//红色
                }
                bs.Fill.GradientFill = GradientFill.Solid;
                //bs.Fill.GradientColor = ChartTools.CalcGradient(bs.Fill.Color, Colors.Black, 50);
                //bs.Fill.GradientDirection = 0;
                //bs.Fill.Style = RectFillStyle.ColorOnly;
                //bs.BarThickness = 100;
                //bs.Shadow.Visible = true;
                //bs.Shadow.Offset.SetValues(2, 2);
                //bs.BorderColor = ChartTools.CalcGradient(bs.Fill.Color, Colors.Black, 70);
                //bs.LabelStyle.Shadow.Style = TextShadowStyle.DropShadow;

                bs.Shadow.Visible = false;
                //bs.Shadow.Offset.SetValues(2, 2);

                //Set label text style
                bs.LabelStyle.Angle = 0;
                bs.LabelStyle.VerticalAlign = BarsTitleVerticalAlign.BarCenter;

                //Assign the value
                bs.Values = data;
                bs.BarThickness = with;

                _chart.ViewXY.BarSeries.Add(bs);
            }
            _chart.EndUpdate();

            gridChart.SizeChanged += gridChart_SizeChanged;
        }

        private void gridChart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_chart.ViewXY.BarSeries.Count == 0)
            {
                return;
            }
            _chart.BeginUpdate();
            int pointsCount = _chart.ViewXY.BarSeries[0].Values.Count();          
            int with = (int)((_chart.ActualWidth - 100) / (pointsCount + 1) - 20) / 3;
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
        //private void UpdateChart(List<BaseAlarmSignal> sglist)
        //{
        //    _chart.BeginUpdate();
        //    _chart.ViewXY.BarSeries.Clear();
        //    int pointsCount = sglist.Count;
        //    var max = sglist.Max(p => p.AlarmCount + p.PreAlarmCount + p.DangerCount);
        //    _chart.ViewXY.YAxes[0].SetRange(0, max);          
        //    // Add a bar series for each point, with a different color.
        //    for (int index = 0; index < pointsCount; index++)
        //    {
        //        if (index >= 16)
        //        {
        //            break;
        //        }
        //        BarSeriesValue[] data = new BarSeriesValue[1];
        //        data[0].Value = sglist[index].PreAlarmCount + sglist[index].AlarmCount + sglist[index].DangerCount;
        //        data[0].Location = index;
        //        //Set label text
        //        data[0].Text = "预警" + sglist[index].PreAlarmCount.ToString("0") + "警告" + sglist[index].AlarmCount.ToString("0") + "危险" + sglist[index].DangerCount.ToString("0");

        //        BarSeries bs = new BarSeries(_chart.ViewXY, _chart.ViewXY.XAxes[0], _chart.ViewXY.YAxes[0]);
        //        //Set series title 
        //        bs.Title.Text = sglist[index].ItemName;
        //        //Set series fill 
        //        //bs.Fill.Color = DefaultColors.SeriesForBlackBackgroundWpf[index];
        //        //bs.Fill.GradientColor = ChartTools.CalcGradient(DefaultColors.SeriesForBlackBackgroundWpf[index], Colors.Black, 50);
        //        //bs.BorderColor = ChartTools.CalcGradient(bs.Fill.Color, Colors.Black, 70); 
        //        Color color = DefaultColors.SeriesWpf[index];
        //        bs.Fill.Color = Color.FromArgb(240, color.R, color.G, color.B);

        //        bs.Fill.GradientColor = ChartTools.CalcGradient(bs.Fill.Color, Colors.Black, 50);
        //        bs.Fill.GradientDirection = 0;
        //        bs.Fill.Style = RectFillStyle.ColorOnly;
        //        bs.BarThickness = 100;
        //        bs.Shadow.Visible = true;
        //        bs.Shadow.Offset.SetValues(2, 2);
        //        bs.BorderColor = ChartTools.CalcGradient(bs.Fill.Color, Colors.Black, 70);
        //        bs.LabelStyle.Shadow.Style = TextShadowStyle.DropShadow;

        //        bs.Shadow.Visible = true;
        //        bs.Shadow.Offset.SetValues(2, 2);

        //        //Set label text style
        //        bs.LabelStyle.Angle = 0;
        //        bs.LabelStyle.VerticalAlign = BarsTitleVerticalAlign.BarTop;             

        //        //Assign the value
        //        bs.Values = data;

        //        _chart.ViewXY.BarSeries.Add(bs);

        //    }
        //    _chart.EndUpdate();
        //}
    }
}