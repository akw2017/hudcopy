using AIC.Core;
using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.OnLineDataPage.ViewModels;
using AIC.OnLineDataPage.Views.SubViews;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Annotations;
using Arction.Wpf.Charting.SeriesXY;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Reactive.Linq;
using System.Linq;
using Arction.Wpf.Charting.Views.ViewXY;
using Arction.Wpf.Charting.Axes;
using System.Collections.Generic;
using Arction.Wpf.Charting.EventMarkers;
using System.Windows.Input;
using Arction.Wpf.Charting.Views;
using AIC.Core.Events;

namespace AIC.OnLineDataPage.Views.SubViews
{
    /// <summary>
    /// Interaction logic for TimeDomainOnLineView.xaml
    /// </summary>
    public partial class MultiDivFreChartView : ChartViewBase
    {
        private LightningChartUltimate m_chart;
        private IDictionary<PointLineSeries, bool> showMarkerDict = new Dictionary<PointLineSeries, bool>();

        public MultiDivFreChartView()
        {
            InitializeComponent();
            CreateChart();
        }

        protected override void ViewModel_Closed(object sender, EventArgs e)
        {
            base.ViewModel_Closed(sender, e);
            // Don't forget to clear chart grid child list.
            gridChart.Children.Clear();
            if (m_chart != null)
            {
                m_chart.Dispose();
                m_chart = null;
            }
        }

        protected override void ViewModel_SignalChanged()
        {
            try
            {
                m_chart.BeginUpdate();
                foreach (var series in m_chart.ViewXY.PointLineSeries)
                {
                    series.SeriesEventMarkers.Clear();
                }
                m_chart.ViewXY.PointLineSeries.Clear();

                double dMinX = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(DateTime.Now) - 10;
                double dMaxX = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(DateTime.Now) + 40;
                m_chart.ViewXY.XAxes[0].SetRange(dMinX, dMaxX);

                if (ViewModel != null && (ViewModel.Signal is BaseDivfreSignal))
                {
                    var vSg = ViewModel.Signal as BaseDivfreSignal;
                    //分频   
                    int count = 0;
                    foreach (var divFre in vSg.DivFres)
                    {
                        PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
                        showMarkerDict.Add(series, true);
                        series.Title.Text = divFre.DisPlayName;
                        series.MouseInteraction = false;
                        series.LineStyle.AntiAliasing = LineAntialias.None;
                        series.LineStyle.Width = 1;
                        series.LineStyle.Color = DefaultColors.SeriesForBlackBackgroundWpf[count];

                        //相位
                        PointLineSeries phaseSeries = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[1]);
                        phaseSeries.Title.Text = divFre.DisPlayName + "相位";
                        phaseSeries.MouseInteraction = false;
                        phaseSeries.LineStyle.AntiAliasing = LineAntialias.None;
                        phaseSeries.LineStyle.Width = 1;
                        phaseSeries.LineStyle.Color = DefaultColors.SeriesForBlackBackgroundWpf[count];
                        phaseSeries.Visible = false;
                        phaseSeries.CursorTrackEnabled = false;

                        m_chart.ViewXY.PointLineSeries.Add(series);
                        m_chart.ViewXY.PointLineSeries.Add(phaseSeries);

                        count++;
                    }
                    if (vSg.DivFres.Count() > 0)
                    {
                        double dMinY = vSg.DivFres.Select(o => o.FreMV).Min();
                        double dMaxY = vSg.DivFres.Select(o => o.FreMV).Max();
                        m_chart.ViewXY.YAxes[0].SetRange(0, dMaxY + 5);
                    }
                }                
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-分频(多)-信号变换", ex));
            }
            finally
            {
                m_chart.EndUpdate();
            }
        }
        protected override void UpdateChart(object args)
        {
            try
            {
                if (ViewModel == null || !(ViewModel.Signal is BaseDivfreSignal))
                {
                    return;
                }

                m_chart.BeginUpdate();
                BaseDivfreSignal signal = (BaseDivfreSignal)ViewModel.Signal;
                if (signal.DivFres.Count() > 0)
                {
                    //有效值趋势
                    foreach (var divFre in signal.DivFres)
                    {
                        if (divFre.IsUpload)
                        {
                            PointLineSeries series = m_chart.ViewXY.PointLineSeries.Where(o => o.Title.Text == divFre.DisPlayName).SingleOrDefault();
                            double m_dLatestX = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(divFre.ACQDatetime);
                            if (series != null)
                            {
                                SeriesPoint[] points = new SeriesPoint[1];
                                points[0].X = m_dLatestX;
                                points[0].Y = divFre.FreMV;
                                string unit = signal.Unit;
                                //if (signal.Unit == Unit.Acceleration)
                                //{
                                //    unit = "m/s^2";
                                //}
                                //else if (signal.Unit == Unit.Velocity)
                                //{
                                //    unit = "mm/s";
                                //}
                                //else if (signal.Unit == Unit.Displacement)
                                //{
                                //    unit = "um";
                                //}
                                points[0].Tag = unit + ";" + divFre.FreV + ";" + divFre.Phase;
                                string alarmTypeStr = JudgeAlarmType(divFre.AlarmType);
                                if (!string.IsNullOrEmpty(alarmTypeStr))
                                {
                                    SeriesEventMarker marker = new SeriesEventMarker(series);
                                    marker.Visible = showMarkerDict[series];
                                    marker.XValue = points[0].X;
                                    marker.YValue = points[0].Y;
                                    marker.HorizontalPosition = SeriesEventMarkerHorizontalPosition.AtXValue;
                                    marker.Symbol.Width = 5;
                                    marker.Symbol.Height = 5;

                                    marker.Label.Text = alarmTypeStr + "\r\n" + divFre.FreV + "|" + divFre.FreMV.ToString("0.000") + "(" + unit + ")";
                                    marker.Label.HorizontalAlign = AlignmentHorizontal.Center;
                                    marker.Label.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9f, System.Drawing.FontStyle.Bold);
                                    marker.Label.VerticalAlign = AlignmentVertical.Top;
                                    marker.Label.Visible = false;
                                    marker.Symbol.GradientFill = GradientFillPoint.Solid;
                                    if (alarmTypeStr.Contains("危险"))
                                    {
                                        marker.Symbol.Color1 = Colors.Red;
                                    }
                                    else if (alarmTypeStr.Contains("警告"))
                                    {
                                        marker.Symbol.Color1 = Colors.DarkOrange;
                                    }
                                    marker.Symbol.Shape = Shape.Circle;
                                    marker.VerticalPosition = SeriesEventMarkerVerticalPosition.AtYValue;
                                    marker.MoveByMouse = false;
                                    marker.MouseOverOn += marker_MouseOverOn;
                                    marker.MouseOverOff += marker_MouseOverOff;
                                    series.SeriesEventMarkers.Add(marker);
                                }
                                series.AddPoints(points, false);
                                series.InvalidateData();
                                if (!(bool)scrollCheckBox.IsChecked)
                                {
                                    m_chart.ViewXY.XAxes[0].ScrollPosition = m_dLatestX;
                                }
                            }
                            //相位
                            PointLineSeries phaseSeries = m_chart.ViewXY.PointLineSeries.Where(o => o.Title.Text == divFre.DisPlayName + "相位").SingleOrDefault();
                            if (phaseSeries != null)
                            {
                                SeriesPoint[] points = new SeriesPoint[1];
                                points[0].X = m_dLatestX;
                                points[0].Y = divFre.Phase;
                                phaseSeries.AddPoints(points, false);
                            }
                        }
                    }
                }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-分频(多)", ex));
                m_chart.EndUpdate();
            }
            finally
            {
                
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

        private string JudgeAlarmType(int alarmType)
        {
            string alarmTypeStr = string.Empty;
            int consts = alarmType & 0X03;
            int dynamic = alarmType & 0X0C;
            int comparative = alarmType & 0X30;
            switch (consts)
            {
                case 2:
                    alarmTypeStr += "常数报警：警告";
                    break;
                case 3:
                    alarmTypeStr += "常数报警：危险";
                    break;
            }
            switch (dynamic)
            {
                case 8:
                    alarmTypeStr += "\r\n" + "曲线报警：警告";
                    break;
                case 12:
                    alarmTypeStr += "\r\n" + "曲线报警：危险";
                    break;
            }
            switch (comparative)
            {
                case 32:
                    alarmTypeStr += "\r\n" + "相对报警：警告";
                    break;
                case 48:
                    alarmTypeStr += "\r\n" + "相对报警：危险";
                    break;
            }
            return alarmTypeStr;
        }

        private void CreateChart()
        {
            gridChart.Children.Clear();
            if (m_chart != null)
            {
                m_chart.Dispose();
                m_chart = null;
            }

            m_chart = new LightningChartUltimate();
            m_chart.BeginUpdate();
            m_chart.Title.Text = "";
            //m_chart.ViewXY.ZoomPanOptions.MouseWheelZooming = MouseWheelZooming.Horizontal;
            // m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Layered;
            m_chart.ViewXY.AxisLayout.YAxisAutoPlacement = YAxisAutoPlacement.LeftThenRight;
            m_chart.ViewXY.DropOldSeriesData = true;
            m_chart.ViewXY.DropOldEventMarkers = true;

            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            //Disable automatic axis layouts 
            m_chart.ViewXY.AxisLayout.AutoAdjustMargins = false;
            m_chart.ViewXY.AxisLayout.XAxisAutoPlacement = XAxisAutoPlacement.Off;
            m_chart.ViewXY.AxisLayout.YAxisAutoPlacement = YAxisAutoPlacement.Off;
            m_chart.ViewXY.AxisLayout.XAxisTitleAutoPlacement = false;
            m_chart.ViewXY.AxisLayout.YAxisTitleAutoPlacement = false;

            //Almost zero margins, bottom is 3u
            m_chart.ViewXY.Margins = new Thickness(0);

            //Setup x-axis
            m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.DateTime;
            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
            //m_chart.ViewXY.XAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.XAxes[0].LabelsPosition = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Alignment = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Alignment = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].VerticalAlign = AlignmentVertical.Top;

            //Setup y-axis
            m_chart.ViewXY.YAxes[0].Title.Visible = false;
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].AxisThickness = 2;
            m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
            //m_chart.ViewXY.YAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.YAxes[0].MiniScale.Visible = false;
            m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Alignment = Alignment.Far;
            m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Alignment = Alignment.Far;
            m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].Alignment = AlignmentHorizontal.Right;

            //Second Y Axis
            AxisY secondYAxis = new AxisY(m_chart.ViewXY, true);
            secondYAxis.ValueType = AxisValueType.Number;
            secondYAxis.Position = 100;
            secondYAxis.Title.Visible = false;
            secondYAxis.MinorGrid.Visible = false;
            secondYAxis.AxisThickness = 2;
            secondYAxis.AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
            //secondYAxis.MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            secondYAxis.AxisColor = Colors.Teal;
            secondYAxis.MinorGrid.Visible = false;
            secondYAxis.LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            secondYAxis.MiniScale.Visible = false;
            secondYAxis.MajorDivTickStyle.Alignment = Alignment.Near;
            secondYAxis.MajorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
            secondYAxis.MinorDivTickStyle.Alignment = Alignment.Near;
            secondYAxis.MinorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
            secondYAxis.Alignment = AlignmentHorizontal.Left;
            secondYAxis.SetRange(0, 360);

            m_chart.ViewXY.LegendBoxes[0].Visible = true;
            m_chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.ViewXY.LegendBoxes[0].Fill.Style = RectFillStyle.None;
            m_chart.ViewXY.LegendBoxes[0].Shadow.Visible = false;
            m_chart.ViewXY.LegendBoxes[0].BorderWidth = 0;
            m_chart.ViewXY.LegendBoxes[0].Position = LegendBoxPositionXY.TopCenter;
            m_chart.ViewXY.LegendBoxes[0].Offset.SetValues(0, 0);
            m_chart.ViewXY.LegendBoxes[0].SeriesTitleFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.LegendBoxes[0].CheckBoxStateChanged += LegendBox_CheckBoxStateChanged;

            //Add cursor
            LineSeriesCursor cursor = new LineSeriesCursor(m_chart.ViewXY, m_chart.ViewXY.XAxes[0]);
            m_chart.ViewXY.LineSeriesCursors.Add(cursor);
            cursor.PositionChanged += cursor_PositionChanged;
            cursor.LineStyle.Color = System.Windows.Media.Color.FromArgb(150, 255, 0, 0);
            cursor.LineStyle.Width = 2;
            cursor.SnapToPoints = true;
            cursor.TrackPoint.Color1 = Colors.White;
            cursor.Visible = false;

            CreateCalloutAnnotation();

            // m_chart.ViewXY.FitView();
            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);
        }
        private void CreateCalloutAnnotation()
        {
            AnnotationXY cursorValueDisplay = new AnnotationXY(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            cursorValueDisplay.Style = AnnotationStyle.Callout;
            cursorValueDisplay.LocationCoordinateSystem = CoordinateSystem.RelativeCoordinatesToTarget;
            cursorValueDisplay.LocationRelativeOffset = new PointFloatXY(80, -50);
            cursorValueDisplay.Sizing = AnnotationXYSizing.Automatic;
            cursorValueDisplay.TextStyle.Font = new WpfFont(System.Drawing.FontFamily.GenericMonospace, 9.5, System.Drawing.FontStyle.Regular);
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
        private void cursor_PositionChanged(LineSeriesCursor sender, double newValue, ref bool cancelRendering)
        {
            cancelRendering = true;
            UpdateCursorResult(sender.ValueAtXAxis);
        }

        private void cursor_PositionChanged(Object sender, Arction.Wpf.Charting.Views.ViewXY.PositionChangedEventArgs e)
        {
            e.CancelRendering = true;
            UpdateCursorResult();
        }

        private void UpdateCursorResult(double xValue)
        {
            try
            {
                m_chart.BeginUpdate();

                AnnotationXY cursorValueDisplay = m_chart.ViewXY.Annotations[0];
                float fTargetYCoord = m_chart.ViewXY.GetMarginsRect().Bottom;
                double dY;
                m_chart.ViewXY.YAxes[0].CoordToValue(fTargetYCoord, out dY);
                cursorValueDisplay.TargetAxisValues.X = xValue;
                cursorValueDisplay.TargetAxisValues.Y = dY;

                LineSeriesCursor lineSeriesCursor = m_chart.ViewXY.LineSeriesCursors[0];
                var serieses = m_chart.ViewXY.PointLineSeries.Where(o => o.AssignYAxisIndex == 0).ToArray();
                if (serieses.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var series in serieses)
                    {
                        int index = GetNearestPointIndex(series, lineSeriesCursor.ValueAtXAxis);
                        if (index != -1)
                        {
                            SeriesPoint point = series.Points[index];
                            string[] tag = point.Tag.ToString().Split(';');
                            sb.AppendLine(series.Title.Text + "-" + string.Format("幅值:{0},频率:{1},相位:{2}", Math.Round(point.Y, 3) + "(" + tag[0] + ")", Math.Round(double.Parse(tag[1]), 3), Math.Round(double.Parse(tag[2]), 1)));
                        }
                    }
                    if (sb.Length > 0)
                    {
                        sb.AppendLine("时间: " + m_chart.ViewXY.XAxes[0].TimeString(xValue, "yyyy-MM-dd HH:mm:ss"));
                        cursorValueDisplay.Text = sb.ToString().Trim();
                        cursorValueDisplay.Visible = true;
                    }
                    else
                    {
                        cursorValueDisplay.Text = string.Empty;
                        cursorValueDisplay.Visible = false;
                    }
                }
                //Allow chart rendering               
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-分频-更新坐标", ex));
            }
            finally
            {
                m_chart.EndUpdate();
            }
        }

        private void UpdateCursorResult()
        {
            try
            {
                m_chart.BeginUpdate();

                //Get cursor
                LineSeriesCursor cursor = m_chart.ViewXY.LineSeriesCursors[0];

                AnnotationXY cursorValueDisplay = m_chart.ViewXY.Annotations[0];
                float fTargetYCoord = m_chart.ViewXY.GetMarginsRect().Bottom;
                double dY;
                m_chart.ViewXY.YAxes[0].CoordToValue(fTargetYCoord, out dY);
                cursorValueDisplay.TargetAxisValues.X = cursor.ValueAtXAxis;
                cursorValueDisplay.TargetAxisValues.Y = dY;

                LineSeriesCursor lineSeriesCursor = m_chart.ViewXY.LineSeriesCursors[0];
                var serieses = m_chart.ViewXY.PointLineSeries.Where(o => o.AssignYAxisIndex == 0).ToArray();
                if (serieses.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var series in serieses)
                    {
                        int index = GetNearestPointIndex(series, lineSeriesCursor.ValueAtXAxis);
                        if (index != -1)
                        {
                            SeriesPoint point = series.Points[index];
                            string[] tag = point.Tag.ToString().Split(';');
                            sb.AppendLine(series.Title.Text + "-" + string.Format("幅值:{0},频率:{1},相位:{2}", Math.Round(point.Y, 3) + "(" + tag[0] + ")", Math.Round(double.Parse(tag[1]), 3), Math.Round(double.Parse(tag[2]), 1)));
                        }
                    }
                    if (sb.Length > 0)
                    {
                        sb.AppendLine("时间: " + m_chart.ViewXY.XAxes[0].TimeString(cursor.ValueAtXAxis, "yyyy-MM-dd HH:mm:ss"));
                        cursorValueDisplay.Text = sb.ToString().Trim();
                        cursorValueDisplay.Visible = true;
                    }
                    else
                    {
                        cursorValueDisplay.Text = string.Empty;
                        cursorValueDisplay.Visible = false;
                    }
                }
                //Allow chart rendering               
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-分频-更新坐标", ex));
            }
            finally
            {
                m_chart.EndUpdate();
            }
        }

        private int GetNearestPointIndex(PointLineSeries series, double xValue)
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
        private void LegendBox_CheckBoxStateChanged(Object sender, CheckBoxStateChangedEventArgs e)
        {
            m_chart.BeginUpdate();
            PointLineSeries pls = sender as PointLineSeries;//可能有问题，htzk123
            showMarkerDict[pls] = e.IsChecked;
            if (e.IsChecked)
            {
                foreach (var marker in pls.SeriesEventMarkers)
                {
                    marker.Visible = true;
                }
            }
            else
            {
                foreach (var marker in pls.SeriesEventMarkers)
                {
                    marker.Visible = false;
                }
            }
            m_chart.EndUpdate(); 
        }
        private void clearBufferBtn_Click(object sender, RoutedEventArgs e)
        {
            m_chart.BeginUpdate();
            foreach (var series in m_chart.ViewXY.PointLineSeries)
            {
                series.SeriesEventMarkers.Clear();
                series.Clear();
            }
            m_chart.EndUpdate();
        }
        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
        private void scrollCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.Annotations[0].Visible = true;
            m_chart.ViewXY.LineSeriesCursors[0].Visible = true;
            m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = (m_chart.ViewXY.XAxes[0].Minimum + m_chart.ViewXY.XAxes[0].Maximum) / 2.0;
        }

        private void scrollCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.LineSeriesCursors[0].Visible = false;
            m_chart.ViewXY.Annotations[0].Visible = false;
        }
    }
}
