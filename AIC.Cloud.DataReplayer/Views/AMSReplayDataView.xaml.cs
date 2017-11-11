using AIC.Cloud.Applications;
using AIC.Cloud.Applications.Events;
using AIC.Cloud.Applications.Services;
using AIC.Cloud.DataReplayer.ViewModels;
using AIC.Server.Storage.Contract;
using Arction.WPF.LightningChartUltimate;
using Arction.WPF.LightningChartUltimate.Annotations;
using Arction.WPF.LightningChartUltimate.Axes;
using Arction.WPF.LightningChartUltimate.EventMarkers;
using Arction.WPF.LightningChartUltimate.SeriesXY;
using Arction.WPF.LightningChartUltimate.Views.ViewXY;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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
using System.Windows.Threading;

namespace AIC.Cloud.DataReplayer.Views
{
    /// <summary>
    /// Interaction logic for AMSReplayDataView.xaml
    /// </summary>
    public partial class AMSReplayDataView : UserControl
    {
        private LightningChartUltimate m_chart;
        private AMSReplayDataViewModel viewModel;
        private IDisposable channelDataChangedSubscription;
        private IDisposable channelAddedSubscription;
        private IDisposable channelRemovedSubscription;

        private static System.Windows.Threading.DispatcherTimer readDataTimer = new System.Windows.Threading.DispatcherTimer();

        public AMSReplayDataView()
        {
            InitializeComponent();
            CreateChart();
            Loaded += AMSReplayDataView_Loaded;

            readDataTimer.Tick += new EventHandler(timeCycle);
            readDataTimer.Interval = new TimeSpan(0, 0, 0, 2);

        }
        private void AMSReplayDataView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= AMSReplayDataView_Loaded;
            viewModel = DataContext as AMSReplayDataViewModel;
            if (viewModel != null)
            {
                channelAddedSubscription = viewModel.WhenChannelAdded.Subscribe(OnChannelAdded);
                channelRemovedSubscription = viewModel.WhenChannelRemoved.Subscribe(OnChannelRemoved);
                channelDataChangedSubscription = viewModel.WhenChannelDataChanged.Subscribe(OnChannelDataChanged);
            }
        }

        private void OnChannelAdded(ChannelToken token)
        {
            try
            {
                m_chart.BeginUpdate();
                if (token is VibrationChannelToken)
                {
                    var vToken = token as VibrationChannelToken;
                    string unit = string.Empty;
                    switch (vToken.DataContracts[0].Unit)
                    {
                        case 0:
                            unit = "m/s2";
                            break;
                        case 1:
                            unit = "mm/s";
                            break;
                        case 2:
                            unit = "um";
                            break;
                    }
                    var axisY = m_chart.ViewXY.YAxes.Where(o => o.Units.Text == unit).SingleOrDefault();
                    if (axisY == null)
                    {
                        axisY = new AxisY(m_chart.ViewXY);
                        axisY.Title.Font = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Regular);
                        axisY.AxisThickness = 2;
                        axisY.AxisColor = Color.FromArgb(100, 135, 205, 238);
                        if (unit == "m/s2")
                        {
                            axisY.Title.Text = "加速度(" + unit + ")";
                        }
                        else if (unit == "mm/s")
                        {
                            axisY.Title.Text = "速度(" + unit + ")";
                        }
                        else if (unit == "um")
                        {
                            axisY.Title.Text = "位移(" + unit + ")";
                        }
                        axisY.Units.Text = unit;
                        axisY.SetRange(0, 10);
                        axisY.SegmentIndex = 0;
                        m_chart.ViewXY.YAxes.Add(axisY);
                        m_chart.ViewXY.AxisLayout.Segments[0].Height = 2;

                        if (m_chart.ViewXY.Annotations.Count == 0)
                        {
                            CreateAnnotation();
                        }
                    }

                    PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], axisY);
                    series.MouseInteraction = false;
                    series.Title.Text = vToken.Channel.Name + vToken.Channel.MSSN + "(" + unit + ")";
                    var childSeries = m_chart.ViewXY.PointLineSeries.Where(o => (o.Tag is DivFreChannelToken) && (((DivFreChannelToken)o.Tag).Channel.ChannelID == vToken.Channel.ChannelID)).SingleOrDefault();
                    if (childSeries != null)
                    {
                        series.LineStyle.Color = childSeries.LineStyle.Color;
                        series.Title.Color = childSeries.Title.Color;
                    }
                    else
                    {
                        int count = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag is VibrationChannelToken).Count();
                        while (count > 15)
                        {
                            count -= 15;
                        }
                        Color color = DefaultColors.SeriesForBlackBackgroundWPF[count];
                        series.LineStyle.Color = color;
                        series.Title.Color = color;
                    }
                    series.LineStyle.AntiAliasing = LineAntialias.Normal;
                    series.LineStyle.Width = 1;
                    series.Tag = token;

                    SeriesPoint[] points = new SeriesPoint[vToken.DataContracts.Length];
                    for (int i = 0; i < points.Length; i++)
                    {
                        points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(vToken.DataContracts[i].Date);
                        points[i].Y = vToken.DataContracts[i].Value;
                    }
                    series.Points = points;
                    m_chart.ViewXY.PointLineSeries.Add(series);

                    double minX = m_chart.ViewXY.PointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Min();
                    double maxX = m_chart.ViewXY.PointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Max();
                    m_chart.ViewXY.XAxes[0].SetRange(minX, maxX);

                    double maxValue = vToken.DataContracts.Select(o => o.Value).Max();
                    double minValue = vToken.DataContracts.Select(o => o.Value).Min();
                    if (minValue < axisY.Minimum + 5)
                    {
                        axisY.Minimum = minValue - 5;
                    }
                    if (maxValue > axisY.Maximum - 5)
                    {
                        axisY.Maximum = maxValue + 5;
                    }
                }
                else if (token is AnalogChannelToken)
                {
                    var anToken = token as AnalogChannelToken;
                    if (anToken.DataContracts != null)
                    {
                        string unit = string.Empty;
                        switch (anToken.DataContracts[0].Unit)
                        {
                            case 3:
                                unit = "°C";
                                break;
                            case 4:
                                unit = "Pa";
                                break;
                            case 5:
                                unit = "rpm";
                                break;
                        }
                        var axisY = m_chart.ViewXY.YAxes.Where(o => o.Units.Text == unit).SingleOrDefault();
                        if (axisY == null)
                        {
                            axisY = new AxisY(m_chart.ViewXY);
                            axisY.Title.Font = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Regular);
                            axisY.AxisThickness = 2;
                            axisY.AxisColor = Color.FromArgb(100, 135, 205, 238);
                            if (unit == "°C")
                            {
                                axisY.Title.Text = "温度(" + unit + ")";
                            }
                            else if (unit == "Pa")
                            {
                                axisY.Title.Text = "压力(" + unit + ")";
                            }
                            else if (unit == "rpm")
                            {
                                axisY.Title.Text = "转速(" + unit + ")";
                            }
                            axisY.Units.Text = unit;
                            axisY.SetRange(0, 10);
                            axisY.SegmentIndex = 1;
                            m_chart.ViewXY.YAxes.Add(axisY);
                            m_chart.ViewXY.AxisLayout.Segments[1].Height = 1;

                            if (m_chart.ViewXY.Annotations.Count == 0)
                            {
                                CreateAnnotation();
                            }
                        }
                        PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], axisY);
                        series.MouseInteraction = false;
                        series.Title.Text = anToken.Channel.Name + anToken.Channel.MSSN + "(" + unit + ")";
                        int count = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag is AnalogChannelToken).Count();
                        while (count > 15)
                        {
                            count -= 15;
                        }
                        Color color = DefaultColors.SeriesForBlackBackgroundWPF[count];
                        series.LineStyle.Color = color;
                        series.Title.Color = color;
                        series.LineStyle.AntiAliasing = LineAntialias.Normal;
                        series.LineStyle.Width = 1;
                        series.Tag = token;

                        SeriesPoint[] points = new SeriesPoint[anToken.DataContracts.Length];
                        for (int i = 0; i < points.Length; i++)
                        {
                            points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(anToken.DataContracts[i].Date);
                            points[i].Y = anToken.DataContracts[i].Value;
                        }
                        series.Points = points;
                        m_chart.ViewXY.PointLineSeries.Add(series);

                        double minX = m_chart.ViewXY.PointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Min();
                        double maxX = m_chart.ViewXY.PointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Max();
                        m_chart.ViewXY.XAxes[0].SetRange(minX, maxX);

                        double maxValue = anToken.DataContracts.Select(o => o.Value).Max();
                        double minValue = anToken.DataContracts.Select(o => o.Value).Min();
                        if (minValue < axisY.Minimum + 5)
                        {
                            axisY.Minimum = minValue - 5;
                        }
                        if (maxValue > axisY.Maximum - 5)
                        {
                            axisY.Maximum = maxValue + 5;
                        }
                    }
                }
                else if (token is DivFreChannelToken)
                {
                    var divToken = token as DivFreChannelToken;
                    string unit = string.Empty;
                    switch (divToken.DataContracts[0].Unit)
                    {
                        case 0:
                            unit = "分频-m/s2";
                            break;
                        case 1:
                            unit = "分频-mm/s";
                            break;
                        case 2:
                            unit = "分频-um";
                            break;
                    }
                    var axisY = m_chart.ViewXY.YAxes.Where(o => o.Units.Text == unit).SingleOrDefault();
                    if (axisY == null)
                    {
                        axisY = new AxisY(m_chart.ViewXY);
                        axisY.Title.Font = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Regular);
                        axisY.AxisThickness = 2;
                        axisY.AxisColor = Color.FromArgb(100, 135, 205, 238);
                        if (unit == "分频-m/s2")
                        {
                            axisY.Title.Text = "加速度(" + unit + ")";
                        }
                        else if (unit == "分频-mm/s")
                        {
                            axisY.Title.Text = "速度(" + unit + ")";
                        }
                        else if (unit == "分频-um")
                        {
                            axisY.Title.Text = "位移(" + unit + ")";
                        }
                        axisY.Units.Text = unit;
                        axisY.SetRange(0, 10);
                        axisY.SegmentIndex = 0;
                        m_chart.ViewXY.YAxes.Add(axisY);
                        m_chart.ViewXY.AxisLayout.Segments[0].Height = 2;

                        if (m_chart.ViewXY.Annotations.Count == 0)
                        {
                            CreateAnnotation();
                        }
                    }
                    Random rand = new Random();
                    PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], axisY);
                    series.MouseInteraction = false;
                    series.Title.Text = divToken.Channel.FreDescription + "(" + unit + ")";
                    var parentSeries = m_chart.ViewXY.PointLineSeries.Where(o => (o.Tag is VibrationChannelToken) && (((VibrationChannelToken)o.Tag).Channel.ChannelID == divToken.Channel.ChannelID)).SingleOrDefault();
                    if (parentSeries != null)
                    {
                        series.LineStyle.Color = parentSeries.LineStyle.Color;
                        series.Title.Color = parentSeries.Title.Color;
                    }
                    else
                    {
                        int count = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag is DivFreChannelToken).Count();
                        while (count > 15)
                        {
                            count -= 15;
                        }
                        Color color = DefaultColors.SeriesForBlackBackgroundWPF[count];
                        series.LineStyle.Color = color;
                        series.Title.Color = color;
                    }
                    series.LineStyle.Pattern = LinePattern.Dot;
                    series.LineStyle.AntiAliasing = LineAntialias.Normal;
                    series.LineStyle.Width = 1;
                    series.Tag = token;

                    SeriesPoint[] points = new SeriesPoint[divToken.DataContracts.Length];
                    for (int i = 0; i < points.Length; i++)
                    {
                        points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(divToken.DataContracts[i].Date);
                        points[i].Y = divToken.DataContracts[i].FreMV;
                    }
                    series.Points = points;
                    m_chart.ViewXY.PointLineSeries.Add(series);

                    double minX = m_chart.ViewXY.PointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Min();
                    double maxX = m_chart.ViewXY.PointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Max();
                    m_chart.ViewXY.XAxes[0].SetRange(minX, maxX);

                    double maxValue = divToken.DataContracts.Select(o => o.FreMV).Max();
                    double minValue = divToken.DataContracts.Select(o => o.FreMV).Min();
                    if (minValue < axisY.Minimum + 5)
                    {
                        axisY.Minimum = minValue - 5;
                    }
                    if (maxValue > axisY.Maximum - 5)
                    {
                        axisY.Maximum = maxValue + 5;
                    }
                }

                m_chart.ViewXY.Annotations[0].AssignYAxisIndex = -1;
                m_chart.ViewXY.Annotations[0].AssignYAxisIndex = 0;

                m_chart.EndUpdate();


                if (m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis < m_chart.ViewXY.XAxes[0].Minimum || m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis > m_chart.ViewXY.XAxes[0].Maximum)
                {
                    m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = (m_chart.ViewXY.XAxes[0].Minimum + m_chart.ViewXY.XAxes[0].Maximum) / 2.0;
                }
                else
                {
                    UpdateCursorResult(m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis);
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-通频趋势-添加通道", ex));
                m_chart.EndUpdate();
            }
        }
        private void OnChannelRemoved(ChannelToken token)
        {
            try
            {
                m_chart.BeginUpdate();
                var series = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag == token).SingleOrDefault();
                if (series != null)
                {
                    series.Clear();
                    m_chart.ViewXY.PointLineSeries.Remove(series);
                    string unit = string.Empty;
                    if (token is VibrationChannelToken)
                    {
                        if (((VibrationChannelToken)token).DataContracts.Length > 0)
                        {
                            switch (((VibrationChannelToken)token).DataContracts[0].Unit)
                            {
                                case 0:
                                    unit = "m/s2";
                                    break;
                                case 1:
                                    unit = "mm/s";
                                    break;
                                case 2:
                                    unit = "um";
                                    break;
                            }
                        }
                    }
                    else if (token is AnalogChannelToken)
                    {
                        if (((AnalogChannelToken)token).DataContracts.Length > 0)
                        {
                            switch (((AnalogChannelToken)token).DataContracts[0].Unit)
                            {
                                case 3:
                                    unit = "°C";
                                    break;
                                case 4:
                                    unit = "Pa";
                                    break;
                                case 5:
                                    unit = "rpm";
                                    break;
                            }
                        }
                    }
                    else if (token is DivFreChannelToken)
                    {
                        if (((DivFreChannelToken)token).DataContracts.Length > 0)
                        {
                            switch (((DivFreChannelToken)token).DataContracts[0].Unit)
                            {
                                case 0:
                                    unit = "分频-m/s2";
                                    break;
                                case 1:
                                    unit = "分频-mm/s";
                                    break;
                                case 2:
                                    unit = "分频-um";
                                    break;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(unit))
                    {
                        var units = m_chart.ViewXY.PointLineSeries.Select(o => Regex.Matches(o.Title.Text, @"\(([^)]*)\)").Cast<Match>().Select(x => x.Groups[1].Value).Last()).ToArray();
                        if (!units.Contains(unit))
                        {
                            var axisY = m_chart.ViewXY.YAxes.Where(o => o.Units.Text == unit).SingleOrDefault();
                            if (axisY != null)
                            {
                                int segmentIndex = axisY.SegmentIndex;
                                m_chart.ViewXY.YAxes.Remove(axisY);
                                if (m_chart.ViewXY.YAxes.Where(o => o.SegmentIndex == segmentIndex).Count() == 0)
                                {
                                    m_chart.ViewXY.AxisLayout.Segments[segmentIndex].Height = 0;
                                }
                                if (m_chart.ViewXY.YAxes.Count > 0)
                                {
                                    m_chart.ViewXY.Annotations[0].AssignYAxisIndex = -1;
                                    m_chart.ViewXY.Annotations[0].AssignYAxisIndex = 0;
                                    AnnotationXY cursorValueDisplay = m_chart.ViewXY.Annotations[0];
                                    float fTargetYCoord = m_chart.ViewXY.GetMarginsRect().Bottom;
                                    double dY;
                                    m_chart.ViewXY.YAxes[0].CoordToValue(fTargetYCoord, out dY);
                                    cursorValueDisplay.TargetAxisValues.Y = dY;
                                }
                            }
                        }
                    }
                }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-通频趋势-移除通道", ex));
                m_chart.EndUpdate();
            }
        }
        private void OnChannelDataChanged(IEnumerable<ChannelToken> tokens)
        {
            try
            {
                m_chart.BeginUpdate();
                foreach (var token in tokens)
                {
                    PointLineSeries series = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag == token).Single();
                    if (token is VibrationChannelToken)
                    {
                        var vToken = token as VibrationChannelToken;
                        SeriesPoint[] points = new SeriesPoint[vToken.DataContracts.Length];
                        for (int i = 0; i < points.Length; i++)
                        {
                            points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(vToken.DataContracts[i].Date);
                            points[i].Y = vToken.DataContracts[i].Value;
                        }
                        series.Points = points;
                    }
                    else if (token is DivFreChannelToken)
                    {
                        var divToken = token as DivFreChannelToken;
                        SeriesPoint[] points = new SeriesPoint[divToken.DataContracts.Length];
                        for (int i = 0; i < points.Length; i++)
                        {
                            points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(divToken.DataContracts[i].Date);
                            points[i].Y = divToken.DataContracts[i].FreMV;
                        }
                        series.Points = points;
                    }
                    else if (token is AnalogChannelToken)
                    {
                        var anToken = token as AnalogChannelToken;
                        SeriesPoint[] points = new SeriesPoint[anToken.DataContracts.Length];
                        for (int i = 0; i < points.Length; i++)
                        {
                            points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(anToken.DataContracts[i].Date);
                            points[i].Y = anToken.DataContracts[i].Value;
                        }
                        series.Points = points;
                    }
                }
                double minX = m_chart.ViewXY.PointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Min();
                double maxX = m_chart.ViewXY.PointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Max();
                m_chart.ViewXY.XAxes[0].SetRange(minX, maxX);
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-通频趋势-数据更新", ex));
                m_chart.EndUpdate();
            }
        }
        private void CreateChart()
        {
            gridChart.Children.Clear();
            if (m_chart != null)
            {
                m_chart.Dispose();
                m_chart = null;
            }

            m_chart = new LightningChartUltimate(LicenseKeyStrings.LightningChartUltimate);
            m_chart.BeginUpdate();
            m_chart.Title.Text = ""; ;
            m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Segmented;
            m_chart.ViewXY.AxisLayout.YAxisAutoPlacement = YAxisAutoPlacement.LeftThenRight;
            // m_chart.ViewXY.BeforeZooming += ViewXY_BeforeZooming;
            //m_chart.ViewXY.ZoomPanOptions.MouseWheelZooming = MouseWheelZooming.Vertical;


            // m_chart.ViewXY.AxisLayout.AutoAdjustAxisGap = 0;
            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.DateTime;
            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            //m_chart.ViewXY.XAxes[0].MinimumDateTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5));
            //m_chart.ViewXY.XAxes[0].MaximumDateTime = DateTime.Now;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].LabelsFont = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            //Create new Y axis for each series 
            //AxisY yAxis = new AxisY(m_chart.ViewXY);
            //yAxis.SetRange(-10, 10);
            //m_chart.ViewXY.YAxes.Add(yAxis);
            //Almost zero margins, bottom is 3u
            //   m_chart.ViewXY.Margins = new Thickness(3);

            m_chart.ViewXY.YAxes.Clear();
            m_chart.ViewXY.AxisLayout.Segments.Clear();
            m_chart.ViewXY.AxisLayout.Segments.Add(new YAxisSegment(m_chart.ViewXY.AxisLayout) { Height = 0 });
            m_chart.ViewXY.AxisLayout.Segments.Add(new YAxisSegment(m_chart.ViewXY.AxisLayout) { Height = 0 });

            m_chart.ViewXY.LegendBox.Visible = true;
            m_chart.ViewXY.LegendBox.Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.ViewXY.LegendBox.Fill.Style = RectFillStyle.None;
            m_chart.ViewXY.LegendBox.Shadow.Visible = false;
            m_chart.ViewXY.LegendBox.BorderWidth = 0;
            m_chart.ViewXY.LegendBox.Position = LegendBoxPosition.TopRight;
            m_chart.ViewXY.LegendBox.Offset.SetValues(-30, 5);
            m_chart.ViewXY.LegendBox.SeriesTitleFont = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.LegendBox.SeriesTitleMouseMoveOverOn += LegendBox_SeriesTitleMouseMoveOverOn;

            //Add cursor
            LineSeriesCursor cursor = new LineSeriesCursor(m_chart.ViewXY, m_chart.ViewXY.XAxes[0]);
            m_chart.ViewXY.LineSeriesCursors.Add(cursor);
            cursor.PositionChanged += new LineSeriesCursor.PositionChangedHandler(cursor_PositionChanged);
            cursor.LineStyle.Color = System.Windows.Media.Color.FromArgb(150, 255, 0, 0);
            cursor.LineStyle.Width = 2;
            cursor.SnapToPoints = true;
            cursor.TrackPoint.Color1 = Colors.White;

            m_chart.ViewXY.FitView();
            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);
        }

        private void ViewXY_BeforeZooming(List<RangeChangeInfo> xRanges, List<RangeChangeInfo> yRanges, bool byWheel, ref bool cancel)
        {
            m_chart.BeginUpdate();
            cancel = true;
            foreach (RangeChangeInfo rci in yRanges)
            {
                double yMid = (rci.OldMin + rci.OldMax) / 2.0;
                double newYRange = rci.NewMax - rci.NewMin;

                rci.Axis.SetRange(yMid - newYRange / 2.0, yMid + newYRange / 2.0);
            }
            m_chart.EndUpdate();
        }

        private void LegendBox_SeriesTitleMouseMoveOverOn(SeriesBaseXY series)
        {
            if (series is PointLineSeries)
            {
                viewModel.AllCount = ((PointLineSeries)series).PointCount;
            }
        }
        private void CreateAnnotation()
        {
            AnnotationXY cursorValueDisplay = new AnnotationXY(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            cursorValueDisplay.Style = AnnotationStyle.Callout;
            cursorValueDisplay.LocationCoordinateSystem = CoordinateSystem.RelativeCoordinatesToTarget;
            cursorValueDisplay.LocationRelativeOffset = new PointFloatXY(80, -50);
            cursorValueDisplay.Sizing = AnnotationXYSizing.Automatic;
            cursorValueDisplay.TextStyle.Font = new WPFFont(System.Drawing.FontFamily.GenericMonospace, 9.5, System.Drawing.FontStyle.Regular);
            cursorValueDisplay.TextStyle.Color = Colors.White;
            cursorValueDisplay.Fill.Color = Color.FromArgb(128, 0, 0, 0);
            cursorValueDisplay.BorderLineStyle.Color = Color.FromArgb(64, 255, 255, 255);
            cursorValueDisplay.BorderLineStyle.Width = 1;
            cursorValueDisplay.Fill.GradientFill = GradientFill.Solid;
            cursorValueDisplay.TargetMoveByMouse = false;
            cursorValueDisplay.AnchorAdjustByMouse = false;
            cursorValueDisplay.ResizeByMouse = false;
            cursorValueDisplay.RotateByMouse = false;
            cursorValueDisplay.Shadow.Visible = false;
            cursorValueDisplay.AutoSizePadding = 5;
            cursorValueDisplay.Text = "";
            cursorValueDisplay.ClipInsideGraph = false;
            m_chart.ViewXY.Annotations.Add(cursorValueDisplay);
        }
        private void CreateBand()
        {
            Band bandX = new Band(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            // bandX.Behind = true;
            bandX.Binding = AxisBinding.XAxis;
            Color cBandX = Colors.Lime;
            bandX.Fill.Color = Color.FromArgb(150, cBandX.R, cBandX.G, cBandX.B);
            bandX.Fill.GradientColor = ChartTools.CalcGradient(bandX.Fill.Color, Color.FromArgb(150, 0, 0, 0), 50);
            bandX.Fill.GradientDirection = 0;
            bandX.ValueBegin = 0;
            bandX.ValueEnd = 0;
            bandX.Title.Visible = true;
            bandX.Title.Text = string.Empty;
            // bandX.Title.Angle = 90;
            bandX.Title.Color = Colors.White;
            bandX.Visible = false;
            bandX.IncludeInAutoFit = true;
            bandX.UseLimits = true;
            bandX.ResizedByMouse += bandX_ResizedByMouse;
            bandX.MovedByMouse += bandX_MovedByMouse;
            bandX.ValuesChanged += bandX_ValuesChanged;
            m_chart.ViewXY.Bands.Add(bandX);
        }
        private void bandX_ValuesChanged(Band sender, double newBeginValue, double newEndValue, ref bool cancelRendering)
        {
            sender.Title.Text = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(newBeginValue) + "\r\n" + m_chart.ViewXY.XAxes[0].AxisValueToDateTime(newEndValue);
        }
        private void bandX_MovedByMouse(object sender, MouseEventArgs e)
        {
            Band band = sender as Band;
            viewModel.StartTime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(band.ValueBegin);
            viewModel.EndTime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(band.ValueEnd);
        }
        private void bandX_ResizedByMouse(Band sender, double ValueBegin, double ValueEnd, ref bool cancelRendering)
        {
            cancelRendering = true;
            viewModel.StartTime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(ValueBegin);
            viewModel.EndTime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(ValueEnd);
        }
        private void cursor_PositionChanged(LineSeriesCursor sender, double newValue, ref bool cancelRendering)
        {
            cancelRendering = true;
            UpdateCursorResult(sender.ValueAtXAxis);
        }
        private void UpdateCursorResult(double xValue)
        {
            try
            {
                m_chart.BeginUpdate();
                List<VibrationChannelToken> channelList = new List<VibrationChannelToken>();
                AnnotationXY cursorValueDisplay = m_chart.ViewXY.Annotations[0];
                float fTargetYCoord = m_chart.ViewXY.GetMarginsRect().Bottom;
                double dY;
                m_chart.ViewXY.YAxes[0].CoordToValue(fTargetYCoord, out dY);
                cursorValueDisplay.TargetAxisValues.X = xValue;
                cursorValueDisplay.TargetAxisValues.Y = dY;

                StringBuilder sb = new StringBuilder();
                int iSeriesNumber = 1;

                string strChannelStringFormat = "{0}: {1}({2})";
                string strValue = "";
                bool bLabelVisible = false;

                foreach (PointLineSeries series in m_chart.ViewXY.PointLineSeries)
                {
                    strValue = "";
                    int index = GetNearestIndex(series, xValue);
                    bLabelVisible = true;
                    if (series.Tag is VibrationChannelToken)
                    {
                        VibrationChannelToken token = series.Tag as VibrationChannelToken;
                        token.CurrentIndex = index;
                        channelList.Add(token);
                        if (index != -1)
                        {
                            VInfoTableAMSContract contract = token.DataContracts[index];  //. series.Points[index].Tag as VInfoTableAMSContract;
                            string unit = string.Empty;
                            switch (contract.Unit)
                            {
                                case 0:
                                    unit = "m/s2";
                                    break;
                                case 1:
                                    unit = "mm/s";
                                    break;
                                case 2:
                                    unit = "mm";
                                    break;
                            }
                            strValue = string.Format("{0}: {1}({2})|{3}", token.Channel.Name + token.Channel.MSSN, Math.Round(contract.Value, 3), unit, Math.Round(contract.RPM ?? 0.0, 3));
                        }
                        else
                        {
                            strValue = string.Format(strChannelStringFormat, token.Channel.Name + token.Channel.MSSN, "---", "Unit");
                        }
                    }
                    else if (series.Tag is AnalogChannelToken)
                    {
                        AnalogChannelToken token = series.Tag as AnalogChannelToken;
                        if (index != -1)
                        {
                            AnInfoTableAMSContract contract = token.DataContracts[index]; //series.Points[index].Tag as AnInfoTableAMSContract;
                            string unit = string.Empty;
                            switch (contract.Unit)
                            {
                                case 3:
                                    unit = "℃";
                                    break;
                                case 4:
                                    unit = "Pa";
                                    break;
                                case 5:
                                    unit = "RPM";
                                    break;
                            }
                            strValue = string.Format(strChannelStringFormat, token.Channel.Name + token.Channel.MSSN, Math.Round(contract.Value, 3), unit);
                        }
                        else
                        {
                            strValue = string.Format(strChannelStringFormat, token.Channel.Name + token.Channel.MSSN, "---", "Unit");
                        }
                    }
                    else if (series.Tag is DivFreChannelToken)
                    {
                        DivFreChannelToken token = series.Tag as DivFreChannelToken;
                        if (index != -1)
                        {
                            DivFreTableFreContract contract = token.DataContracts[index];
                            string unit = string.Empty;
                            switch (contract.Unit)
                            {
                                case 0:
                                    unit = "分频-m/s2";
                                    break;
                                case 1:
                                    unit = "分频-mm/s";
                                    break;
                                case 2:
                                    unit = "分频-mm";
                                    break;
                            }
                            strValue = string.Format(strChannelStringFormat, token.Channel.FreDescription, Math.Round(contract.FreMV, 3), unit);
                        }
                        else
                        {
                            strValue = string.Format(strChannelStringFormat, token.Channel.FreDescription, "---", "Unit");
                        }
                    }

                    sb.AppendLine(strValue);
                    // series.Title.Text = strValue;
                    iSeriesNumber++;
                }
                sb.AppendLine("Time: " + m_chart.ViewXY.XAxes[0].TimeString(xValue, "yyyy/MM/dd HH:mm:ss"));
                //Set text
                cursorValueDisplay.Text = sb.ToString().Trim();
                cursorValueDisplay.Visible = bLabelVisible;
                //Allow chart rendering
                m_chart.EndUpdate();

                if (channelList.Count > 0)
                {
                    viewModel.RaiseTrackChanged(channelList);
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-通频趋势-Track", ex));
                m_chart.EndUpdate();
            }
        }
        private bool SolveValueAccurate(PointLineSeries series, double xValue, out double yValue)
        {
            AxisY axisY = m_chart.ViewXY.YAxes[series.AssignYAxisIndex];
            yValue = 0;

            LineSeriesValueSolveResult lssvs = series.SolveYValueAtXValue(xValue);
            if (lssvs.SolveStatus == LineSeriesSolveStatus.OK)
            {
                //PointLineSeries may have two or more points at same X value. If so, center it between min and max 
                yValue = (lssvs.YMax + lssvs.YMin) / 2.0;
                return true;
            }
            else
            {
                return false;
            }
        }
        private int GetNearestIndex(PointLineSeries series, double xValue)
        {
            double x = 0;
            double y = 0;
            double yValue = 0;
            int index = 0;
            LineSeriesValueSolveResult lssvs = series.SolveYValueAtXValue(xValue);
            if (lssvs.SolveStatus == LineSeriesSolveStatus.OK)
            {
                //PointLineSeries may have two or more points at same X value. If so, center it between min and max 
                yValue = (lssvs.YMax + lssvs.YMin) / 2.0;
                series.SolveNearestDataPointByValue(xValue, yValue, out x, out y, out index);
                return index;
            }
            else
            {
                return -1;
            }
        }
        private void BandTogBtnClick(object sender, RoutedEventArgs e)
        {
            ToggleButton tgBtn = sender as ToggleButton;
            if ((bool)tgBtn.IsChecked)
            {
                Band bandx = m_chart.ViewXY.Bands[0];
                if (viewModel != null)
                {
                    viewModel.StartTime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(bandx.ValueBegin);
                    viewModel.EndTime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(bandx.ValueEnd);
                }
                bandx.Visible = true;
            }
            else
            {
                m_chart.ViewXY.Bands[0].Visible = false;
            }
        }
        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.BeginUpdate();
            foreach (var axisY in m_chart.ViewXY.YAxes)
            {
                axisY.SetRange(axisY.Minimum - 5, axisY.Maximum + 5);
            }
            m_chart.EndUpdate();
        }
        private void MovePrevious_Click(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis -= 1;

            //20170407add by htzk123
            if (m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis < m_chart.ViewXY.XAxes[0].Minimum)
            {
                m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = m_chart.ViewXY.XAxes[0].Maximum;
            }

            double nowValue = m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis;
            double nextValue = m_chart.ViewXY.XAxes[0].Minimum;
            for (int i = 0; i < m_chart.ViewXY.PointLineSeries.Count; i++)
            {
                double? tempValue = (from g in m_chart.ViewXY.PointLineSeries[i].Points where g.X <= nowValue select g.X).LastOrDefault();
                double valid = tempValue ?? m_chart.ViewXY.XAxes[0].Minimum;

                nextValue = Math.Max(nextValue, valid);
            }

            m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = nextValue;
            //end add
        }
        private void MoveNext_Click(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis += 1;

            //20170407add by htzk123
            if (m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis > m_chart.ViewXY.XAxes[0].Maximum)
            {
                m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = m_chart.ViewXY.XAxes[0].Minimum;
            }

            double nowValue = m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis;
            double nextValue = m_chart.ViewXY.XAxes[0].Maximum;
            for (int i = 0; i < m_chart.ViewXY.PointLineSeries.Count; i++)
            {
                double? tempValue = (from g in m_chart.ViewXY.PointLineSeries[i].Points where g.X >= nowValue select g.X).FirstOrDefault();
                double valid = tempValue ?? m_chart.ViewXY.XAxes[0].Maximum;

                nextValue = Math.Min(nextValue, valid);
            }

            m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = nextValue;
            //end add
        }

        //20170407add by htzk123
        private void MovePlay_Click(object sender, RoutedEventArgs e)
        {
            RepeatButton btn = (RepeatButton)sender;
            TransformGroup transformGroup = new TransformGroup();

            if (readDataTimer.IsEnabled)
            {
                btn.Content = "7";
                readDataTimer.Stop();

                ScaleTransform scaleTransform = new ScaleTransform();
                scaleTransform.ScaleX = 1;
                scaleTransform.ScaleY = 1;
                transformGroup.Children.Add(scaleTransform);
                RotateTransform rotateTransform = new RotateTransform(270, 0.5, 0.5);
                transformGroup.Children.Add(rotateTransform);
                TranslateTransform translateTransform = new TranslateTransform(0, -1);
                transformGroup.Children.Add(translateTransform);
                btn.RenderTransform = transformGroup;

            }
            else
            {
                btn.Content = "g";
                readDataTimer.Start();

                ScaleTransform scaleTransform = new ScaleTransform();
                scaleTransform.ScaleX = 0.8;
                scaleTransform.ScaleY = 0.8;
                transformGroup.Children.Add(scaleTransform);
                RotateTransform rotateTransform = new RotateTransform(270, 0.5, 0.5);
                transformGroup.Children.Add(rotateTransform);
                TranslateTransform translateTransform = new TranslateTransform(0, -1);
                transformGroup.Children.Add(translateTransform);
                btn.RenderTransform = transformGroup;
            }
        }

        public void timeCycle(object sender, EventArgs e)
        {
            MoveNext_Click(null, null);
        }

        //end add
    }
}
