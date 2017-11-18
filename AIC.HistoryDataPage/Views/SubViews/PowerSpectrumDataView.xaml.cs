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
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.SeriesXY;
using Arction.Wpf.Charting.Annotations;
using Arction.Wpf.Charting.Views.ViewXY;
using AIC.Core.Events;
using AIC.MatlabMath;

namespace AIC.HistoryDataPage.Views
{
    /// <summary>
    /// Interaction logic for PowerSpectrumDataView.xaml
    /// </summary>
    public partial class PowerSpectrumDataView : UserControl
    {
        private LightningChartUltimate m_chart;
        private PowerSpectrumDataViewModel viewModel;
        private IDisposable channelDataChangedSubscription;
        private IDisposable channelAddedSubscription;
        private IDisposable channelRemovedSubscription;
        public PowerSpectrumDataView()
        {
            InitializeComponent();
            CreateChart();
            Loaded += PowerSpectrumDataView_Loaded;
        }

        private void PowerSpectrumDataView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= PowerSpectrumDataView_Loaded;
            viewModel = DataContext as PowerSpectrumDataViewModel;
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

                int count = m_chart.ViewXY.PointLineSeries.Count;
                while (count > 15)
                {
                    count -= 15;
                }
                Color color = DefaultColors.SeriesForBlackBackgroundWpf[count];
                PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], axisY);
                series.MouseInteraction = false;
                series.LineStyle.Color = color;
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

                if (m_chart.ViewXY.Annotations.Count == 0)
                {
                    CreateCalloutAnnotation();
                    CreateFAAnnotation();
                }

                //Update Annotation
                AnnotationXY annotation = m_chart.ViewXY.Annotations[1];
                string[] branches = annotation.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < branches.Length; i++)
                {
                    sb.AppendLine(branches[i]);
                }
                string freText = "F";
                string ampText = "A";
                if (vToken.VData != null)
                {
                    int length = vToken.VData.FFTLength;
                    SeriesPoint[] points = new SeriesPoint[length];
                    SeriesPoint[] phasePoints = new SeriesPoint[length];
                    for (int i = 0; i < length; i++)
                    {
                        points[i] = new SeriesPoint(vToken.VData.Frequency[i], vToken.VData.PowerSpectrum[i]);
                        phasePoints[i] = new SeriesPoint(vToken.VData.Frequency[i], vToken.VData.Phase[i]);
                    }
                    series.Points = points;

                    var fftValuesDict = vToken.VData.PowerSpectrum.Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
                    foreach (var item in fftValuesDict)
                    {
                        freText += string.Format("{0,6}|", vToken.VData.Frequency[item.Key].ToString("0.00"));
                        ampText += string.Format("{0,6}|", item.Value.ToString("0.00"));
                    }
                }
                sb.AppendLine(freText);
                sb.AppendLine(ampText);
                annotation.Text = sb.ToString();

                m_chart.ViewXY.PointLineSeries.Add(series);

                m_chart.ViewXY.Annotations[0].AssignYAxisIndex = -1;
                m_chart.ViewXY.Annotations[0].AssignYAxisIndex = 0;

                m_chart.ViewXY.ZoomToFit();
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                m_chart.EndUpdate();
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-功率谱-添加通道", ex));
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
                }

                var yAxis = m_chart.ViewXY.YAxes.Where(o => o.Tag == token).SingleOrDefault();
                if (yAxis != null)
                {
                    int firstIndex = m_chart.ViewXY.YAxes.IndexOf(yAxis);
                    m_chart.ViewXY.YAxes.Remove(yAxis);

                    AnnotationXY annotation = m_chart.ViewXY.Annotations[1];
                    var branches = annotation.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    branches.RemoveRange(firstIndex, 2);
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
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-功率谱-删除通道", ex));
            }
        }
        private async void OnChannelDataChanged(IEnumerable<BaseWaveChannelToken> tokens2)
        {
            try
            {
                if (viewModel == null) return;
                if (m_chart.ViewXY.PointLineSeries.Count == 0)
                {
                    return;
                }
                var tokens = tokens2.ToArray();
                await AlgorithmAllAsync(tokens);
                UpdateChartAsync(tokens);
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-功率谱-更新通道数据", ex));
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
           
            m_chart.ViewXY.YAxes.Clear();
            AxisY axisYnone = new AxisY(m_chart.ViewXY);
            axisYnone.Title.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Regular);
            axisYnone.AxisThickness = 2;
            axisYnone.AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            axisYnone.Units.Text = "none";
            m_chart.ViewXY.YAxes.Add(axisYnone);

            m_chart.ViewXY.LegendBoxes[0].Visible = true;
            m_chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.ViewXY.LegendBoxes[0].Fill.Style = RectFillStyle.None;
            m_chart.ViewXY.LegendBoxes[0].Shadow.Visible = false;
            m_chart.ViewXY.LegendBoxes[0].BorderWidth = 0;
            m_chart.ViewXY.LegendBoxes[0].Position = LegendBoxPositionXY.TopRight;
            m_chart.ViewXY.LegendBoxes[0].Offset.SetValues(-80, 10);
            m_chart.ViewXY.LegendBoxes[0].SeriesTitleFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

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
        private void CreateFAAnnotation()
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
            cursorValueDisplay.Text = "";
            cursorValueDisplay.ClipInsideGraph = false;
            m_chart.ViewXY.Annotations.Add(cursorValueDisplay);
            Binding b = new Binding();
            b.Source = DataContext as FrequencyDomainDataViewModel;
            b.Path = new PropertyPath("ShowDetail");
            b.Mode = BindingMode.TwoWay;
            //BindingOperations.SetBinding(cursorValueDisplay, AnnotationXY.VisibleProperty, b);//htzk123
        }
        private void cursor_PositionChanged(Object sender, PositionChangedEventArgs e)
        {
            e.CancelRendering = true;
            UpdateCursorResult(e.Cursor.ValueAtXAxis);
        }
        private void UpdateCursorResult(double xValue)
        {
            m_chart.BeginUpdate();
         
            AnnotationXY cursorValueDisplay = m_chart.ViewXY.Annotations[0];
            float fTargetYCoord = m_chart.ViewXY.GetMarginsRect().Bottom;
            double dY;
            m_chart.ViewXY.YAxes[0].CoordToValue(fTargetYCoord, out dY);
            cursorValueDisplay.TargetAxisValues.X = xValue;
            cursorValueDisplay.TargetAxisValues.Y = dY;

            StringBuilder sb = new StringBuilder();
            int iSeriesNumber = 1;

            string strValue = "";
            bool bLabelVisible = false;

            int seriesCount = m_chart.ViewXY.PointLineSeries.Count;
            for (int i = 0; i < seriesCount; i++)
            {
                var series = m_chart.ViewXY.PointLineSeries[i];
                strValue = iSeriesNumber + ":";
                BaseWaveChannelToken token = series.Tag as BaseWaveChannelToken;
                if (token.VData != null)
                {
                    bool bResolvedOK = false;
                    double yValue = 0;
                    bResolvedOK = SolveValueAccurate(series, xValue, out yValue);
                    if (bResolvedOK)
                    {
                        bLabelVisible = true;
                        //strValue = string.Format(strChannelStringFormat, iSeriesNumber, Math.Round(yValue, 2), unit);
                        strValue += Math.Round(yValue, 2) + "(" + token.VData.Unit + ")";
                    }
                    else
                    {
                        //strValue = string.Format(strChannelStringFormat, iSeriesNumber, "---", "Unit");
                        strValue += "---" + "(Unit)";
                    }
                }

                sb.AppendLine(strValue);
                iSeriesNumber++;
            }

            sb.AppendLine("频率: " + xValue.ToString("0.00") + "Hz");
            //Set text
            cursorValueDisplay.Text = sb.ToString().Trim();
            cursorValueDisplay.Visible = bLabelVisible;

            //Allow chart rendering
            m_chart.EndUpdate();
        }
        private bool SolveValueAccurate(PointLineSeries series, double xValue, out double yValue)
        {
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
                if (m_chart.ViewXY.PointLineSeries.Count == 0)
                {
                    return;
                }
                var tokens = m_chart.ViewXY.PointLineSeries.Select(o => o.Tag).OfType<BaseWaveChannelToken>().ToArray();
                await AlgorithmAllAsync(tokens);
                UpdateChartAsync(tokens);
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-功率谱-滤波", ex));
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
                if (m_chart.ViewXY.PointLineSeries.Count == 0)
                {
                    return;
                }
                if (isRender)
                {
                    var tokens = m_chart.ViewXY.PointLineSeries.Select(o => o.Tag).OfType<BaseWaveChannelToken>().ToArray();
                    await AlgorithmAllAsync(tokens);
                    UpdateChartAsync(tokens);
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-功率谱-滤波", ex));
            }
        }

        private async void dbCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (viewModel == null) return;
            if (m_chart.ViewXY.PointLineSeries.Count == 0)
            {
                return;
            }
            var tokens = m_chart.ViewXY.PointLineSeries.Select(o => o.Tag).OfType<BaseWaveChannelToken>().ToArray();
            await AlgorithmAllAsync(tokens);
            UpdateChartAsync(tokens);
        }
        private async void dbCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (viewModel == null) return;
            if (m_chart.ViewXY.PointLineSeries.Count == 0)
            {
                return;
            }
            var tokens = m_chart.ViewXY.PointLineSeries.Select(o => o.Tag).OfType<BaseWaveChannelToken>().ToArray();
            await AlgorithmAllAsync(tokens);
            UpdateChartAsync(tokens);
        }

        private void UpdateChartAsync(BaseWaveChannelToken[] tokens)
        {
            m_chart.BeginUpdate();           

            AnnotationXY annotation = m_chart.ViewXY.Annotations[1];
            for (int k = 0; k < tokens.Length; k++)
            {   
                var series = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag == tokens[k]).Single();
                string[] branches = annotation.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                int index = m_chart.ViewXY.PointLineSeries.IndexOf(series);

                if (tokens[k].VData != null)
                {
                    int length = tokens[k].VData.FFTLength;
                    if (series.Points == null || series.Points.Length != length)
                    {
                        series.Points = new SeriesPoint[length];
                    }


                    for (int i = 0; i < length; i++)
                    {
                        series.Points[i].X = tokens[k].VData.Frequency[i];
                        series.Points[i].Y = tokens[k].VData.PowerSpectrum[i];
                    }

                    string freText = "F";// string.Format("{0}-F", index / 2 + 1);
                    string ampText = "A";// string.Format("{0}-A", index / 2 + 1);

                    var fftValuesDict = tokens[k].VData.PowerSpectrum.Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
                    foreach (var item in fftValuesDict)
                    {
                        freText += string.Format("{0,6}|", tokens[k].VData.Frequency[item.Key].ToString("0.00"));
                        ampText += string.Format("{0,6}|", item.Value.ToString("0.00"));
                    }
                    branches[index * 2] = freText;
                    branches[index * 2 + 1] = ampText;
                }
                else
                {
                    series.Clear();
                    branches[index * 2] = string.Format("{0}:{1,6}|{2,6}|", index / 2 + 1, " ", " ");
                    branches[index * 2 + 1] = string.Format("{0}:{1,6}|{2,6}|", index / 2 + 1, " ", " ");
                }
                series.InvalidateData();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < branches.Length; i++)
                {
                    sb.AppendLine(branches[i]);
                }
                annotation.Text = sb.ToString().Trim();               
            }
            m_chart.ViewXY.ZoomToFit();
            m_chart.EndUpdate();

            m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = (m_chart.ViewXY.XAxes[0].Minimum + m_chart.ViewXY.XAxes[0].Maximum) / 2.0;
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
            int length = (int)(samplePoint / 2.56) + 1;
            token.VData.PowerSpectrum = Algorithm.Instance.PowerSpectrumAction(input, sampleFre, samplePoint, dbCheckBox.IsChecked.Value).Take(length).ToArray();
        }
    }
}
