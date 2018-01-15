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
using Arction.Wpf.Charting.EventMarkers;
using System.Windows.Input;
using AIC.Core.Events;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using AIC.ServiceInterface;
using AIC.M9600.Common.SlaveDB.Generated;
using System.Threading.Tasks;
using AIC.Core.DataModels;

namespace AIC.OnLineDataPage.Views.SubViews
{
    /// <summary>
    /// Interaction logic for TimeDomainOnLineView.xaml
    /// </summary>
    public partial class RMSTrendChartView : ChartViewBase
    {
        private LightningChartUltimate m_chart;
        private double m_dLatestX;

        public RMSTrendChartView()
        {
            InitializeComponent();
            CreateChart();
        }

        protected override void ViewModel_Closed(object sender, EventArgs e)
        {
            base.ViewModel_Closed(sender, e);
            // Don't forget to clear chart grid child list.
            //gridChart.Children.Clear();
            //if (m_chart != null)
            //{
            //    m_chart.Dispose();
            //    m_chart = null;               
            //}
        }

        protected override async void ViewModel_SignalChanged()
        {
            try
            {
                initializing = true;
                txtValue.Text = string.Empty;
                m_chart.BeginUpdate();
                m_chart.ViewXY.PointLineSeries[0].SeriesEventMarkers.Clear();
                m_chart.ViewXY.PointLineSeries[0].Clear();

                if (ViewModel != null && (ViewModel.Signal is BaseAlarmSignal) && (ViewModel.Signal as BaseAlarmSignal).Result != null && (ViewModel.Signal as BaseAlarmSignal).ACQDatetime != null)
                {
                    double dMinX = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue((ViewModel.Signal as BaseAlarmSignal).ACQDatetime.Value.AddHours(-24)) - 10;
                    double dMaxX = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue((ViewModel.Signal as BaseAlarmSignal).ACQDatetime.Value) + 5000;
                    m_chart.ViewXY.XAxes[0].SetRange(dMinX, dMaxX);
                    m_chart.ViewXY.YAxes[0].SetRange(((BaseAlarmSignal)ViewModel.Signal).Result.Value * 0.5, ((BaseAlarmSignal)ViewModel.Signal).Result.Value * 1.5);
                }
                await InitDataChart();
            }
            catch (Exception ex)
            {               
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-趋势-信号变换", ex));
            }
            finally
            {
                m_chart.EndUpdate();
                initializing = false;
            }
        }

        private IDatabaseComponent _databaseComponent;

        protected async Task InitDataChart()
        {
            try
            {
                //有效值趋势
                if (ViewModel == null || !(ViewModel.Signal is BaseAlarmSignal) || ViewModel.Signal.ACQDatetime == null)
                {
                    return;
                }
                
                var sg = ViewModel.Signal as BaseAlarmSignal;
                DateTime lasttime = new DateTime();
                if (m_chart.ViewXY.PointLineSeries[0].Points != null && m_chart.ViewXY.PointLineSeries[0].Points.Count() > 0)
                {
                    lasttime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(m_chart.ViewXY.PointLineSeries[0].Points.Select(p => p.X).Max());
                }
                _databaseComponent = ServiceLocator.Current.GetInstance<IDatabaseComponent>();
                List<TrendPointData> datas = new List<TrendPointData>();
                if (sg is WirelessScalarChannelSignal)
                {
                    var results = await _databaseComponent.GetHistoryData<D_WirelessScalarSlot>(sg.ServerIP, sg.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade" }, sg.ACQDatetime.Value.AddHours(-24), sg.ACQDatetime.Value, null, null);
                    if (results == null || results.Count == 0)
                    {
                        return;
                    }
                    else
                    {
                        datas = results.Where(p => p.ACQDatetime > lasttime).OrderBy(p => p.ACQDatetime).Select(p => new TrendPointData(p.ACQDatetime, p.Result.Value, p.Unit, p.AlarmGrade)).ToList();
                    }
                }
                else  if (sg is WirelessVibrationChannelSignal)
                {
                    var results = await _databaseComponent.GetHistoryData<D_WirelessVibrationSlot>(sg.ServerIP, sg.Guid, new string[] { "ACQDatetime", "Result", "Unit", "AlarmGrade" }, sg.ACQDatetime.Value.AddHours(-24), sg.ACQDatetime.Value, null, null);
                    if (results == null || results.Count == 0)
                    {
                        return;
                    }
                    else
                    {
                        datas = results.Where(p => p.ACQDatetime > lasttime).OrderBy(p => p.ACQDatetime).Select(p => new TrendPointData(p.ACQDatetime, p.Result.Value, p.Unit, p.AlarmGrade)).ToList();
                    }
                }
               
                SubInitDataChart(datas);
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-趋势", ex));
            }
        }

        private void SubInitDataChart(List<TrendPointData> datas)
        {
            SeriesPoint[] points = new SeriesPoint[1];

            PointLineSeries series = m_chart.ViewXY.PointLineSeries[0];
            for (int i = 0; i < datas.Count; i++)
            {
                m_dLatestX = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(datas[i].ACQDateTime);
                points[0].X = m_dLatestX;
                points[0].Y = datas[i].Result;
                points[0].Tag = datas[i].Unit;

                AddMarker(series, points[0], datas[i].AlarmGrade);

                series.AddPoints(points, false);
            }

            double maxValue = 10, minValue = 0;
            if (datas.Count > 0)
            {
                maxValue = datas.Select(p => p.Result).Max() * 1.5;
                minValue = datas.Select(p => p.Result).Min() * 0.5;
            }

            if (ViewModel != null && (ViewModel.Signal is BaseAlarmSignal) && (ViewModel.Signal as BaseAlarmSignal).Result != null)
            {
                m_chart.ViewXY.YAxes[0].SetRange(minValue, maxValue);
            }
            if (!(bool)scrollCheckBox.IsChecked)
            {
                m_chart.ViewXY.XAxes[0].ScrollPosition = m_dLatestX;
            }
            m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = m_dLatestX;
        }

        private bool initializing = false;
        protected override void UpdateChart(object args)
        {
            if (initializing == true)
            {
                return;
            }
            //有效值趋势
            if (ViewModel == null || !(ViewModel.Signal is BaseAlarmSignal))
            {
                return;
            }

            var sg = ViewModel.Signal as BaseAlarmSignal;
            if (sg.Result == null || sg.ACQDatetime == null)
            {
                return;
            }
            if (ViewModel.IsUpdated == false)
            {
                //加入缓存数据
                return;
            }
            try
            {
                List<TrendPointData> datas = new List<TrendPointData>();
                DateTime lasttime = new DateTime();
                if (m_chart.ViewXY.PointLineSeries[0].Points != null && m_chart.ViewXY.PointLineSeries[0].Points.Count() > 0)
                {
                    lasttime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(m_chart.ViewXY.PointLineSeries[0].Points.Select(p => p.X).Max());
                }
                datas.AddRange(sg.BufferData.Where(p => p.ACQDateTime > lasttime));
                if (sg.ACQDatetime.Value > lasttime)
                {
                    datas.Add(new TrendPointData(sg.ACQDatetime.Value, sg.Result.Value, sg.Unit, (int)sg.AlarmGrade));                   
                }
                
                if (datas.Count == 0)//无数据直接返回
                {
                    return;
                }

                m_chart.BeginUpdate();
                SeriesPoint[] points = new SeriesPoint[1];

                PointLineSeries series = m_chart.ViewXY.PointLineSeries[0];
                string unit = sg.Unit;
                for (int i = 0; i < datas.Count; i++)
                {
                    m_dLatestX = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(datas[i].ACQDateTime);
                    points[0].X = m_dLatestX;
                    points[0].Y = datas[i].Result;
                    points[0].Tag = datas[i].Unit;

                    AddMarker(series, points[0], datas[i].AlarmGrade);

                    series.AddPoints(points, false);
                }
              
                if (!(bool)scrollCheckBox.IsChecked)
                {
                    m_chart.ViewXY.XAxes[0].ScrollPosition = m_dLatestX;
                }
                m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = m_dLatestX;
                m_chart.EndUpdate();              
            }
            catch (Exception ex)
            {
                m_chart.EndUpdate();
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-趋势", ex));
            }
            finally
            {

            }
        }
        private string JudgeAlarmGrade(int alarmGrade)
        {
            string alarmTypeStr = string.Empty;
            AlarmGrade consts = (AlarmGrade)(alarmGrade & 0x00ffff00);
            //int dynamic = alarmType & 0X0C;
            //int comparative = alarmType & 0X30;
            switch (consts)
            {
                case AlarmGrade.LowPreAlarm:
                case AlarmGrade.HighPreAlarm:
                    alarmTypeStr += "常数报警：预警";
                    break;
                case AlarmGrade.LowAlarm:
                case AlarmGrade.HighAlarm:
                    alarmTypeStr += "常数报警：警告";
                    break;
                case AlarmGrade.LowDanger:
                case AlarmGrade.HighDanger:
                    alarmTypeStr += "常数报警：危险";
                    break;
            }
            //switch (dynamic)
            //{
            //    case 8:
            //        alarmTypeStr += "\r\n" + "曲线报警：警告";
            //        break;
            //    case 12:
            //        alarmTypeStr += "\r\n" + "曲线报警：危险";
            //        break;
            //}
            //switch (comparative)
            //{
            //    case 32:
            //        alarmTypeStr += "\r\n" + "相对报警：警告";
            //        break;
            //    case 48:
            //        alarmTypeStr += "\r\n" + "相对报警：危险";
            //        break;
            //}
            return alarmTypeStr;
        }
        private void AddMarker(PointLineSeries series, SeriesPoint point, int AlarmGrade)
        {
            string alarmTypeStr = JudgeAlarmGrade(AlarmGrade);
            if (!string.IsNullOrEmpty(alarmTypeStr))
            {
                SeriesEventMarker marker = new SeriesEventMarker(series);
                marker.XValue = point.X;
                marker.YValue = point.Y;
                marker.HorizontalPosition = SeriesEventMarkerHorizontalPosition.AtXValue;
                marker.Symbol.Width = 5;
                marker.Symbol.Height = 5;
                //store values in label text    
                marker.Label.Text = alarmTypeStr + "\r\n" + "X:" + m_chart.ViewXY.XAxes[0].TimeString(point.X, "yyyy-MM-dd HH:mm:ss") + "\r\n" + "Y:" + point.Y.ToString("0.000");
                marker.Label.HorizontalAlign = AlignmentHorizontal.Center;
                marker.Label.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9f, System.Drawing.FontStyle.Bold);
                marker.Label.Shadow.Style = TextShadowStyle.HighContrast;
                marker.Label.Shadow.ContrastColor = Colors.Black;
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
                else if (alarmTypeStr.Contains("预警"))
                {
                    marker.Symbol.Color1 = Colors.Yellow;
                }
                marker.Symbol.Shape = Arction.Wpf.Charting.Shape.Circle;
                marker.VerticalPosition = SeriesEventMarkerVerticalPosition.AtYValue;
                marker.MoveByMouse = false;
                marker.MouseOverOn += new MouseEventHandler(marker_MouseOverOn);
                marker.MouseOverOff += new MouseEventHandler(marker_MouseOverOff);
                series.SeriesEventMarkers.Add(marker);
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

            m_chart.ViewXY.DropOldSeriesData = true;
            m_chart.ViewXY.DropOldEventMarkers = true;

            //m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Stacked;
            m_chart.ViewXY.LegendBoxes[0].Visible = false;

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
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
            //m_chart.ViewXY.XAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff); 
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.XAxes[0].LabelsPosition = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Alignment = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Alignment = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].VerticalAlign = AlignmentVertical.Top;
            m_chart.ViewXY.XAxes[0].ScrollMode = XAxisScrollMode.Scrolling;
            //Setup y-axis
            m_chart.ViewXY.YAxes[0].Title.Visible = false;
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].AxisThickness = 2;
            m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
            //m_chart.ViewXY.YAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.YAxes[0].MiniScale.Visible = false;
            m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Alignment = Alignment.Far;
            m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Alignment = Alignment.Far;
            m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].Alignment = AlignmentHorizontal.Right;

            PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            //series.PointCountLimitEnabled = true;
            //series.PointCountLimit = 1800;
            series.MouseInteraction = false;
            series.LineStyle.AntiAliasing = LineAntialias.None;
            series.LineStyle.Width = 1;
            series.LimitYToStackSegment = true;

            m_chart.ViewXY.PointLineSeries.Add(series);

            //Add cursor
            LineSeriesCursor cursor = new LineSeriesCursor(m_chart.ViewXY, m_chart.ViewXY.XAxes[0]);
            m_chart.ViewXY.LineSeriesCursors.Add(cursor);
            cursor.PositionChanged += cursor_PositionChanged;
            cursor.LineStyle.Color = System.Windows.Media.Color.FromArgb(150, 255, 0, 0);
            cursor.LineStyle.Width = 2;
            cursor.SnapToPoints = true;
            cursor.TrackPoint.Color1 = Colors.White;
            //cursor.Visible = true;

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
        private void cursor_PositionChanged(Object sender, Arction.Wpf.Charting.Views.ViewXY.PositionChangedEventArgs e)
        {
            // Cancel the on-going rendering because the code below updates 
            // the chart.
            e.CancelRendering = true;

            UpdateCursorResult(e.Cursor.ValueAtXAxis);
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
                var series = m_chart.ViewXY.PointLineSeries[0];

                StringBuilder sb = new StringBuilder();

                int index = GetNearestPointIndex(series, lineSeriesCursor.ValueAtXAxis);
                if (index == -1)
                {
                    cursorValueDisplay.Text = string.Empty;
                    cursorValueDisplay.Visible = false;
                }
                else
                {
                    SeriesPoint point = series.Points[index];
                    string[] tag = point.Tag.ToString().Split(';');
                    sb.AppendLine(string.Format("幅值:{0}", Math.Round(point.Y, 3) + "(" + point.Tag.ToString() + ")"));
                    sb.AppendLine("时间:" + m_chart.ViewXY.XAxes[0].TimeString(xValue, "yyyy-MM-dd HH:mm:ss"));
                    cursorValueDisplay.Text = sb.ToString().Trim();
                    cursorValueDisplay.Visible = false;
                    txtValue.Text = string.Format("幅值:{0} 时间:{1}", Math.Round(point.Y, 3) + point.Tag.ToString(), m_chart.ViewXY.XAxes[0].TimeString(xValue, "yyyy-MM-dd HH:mm:ss"));
                }          
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-趋势-更新坐标", ex));
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
        private void clearBufferBtn_Click(object sender, RoutedEventArgs e)
        {
            m_chart.BeginUpdate();
            PointLineSeries series = m_chart.ViewXY.PointLineSeries[0];
            series.SeriesEventMarkers.Clear();
            series.Clear();
            m_chart.EndUpdate();
        }
        private void scrollCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //m_chart.ViewXY.LineSeriesCursors[0].Visible = true;
            //m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = (m_chart.ViewXY.XAxes[0].Minimum + m_chart.ViewXY.XAxes[0].Maximum) / 2.0;
            m_chart.ViewXY.DropOldSeriesData = false;
        }
        private void scrollCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //m_chart.ViewXY.LineSeriesCursors[0].Visible = false;
            //m_chart.ViewXY.Annotations[0].Visible = false;
            m_chart.ViewXY.DropOldSeriesData = true;
        }
        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }

    
}