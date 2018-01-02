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
using AIC.OnLineDataPage.ViewModels.SubViewModels;
using AIC.Core.Events;

namespace AIC.OnLineDataPage.Views.SubViews
{
    /// <summary>
    /// Interaction logic for TimeDomainOnLineView.xaml
    /// </summary>
    public partial class OrthoChartView : ChartViewBase
    {
        private LightningChartUltimate m_chart;
    
        public OrthoChartView()
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

        protected override void ViewModel_SignalChanged()
        {
            try
            {
                m_chart.BeginUpdate();
                m_chart.ViewXY.FreeformPointLineSeries[0].Clear();               
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-轴心轨迹-信号变换", ex));
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

                BaseWaveSignal signal = (BaseWaveSignal)ViewModel.Signal;

                if (((OrthoChartViewModel)ViewModel).SelectedRelativeSignal == null)
                {
                    return;
                }

                m_chart.BeginUpdate();
                BaseWaveSignal relativeSg = ((OrthoChartViewModel)ViewModel).SelectedRelativeSignal;
                if (signal.SamplePoint != relativeSg.SamplePoint)
                {
                    Exception exception = new Exception("两信号采样点数不同");
                    exception.Data.Add("X", signal.SamplePoint);
                    exception.Data.Add("Y", relativeSg.SamplePoint);
                    throw exception;
                }

                int N = signal.SamplePoint;
                if (relativeSg.ACQDatetime == signal.ACQDatetime)
                {
                    FreeformPointLineSeries series = m_chart.ViewXY.FreeformPointLineSeries[0];
                    SeriesPoint[] orthoPoints = new SeriesPoint[N];
                    if (series.Points == null || series.Points.Length != N)
                    {
                        series.Points = new SeriesPoint[N];
                    }
                    if ((bool)divCheckBox.IsChecked)
                    {
                        if ((double)multiplierUpDown.Value <= 0)
                        {
                            series.Clear();
                            throw new Exception("倍频数应大于0");
                        }
                        if (signal.RPM <= 0)
                        {
                            series.Clear();
                            throw new Exception("转速应大于0");
                        }

                        double divx = (signal.RPM / 60) * (double)multiplierUpDown.Value;
                        double intervalx = signal.SampleFre / signal.SamplePoint;
                        double indexx = divx / intervalx;
                        double minx = Math.Floor(indexx);
                        double maxx = Math.Ceiling(indexx);
                        int index;
                        if (signal.Amplitude[(int)maxx] > signal.Amplitude[(int)minx])
                        {
                            index = (int)maxx;
                        }
                        else
                        {
                            index = (int)minx;
                        }

                        double divy = (relativeSg.RPM / 60) * (double)multiplierUpDown.Value;
                        double intervaly = relativeSg.SampleFre / relativeSg.SamplePoint;
                        double indexy = divy / intervaly;
                        double miny = Math.Floor(indexy);
                        double maxy = Math.Ceiling(indexy);
                        double amplitude = 0;
                        double phase = 0;
                        double frequency = 0;                        
                        //var input = Algorithm.Instance.ByteToSingle(relativeSg.Bytes);
                        //var output = Algorithm.Instance.FFT2AndPhaseAction(input, relativeSg.SamplePoint);

                        //double[] relativeAmp = output[0];
                        //double[] relativePhase = output[1];
                        //if (relativeAmp[(int)maxy] > relativeAmp[(int)miny])
                        //{
                        //    amplitude = relativeAmp[(int)maxy];
                        //    phase = relativePhase[(int)maxy];
                        //    frequency = intervaly * (int)maxy;
                        //}
                        //else
                        //{
                        //    amplitude = relativeAmp[(int)miny];
                        //    phase = relativePhase[(int)miny];
                        //    frequency = intervaly * (int)miny;
                        //}
                        //for (int i = 0; i < N; i++)
                        //{
                        //    series.Points[i].X = signal.Amplitude[index] * Math.Sin(2 * Math.PI * signal.Frequency[index] * i / N + Math.PI * signal.Phase[index] / 180);
                        //    series.Points[i].Y = amplitude * Math.Sin(2 * Math.PI * frequency * i / N + Math.PI * phase / 180);
                        //}
                    }
                    else
                    {
                        //var vData = Algorithm.Instance.ByteToSingle(relativeSg.Bytes);
                        //for (int i = 0; i < N; i++)
                        //{
                        //    series.Points[i].X = signal.Waveform[i];
                        //    series.Points[i].Y = vData[i];
                        //}
                    }
                    series.InvalidateData();
                    if (fitViewCheckBox.IsChecked == true)
                    {
                        m_chart.ViewXY.ZoomToFit();
                    }
                }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-轴心轨迹", ex));
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

            m_chart = new LightningChartUltimate();
            m_chart.BeginUpdate();
            m_chart.Title.Text = "";
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
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.XAxes[0].LabelsPosition = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Alignment = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Alignment = Alignment.Near;
            m_chart.ViewXY.XAxes[0].VerticalAlign = AlignmentVertical.Top;

            //Setup y-axis
            m_chart.ViewXY.YAxes[0].Title.Visible = false;
            m_chart.ViewXY.YAxes[0].Title.Visible = false;
            m_chart.ViewXY.YAxes[0].AxisThickness = 2;
            m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].MiniScale.Visible = false;
            m_chart.ViewXY.YAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Alignment = Alignment.Far;
            m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Color = Colors.Gray;
            m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Alignment = Alignment.Far;
            m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Color = Colors.Gray;
            m_chart.ViewXY.YAxes[0].Alignment = AlignmentHorizontal.Right;

            FreeformPointLineSeries series = new FreeformPointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            series.MouseInteraction = false;
            series.LineStyle.AntiAliasing = LineAntialias.None;
            series.LineStyle.Width = 1;

            m_chart.ViewXY.FreeformPointLineSeries.Add(series);

            m_chart.ViewXY.ZoomToFit();
            m_chart.EndUpdate();
            gridChart.Children.Add(m_chart);
        }
        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
