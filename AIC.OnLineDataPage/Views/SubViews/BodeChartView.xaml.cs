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
    public partial class BodeChartView : ChartViewBase
    {
        private LightningChartUltimate m_chart;
        private IDictionary<FreeformPointLineSeries, bool> showMarkerDict = new Dictionary<FreeformPointLineSeries, bool>();

        public BodeChartView()
        {
            InitializeComponent();
            CreateChart();
        }

        protected override void ChartViewBase_Unloaded(object sender, RoutedEventArgs e)
        {
            base.ChartViewBase_Unloaded(sender, e);
            if (m_chart != null)
            {
                m_chart.ChartRenderOptions.DeviceType = RendererDeviceType.None;
            }            
        }

        protected override void ChartViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            base.ChartViewBase_Loaded(sender, e);
            if (m_chart != null)
            {
                m_chart.ChartRenderOptions.DeviceType = RendererDeviceType.SoftwareOnlyD11;
            }
        }

        protected override void ViewModel_Disposed(object sender, EventArgs e)
        {
            base.ViewModel_Disposed(sender, e);
            // Don't forget to clear chart grid child list.
            gridChart.Children.Clear();
            if (m_chart != null)
            {
                m_chart.Dispose();
                m_chart = null;
            }
            base.Dispose();            
        }

        protected override void ViewModel_Closed(object sender, EventArgs e)
        {
            this.ViewModel_Disposed(sender, e);
            base.GCCollect();
        }


        void showLineCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (m_chart != null)
            {
                foreach (var series in m_chart.ViewXY.FreeformPointLineSeries)
                {
                    series.LineVisible = true;
                }
            }
        }

        void showLineCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_chart != null)
            {
                foreach (var series in m_chart.ViewXY.FreeformPointLineSeries)
                {
                    series.LineVisible = false;
                }
            }
        }

        protected override void ViewModel_SignalChanged()
        {
            try
            {
                m_chart.BeginUpdate();
                foreach (var series in m_chart.ViewXY.FreeformPointLineSeries)
                {
                    series.SeriesEventMarkers.Clear();
                    series.Clear();
                }
                m_chart.ViewXY.FreeformPointLineSeries.Clear();
                if (ViewModel != null && (ViewModel.Signal is BaseDivfreSignal))
                {
                    var vSg = ViewModel.Signal as BaseDivfreSignal;

                    //分频 
                    int count = 0;
                    foreach (var divFre in vSg.DivFres)
                    {
                        FreeformPointLineSeries series = new FreeformPointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
                        showMarkerDict.Add(series, true);
                        series.Title.Text = divFre.DisPlayName;
                        series.MouseInteraction = false;
                        series.PointCountLimit = 1000;
                        series.PointCountLimitEnabled = true;
                        series.LineStyle.AntiAliasing = LineAntialias.None;
                        series.LimitYToStackSegment = true;
                        series.LineStyle.Width = 1;
                        if ((bool)showLineCheckBox.IsChecked)
                        {
                            series.LineVisible = true;
                        }
                        else
                        {
                            series.LineVisible = false;
                        }
                        series.PointsVisible = true;
                        series.PointStyle.Shape = Shape.Circle;
                        series.PointStyle.Width = 5;
                        series.PointStyle.Height = 5;
                        series.PointStyle.Color1 = DefaultColors.SeriesForBlackBackgroundWpf[count];
                        series.LineStyle.Color = DefaultColors.SeriesForBlackBackgroundWpf[count];

                        //相位
                        FreeformPointLineSeries phaseSeries = new FreeformPointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[1]);
                        phaseSeries.Title.Text = divFre.DisPlayName + "相位";
                        phaseSeries.PointCountLimit = 1000;
                        phaseSeries.PointCountLimitEnabled = true;
                        phaseSeries.MouseInteraction = false;
                        phaseSeries.LineStyle.AntiAliasing = LineAntialias.None;
                        series.LimitYToStackSegment = true;
                        phaseSeries.LineStyle.Width = 1;
                        if ((bool)showLineCheckBox.IsChecked)
                        {
                            phaseSeries.LineVisible = true;
                        }
                        else
                        {
                            phaseSeries.LineVisible = false;
                        }
                        phaseSeries.PointsVisible = true;
                        phaseSeries.PointStyle.Shape = Shape.Circle;
                        phaseSeries.PointStyle.Width = 5;
                        phaseSeries.PointStyle.Height = 5;
                        phaseSeries.PointStyle.Color1 = DefaultColors.SeriesForBlackBackgroundWpf[count];
                        phaseSeries.LineStyle.Color = DefaultColors.SeriesForBlackBackgroundWpf[count];
                        m_chart.ViewXY.FreeformPointLineSeries.Add(series);
                        m_chart.ViewXY.FreeformPointLineSeries.Add(phaseSeries);
                        count++;
                    }
                }                
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-伯德-信号变换", ex));
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
                    foreach (var divFre in signal.DivFres)
                    {
                        if (divFre.IsUpload)
                        {
                            FreeformPointLineSeries series = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == divFre.DisPlayName).SingleOrDefault();
                            if (series != null)
                            {
                                SeriesPoint[] points = new SeriesPoint[1];
                                points[0].X = divFre.RPM;
                                points[0].Y = divFre.FreMV;
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
                                    //store values in label text 
                                    marker.Label.Text = alarmTypeStr;// ViewModel.Signal.STIME + " ; " + points[0].Y.ToString("0.000");
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
                            }
                            //相位
                            FreeformPointLineSeries phaseSeries = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == divFre.DisPlayName + "相位").SingleOrDefault();
                            if (phaseSeries != null)
                            {
                                SeriesPoint[] points = new SeriesPoint[1];
                                points[0].X = divFre.RPM;
                                points[0].Y = divFre.Phase;
                                phaseSeries.AddPoints(points, false);
                            }
                        }
                    }
                }
                if (fitViewCheckBox.IsChecked == true)
                {
                    m_chart.ViewXY.ZoomToFit();
                }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-伯德", ex));
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

            try
            {
                m_chart = new LightningChartUltimate();
                m_chart.BeginUpdate();
                m_chart.Title.Text = "";
                // m_chart.ViewXY.ZoomPanOptions.MouseWheelZooming = MouseWheelZooming.Horizontal;
                m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Stacked;
                m_chart.ViewXY.BeforeZooming += ViewXY_BeforeZooming;

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
                m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.Number;
                m_chart.ViewXY.XAxes[0].Title.Visible = false;
                m_chart.ViewXY.XAxes[0].AxisThickness = 2;
                m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
                                                                                           //m_chart.ViewXY.XAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
                m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
                m_chart.ViewXY.XAxes[0].LabelsPosition = Alignment.Near;
                m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Alignment = Alignment.Near;
                m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Alignment = Alignment.Near;
                m_chart.ViewXY.XAxes[0].VerticalAlign = AlignmentVertical.Top;

                //Setup y-axis
                m_chart.ViewXY.YAxes[0].Title.Visible = false;
                m_chart.ViewXY.YAxes[0].AxisThickness = 2;
                m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                                                                                           //m_chart.ViewXY.YAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
                m_chart.ViewXY.YAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
                m_chart.ViewXY.YAxes[0].MiniScale.Visible = false;
                m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Alignment = Alignment.Far;
                m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Color = Colors.Gray;
                m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Alignment = Alignment.Far;
                m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Color = Colors.Gray;
                m_chart.ViewXY.YAxes[0].Alignment = AlignmentHorizontal.Right;

                //Setup Second Y Axis
                AxisY secondYAxis = new AxisY(m_chart.ViewXY, true);
                secondYAxis.Title.Visible = false;
                secondYAxis.AxisThickness = 2;
                secondYAxis.AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                                                                               //secondYAxis.MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
                secondYAxis.ValueType = AxisValueType.Number;
                secondYAxis.AxisColor = Colors.Teal;
                secondYAxis.LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
                secondYAxis.MiniScale.Visible = false;
                secondYAxis.MajorDivTickStyle.Alignment = Alignment.Far;
                secondYAxis.MajorDivTickStyle.Color = Colors.Gray;
                secondYAxis.MinorDivTickStyle.Alignment = Alignment.Far;
                secondYAxis.MinorDivTickStyle.Color = Colors.Gray;
                secondYAxis.Alignment = AlignmentHorizontal.Right;

                m_chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;
                m_chart.ViewXY.LegendBoxes[0].Fill.Style = RectFillStyle.None;
                m_chart.ViewXY.LegendBoxes[0].Shadow.Visible = false;
                m_chart.ViewXY.LegendBoxes[0].BorderWidth = 0;
                m_chart.ViewXY.LegendBoxes[0].Position = LegendBoxPositionXY.TopCenter;
                m_chart.ViewXY.LegendBoxes[0].Offset.SetValues(0, 0);
                m_chart.ViewXY.LegendBoxes[0].SeriesTitleFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
                m_chart.ViewXY.LegendBoxes[0].CheckBoxStateChanged += LegendBox_CheckBoxStateChanged;

                m_chart.ViewXY.ZoomToFit();
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-伯德-初始化", ex));
                if (m_chart != null)
                {
                    m_chart.EndUpdate();
                }
            }
            finally
            {

            }

            gridChart.Children.Add(m_chart);
        }       

        void ViewXY_BeforeZooming(object sender, BeforeZoomingXYEventArgs e)
        {
            //Do custom zooming for Y axes. 

            m_chart.BeginUpdate();
            e.Cancel = true;
            foreach (RangeChangeInfo rci in e.YRanges)
            {
                double yMid = (rci.OldMin + rci.OldMax) / 2.0;
                double newYRange = rci.NewMax - rci.NewMin;

                rci.Axis.SetRange(yMid - newYRange / 2.0, yMid + newYRange / 2.0);
            }
            foreach (RangeChangeInfo rci in e.XRanges)
            {
                rci.Axis.SetRange(rci.NewMin, rci.NewMax);
            }
            m_chart.EndUpdate();
        }

        private void LegendBox_CheckBoxStateChanged(Object sender, CheckBoxStateChangedEventArgs e)
        {
            m_chart.BeginUpdate();
            FreeformPointLineSeries fpls = sender as FreeformPointLineSeries;//可能有问题
            showMarkerDict[fpls] = e.IsChecked;
            foreach (var marker in fpls.SeriesEventMarkers)
            {
                marker.Visible = e.IsChecked;
            }
            m_chart.EndUpdate();
        }    
       

        private void clearBufferBtn_Click(object sender, RoutedEventArgs e)
        {
            m_chart.BeginUpdate();
            foreach (var series in m_chart.ViewXY.FreeformPointLineSeries)
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
    }
}
