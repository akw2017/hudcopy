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
    public partial class TimeDomainChartView : ChartViewBase
    {
        private LightningChartUltimate m_chart;

        public TimeDomainChartView()
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
                m_chart.ViewXY.SampleDataSeries[0].Clear();
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-时域-信号变换", ex));
            }
        }

        protected override void UpdateChart(object args)
        {
            try
            {
                if (ViewModel == null || !(ViewModel.Signal is BaseWaveSignal))
                {
                    m_chart.BeginUpdate();
                    m_chart.ViewXY.SampleDataSeries[0].Clear();
                    m_chart.EndUpdate();
                    return;
                }               

                BaseWaveSignal sg = (BaseWaveSignal)ViewModel.Signal;
                if (sg.Waveform == null)
                {
                    return;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("有效值:{0}", sg.RmsValue.ToString("0.00")));
                sb.AppendLine(string.Format("峰值:{0}", sg.PeakValue.ToString("0.00")));
                sb.AppendLine(string.Format("峰峰值:{0}", sg.PeakPeakValue.ToString("0.00")));
                sb.AppendLine(string.Format("斜度:{0}", sg.Slope.ToString("0.00")));
                sb.AppendLine(string.Format("峭度:{0}", sg.Kurtosis.ToString("0.00")));
                sb.AppendLine(string.Format("峭度指标:{0}", sg.KurtosisValue.ToString("0.00")));
                sb.AppendLine(string.Format("波形指标:{0}", sg.WaveIndex.ToString("0.00")));
                sb.AppendLine(string.Format("峰值指标:{0}", sg.PeakIndex.ToString("0.00")));
                sb.AppendLine(string.Format("脉冲指标:{0}", sg.ImpulsionIndex.ToString("0.00")));
                sb.AppendLine(string.Format("裕度指标:{0}", sg.ToleranceIndex.ToString("0.00")));

                m_chart.BeginUpdate();
                if (sg.SignalProcessTypes.Contains(SignalProcessorType.Cepstrum))
                {
                    m_chart.ViewXY.SampleDataSeries[0].SamplingFrequency = sg.SampleFre / 1000;
                }
                else
                {
                    m_chart.ViewXY.SampleDataSeries[0].SamplingFrequency = 1;
                }
                m_chart.ViewXY.SampleDataSeries[0].SamplesDouble = sg.Waveform;
                m_chart.ViewXY.SampleDataSeries[0].InvalidateData();
                m_chart.ViewXY.Annotations[0].Text = sb.ToString().Trim();

                txtValue.Text = string.Format("有效值:{0},峰值:{1},峰峰值:{2}",sg.RmsValue.ToString("0.00"), sg.PeakValue.ToString("0.00"), sg.PeakPeakValue.ToString("0.00"));

                if (fitViewCheckBox.IsChecked == true)
                {
                    m_chart.ViewXY.ZoomToFit();
                }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-时域", ex));
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

            // (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffaacc"));
            // (Color)ColorConverter.ConvertFromString("#7F243859");
            m_chart = new LightningChartUltimate();
            m_chart.BeginUpdate();
            m_chart.Title.Text = string.Empty;
            m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Stacked;

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
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            //m_chart.ViewXY.XAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;     
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.XAxes[0].LabelsPosition = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Alignment = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Alignment = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].VerticalAlign = AlignmentVertical.Top;

            //Setup y-axis
            m_chart.ViewXY.YAxes[0].Title.Visible = false;
            m_chart.ViewXY.YAxes[0].AxisThickness = 2;
            m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            //m_chart.ViewXY.YAxes[0].MajorGrid.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;           
            m_chart.ViewXY.YAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.YAxes[0].MiniScale.Visible = false;
            m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Alignment = Alignment.Far;
            m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Alignment = Alignment.Far;
            m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].Alignment = AlignmentHorizontal.Right;


            m_chart.ViewXY.LegendBoxes[0].Visible = false;

            SampleDataSeries series = new SampleDataSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            series.SampleFormat = SampleFormat.DoubleFloat;
            series.MouseInteraction = false;
            // series.LineStyle.AntiAliasing = LineAntialias.None;
            series.LineStyle.Width = 1f;
            //series.LineStyle.Color = Color.FromArgb(100, 41, 160, 216);
            //series.LineStyle.Width = 2;
            //series.LineStyle.Pattern = LinePattern.Solid;
            //series.LineStyle.PatternScale = 2;

            m_chart.ViewXY.SampleDataSeries.Add(series);

            AnnotationXY cursorValueDisplay = new AnnotationXY(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            cursorValueDisplay.Style = AnnotationStyle.RoundedRectangle;
            cursorValueDisplay.LocationCoordinateSystem = CoordinateSystem.ScreenCoordinates;
            cursorValueDisplay.LocationScreenCoords = new PointFloatXY(200, 65);
            cursorValueDisplay.Sizing = AnnotationXYSizing.Automatic;
            cursorValueDisplay.TextStyle.Font = new WpfFont(System.Drawing.FontFamily.GenericMonospace, 9, System.Drawing.FontStyle.Regular);
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
            cursorValueDisplay.Text = "有效值:" + "\r\n" + "峰值:" + "\r\n" + "峰峰值:";
            cursorValueDisplay.ClipInsideGraph = false;
            m_chart.ViewXY.Annotations.Add(cursorValueDisplay);
            cursorValueDisplay.Visible = false;//不显示
            m_chart.ViewXY.ZoomToFit();
            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);
        }

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Png);
        }
    }
}
