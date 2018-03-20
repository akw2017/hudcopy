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
using AIC.Core.Events;

namespace AIC.OnLineDataPage.Views.SubViews
{
    /// <summary>
    /// Interaction logic for TimeDomainOnLineView.xaml
    /// </summary>
    public partial class PowerSpectrumDensityChartView : ChartViewBase
    {
        private LightningChartUltimate m_chart;
    
        public PowerSpectrumDensityChartView()
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
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-功率谱-信号变换", ex));
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
                                if (signal.FFTLength == 0 || signal.Frequency == null || signal.PowerSpectrumDensity == null)
                                {
                                    return;
                                }

                                m_chart.BeginUpdate();

                                var series = m_chart.ViewXY.PointLineSeries[0];
                                if (series.Points == null || series.Points.Length != signal.FFTLength)
                                {
                                    series.Points = new SeriesPoint[signal.FFTLength];
                                }
                                for (int i = 0; i < signal.FFTLength; i++)
                                {
                                    series.Points[i].X = signal.Frequency[i];
                                    series.Points[i].Y = signal.PowerSpectrumDensity[i];
                                }

                                AnnotationXY spectrumAnnotation = m_chart.ViewXY.Annotations[0];
                                StringBuilder spectrumSB = new StringBuilder();
                                spectrumSB.AppendLine("频率" + "  " + "幅值");
                                txtValue.Text = "频率/幅值:";

                                var fftValuesDict = signal.PowerSpectrumDensity.Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
                                foreach (var item in fftValuesDict)
                                {
                                    spectrumSB.AppendLine(signal.Frequency[item.Key].ToString("0.00") + "; " + item.Value.ToString("0.00"));
                                    txtValue.Text += signal.Frequency[item.Key].ToString("0.00") + "/" + item.Value.ToString("0.00") + "  ";
                                }
                                txtValue.Text = txtValue.Text.Substring(0, txtValue.Text.Length - 1);
                                spectrumAnnotation.Text = spectrumSB.ToString().Trim();

                                m_chart.ViewXY.PointLineSeries[0].InvalidateData();

                                if (fitViewCheckBox.IsChecked == true)
                                {
                                    m_chart.ViewXY.ZoomToFit();
                                }
                                m_chart.EndUpdate();
                            }
                            else
                            {
                                BaseWaveSignal signal = (BaseWaveSignal)ViewModel.Signal;
                                if (signal.FilterFFTLength == 0 || signal.FilterFrequency == null || signal.FilterPowerSpectrumDensity == null)
                                {
                                    return;
                                }

                                m_chart.BeginUpdate();

                                var series = m_chart.ViewXY.PointLineSeries[0];
                                if (series.Points == null || series.Points.Length != signal.FilterFFTLength)
                                {
                                    series.Points = new SeriesPoint[signal.FilterFFTLength];
                                }
                                for (int i = 0; i < signal.FilterFFTLength; i++)
                                {
                                    series.Points[i].X = signal.FilterFrequency[i];
                                    series.Points[i].Y = signal.FilterPowerSpectrumDensity[i];
                                }

                                AnnotationXY spectrumAnnotation = m_chart.ViewXY.Annotations[0];
                                StringBuilder spectrumSB = new StringBuilder();
                                spectrumSB.AppendLine("频率" + "  " + "幅值");
                                txtValue.Text = "频率/幅值:";

                                var fftValuesDict = signal.FilterPowerSpectrumDensity.Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
                                foreach (var item in fftValuesDict)
                                {
                                    spectrumSB.AppendLine(signal.FilterFrequency[item.Key].ToString("0.00") + "; " + item.Value.ToString("0.00"));
                                    txtValue.Text += signal.FilterFrequency[item.Key].ToString("0.00") + "/" + item.Value.ToString("0.00") + "  ";
                                }
                                txtValue.Text = txtValue.Text.Substring(0, txtValue.Text.Length - 1);
                                spectrumAnnotation.Text = spectrumSB.ToString().Trim();

                                m_chart.ViewXY.PointLineSeries[0].InvalidateData();

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
                    if (signal.ProcessingFFTLength(processName) == 0 || signal.FrequencyList == null || signal.PowerSpectrumDensityList == null 
                        || !signal.FrequencyList.ContainsKey(processName) || !signal.PowerSpectrumDensityList.ContainsKey(processName))
                    {
                        return;
                    }

                    m_chart.BeginUpdate();

                    var series = m_chart.ViewXY.PointLineSeries[0];
                    if (series.Points == null || series.Points.Length != signal.ProcessingFFTLength(processName))
                    {
                        series.Points = new SeriesPoint[signal.ProcessingFFTLength(processName)];
                    }
                    for (int i = 0; i < signal.ProcessingFFTLength(processName); i++)
                    {
                        series.Points[i].X = signal.FrequencyList[processName][i];
                        series.Points[i].Y = signal.PowerSpectrumDensityList[processName][i];
                    }

                    AnnotationXY spectrumAnnotation = m_chart.ViewXY.Annotations[0];
                    StringBuilder spectrumSB = new StringBuilder();
                    spectrumSB.AppendLine("频率" + "  " + "幅值");
                    txtValue.Text = "频率/幅值:";

                    var fftValuesDict = signal.PowerSpectrumDensityList[processName].Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
                    foreach (var item in fftValuesDict)
                    {
                        spectrumSB.AppendLine(signal.FrequencyList[processName][item.Key].ToString("0.00") + "; " + item.Value.ToString("0.00"));
                        txtValue.Text += signal.FrequencyList[processName][item.Key].ToString("0.00") + "/" + item.Value.ToString("0.00") + "  ";
                    }
                    txtValue.Text = txtValue.Text.Substring(0, txtValue.Text.Length - 1);
                    spectrumAnnotation.Text = spectrumSB.ToString().Trim();

                    m_chart.ViewXY.PointLineSeries[0].InvalidateData();

                    if (fitViewCheckBox.IsChecked == true)
                    {
                        m_chart.ViewXY.ZoomToFit();
                    }
                    m_chart.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-功率谱密度", ex));
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
                m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.Number;
                m_chart.ViewXY.XAxes[0].Title.Visible = false;
                m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
                m_chart.ViewXY.XAxes[0].AxisThickness = 2;
                m_chart.ViewXY.XAxes[0].AxisColor = whiteColor;//  Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
                                                               //m_chart.ViewXY.XAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
                m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
                m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
                m_chart.ViewXY.XAxes[0].LabelsPosition = Alignment.Near;
                m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Alignment = Alignment.Near;
                m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Color = whiteColor;// Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Alignment = Alignment.Near;
                m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Color = whiteColor;// Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.XAxes[0].VerticalAlign = AlignmentVertical.Top;

                //Setup y-axis
                m_chart.ViewXY.YAxes[0].Title.Visible = false;
                m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
                m_chart.ViewXY.YAxes[0].AxisThickness = 2;
                m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
                                                                                            //m_chart.ViewXY.YAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(0xff, 0xff, 0xff, 0xff);
                m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
                m_chart.ViewXY.YAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
                m_chart.ViewXY.YAxes[0].MiniScale.Visible = false;
                m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Alignment = Alignment.Far;
                m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Color = whiteColor;//  Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Alignment = Alignment.Far;
                m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Color = whiteColor;// Color.FromArgb(0xff, 0xff, 0xff, 0xff); //Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.YAxes[0].Alignment = AlignmentHorizontal.Right;

                PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
                series.Title.Text = "幅值";
                series.MouseInteraction = false;
                // series.LineStyle.AntiAliasing = LineAntialias.None;
                series.LineStyle.Width = 1;
                m_chart.ViewXY.PointLineSeries.Add(series);

                CreateFAAnnotation();

                m_chart.ViewXY.ZoomToFit();
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-功率谱密度-初始化", ex));
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

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Png);
        }
    }
}
