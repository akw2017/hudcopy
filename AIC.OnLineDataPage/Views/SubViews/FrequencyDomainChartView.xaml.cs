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
using AIC.Core.Events;
using AIC.MatlabMath;

namespace AIC.OnLineDataPage.Views.SubViews
{
    /// <summary>
    /// Interaction logic for TimeDomainOnLineView.xaml
    /// </summary>
    public partial class FrequencyDomainChartView : ChartViewBase
    {
        public bool IsHideProccess
        {
            get { return Convert.ToBoolean(GetValue(IsHideProccessProperty)); }
            set { SetValue(IsHideProccessProperty, value); }
        }

        public static readonly DependencyProperty IsHideProccessProperty =
            DependencyProperty.RegisterAttached(nameof(IsHideProccess), typeof(bool), typeof(FrequencyDomainChartView), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsHideProccessChanged)));
        private static void OnIsHideProccessChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrequencyDomainChartView pager = (FrequencyDomainChartView)sender;
            if ((bool)e.NewValue == true)
            {
                pager.filterCheckBox.Visibility = Visibility.Collapsed;
                pager.dropDownButton.Visibility = Visibility.Collapsed;
                pager.preCheckBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                pager.filterCheckBox.Visibility = Visibility.Visible;
                pager.dropDownButton.Visibility = Visibility.Visible;
                pager.preCheckBox.Visibility = Visibility.Visible;
            }
        }

        private LightningChartUltimate m_chart;
    
        public FrequencyDomainChartView()
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

        protected override void ViewModel_SignalChanged()
        {
            try
            {
                m_chart.BeginUpdate();

                m_chart.ViewXY.PointLineSeries[0].Clear();
                txtValue.Text = string.Empty;
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-频域-信号变换", ex));
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
                if (ViewModel == null || !(ViewModel.Signal is BaseWaveSignal))
                {
                    return;
                }
              

                string processName = null;
                switch (ViewModel.SignalPreProccessType)
                {
                    case SignalPreProccessType.None:
                        {
                            if (ViewModel.IsFilter == false)
                            {
                                BaseWaveSignal signal = (BaseWaveSignal)ViewModel.Signal;
                                if (signal.FFTLength == 0 || signal.Frequency == null || signal.Amplitude == null || signal.Phase == null)
                                {
                                    return;
                                }
                                m_chart.BeginUpdate();

                                if (signal.TriggerN == AIC.CoreType.TriggerType.Angle)
                                {
                                    if (signal.RPM <= 0)
                                    {
                                        Exception exception = new Exception("等角度触发模式下转速应大于0");
                                        exception.Data.Add("RPM", signal.RPM);
                                        throw exception;
                                    }
                                    if (signal.TeethNumber <= 0)
                                    {
                                        Exception exception = new Exception("等角度触发模式下齿轮齿数应大于0");
                                        exception.Data.Add("TeethNumber", signal.TeethNumber);
                                        throw exception;
                                    }
                                }

                                var series = m_chart.ViewXY.PointLineSeries[0];
                                var phaseSeries = m_chart.ViewXY.PointLineSeries[1];

                                if (series.Points == null || series.Points.Length != signal.FFTLength)
                                {
                                    series.Points = new SeriesPoint[signal.FFTLength];
                                }
                                if (phaseSeries.Points == null || phaseSeries.Points.Length != signal.FFTLength)
                                {
                                    phaseSeries.Points = new SeriesPoint[signal.FFTLength];
                                }

                                for (int i = 0; i < signal.FFTLength; i++)
                                {
                                    series.Points[i].X = signal.Frequency[i];
                                    series.Points[i].Y = signal.Amplitude[i];

                                    phaseSeries.Points[i].X = signal.Frequency[i];
                                    phaseSeries.Points[i].Y = signal.Phase[i];
                                }

                                if (m_chart.ViewXY.Annotations[1].Visible)
                                {
                                    LineSeriesCursor lineSeriesCursor = m_chart.ViewXY.LineSeriesCursors[0];
                                    int index = GetNearestPointIndex(series, lineSeriesCursor.ValueAtXAxis);
                                    if (index == -1)
                                    {
                                        m_chart.ViewXY.Annotations[1].Text = string.Empty;
                                    }
                                    else
                                    {
                                        SeriesPoint point = series.Points[index];
                                        m_chart.ViewXY.Annotations[1].Text = string.Format("幅值:{0}", Math.Round(point.Y, 3)) + "\r\n" + string.Format("频率:{0}", Math.Round(point.X, 3));
                                    }
                                }

                                m_chart.ViewXY.PointLineSeries[0].InvalidateData();
                                m_chart.ViewXY.PointLineSeries[1].InvalidateData();
                                AnnotationXY spectrumAnnotation = m_chart.ViewXY.Annotations[0];
                                StringBuilder spectrumSB = new StringBuilder();
                                spectrumSB.AppendLine("频率" + "  " + "幅值");
                                txtValue.Text = "频率/幅值:";

                                var fftValuesDict = signal.Amplitude.Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
                                foreach (var item in fftValuesDict)
                                {
                                    spectrumSB.AppendLine(signal.Frequency[item.Key].ToString("0.00") + "; " + item.Value.ToString("0.00"));
                                    txtValue.Text += signal.Frequency[item.Key].ToString("0.00") + "/" + item.Value.ToString("0.00") + "  ";
                                }
                                txtValue.Text = txtValue.Text.Substring(0, txtValue.Text.Length - 1);
                                spectrumAnnotation.Text = spectrumSB.ToString().Trim();

                                if (fitViewCheckBox.IsChecked == true)
                                {
                                    m_chart.ViewXY.ZoomToFit();
                                }
                                m_chart.EndUpdate();
                            }
                            else
                            {
                                BaseWaveSignal signal = (BaseWaveSignal)ViewModel.Signal;
                                if (signal.FilterFFTLength == 0 || signal.FilterFrequency == null || signal.FilterAmplitude == null || signal.FilterPhase == null)
                                {
                                    return;
                                }
                                m_chart.BeginUpdate();

                                if (signal.TriggerN == AIC.CoreType.TriggerType.Angle)
                                {
                                    if (signal.RPM <= 0)
                                    {
                                        Exception exception = new Exception("等角度触发模式下转速应大于0");
                                        exception.Data.Add("RPM", signal.RPM);
                                        throw exception;
                                    }
                                    if (signal.TeethNumber <= 0)
                                    {
                                        Exception exception = new Exception("等角度触发模式下齿轮齿数应大于0");
                                        exception.Data.Add("TeethNumber", signal.TeethNumber);
                                        throw exception;
                                    }
                                }

                                var series = m_chart.ViewXY.PointLineSeries[0];
                                var phaseSeries = m_chart.ViewXY.PointLineSeries[1];

                                if (series.Points == null || series.Points.Length != signal.FilterFFTLength)
                                {
                                    series.Points = new SeriesPoint[signal.FilterFFTLength];
                                }
                                if (phaseSeries.Points == null || phaseSeries.Points.Length != signal.FilterFFTLength)
                                {
                                    phaseSeries.Points = new SeriesPoint[signal.FilterFFTLength];
                                }

                                for (int i = 0; i < signal.FilterFFTLength; i++)
                                {
                                    series.Points[i].X = signal.FilterFrequency[i];
                                    series.Points[i].Y = signal.FilterAmplitude[i];

                                    phaseSeries.Points[i].X = signal.FilterFrequency[i];
                                    phaseSeries.Points[i].Y = signal.FilterPhase[i];
                                }

                                if (m_chart.ViewXY.Annotations[1].Visible)
                                {
                                    LineSeriesCursor lineSeriesCursor = m_chart.ViewXY.LineSeriesCursors[0];
                                    int index = GetNearestPointIndex(series, lineSeriesCursor.ValueAtXAxis);
                                    if (index == -1)
                                    {
                                        m_chart.ViewXY.Annotations[1].Text = string.Empty;
                                    }
                                    else
                                    {
                                        SeriesPoint point = series.Points[index];
                                        m_chart.ViewXY.Annotations[1].Text = string.Format("幅值:{0}", Math.Round(point.Y, 3)) + "\r\n" + string.Format("频率:{0}", Math.Round(point.X, 3));
                                    }
                                }

                                m_chart.ViewXY.PointLineSeries[0].InvalidateData();
                                m_chart.ViewXY.PointLineSeries[1].InvalidateData();
                                AnnotationXY spectrumAnnotation = m_chart.ViewXY.Annotations[0];
                                StringBuilder spectrumSB = new StringBuilder();
                                spectrumSB.AppendLine("频率" + "  " + "幅值");
                                txtValue.Text = "频率/幅值:";

                                var fftValuesDict = signal.FilterAmplitude.Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
                                foreach (var item in fftValuesDict)
                                {
                                    spectrumSB.AppendLine(signal.FilterFrequency[item.Key].ToString("0.00") + "; " + item.Value.ToString("0.00"));
                                    txtValue.Text += signal.FilterFrequency[item.Key].ToString("0.00") + "/" + item.Value.ToString("0.00") + "  ";
                                }
                                txtValue.Text = txtValue.Text.Substring(0, txtValue.Text.Length - 1);
                                spectrumAnnotation.Text = spectrumSB.ToString().Trim();

                                if (fitViewCheckBox.IsChecked == true)
                                {
                                    m_chart.ViewXY.ZoomToFit();
                                }
                                m_chart.EndUpdate();
                            }
                            return;
                        }
                    case SignalPreProccessType.Envelope:
                        {
                            if (ViewModel.IsFilter == false)
                            {
                                processName = "Envelope";
                            }
                            else
                            {
                                processName = "FilterEnvelope";
                            }
                            break;
                        }
                    case SignalPreProccessType.TFF:
                        {
                            if (ViewModel.IsFilter == false)
                            {
                                processName = "TFF";
                            }
                            else
                            {
                                processName = "FilterTFF";
                            }
                            break;
                        }
                    case SignalPreProccessType.Cepstrum:
                        {
                            if (ViewModel.IsFilter == false)
                            {
                                processName = "Cepstrum";
                            }
                            else
                            {
                                processName = "FilterCepstrum";
                            }
                            break;
                        }
                }
                if (processName != null)
                {
                    BaseWaveSignal signal = (BaseWaveSignal)ViewModel.Signal;
                    if (signal.ProcessingFFTLength(processName) == 0 || signal.FrequencyList == null || signal.AmplitudeList  == null || signal.PhaseList == null 
                        || !signal.FrequencyList.ContainsKey(processName) || !signal.AmplitudeList.ContainsKey(processName)|| !signal.PhaseList.ContainsKey(processName))
                    {
                        return;
                    }
                    m_chart.BeginUpdate();

                    if (signal.TriggerN == AIC.CoreType.TriggerType.Angle)
                    {
                        if (signal.RPM <= 0)
                        {
                            Exception exception = new Exception("等角度触发模式下转速应大于0");
                            exception.Data.Add("RPM", signal.RPM);
                            throw exception;
                        }
                        if (signal.TeethNumber <= 0)
                        {
                            Exception exception = new Exception("等角度触发模式下齿轮齿数应大于0");
                            exception.Data.Add("TeethNumber", signal.TeethNumber);
                            throw exception;
                        }
                    }

                    var series = m_chart.ViewXY.PointLineSeries[0];
                    var phaseSeries = m_chart.ViewXY.PointLineSeries[1];

                    if (series.Points == null || series.Points.Length != signal.ProcessingFFTLength(processName))
                    {
                        series.Points = new SeriesPoint[signal.ProcessingFFTLength(processName)];
                    }
                    if (phaseSeries.Points == null || phaseSeries.Points.Length != signal.ProcessingFFTLength(processName))
                    {
                        phaseSeries.Points = new SeriesPoint[signal.ProcessingFFTLength(processName)];
                    }

                    for (int i = 0; i < signal.ProcessingFFTLength(processName); i++)
                    {
                        series.Points[i].X = signal.FrequencyList[processName][i];
                        series.Points[i].Y = signal.AmplitudeList[processName][i];

                        phaseSeries.Points[i].X = signal.FrequencyList[processName][i];
                        phaseSeries.Points[i].Y = signal.PhaseList[processName][i];
                    }

                    if (m_chart.ViewXY.Annotations[1].Visible)
                    {
                        LineSeriesCursor lineSeriesCursor = m_chart.ViewXY.LineSeriesCursors[0];
                        int index = GetNearestPointIndex(series, lineSeriesCursor.ValueAtXAxis);
                        if (index == -1)
                        {
                            m_chart.ViewXY.Annotations[1].Text = string.Empty;
                        }
                        else
                        {
                            SeriesPoint point = series.Points[index];
                            m_chart.ViewXY.Annotations[1].Text = string.Format("幅值:{0}", Math.Round(point.Y, 3)) + "\r\n" + string.Format("频率:{0}", Math.Round(point.X, 3));
                        }
                    }

                    m_chart.ViewXY.PointLineSeries[0].InvalidateData();
                    m_chart.ViewXY.PointLineSeries[1].InvalidateData();
                    AnnotationXY spectrumAnnotation = m_chart.ViewXY.Annotations[0];
                    StringBuilder spectrumSB = new StringBuilder();
                    spectrumSB.AppendLine("频率" + "  " + "幅值");
                    txtValue.Text = "频率/幅值:";

                    var fftValuesDict = signal.AmplitudeList[processName].Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
                    foreach (var item in fftValuesDict)
                    {
                        spectrumSB.AppendLine(signal.FrequencyList[processName][item.Key].ToString("0.00") + "; " + item.Value.ToString("0.00"));
                        txtValue.Text += signal.FrequencyList[processName][item.Key].ToString("0.00") + "/" + item.Value.ToString("0.00") + "  ";
                    }
                    txtValue.Text = txtValue.Text.Substring(0, txtValue.Text.Length - 1);
                    spectrumAnnotation.Text = spectrumSB.ToString().Trim();

                    if (fitViewCheckBox.IsChecked == true)
                    {
                        m_chart.ViewXY.ZoomToFit();
                    }
                    m_chart.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-频域", ex));
                m_chart.EndUpdate();
            }
            finally
            {

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

            try
            {
                m_chart = new LightningChartUltimate();
                m_chart.BeginUpdate();

                Color whiteColor = ((SolidColorBrush)Application.Current.Resources["ChartWhiteAccentColorBrush"]).Color;
                m_chart.Title.Text = string.Empty;
                m_chart.ViewXY.AxisLayout.YAxisAutoPlacement = YAxisAutoPlacement.LeftThenRight;

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
                m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
                m_chart.ViewXY.XAxes[0].AxisThickness = 2;
                m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);// Color.FromArgb(100, 135, 205, 238);
                //m_chart.ViewXY.XAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
                m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
                m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
                m_chart.ViewXY.XAxes[0].LabelsPosition = Alignment.Near;
                m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Alignment = Alignment.Near;
                m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Color = whiteColor;// Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Alignment = Alignment.Near;
                m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Color = whiteColor;// Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.XAxes[0].VerticalAlign = AlignmentVertical.Top;

                //Setup y-axis
                m_chart.ViewXY.YAxes[0].Title.Visible = false;
                m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
                m_chart.ViewXY.YAxes[0].AxisThickness = 2;
                m_chart.ViewXY.YAxes[0].AxisColor = whiteColor;//  Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                //m_chart.ViewXY.YAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
                m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
                m_chart.ViewXY.YAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
                m_chart.ViewXY.YAxes[0].MiniScale.Visible = false;
                m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Alignment = Alignment.Far;
                m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Color = whiteColor;//  Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Alignment = Alignment.Far;
                m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Color = whiteColor;// Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.YAxes[0].Alignment = AlignmentHorizontal.Right;

                //Second Y Axis
                AxisY secondYAxis = new AxisY(m_chart.ViewXY, true);
                secondYAxis.ValueType = AxisValueType.Number;
                secondYAxis.Position = 100;
                secondYAxis.Title.Visible = false;
                secondYAxis.MinorGrid.Visible = false;
                secondYAxis.AxisThickness = 2;
                secondYAxis.AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                //secondYAxis.MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
                //secondYAxis.AxisColor = Colors.Teal;
                secondYAxis.MinorGrid.Visible = false;
                secondYAxis.LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
                secondYAxis.MiniScale.Visible = false;
                secondYAxis.MajorDivTickStyle.Alignment = Alignment.Near;
                secondYAxis.MajorDivTickStyle.Color = whiteColor;// Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                secondYAxis.MinorDivTickStyle.Alignment = Alignment.Near;
                secondYAxis.MinorDivTickStyle.Color = whiteColor;//  Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                secondYAxis.Alignment = AlignmentHorizontal.Left;

                m_chart.ViewXY.LegendBoxes[0].Visible = true;
                m_chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;
                m_chart.ViewXY.LegendBoxes[0].Fill.Style = RectFillStyle.None;
                m_chart.ViewXY.LegendBoxes[0].Shadow.Visible = false;
                m_chart.ViewXY.LegendBoxes[0].BorderWidth = 0;
                m_chart.ViewXY.LegendBoxes[0].Position = LegendBoxPositionXY.TopRight;
                m_chart.ViewXY.LegendBoxes[0].Offset.SetValues(-30, 5);
                m_chart.ViewXY.LegendBoxes[0].SeriesTitleFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

                PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
                series.Title.Text = "幅值";
                series.MouseInteraction = false;
                // series.LineStyle.AntiAliasing = LineAntialias.None;
                series.LineStyle.Width = 1;
                m_chart.ViewXY.PointLineSeries.Add(series);

                PointLineSeries series2 = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[1]);
                series2.Title.Text = "相位";
                series2.Title.Color = Colors.Wheat;
                series2.LineStyle.Color = Colors.Wheat;
                series2.MouseInteraction = false;
                series2.Visible = false;
                //  series2.LineStyle.AntiAliasing = LineAntialias.None;
                series2.LineStyle.Width = 1;
                series2.CursorTrackEnabled = false;
                m_chart.ViewXY.PointLineSeries.Add(series2);

                CreateFAAnnotation();

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

                m_chart.ViewXY.ZoomToFit();
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-频域-初始化", ex));
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
        private void CreateFAAnnotation()
        {
            AnnotationXY cursorValueDisplay = new AnnotationXY(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            cursorValueDisplay.Style = AnnotationStyle.RoundedRectangle;
            cursorValueDisplay.LocationCoordinateSystem = CoordinateSystem.ScreenCoordinates;
            cursorValueDisplay.LocationScreenCoords = new PointFloatXY(230, 55);
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
            cursorValueDisplay.Visible = false;
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
            cursorValueDisplay.Visible = false;
            m_chart.ViewXY.Annotations.Add(cursorValueDisplay);
            cursorValueDisplay.Visible = false;
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

                AnnotationXY cursorValueDisplay = m_chart.ViewXY.Annotations[1];
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
                    sb.AppendLine(string.Format("幅值:{0}", Math.Round(point.Y, 3)));
                    sb.AppendLine(string.Format("频率:{0}", Math.Round(point.X, 3)));
                    cursorValueDisplay.Text = sb.ToString().Trim();
                    cursorValueDisplay.Visible = true;
                }

                //Allow chart rendering

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
        private void scrollCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //m_chart.ViewXY.Annotations[1].Visible = true;
            m_chart.ViewXY.LineSeriesCursors[0].Visible = true;
            m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = (m_chart.ViewXY.XAxes[0].Minimum + m_chart.ViewXY.XAxes[0].Maximum) / 2.0;
        }
        private void scrollCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.LineSeriesCursors[0].Visible = false;
            m_chart.ViewXY.Annotations[1].Visible = false;
        }
        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
