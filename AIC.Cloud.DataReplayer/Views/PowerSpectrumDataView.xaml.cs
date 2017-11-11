using AIC.Cloud.Applications;
using AIC.Cloud.Applications.Events;
using AIC.Cloud.Applications.Services;
using AIC.Cloud.DataReplayer.ViewModels;
using AIC.Cloud.Domain;
using AIC.CoreType;
using AIC.Server.Storage.Contract;
using AICMath;
using Arction.WPF.LightningChartUltimate;
using Arction.WPF.LightningChartUltimate.Annotations;
using Arction.WPF.LightningChartUltimate.Axes;
using Arction.WPF.LightningChartUltimate.SeriesXY;
using Arction.WPF.LightningChartUltimate.Views.ViewXY;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIC.Cloud.DataReplayer.Views
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
                if (viewModel == null || !(token is VibrationChannelToken)) return;
                m_chart.BeginUpdate();

                VibrationChannelToken vToken = token as VibrationChannelToken;

                AxisY axisY = new AxisY(m_chart.ViewXY);
                axisY.Tag = vToken;
                axisY.Title.Visible = false;
                axisY.AxisThickness = 2;
                axisY.AxisColor = Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.YAxes.Add(axisY);

                int count = m_chart.ViewXY.PointLineSeries.Count;
                while (count > 15)
                {
                    count -= 15;
                }
                Color color = DefaultColors.SeriesForBlackBackgroundWPF[count];
                PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], axisY);
                series.MouseInteraction = false;
                series.LineStyle.Color = color;
                series.LineStyle.AntiAliasing = LineAntialias.None;
                series.LineStyle.Width = 1;
                series.Tag = vToken;
                series.Title.Text = vToken.Channel.Name + vToken.Channel.MSSN;
                series.Title.Font = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 10f, System.Drawing.FontStyle.Bold);
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

                m_chart.ViewXY.FitView();
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

                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                m_chart.EndUpdate();
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-功率谱-删除通道", ex));
            }
        }
        private async void OnChannelDataChanged(IEnumerable<VibrationChannelToken> tokens2)
        {
            try
            {
                if (viewModel == null) return;
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

            m_chart = new LightningChartUltimate(LicenseKeyStrings.LightningChartUltimate);
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
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].LabelsFont = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            //m_chart.ViewXY.XAxes[0].LabelsPosition = Alignment.Near;
            //m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Alignment = Alignment.Near;
            //m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Color = Color.FromArgb(100, 135, 205, 238);
            //m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Alignment = Alignment.Near;
            //m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Color = Color.FromArgb(100, 135, 205, 238);
            //m_chart.ViewXY.XAxes[0].VerticalAlign = AlignmentVertical.Top;

            m_chart.ViewXY.YAxes.Clear();

            m_chart.ViewXY.LegendBox.Visible = true;
            m_chart.ViewXY.LegendBox.Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.ViewXY.LegendBox.Fill.Style = RectFillStyle.None;
            m_chart.ViewXY.LegendBox.Shadow.Visible = false;
            m_chart.ViewXY.LegendBox.BorderWidth = 0;
            m_chart.ViewXY.LegendBox.Position = LegendBoxPosition.TopRight;
            m_chart.ViewXY.LegendBox.Offset.SetValues(-50, 10);
            m_chart.ViewXY.LegendBox.SeriesTitleFont = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            //Add cursor
            LineSeriesCursor cursor = new LineSeriesCursor(m_chart.ViewXY, m_chart.ViewXY.XAxes[0]);
            m_chart.ViewXY.LineSeriesCursors.Add(cursor);
            cursor.PositionChanged += new LineSeriesCursor.PositionChangedHandler(cursor_PositionChanged);
            cursor.LineStyle.Color = System.Windows.Media.Color.FromArgb(150, 255, 0, 0);
            cursor.LineStyle.Width = 2;
            cursor.SnapToPoints = true;
            cursor.TrackPoint.Color1 = Colors.White;

            m_chart.ViewXY.FitView();
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
            cursorValueDisplay.TextStyle.Font = new WPFFont(System.Drawing.FontFamily.GenericMonospace, 9.5, System.Drawing.FontStyle.Regular);
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
            cursorValueDisplay.LocationScreenCoords = new PointFloatXY(230, 37);
            cursorValueDisplay.Sizing = AnnotationXYSizing.Automatic;
            cursorValueDisplay.TextStyle.Font = new WPFFont(System.Drawing.FontFamily.GenericMonospace, 9.5, System.Drawing.FontStyle.Regular);
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
            BindingOperations.SetBinding(cursorValueDisplay, AnnotationXY.VisibleProperty, b);
        }
        private void cursor_PositionChanged(LineSeriesCursor sender, double newValue, ref bool cancelRendering)
        {
            cancelRendering = true;
            UpdateCursorResult();
        }
        private void UpdateCursorResult()
        {
            m_chart.BeginUpdate();

            LineSeriesCursor cursor = m_chart.ViewXY.LineSeriesCursors[0];
            AnnotationXY cursorValueDisplay = m_chart.ViewXY.Annotations[0];
            float fTargetYCoord = m_chart.ViewXY.GetMarginsRect().Bottom;
            double dY;
            m_chart.ViewXY.YAxes[0].CoordToValue(fTargetYCoord, out dY);
            cursorValueDisplay.TargetAxisValues.X = cursor.ValueAtXAxis;
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
                VibrationChannelToken token = series.Tag as VibrationChannelToken;
                if (token.VData != null)
                {
                    bool bResolvedOK = false;
                    double yValue = 0;
                    bResolvedOK = SolveValueAccurate(series, cursor.ValueAtXAxis, out yValue);
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

            sb.AppendLine("频率: " + cursor.ValueAtXAxis.ToString("0.00") + "Hz");
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
                var tokens = m_chart.ViewXY.PointLineSeries.Select(o => o.Tag).OfType<VibrationChannelToken>().ToArray();
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
                if(isRender)
                {
                    var tokens = m_chart.ViewXY.PointLineSeries.Select(o => o.Tag).OfType<VibrationChannelToken>().ToArray();
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
            var tokens = m_chart.ViewXY.PointLineSeries.Select(o => o.Tag).OfType<VibrationChannelToken>().ToArray();
            await AlgorithmAllAsync(tokens);
            UpdateChartAsync(tokens);
        }
        private async void dbCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (viewModel == null) return;
            var tokens = m_chart.ViewXY.PointLineSeries.Select(o => o.Tag).OfType<VibrationChannelToken>().ToArray();
            await AlgorithmAllAsync(tokens);
            UpdateChartAsync(tokens);
        }

        private void UpdateChartAsync(VibrationChannelToken[] tokens)
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
            m_chart.ViewXY.FitView();
            m_chart.EndUpdate();
        }

        private async Task AlgorithmAllAsync(IEnumerable<VibrationChannelToken> tokens)
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

        private async Task AlgorithmAsync(VibrationChannelToken token)
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
