using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.OnLineDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.SeriesXY;
using Arction.Wpf.Charting.Views.ViewPie3D;
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

namespace AIC.OnLineDataPage.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class OnlineDataStatisticsView : UserControl, ICloseable
    {
        private readonly IEventAggregator _eventAggregator;
        public OnlineDataStatisticsView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            CreateChart();

            this.Closer = new CloseableHeader((string)Application.Current.Resources["menuOnlineDataStatistics"], true);
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DataStatusEvent>().Subscribe(UpdateChart);
            this.Loaded += new RoutedEventHandler(Window_Loaded);

            ViewModel = this.DataContext as OnlineDataStatisticsViewModel;
        }
        public CloseableHeader Closer { get; private set; }

        OnlineDataStatisticsViewModel ViewModel;
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

            // Disable rendering, strongly recommended before updating chart properties.
            _chart.BeginUpdate();

            // Change active view to Pie3D view.
            _chart.ActiveView = ActiveView.ViewPie3D;

            _chart.ChartName = "";
            _chart.Title.Text = "";

            // Configure background.
            _chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            _chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            _chart.ChartBackground.GradientFill = GradientFill.Solid;
            _chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            _chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            _chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            // Configure 3D pie view.
            _chart.ViewPie3D.Style = PieStyle3D.Pie;
            _chart.ViewPie3D.SetPredefinedLightingScheme(LightingScheme.DirectionalFromCamera); // Configure lights, directional from camera.
            _chart.ViewPie3D.Material.SpecularColor = Colors.Black;                              // Disable specular color effect.
            _chart.ViewPie3D.Camera.SetPredefinedCamera(PredefinedCamera.PieTop);               // Set camera angle from top.
            _chart.ViewPie3D.Rounding = 15;                                                     // Set pie rounding.

            // Configure legend.
            _chart.ViewPie3D.LegendBox3DPie.Layout = LegendBoxLayout.Vertical;
            _chart.ViewPie3D.LegendBox3DPie.Position = LegendBoxPosition.BottomRight;

            // Configure pie titles.
            _chart.ViewPie3D.TitlesStyle = PieTitleStyle.Values;
            _chart.ViewPie3D.TitlesNumberFormat = "0";

            // Disable zooming.
            _chart.ViewPie3D.ZoomPanOptions.MouseWheelZoomEnabled = false;
            _chart.ViewPie3D.ZoomPanOptions.LeftMouseButtonAction = MouseButtonAction3D.None;
            _chart.ViewPie3D.ZoomPanOptions.RightMouseButtonAction = MouseButtonAction3D.None;

            // Add pie slice data.
            // By using TRUE as a last parameter, the slice will be automatically added to chart.ViewPie3D.Values collection
            PieSlice slice1 = new PieSlice("正常", Color.FromArgb(0xff, 0x87, 0xcd, 0xee), 0, _chart.ViewPie3D, true);
            PieSlice slice2 = new PieSlice("预警", Color.FromArgb(0xff, 0xff, 0xd4, 0x00), 0, _chart.ViewPie3D, true);
            PieSlice slice3 = new PieSlice("警告", Color.FromArgb(0xff, 0xf4, 0x79, 0x20), 0, _chart.ViewPie3D, true);
            PieSlice slice4 = new PieSlice("危险", Color.FromArgb(0xff, 0xd7, 0x13, 0x45), 0, _chart.ViewPie3D, true);
            PieSlice slice5 = new PieSlice("无效", Color.FromArgb(0xff, 0xf1, 0x73, 0xac), 0, _chart.ViewPie3D, true);
            PieSlice slice6 = new PieSlice("掉线", Color.FromArgb(0xff, 0x84, 0x02, 0x28), 0, _chart.ViewPie3D, true);

            slice1.MouseClick += Slice_MouseClick;
            slice2.MouseClick += Slice_MouseClick;
            slice3.MouseClick += Slice_MouseClick;
            slice4.MouseClick += Slice_MouseClick;
            slice5.MouseClick += Slice_MouseClick;
            slice6.MouseClick += Slice_MouseClick;

            _chart.ViewPie3D.SetPieSize(180);

            // Allow chart rendering.
            _chart.EndUpdate();

            gridChart.Children.Add(_chart);
        }

        private void Slice_MouseClick(object sender, MouseEventArgs e)
        {
            PieSlice slice = sender as PieSlice;
            switch (slice.Title.Text)
            {
                case "正常": ViewModel.SliceClick(AlarmGrade.Normal);  break;
                case "预警": ViewModel.SliceClick(AlarmGrade.PreAlarm); break;
                case "警告": ViewModel.SliceClick(AlarmGrade.Alarm); break;
                case "危险": ViewModel.SliceClick(AlarmGrade.Danger); break;
                case "无效": ViewModel.SliceClick(AlarmGrade.Invalid); break;
                case "掉线": ViewModel.SliceClick(AlarmGrade.DisConnect); break;
            }
        }

        private void UpdateChart(List<int> valuelist)
        {
            if (valuelist == null || valuelist.Count < 6)
            {
                return;
            }
            _chart.BeginUpdate();
            _chart.ViewPie3D.Values[0].Value = valuelist[0];
            _chart.ViewPie3D.Values[1].Value = valuelist[1];
            _chart.ViewPie3D.Values[2].Value = valuelist[2];
            _chart.ViewPie3D.Values[3].Value = valuelist[3];
            _chart.ViewPie3D.Values[4].Value = valuelist[4];
            _chart.ViewPie3D.Values[5].Value = valuelist[5];
            _chart.EndUpdate();
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
}