using AIC.Cloud.Applications;
using AIC.Cloud.Applications.Events;
using AIC.Cloud.Applications.Services;
using AIC.Cloud.DataReplayer.ViewModels;
using AIC.Cloud.Domain;
using AIC.CoreType;
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
    /// Interaction logic for FrequencyDomainDataView.xaml
    /// </summary>
    public partial class FrequencyDomainDataView : UserControl
    {
        private LightningChartUltimate m_chart;
        private FrequencyDomainDataViewModel viewModel;
        private IDisposable channelDataChangedSubscription;
        private IDisposable channelAddedSubscription;
        private IDisposable channelRemovedSubscription;
        public FrequencyDomainDataView()
        {
            InitializeComponent();
            CreateChart();
            Loaded += FrequencyDomainDataView_Loaded;
        }
        private void FrequencyDomainDataView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= FrequencyDomainDataView_Loaded;
            viewModel = DataContext as FrequencyDomainDataViewModel;
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
                //Create new Y axis for each series 
                AxisY axisY = new AxisY(m_chart.ViewXY);
                axisY.Tag = vToken;
                axisY.Title.Visible = false;
                axisY.AxisThickness = 2;
                axisY.AxisColor = Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.YAxes.Add(axisY);
                //Create a point-line series
                int count = m_chart.ViewXY.PointLineSeries.Count / 2;
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

                AxisY axisYPhase = new AxisY(m_chart.ViewXY);
                axisYPhase.Tag = vToken;
                axisYPhase.Title.Visible = false;
                axisYPhase.AxisThickness = 2;
                axisYPhase.AxisColor = Color.FromArgb(100, 135, 205, 238);
                m_chart.ViewXY.YAxes.Add(axisYPhase);
                PointLineSeries phaseSeries = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], axisYPhase);
                phaseSeries.MouseInteraction = false;
                phaseSeries.LineStyle.Color = color;
                phaseSeries.LineStyle.AntiAliasing = LineAntialias.None;
                phaseSeries.LineStyle.Width = 1;
                phaseSeries.Tag = vToken;
                phaseSeries.Title.Text = vToken.Channel.Name + vToken.Channel.MSSN + "相位";
                phaseSeries.Visible = false;

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
                        points[i] = new SeriesPoint(vToken.VData.Frequency[i], vToken.VData.Amplitude[i]);
                        phasePoints[i] = new SeriesPoint(vToken.VData.Frequency[i], vToken.VData.Phase[i]);
                    }
                    series.Points = points;
                    phaseSeries.Points = phasePoints;

                    var fftValuesDict = vToken.VData.Amplitude.Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
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
                m_chart.ViewXY.PointLineSeries.Add(phaseSeries);

                m_chart.ViewXY.AxisLayout.Segments.Add(new YAxisSegment(m_chart.ViewXY.AxisLayout));
                axisY.SegmentIndex = m_chart.ViewXY.AxisLayout.Segments.Count - 1;
                axisYPhase.SegmentIndex = m_chart.ViewXY.AxisLayout.Segments.Count - 1;

                m_chart.ViewXY.Annotations[0].AssignYAxisIndex = -1;
                m_chart.ViewXY.Annotations[0].AssignYAxisIndex = 0;

                m_chart.ViewXY.FitView();
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-频域-添加通道", ex));
            }
        }
        private void OnChannelRemoved(ChannelToken token)
        {
            try
            {
                m_chart.BeginUpdate();
                var serieses = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag == token).ToArray();
                if (serieses.Length > 0)
                {
                    foreach (var series in serieses)
                    {
                        series.Clear();
                        m_chart.ViewXY.PointLineSeries.Remove(series);
                    }

                    var yAxises = m_chart.ViewXY.YAxes.Where(o => o.Tag == token).ToArray();
                    int firstIndex = m_chart.ViewXY.YAxes.IndexOf(yAxises.First());
                    m_chart.ViewXY.AxisLayout.Segments.RemoveAt(yAxises[0].SegmentIndex);
                    foreach (var axis in yAxises)
                    {
                        m_chart.ViewXY.YAxes.Remove(axis);
                    }
                    if (m_chart.ViewXY.YAxes.Count > firstIndex)
                    {
                        for (int i = firstIndex / 2; i < m_chart.ViewXY.AxisLayout.Segments.Count; i++)
                        {
                            m_chart.ViewXY.YAxes[i * 2].SegmentIndex = i;
                            m_chart.ViewXY.YAxes[i * 2 + 1].SegmentIndex = i;
                        }
                    }
                    AnnotationXY annotation = m_chart.ViewXY.Annotations[1];
                    var branches = annotation.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    branches.RemoveRange(firstIndex, 2);
                    StringBuilder sb = new StringBuilder();
                    foreach (var branch in branches)
                    {
                        sb.AppendLine(branch);
                    }
                    annotation.Text = sb.ToString();

                    if (m_chart.ViewXY.YAxes.Count != 0)
                    {
                        m_chart.ViewXY.Annotations[0].AssignYAxisIndex = -1;
                        m_chart.ViewXY.Annotations[0].AssignYAxisIndex = 0;
                    }
                    else
                    {
                        m_chart.ViewXY.Annotations.Clear();
                    }
                }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-频域-删除通道", ex));
            }
        }
        private async void OnChannelDataChanged(IEnumerable<VibrationChannelToken> tokens)
        {
            try
            {
                if (viewModel == null) return;

                await FFTAndPhase(tokens);
                UpdateChartAsync(tokens);

                //if (m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis <= m_chart.ViewXY.XAxes[0].Minimum || m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis >= m_chart.ViewXY.XAxes[0].Maximum)
                //{
                //    m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = (m_chart.ViewXY.XAxes[0].Minimum + m_chart.ViewXY.XAxes[0].Maximum) / 2.0;
                //}
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-频域-更新通道数据", ex));
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
            m_chart.Title.Text = "";
            m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Segmented;
            m_chart.ViewXY.AxisLayout.YAxisAutoPlacement = YAxisAutoPlacement.LeftThenRight;
           // m_chart.ViewXY.AxisLayout.AutoAdjustAxisGap = 0;        

            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.Number;
            m_chart.ViewXY.XAxes[0].Minimum = 0;
            m_chart.ViewXY.XAxes[0].Maximum = 1000;
            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].LabelsFont = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            m_chart.ViewXY.YAxes.Clear();
            m_chart.ViewXY.AxisLayout.Segments.Clear();

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

            int segmentCount = m_chart.ViewXY.AxisLayout.Segments.Count;
            for (int i = 0; i < segmentCount; i++)
            {
                var series = m_chart.ViewXY.PointLineSeries[i*2];
                var phaseSeries = m_chart.ViewXY.PointLineSeries[i * 2 + 1];
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

                if (phaseSeries.Visible)
                {
                    VibrationChannelToken phaseToken = phaseSeries.Tag as VibrationChannelToken;
                    if (phaseToken.VData != null)
                    {
                        bool bResolvedOK = false;
                        double yValue = 0;
                        bResolvedOK = SolveValueAccurate(phaseSeries, cursor.ValueAtXAxis, out yValue);
                        if (bResolvedOK)
                        {
                            bLabelVisible = true;
                            strValue += "|" + Math.Round(yValue, 2);
                        }
                        else
                        {
                            //strValue = string.Format(strChannelStringFormat, iSeriesNumber, "---", "Unit");
                            strValue += "|---";
                        }
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

        private void MovePrevious_Click(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis -= 1;
        }

        private void MoveNext_Click(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis += 1;
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
                var tokens = m_chart.ViewXY.PointLineSeries.Select(o => o.Tag).OfType<VibrationChannelToken>().Distinct();
                await FFTAndPhase(tokens);
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
        private async void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (viewModel == null) return;
                if(isRender)
                {
                    var tokens = m_chart.ViewXY.PointLineSeries.Select(o => o.Tag).OfType<VibrationChannelToken>().Distinct().ToArray();
                    await FFTAndPhase(tokens);
                    UpdateChartAsync(tokens);
                }    
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-频域-滤波", ex));
            }
        }

        private void UpdateChartAsync(IEnumerable<VibrationChannelToken> tokens)
        {
            m_chart.BeginUpdate();
            foreach (var token in tokens)
            {
                var series = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag == token).First();
                var phaseSeries = m_chart.ViewXY.PointLineSeries.Where(o => o.Tag == token).Last();
                int index = m_chart.ViewXY.PointLineSeries.IndexOf(series);

                AnnotationXY annotation = m_chart.ViewXY.Annotations[1];
                string[] branches = annotation.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (token.VData != null)
                {

                    int length = token.VData.FFTLength;
                    if (series.Points == null || series.Points.Length != length)
                    {
                        series.Points = new SeriesPoint[length];
                    }
                    if (phaseSeries.Points == null || phaseSeries.Points.Length != length)
                    {
                        phaseSeries.Points = new SeriesPoint[length];
                    }

                    for (int i = 0; i < length; i++)
                    {
                        series.Points[i].X = token.VData.Frequency[i];
                        series.Points[i].Y = token.VData.Amplitude[i];
                        phaseSeries.Points[i].X = token.VData.Frequency[i];
                        phaseSeries.Points[i].Y = token.VData.Phase[i];
                    }

                    string freText = "F";// string.Format("{0}-F", index / 2 + 1);
                    string ampText = "A";// string.Format("{0}-A", index / 2 + 1);

                    var fftValuesDict = token.VData.Amplitude.Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
                    foreach (var item in fftValuesDict)
                    {
                        freText += string.Format("{0,6}|", token.VData.Frequency[item.Key].ToString("0.00"));
                        ampText += string.Format("{0,6}|", item.Value.ToString("0.00"));
                    }
                    branches[index] = freText;
                    branches[index + 1] = ampText;
                }
                else
                {
                    series.Clear();
                    phaseSeries.Clear();
                    branches[index] = string.Format("{0}:{1,6}|{2,6}|", index / 2 + 1, " ", " ");
                    branches[index + 1] = string.Format("{0}:{1,6}|{2,6}|", index / 2 + 1, " ", " ");
                }
                series.InvalidateData();
                phaseSeries.InvalidateData();
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

        private async Task FFTAndPhase(IEnumerable<VibrationChannelToken> tokens)
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

            var output = Algorithm.Instance.FFT2AndPhaseAction(input, samplePoint);

            double frequencyInterval = sampleFre / samplePoint;
            int length = (int)(samplePoint / 2.56) + 1;
            if (token.VData.Frequency == null || token.VData.Frequency.Length!=length)
            {
                token.VData.Frequency = new double[length];
            }
            for (int i = 0; i < length; i++)
            {
                token.VData.Frequency[i] = frequencyInterval * i;
            }
            token.VData.Amplitude = output[0].Take(length).ToArray();
            token.VData.Phase = output[1].Take(length).ToArray();
        }
    }
}
