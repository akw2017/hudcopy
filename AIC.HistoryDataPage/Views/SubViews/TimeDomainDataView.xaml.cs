using AIC.HistoryDataPage.ViewModels;
using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.SeriesXY;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.Annotations;
using AIC.Core.Events;
using AIC.MatlabMath;
using AIC.HistoryDataPage.Models;

namespace AIC.HistoryDataPage.Views
{
    /// <summary>
    /// Interaction logic for TimeDomainDataView.xaml
    /// </summary>
    public partial class TimeDomainDataView : UserControl
    {
        private LightningChartUltimate m_chart;
        private TimeDomainDataViewModel viewModel;
        private IDisposable channelDataChangedSubscription;
        private IDisposable channelAddedSubscription;
        private IDisposable channelRemovedSubscription;
        public TimeDomainDataView()
        {
            InitializeComponent();
            CreateChart();
            Loaded += TimeDomainDataView_Loaded;
        }

        private void TimeDomainDataView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= TimeDomainDataView_Loaded;
            viewModel = DataContext as TimeDomainDataViewModel;
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
                if (viewModel == null || !(token is BaseWaveChannelToken)) return;
                m_chart.BeginUpdate();

                var axisYnone = m_chart.ViewXY.YAxes.Where(o => o.Units.Text == "none").SingleOrDefault();
                m_chart.ViewXY.YAxes.Remove(axisYnone);

                BaseWaveChannelToken vToken = token as BaseWaveChannelToken;

                AxisY axisY = new AxisY(m_chart.ViewXY);
                axisY.Tag = vToken;
                axisY.Title.Visible = false;
                axisY.AxisThickness = 2;
                axisY.AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.YAxes.Add(axisY);
                if (m_chart.ViewXY.Annotations.Count == 0)
                {
                    CreateAnnotation();
                }
                AnnotationXY annotation = m_chart.ViewXY.Annotations[0];
                int count = m_chart.ViewXY.SampleDataSeries.Count;
                while (count > 15)
                {
                    count -= 15;
                }
                //Color color = DefaultColors.SeriesForBlackBackgroundWpf[count];
                SampleDataSeries series = new SampleDataSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], axisY);
                series.SampleFormat = SampleFormat.DoubleFloat;
                series.MouseInteraction = false;
                series.LineStyle.Color = vToken.SolidColorBrush.Color; //color;
                series.LineStyle.AntiAliasing = LineAntialias.None;
                series.LineStyle.Width = 1;
                series.Tag = vToken;
                series.Title.Text = vToken.DisplayName;//htzk123
                series.Title.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 10f, System.Drawing.FontStyle.Bold);
                series.Title.Color = ChartTools.CalcGradient(Colors.White, Colors.White, 50);
                series.Title.HorizontalAlign = AlignmentHorizontal.Left;
                series.Title.VerticalAlign = AlignmentVertical.Top;
                series.Title.MoveByMouse = false;
                series.Title.MouseInteraction = false;
                series.Title.Offset = new PointIntXY(5, 5);
                series.Title.Visible = true;

                //Update Annotation      
                StringBuilder sb = new StringBuilder();
                string[] branches = annotation.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < branches.Length; i++)
                {
                    sb.AppendLine(branches[i]);
                }
                string text = string.Format("{0}:", m_chart.ViewXY.YAxes.Count);
                if (vToken.VData != null)
                {
                    series.SamplesDouble = vToken.VData.Waveform;
                    axisY.Title.Text += "\r\n" + "  (" + vToken.VData.Unit + ")";
                    text = string.Format("{0,6}|{1,6}|{2,6}|{3,7}|{4,6}|{5,9}|{6,9}|{7,9}|{8,9}|{9,9}|{10,9}",
                        vToken.VData.RMSValue.ToString("0.00"),
                        vToken.VData.PeakValue.ToString("0.00"),
                        vToken.VData.PeakPeakValue.ToString("0.00"),
                        vToken.VData.Slope.ToString("0.00"),
                        vToken.VData.Kurtosis.ToString("0.00"),
                        vToken.VData.KurtosisValue.ToString("0.00"),
                        vToken.VData.WaveIndex.ToString("0.00"),
                        vToken.VData.PeakIndex.ToString("0.00"),
                        vToken.VData.ImpulsionIndex.ToString("0.00"),
                        vToken.VData.RootAmplitude.ToString("0.00"),
                        vToken.VData.ToleranceIndex.ToString("0.00"));
                }
                sb.Append(text);
                annotation.Text = sb.ToString().Trim();
                m_chart.ViewXY.SampleDataSeries.Add(series);

                m_chart.ViewXY.ZoomToFit();
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                m_chart.EndUpdate();
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-时域-添加通道", ex));
            }
        }
        private void OnChannelRemoved(ChannelToken token)
        {
            try
            {
                m_chart.BeginUpdate();
                var series = m_chart.ViewXY.SampleDataSeries.Where(o => o.Tag == token).SingleOrDefault();
                if (series != null)
                {
                    series.Clear();
                    m_chart.ViewXY.SampleDataSeries.Remove(series);
                }

                var yAxis = m_chart.ViewXY.YAxes.Where(o => o.Tag == token).SingleOrDefault();
                if (yAxis != null)
                {
                    int firstIndex = m_chart.ViewXY.YAxes.IndexOf(yAxis);
                    m_chart.ViewXY.YAxes.Remove(yAxis);

                    AnnotationXY annotation = m_chart.ViewXY.Annotations[0];
                    var branches = annotation.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    branches.RemoveAt(firstIndex + 1);
                    StringBuilder sb = new StringBuilder();
                    foreach (var branch in branches)
                    {
                        sb.AppendLine(branch);
                    }
                    annotation.Text = sb.ToString().Trim(); 
                }

                if (m_chart.ViewXY.YAxes.Count == 0)
                {
                    m_chart.ViewXY.Annotations.Clear();

                    AxisY axisYnone = new AxisY(m_chart.ViewXY);
                    axisYnone.Title.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Regular);
                    axisYnone.AxisThickness = 2;
                    axisYnone.AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
                    axisYnone.Units.Text = "none";
                    m_chart.ViewXY.YAxes.Add(axisYnone);
                }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                m_chart.EndUpdate();
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-时域-删除通道", ex));
            }
        }
        private async void OnChannelDataChanged(IEnumerable<BaseWaveChannelToken> tokens2)
        {
            try
            {
                if (viewModel == null) return;
                if (m_chart.ViewXY.SampleDataSeries.Count == 0)
                {
                    return;
                }
                var tokens = tokens2.ToArray();
                await AlgorithmAllAsync(tokens);
                UpdateChartAsync(tokens);
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-时域-更新通道数据", ex));
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
            m_chart.Title.Visible = false;
            m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Stacked;

            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.Number;
            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            //m_chart.ViewXY.XAxes[0].LabelsPosition = Alignment.Near;
            //m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Alignment = Alignment.Near;
            //m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Color = Color.FromArgb(100, 135, 205, 238);
            //m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Alignment = Alignment.Near;
            //m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Color = Color.FromArgb(100, 135, 205, 238);
            //m_chart.ViewXY.XAxes[0].VerticalAlign = AlignmentVertical.Top;

            m_chart.ViewXY.YAxes.Clear();
            AxisY axisYnone = new AxisY(m_chart.ViewXY);
            axisYnone.Title.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Regular);
            axisYnone.AxisThickness = 2;
            axisYnone.AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            axisYnone.Units.Text = "none";
            m_chart.ViewXY.YAxes.Add(axisYnone);

            m_chart.ViewXY.LegendBoxes[0].Visible = false;

            m_chart.ViewXY.ZoomToFit();
            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);
        }
        private void CreateAnnotation()
        {
            AnnotationXY cursorValueDisplay = new AnnotationXY(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            cursorValueDisplay.Style = AnnotationStyle.Rectangle;
            cursorValueDisplay.LocationCoordinateSystem = CoordinateSystem.ScreenCoordinates;
            cursorValueDisplay.LocationScreenCoords = new PointFloatXY(500, 37);
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
            cursorValueDisplay.Text = "  有效值|  峰值| 峰峰值|  斜度|  峭度| 峭度指标| 波形指标| 峰值指标| 脉冲指标| 方根幅值| 裕度指标|";

            cursorValueDisplay.ClipInsideGraph = false;        
            m_chart.ViewXY.Annotations.Add(cursorValueDisplay);
            Binding b = new Binding();
            b.Source = DataContext as TimeDomainDataViewModel;
            b.Path = new PropertyPath("ShowDetail");
            b.Mode = BindingMode.TwoWay;
            //BindingOperations.SetBinding(cursorValueDisplay, AnnotationXY.VisibleProperty, b);//htzk123
        }

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }

        private bool isRender;
        private async void filterCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((CheckBox)sender).Name == "envelopeCheckBox")
                {
                    isRender = false;
                    tffCheckBox.IsChecked = false;
                    cepstrumCheckBox.IsChecked = false;
                }
                else if (((CheckBox)sender).Name == "tffCheckBox")
                {
                    isRender = false;
                    envelopeCheckBox.IsChecked = false;
                    cepstrumCheckBox.IsChecked = false;
                }
                else if (((CheckBox)sender).Name == "cepstrumCheckBox")
                {
                    isRender = false;
                    envelopeCheckBox.IsChecked = false;
                    tffCheckBox.IsChecked = false;
                }

                if (viewModel == null) return;
                if (m_chart.ViewXY.SampleDataSeries.Count == 0)
                {
                    return;
                }
                var tokens = m_chart.ViewXY.SampleDataSeries.Select(o => o.Tag).OfType<BaseWaveChannelToken>().ToArray();
                await AlgorithmAllAsync(tokens);
                UpdateChartAsync(tokens);
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-频域-滤波", ex));
            }
            finally
            {
                isRender = true;
            }
        }
        private async void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel == null) return;
                if (m_chart.ViewXY.SampleDataSeries.Count == 0)
                {
                    return;
                }
                if (isRender)
                {
                    var tokens = m_chart.ViewXY.SampleDataSeries.Select(o => o.Tag).OfType<BaseWaveChannelToken>().ToArray();
                    await AlgorithmAllAsync(tokens);
                    UpdateChartAsync(tokens);
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-频域-滤波", ex));
            }
        }      

        private void UpdateChartAsync(BaseWaveChannelToken[] tokens)
        {
            m_chart.BeginUpdate();

            AnnotationXY annotation = m_chart.ViewXY.Annotations[0];
            for (int k = 0; k < tokens.Length; k++)
            {
                var series = m_chart.ViewXY.SampleDataSeries.Where(o => o.Tag == tokens[k]).SingleOrDefault();
                if (series == null)
                {
                    continue;
                }
                string[] branches = annotation.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (tokens[k].VData != null)
                {
                    if (cepstrumCheckBox.IsChecked == true)
                    {
                        series.SamplingFrequency = tokens[k].VData.SampleFre / 1000;
                    }
                    else
                    {
                        series.SamplingFrequency = 1;
                    }
                    series.SamplesDouble = tokens[k].VData.FilterWaveform;

                    branches[k + 1] = string.Format("{0,6}|{1,6}|{2,6}|{3,7}|{4,6}|{5,9}|{6,9}|{7,9}|{8,9}|{9,9}|{10,9}",
                        tokens[k].VData.RMSValue.ToString("0.00"),
                        tokens[k].VData.PeakValue.ToString("0.00"),
                        tokens[k].VData.PeakPeakValue.ToString("0.00"),
                        tokens[k].VData.Slope.ToString("0.00"),
                        tokens[k].VData.Kurtosis.ToString("0.00"),
                        tokens[k].VData.KurtosisValue.ToString("0.00"),
                        tokens[k].VData.WaveIndex.ToString("0.00"),
                        tokens[k].VData.PeakIndex.ToString("0.00"),
                        tokens[k].VData.ImpulsionIndex.ToString("0.00"),
                        tokens[k].VData.RootAmplitude.ToString("0.00"),
                        tokens[k].VData.ToleranceIndex.ToString("0.00"));
                }
                else
                {
                    series.Clear();
                    branches[k + 1] = string.Format("{0,6}|{1,6}|{2,6}|{3,7}|{4,6}|{5,9}|{6,9}|{7,9}|{8,9}|{9,9}|{10,9}", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < branches.Length; i++)
                {
                    sb.AppendLine(branches[i]);
                }
                annotation.Text = sb.ToString().Trim();
            }
            m_chart.ViewXY.ZoomToFit();
            m_chart.EndUpdate();
        }

        private async Task AlgorithmAllAsync(IEnumerable<BaseWaveChannelToken> tokens)
        {
            List<Task> taskList = new List<Task>();
            foreach (var token in tokens)
            {
                if (token.VData != null)
                {
                    taskList.Add(AlgorithmAsync(token));
                }
            }
           await Task.WhenAll(taskList);
        }

        private async Task AlgorithmAsync(BaseWaveChannelToken token)
        {
            double sampleFre = token.VData.SampleFre;
            if (token.VData.Trigger == TriggerType.Angle)
            {
                sampleFre = token.VData.RPM * token.VData.TeethNumber / 60;
            }
            int samplePoint = token.VData.SamplePoint;
            double rpm = token.VData.RPM;
            var input = token.VData.Waveform;

            if (filterCheckBox.IsChecked == true)
            {
                input = await Task.Run(() => { return viewModel.Filter(input, samplePoint, sampleFre, rpm); });
            }
            if (envelopeCheckBox.IsChecked == true)
            {
                input = await Task.Run(() => { return Algorithm.Instance.Envelope(input, samplePoint); });
            }
            else if (tffCheckBox.IsChecked == true)
            {
                input = await Task.Run(() => { return Algorithm.Instance.TFF(input, samplePoint, sampleFre); });
            }
            else if (cepstrumCheckBox.IsChecked == true)
            {
                input = await Task.Run(() => { return Algorithm.Instance.Cepstrum(input, samplePoint); });
            }
            token.VData.FilterWaveform = input;
        }
    }
}
