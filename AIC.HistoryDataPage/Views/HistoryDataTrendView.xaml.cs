using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.CoreType;
using AIC.HistoryDataPage.Models;
using AIC.OnLineDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Annotations;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.EventMarkers;
using Arction.Wpf.Charting.SeriesXY;
using Arction.Wpf.Charting.Views.ViewXY;
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
    public partial class HistoryDataTrendView : UserControl, ICloseable
    {
        public HistoryDataTrendView()
        {
            InitializeComponent();

            this.Closer = new CloseableHeader((string)Application.Current.Resources["menuHistoryDataTrend"], true);

            ViewModel = this.DataContext as HistoryDataTrendViewModel;
            ViewModel.SignalAdded += ViewModel_SignalAdded;
            ViewModel.SignalRemoved += ViewModel_SignalRemoved;
            ViewModel.SignalShowChanged += ViewModel_SignalShowChanged;
            ViewModel.SignalSelected += ViewModel_SignalSelected;
            ViewModel.SignalRefresh += ViewModel_SignalRefresh;
            CreateChart();
            this.Loaded += new RoutedEventHandler(Window_Loaded);
            listBox.SizeChanged += listBox_SizeChanged;
        }    

        private void ViewModel_SignalAdded(SignalToken token, DateTime time, int size)
        {
            try
            {
                m_chart.BeginUpdate();

                var axisYnone = m_chart.ViewXY.YAxes.Where(o => o.Units.Text == "none").SingleOrDefault();
                m_chart.ViewXY.YAxes.Remove(axisYnone);

                #region
                AxisY axisY = new AxisY(m_chart.ViewXY);
                if (token is BaseDivfreSignalToken)
                {
                    var vToken = token as BaseDivfreSignalToken;
                    string unit = (vToken.DataContracts == null || vToken.DataContracts.Count == 0 || vToken.DataContracts[0].Unit == null) ? "" : vToken.DataContracts[0].Unit;
                  
                    axisY.Title.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Regular);
                    axisY.AxisThickness = 2;
                    axisY.AxisColor = vToken.SolidColorBrush.Color;//Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);  
                    axisY.Title.Text = vToken.DisplayName + "(" + unit + ")";
                    axisY.SetRange(0, 10);
                    axisY.Tag = token;                   
                    m_chart.ViewXY.YAxes.Add(axisY);

                    if (m_chart.ViewXY.Annotations.Count == 0)
                    {
                        CreateAnnotation();
                    }

                    PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], axisY);
                    series.MouseInteraction = false;
                    series.Title.Text = vToken.DisplayName + "(" + unit + ")";
                    series.LineStyle.Color = token.SolidColorBrush.Color;
                    series.Title.Color = token.SolidColorBrush.Color;                   
                    series.LineStyle.AntiAliasing = LineAntialias.Normal;
                    series.LineStyle.Width = 1;                 
                    series.Tag = token;

                    if (vToken.DataContracts != null)//加入数据
                    {
                        SeriesPoint[] points = new SeriesPoint[vToken.DataContracts.Count];
                        for (int i = 0; i < points.Length; i++)
                        {
                            points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(vToken.DataContracts[i].ACQDatetime);
                            points[i].Y = vToken.DataContracts[i].Result.Value;
                            AddMarker(series, points[i], vToken.DataContracts[i].AlarmGrade);
                        }
                        series.Points = points;
                    }
                    m_chart.ViewXY.PointLineSeries.Add(series);                   

                    if (vToken.DataContracts != null)
                    {
                        axisY.Maximum = vToken.UpperLimit;
                        axisY.Minimum = vToken.LowerLimit;
                    }
                   
                }
                #endregion
                #region
                else if (token is BaseAlarmSignalToken)
                {
                    var anToken = token as BaseAlarmSignalToken;
                    if (anToken.DataContracts != null)
                    {
                        string unit = (anToken.DataContracts == null || anToken.DataContracts.Count == 0 || anToken.DataContracts[0].Unit == null) ? "" : anToken.DataContracts[0].Unit;

                        axisY.Title.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Regular);
                        axisY.AxisThickness = 2;
                        axisY.AxisColor = anToken.SolidColorBrush.Color; //Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                        axisY.Title.Text = anToken.DisplayName + "(" + unit + ")";
                        axisY.SetRange(0, 10);
                        axisY.Tag = token;
                        m_chart.ViewXY.YAxes.Add(axisY);

                        if (m_chart.ViewXY.Annotations.Count == 0)
                        {
                            CreateAnnotation();
                        }

                        PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], axisY);
                        series.MouseInteraction = false;
                        series.Title.Text = anToken.DisplayName + "(" + unit + ")";
                        series.LineStyle.Color = token.SolidColorBrush.Color;
                        series.Title.Color = token.SolidColorBrush.Color;
                        series.LineStyle.AntiAliasing = LineAntialias.Normal;
                        series.LineStyle.Width = 1;
                        series.Tag = token;

                        if (anToken.DataContracts != null)
                        {
                            SeriesPoint[] points = new SeriesPoint[anToken.DataContracts.Count];
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(anToken.DataContracts[i].ACQDatetime);
                                points[i].Y = anToken.DataContracts[i].Result.Value;
                                AddMarker(series, points[i], anToken.DataContracts[i].AlarmGrade);
                            }
                            series.Points = points;
                        }
                        m_chart.ViewXY.PointLineSeries.Add(series);

                        if (anToken.DataContracts != null)
                        { 
                            axisY.Maximum = anToken.UpperLimit; ;
                            axisY.Minimum = anToken.LowerLimit; ;
                        }
                    }
                }
                #endregion

                m_chart.ViewXY.Annotations[0].AssignYAxisIndex = -1;
                m_chart.ViewXY.Annotations[0].AssignYAxisIndex = 0;

                m_chart.EndUpdate();
                m_chart.ViewXY.XAxes[0].SetRange(m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(time), m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(time.AddMinutes(size)));
                token.LimitChanged += Token_LimitChanged;
                axisY.RangeChanged += AxisY_RangeChanged;

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
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("趋势-添加信号", ex));
                m_chart.EndUpdate();
            }
        }

        private void Token_LimitChanged(SignalToken token)
        {
            var axisY = m_chart.ViewXY.YAxes.Where(o => o.Tag == token).SingleOrDefault();            
            if (axisY != null)
            {
                axisY.RangeChanged -= AxisY_RangeChanged;
                axisY.Maximum = token.UpperLimit;
                axisY.Minimum = token.LowerLimit;
                axisY.RangeChanged += AxisY_RangeChanged;
            }
        }

        private void AxisY_RangeChanged(object sender, RangeChangedEventArgs e)
        {
            AxisY axisY = sender as AxisY;
            (axisY.Tag as SignalToken).UpperLimit = Math.Round(axisY.Maximum, 1);
            (axisY.Tag as SignalToken).LowerLimit = Math.Round(axisY.Minimum, 1);
        }

        private void ViewModel_SignalRemoved(SignalToken token)
        {
            try
            {
                m_chart.BeginUpdate();
                var series = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag == token).SingleOrDefault();
                if (series != null)
                {
                    series.Clear();
                    m_chart.ViewXY.PointLineSeries.Remove(series);                   
                }

                var axisY = m_chart.ViewXY.YAxes.Where(o => o.Tag == token).SingleOrDefault();
                if (axisY != null)
                {                    
                    m_chart.ViewXY.YAxes.Remove(axisY);                   
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
                    else
                    {
                        AxisY axisYnone = new AxisY(m_chart.ViewXY);
                        axisYnone.Title.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Regular);
                        axisYnone.AxisThickness = 2;
                        axisYnone.AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                        axisYnone.Units.Text = "none";
                        m_chart.ViewXY.YAxes.Add(axisYnone);
                    }
                }

                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("趋势趋势-移除信号", ex));
                m_chart.EndUpdate();
            }
        }

        private void ViewModel_SignalRefresh(IEnumerable<SignalToken> tokens, DateTime time, int size, bool refresh)
        {
            try
            {
                m_chart.BeginUpdate();
                m_chart.ViewXY.PointLineSeries.ForEach(p => p.SeriesEventMarkers.Clear());
                foreach (var token in tokens)
                {
                    PointLineSeries series = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag == token).SingleOrDefault();
                    AxisY axisY = m_chart.ViewXY.YAxes.Where(o => o.Tag == token).SingleOrDefault();
                    if (token is BaseDivfreSignalToken)
                    {
                        var divToken = token as BaseDivfreSignalToken;
                        if (divToken.DataContracts != null && divToken.DataContracts.Count > 0)//加入数据
                        {
                            
                            SeriesPoint[] points = new SeriesPoint[divToken.DataContracts.Count];
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(divToken.DataContracts[i].ACQDatetime);
                                points[i].Y = divToken.DataContracts[i].Result.Value;
                                AddMarker(series, points[i], divToken.DataContracts[i].AlarmGrade);
                            }
                            series.Points = points;
                            if (refresh == true)
                            {
                                axisY.Title.Text = divToken.DisplayName + "(" + divToken.DataContracts[0].Unit + ")";
                                axisY.Maximum = divToken.UpperLimit;
                                axisY.Minimum = divToken.LowerLimit;
                            }
                        }
                        else
                        {
                            series.Clear();
                        }
                    }
                    else if (token is BaseWaveSignalToken)
                    {
                        var vToken = token as BaseWaveSignalToken;
                        if (vToken.DataContracts != null && vToken.DataContracts.Count > 0)//加入数据
                        {
                            SeriesPoint[] points = new SeriesPoint[vToken.DataContracts.Count];
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(vToken.DataContracts[i].ACQDatetime);
                                points[i].Y = vToken.DataContracts[i].Result.Value;
                                AddMarker(series, points[i], vToken.DataContracts[i].AlarmGrade);
                            }
                            series.Points = points;
                            if (refresh == true)
                            {
                                axisY.Title.Text = vToken.DisplayName + "(" + vToken.DataContracts[0].Unit + ")";
                                axisY.Maximum = vToken.UpperLimit;
                                axisY.Minimum = vToken.LowerLimit;
                            }
                        }
                        else
                        {
                            series.Clear();
                        }
                    }
                    else if (token is BaseAlarmSignalToken)
                    {
                        var anToken = token as BaseAlarmSignalToken;
                        if (anToken.DataContracts != null && anToken.DataContracts.Count > 0)//加入数据
                        {                            
                            SeriesPoint[] points = new SeriesPoint[anToken.DataContracts.Count];
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(anToken.DataContracts[i].ACQDatetime);
                                points[i].Y = anToken.DataContracts[i].Result.Value;
                                AddMarker(series, points[i], anToken.DataContracts[i].AlarmGrade);
                            }
                            series.Points = points;
                            if (refresh == true)
                            {
                                axisY.Title.Text = anToken.DisplayName + "(" + anToken.DataContracts[0].Unit + ")";
                                axisY.Maximum = anToken.UpperLimit;
                                axisY.Minimum = anToken.LowerLimit;
                            }
                        }
                        else
                        {
                            series.Clear();
                        }
                    }
                }
                m_chart.ViewXY.XAxes[0].SetRange(m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(time), m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(time.AddMinutes(size)));
                m_chart.EndUpdate();

                if (tokens != null && tokens.Count() > 0)
                {
                    if (m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis < m_chart.ViewXY.XAxes[0].Minimum || m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis > m_chart.ViewXY.XAxes[0].Maximum)
                    {
                        m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = (m_chart.ViewXY.XAxes[0].Minimum + m_chart.ViewXY.XAxes[0].Maximum) / 2.0;
                    }
                    else
                    {
                        UpdateCursorResult(m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis);
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("趋势-数据翻页", ex));
                m_chart.EndUpdate();
            }
        }

        private void ViewModel_SignalShowChanged(SignalToken signalToken)
        {
            var series = m_chart.ViewXY.PointLineSeries.Where(p => p.Tag == signalToken).FirstOrDefault();
            if (series != null)
            {
                series.Visible = !series.Visible;
            }
        }

        private void ViewModel_SignalSelected(SignalToken signalToken)
        {
            var series = m_chart.ViewXY.PointLineSeries.Where(p => p.Tag == signalToken).FirstOrDefault();
            if (series != null)
            {
                series.SetHighlight();
                
            }
            m_chart.ViewXY.PointLineSeries.ForEach(p =>
            {
                if (p.Tag == signalToken)
                {
                    p.MouseHighlight = MouseOverHighlight.Simple;
                }
                else
                {
                    p.MouseHighlight = MouseOverHighlight.None;
                }
            });
        }

        public CloseableHeader Closer { get; private set; }

        private LightningChartUltimate m_chart;
        HistoryDataTrendViewModel ViewModel;

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
            m_chart.Title.Text = ""; ;
            m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Layered;
            m_chart.ViewXY.AxisLayout.YAxisAutoPlacement = YAxisAutoPlacement.LeftThenRight;

            // m_chart.ViewXY.AxisLayout.AutoAdjustAxisGap = 0;
            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.DateTime;
            m_chart.ViewXY.XAxes[0].AutoFormatLabels = false;
            m_chart.ViewXY.XAxes[0].LabelsTimeFormat  = "MM-dd HH:mm";
            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            //m_chart.ViewXY.XAxes[0].MinimumDateTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5));
            //m_chart.ViewXY.XAxes[0].MaximumDateTime = DateTime.Now;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            m_chart.ViewXY.YAxes.Clear();
            AxisY axisYnone = new AxisY(m_chart.ViewXY);
            axisYnone.Title.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Regular);
            axisYnone.AxisThickness = 2;
            axisYnone.AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            axisYnone.Units.Text = "none";
            m_chart.ViewXY.YAxes.Add(axisYnone);

            m_chart.ViewXY.LegendBoxes[0].Visible = false;
            m_chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.ViewXY.LegendBoxes[0].Fill.Style = RectFillStyle.None;
            m_chart.ViewXY.LegendBoxes[0].Shadow.Visible = false;
            m_chart.ViewXY.LegendBoxes[0].BorderWidth = 0;
            m_chart.ViewXY.LegendBoxes[0].Position = LegendBoxPositionXY.TopRight;
            m_chart.ViewXY.LegendBoxes[0].Offset.SetValues(-80, 5);
            m_chart.ViewXY.LegendBoxes[0].SeriesTitleFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.LegendBoxes[0].SeriesTitleMouseMoveOverOn += LegendBox_SeriesTitleMouseMoveOverOn;

            //Add cursor
            LineSeriesCursor cursor = new LineSeriesCursor(m_chart.ViewXY, m_chart.ViewXY.XAxes[0]);
            m_chart.ViewXY.LineSeriesCursors.Add(cursor);
            cursor.PositionChanged += cursor_PositionChanged;
            cursor.LineStyle.Color = System.Windows.Media.Color.FromArgb(150, 255, 0, 0);
            cursor.LineStyle.Width = 2;
            cursor.SnapToPoints = true;
            cursor.TrackPoint.Color1 = Colors.White;
         
            m_chart.ViewXY.ZoomToFit();
        
            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);

            chkZoom.Checked += chkZoom_Checked;
            chkZoom.Unchecked += chkZoom_Unchecked;
            chkZoom_Checked(null, null);
        }

        private void chkZoom_Checked(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.ZoomPanOptions.MouseWheelZooming = MouseWheelZooming.Off;
            m_chart.ViewXY.ZoomPanOptions.LeftMouseButtonAction = MouseButtonAction.None;
            m_chart.ViewXY.ZoomPanOptions.RightMouseButtonAction = MouseButtonAction.None;
        }

        private void chkZoom_Unchecked(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.ZoomPanOptions.MouseWheelZooming = MouseWheelZooming.HorizontalAndVertical;
            m_chart.ViewXY.ZoomPanOptions.LeftMouseButtonAction = MouseButtonAction.Pan;
            m_chart.ViewXY.ZoomPanOptions.RightMouseButtonAction = MouseButtonAction.Pan;
        }

        private void LegendBox_SeriesTitleMouseMoveOverOn(Object sender, Arction.Wpf.Charting.Views.ViewXY.SeriesTitleMouseMovedEventArgs e)
        {
            if (e.Series is PointLineSeries)
            {
                ViewModel.TotalPoint = ((PointLineSeries)e.Series).PointCount;
            }
        }
  
        private void CreateAnnotation()
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
            e.CancelRendering = true;
            UpdateCursorResult(e.Cursor.ValueAtXAxis);
        }

        private void UpdateCursorResult(double xValue)
        {
            try
            {
                m_chart.BeginUpdate();
                List<BaseWaveSignalToken> channelList = new List<BaseWaveSignalToken>();
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
                    if (series.Tag is BaseDivfreSignalToken)
                    {
                        BaseDivfreSignalToken token = series.Tag as BaseDivfreSignalToken;
                        token.CurrentIndex = index;
                        channelList.Add(token);
                        if (index != -1)
                        {
                            var contract = token.DataContracts[index];  //. series.Points[index].Tag as VInfoTableAMSContract;
                            string unit = contract.Unit;
                            strValue = string.Format("{0}: {1}({2})|{3}", token.DisplayName, Math.Round(contract.Result ?? 0.0, 3), unit, Math.Round(contract.RPM ?? 0.0, 3));
                            token.SelectedResult = Math.Round(contract.Result ?? 0.0, 3);
                            token.SelectedTime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(xValue);
                        }
                        else
                        {
                            strValue = string.Format(strChannelStringFormat, token.DisplayName, "---", "Unit");
                        }
                    }
                    else if (series.Tag is BaseWaveSignalToken)
                    {
                        BaseWaveSignalToken token = series.Tag as BaseWaveSignalToken;
                        token.CurrentIndex = index;
                        channelList.Add(token);
                        if (index != -1)
                        {
                            var contract = token.DataContracts[index];  //. series.Points[index].Tag as VInfoTableAMSContract;
                            string unit = contract.Unit;
                            strValue = string.Format("{0}: {1}({2})", token.DisplayName, Math.Round(contract.Result ?? 0.0, 3), unit);
                            token.SelectedResult = Math.Round(contract.Result ?? 0.0, 3);
                            token.SelectedTime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(xValue);
                        }
                        else
                        {
                            strValue = string.Format(strChannelStringFormat, token.DisplayName, "---", "Unit");
                        }
                    }
                    else if (series.Tag is BaseAlarmSignalToken)
                    {
                        BaseAlarmSignalToken token = series.Tag as BaseAlarmSignalToken;
                        if (index != -1)
                        {
                            var contract = token.DataContracts[index]; //series.Points[index].Tag as AnInfoTableAMSContract;
                            string unit = contract.Unit;
                            strValue = string.Format(strChannelStringFormat, token.DisplayName, Math.Round(contract.Result ?? 0.0, 3), unit);
                            token.SelectedResult = Math.Round(contract.Result ?? 0.0, 3);
                            token.SelectedTime = m_chart.ViewXY.XAxes[0].AxisValueToDateTime(xValue);
                        }
                        else
                        {
                            strValue = string.Format(strChannelStringFormat, token.DisplayName, "---", "Unit");
                        }
                    }

                    sb.AppendLine(strValue);
                    // series.Title.Text = strValue;
                    iSeriesNumber++;
                }
                sb.AppendLine("Time: " + m_chart.ViewXY.XAxes[0].TimeString(xValue, "yyyy-MM-dd HH:mm:ss"));
                ////Set text
                cursorValueDisplay.Text = sb.ToString().Trim();
                cursorValueDisplay.Visible = bLabelVisible;
                //Allow chart rendering
                m_chart.EndUpdate();

                if (channelList.Count > 0)
                {
                    ViewModel.TrackChanged(channelList);
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-趋势趋势-Track", ex));
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

        private string JudgeAlarmType(int alarmType)
        {
            string alarmTypeStr = string.Empty;
            AlarmGrade consts = (AlarmGrade)(alarmType & 0x00ffff00);
            //int dynamic = alarmType & 0X0C;
            //int comparative = alarmType & 0X30;
            switch (consts)
            {
                case AlarmGrade.LowPreAlert:
                case AlarmGrade.HighPreAlert:
                    alarmTypeStr += "常数报警：预警";
                    break;
                case AlarmGrade.LowAlert:
                case AlarmGrade.HighAlert:
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
            string alarmTypeStr = JudgeAlarmType(AlarmGrade);
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

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Window_Loaded;
            VerticalAlignButtonClick(null, null);
            //获取GridSplitterr的cotrolTemplate中的按钮btn，必须在Loaded之后才能获取到
            Button btnGrdSplitter = gsSplitterr.Template.FindName("btnExpend", gsSplitterr) as Button;
            if (btnGrdSplitter != null)
                btnGrdSplitter.Click += new RoutedEventHandler(btnGrdSplitter_Click);
            Button btnGrdSecondSplitter = gsSecondSplitterr.Template.FindName("btnExpend", gsSecondSplitterr) as Button;
            if (btnGrdSecondSplitter != null)
                btnGrdSecondSplitter.Click += new RoutedEventHandler(btnGrdSecondSplitter_Click);
        }

        private void listBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            VerticalAlignButtonClick(null, null);
        }

        private void VerticalAlignButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ItemWidth = listBox.ActualWidth - 5;
            ViewModel.ItemHeight = listBox.ActualHeight / ViewModel.HistoricalDatas.Where(o => o.IsVisible == true).Count() - 2;
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

        GridLength m_SecondWidthCache = new GridLength(500);
        void btnGrdSecondSplitter_Click(object sender, RoutedEventArgs e)
        {
            GridLength temp = grdSecondWorkbench.ColumnDefinitions[2].Width;
            GridLength zero = new GridLength(0);
            if (!temp.Equals(zero))
            {
                //折叠
                m_SecondWidthCache = grdSecondWorkbench.ColumnDefinitions[2].Width;
                grdSecondWorkbench.ColumnDefinitions[2].Width = new GridLength(0);
            }
            else
            {
                //恢复
                grdSecondWorkbench.ColumnDefinitions[2].Width = m_SecondWidthCache;
            }
        }

    }
}