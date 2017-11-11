
using AIC.HistoryDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.SeriesXY;
using Arction.Wpf.Charting.Views.ViewXY;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIC.HistoryDataPage.Views
{
    /// <summary>
    /// Interaction logic for AlarmPointTrendDataView.xaml
    /// </summary>
    public partial class AlarmPointTrendDataView : UserControl
    {
        private LightningChartUltimate m_chart;
        private AlarmPointTrendDataViewModel viewModel;
        private readonly IEventAggregator _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        private IDisposable snapshotDataChangedSubscription;
        private IDisposable showTimeOrFrequencySubscription;
        public AlarmPointTrendDataView()
        {
            InitializeComponent();
            CreateChart();
            showAMSCheckBox.IsChecked = true;
            Loaded += AlarmPointTrendDataView_Loaded;
        }
        private void AlarmPointTrendDataView_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = DataContext as AlarmPointTrendDataViewModel;
            if (viewModel != null)
            {
                snapshotDataChangedSubscription = viewModel.WhenSnapshotDataChanged.Subscribe(OnSnapshotDataChanged);
                showTimeOrFrequencySubscription = viewModel.WhenShowTimeOrFrequency.Subscribe(OnShowTimeOrFrequencyS);  
            }
        }
        private void OnSnapshotDataChanged(SnapshotAMSContract2 contract)
        {
            try
            {
                showAMSCheckBox.IsChecked = true;
                m_chart.BeginUpdate();
                if (contract != null)
                {
                    if (m_chart.ViewXY.PointLineSeries[1].PointCount > 0)
                    {
                        m_chart.ViewXY.PointLineSeries[1].Clear();
                    }

                    //htzk123,数据
                    //var snapshotItems = contract.Item.OrderBy(o => o.STIME).ToArray();
                    //int length = snapshotItems.Length;
                    //SeriesPoint[] points = new SeriesPoint[length];
                    //for (int i = 0; i < length; i++)
                    //{
                    //    points[i].X = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(snapshotItems[i].STIME);
                    //    points[i].Y = snapshotItems[i].AMS;
                    //    points[i].Tag = snapshotItems[i].id;
                    //}
                    //if (length > 0)
                    //{
                    //    if (m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis < points[0].X || m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis > points[length-1].X)
                    //        m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = points[length / 2].X;
                    //}
                    //
                    //m_chart.ViewXY.PointLineSeries[0].Points = points;
                    m_chart.ViewXY.ZoomToFit();
                }
                else
                {
                    viewModel.CurrentSnapshotContract = null;
                    if (m_chart.ViewXY.PointLineSeries[0].PointCount > 0)
                    {
                        m_chart.ViewXY.PointLineSeries[0].Clear();
                    }
                }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                //_eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-报警点趋势-趋势", ex));
                m_chart.EndUpdate();
            }
        }
        private void OnShowTimeOrFrequencyS(object args)
        {
            if ((bool)timeRadioBtn.IsChecked)
            {
                DrawTimeDomain(true);
            }
            else if ((bool)frequencyRadioBtn.IsChecked)
            {
                DrawFrequencyDomain(true);
            }
        }
        private void DrawTimeDomain(bool fitView)
        {
            try
            {
                if (viewModel == null || viewModel.CurrentSnapshotContract == null) return;

                PointLineSeries series = m_chart.ViewXY.PointLineSeries[0];
                PointLineSeries series1 = m_chart.ViewXY.PointLineSeries[1];
                LineSeriesCursor lineSeriesCursor = m_chart.ViewXY.LineSeriesCursors[0];
                int index = GetNearestPointIndex(series, lineSeriesCursor.ValueAtXAxis);
                if (index == -1)
                {
                    if (series1.PointCount>0)
                    {
                        m_chart.BeginUpdate();
                        series1.Clear();
                        m_chart.EndUpdate();
                    }
                    return;
                }

                //htzk123,数据
                //SeriesPoint point = series.Points[index];
                //Guid id = (Guid)point.Tag;
                //SnapshotItemContract2 itemContract2 = viewModel.CurrentSnapshotContract.Item.Where(o => o.id == id).SingleOrDefault();
                //if (itemContract2 == null) return;
                //ChannelDataContract channelData = itemContract2.Data;

                //if (channelData != null)
                //{
                //    int length = channelData.VData.Length / 4;
                //    SeriesPoint[] points = new SeriesPoint[length];
                //    for (int i = 0; i < length; i++)
                //    {
                //        points[i].X = i;
                //        points[i].Y = BitConverter.ToSingle(channelData.VData, i * 4);
                //    }

                //    m_chart.BeginUpdate();
                //    series1.Points = points;
                //    if (fitView)
                //    {
                //        m_chart.ViewXY.ZoomToFit();
                //    }
                //    m_chart.EndUpdate();
                //}
            }
            catch (Exception ex)
            {
                //_eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-报警点趋势-时域", ex));
            }
        }
        private void DrawFrequencyDomain(bool fitView)
        {
            try
            {
                if (viewModel == null || viewModel.CurrentSnapshotContract == null) return;

                PointLineSeries series = m_chart.ViewXY.PointLineSeries[0];
                PointLineSeries series1 = m_chart.ViewXY.PointLineSeries[1];
                LineSeriesCursor lineSeriesCursor = m_chart.ViewXY.LineSeriesCursors[0];
                int index = GetNearestPointIndex(series, lineSeriesCursor.ValueAtXAxis);
                if (index == -1)
                {
                    if (series1.PointCount > 0)
                    {
                        m_chart.BeginUpdate();
                        series1.Clear();
                        m_chart.EndUpdate();
                    } 
                    return;
                }
                else
                {
                    SeriesPoint point = series.Points[index];
                    Guid id = (Guid)point.Tag;

                    //htzk123,数据
                    //SnapshotItemContract2 itemContract2 = viewModel.CurrentSnapshotContract.Item.Where(o => o.id == id).SingleOrDefault();
                    //if (itemContract2 == null) return;
                    //ChannelDataContract channelData = itemContract2.Data;

                    //if (channelData != null)
                    //{
                    //    int length = channelData.VData.Length / 4;
                    //    double[] vdata = new double[length];
                    //    for (int i = 0; i < length; i++)
                    //    {
                    //        vdata[i] = BitConverter.ToSingle(channelData.VData, i * 4);
                    //    }

                    //    double frequencyInterval = channelData.SampleFre / channelData.SamplePoint;
                    //    if (channelData.TriggerN == 2)
                    //    {
                    //        frequencyInterval = ((int)channelData.TeethNumber / 2) / length;
                    //    }
                    //    int frequencyLength = (int)(channelData.SamplePoint / 2.56);

                    //    var outputAmp = Algorithm.Instance.FFT2Action(vdata, channelData.SamplePoint);

                    //    m_chart.BeginUpdate();
                    //    if (series1.Points==null||series1.Points.Length != frequencyLength)
                    //    {
                    //        series1.Points = new SeriesPoint[frequencyLength];
                    //    }
                    //    for (int i = 0; i < frequencyLength; i++)
                    //    {
                    //        series1.Points[i].X = frequencyInterval * i;
                    //        series1.Points[i].Y = outputAmp[i];
                    //    }
                    //    if (fitView)
                    //    {
                    //        m_chart.ViewXY.ZoomToFit();
                    //    }
                    //    m_chart.EndUpdate();
                    //}  
                }
            }
            catch (Exception ex)
            {
                //_eventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-报警点趋势-频域", ex));
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
            m_chart.ViewXY.AxisLayout.XAxisAutoPlacement = XAxisAutoPlacement.BottomThenTop;
            m_chart.ViewXY.AxisLayout.YAxisAutoPlacement = YAxisAutoPlacement.LeftThenRight;
            m_chart.ViewXY.AxisLayout.YAxisTitleAutoPlacement = false;
            m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Layered;

            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            m_chart.ViewXY.LegendBoxes[0].Visible = false;

            m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.DateTime;
            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);


            m_chart.ViewXY.YAxes[0].Title.Visible = false;
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].AxisThickness = 2;
            m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.YAxes[0].Title.Text = "";

            AxisX xAxis2 = new AxisX(m_chart.ViewXY);
            xAxis2.LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            xAxis2.Title.Text = "";
            xAxis2.AxisColor = DefaultColors.SeriesForBlackBackgroundWpf[1];
            xAxis2.MajorGrid.Visible = false;
            xAxis2.ValueType = AxisValueType.Number;
            m_chart.ViewXY.XAxes.Add(xAxis2);

            AxisY yAxis2 = new AxisY(m_chart.ViewXY);
            yAxis2.LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            yAxis2.Title.Text = "";
            yAxis2.AxisColor = DefaultColors.SeriesForBlackBackgroundWpf[1];
            yAxis2.MajorGrid.Visible = false;
            m_chart.ViewXY.YAxes.Add(yAxis2);

            PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            series.MouseInteraction = false;
            series.LineStyle.AntiAliasing = LineAntialias.None;
            series.LineStyle.Width = 1;
            m_chart.ViewXY.PointLineSeries.Add(series);

            PointLineSeries series2 = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[1], m_chart.ViewXY.YAxes[1]);
            series2.MouseInteraction = false;
            series2.LineStyle.Color = DefaultColors.SeriesForBlackBackgroundWpf[1];
            series2.LineStyle.AntiAliasing = LineAntialias.None;
            series2.LineStyle.Width = 1;
            series2.CursorTrackEnabled = false;
            m_chart.ViewXY.PointLineSeries.Add(series2);

            //Add cursor
            LineSeriesCursor cursor = new LineSeriesCursor(m_chart.ViewXY, m_chart.ViewXY.XAxes[0]);
            cursor.PositionChanged += AlarmPointTrendDataView_PositionChanged;
            //cursor.AssignXAxisIndex = 0;
            m_chart.ViewXY.LineSeriesCursors.Add(cursor);
            cursor.LineStyle.Color = System.Windows.Media.Color.FromArgb(150, 255, 0, 0);
            cursor.SnapToPoints = true;
            cursor.LineStyle.Width = 2;
            cursor.TrackPoint.Color1 = Colors.White;

            m_chart.ViewXY.ZoomToFit();
            m_chart.EndUpdate();
            gridChart.Children.Add(m_chart);
        }
        private void AlarmPointTrendDataView_PositionChanged(Object sender, PositionChangedEventArgs e)
        {
            if ((bool)timeRadioBtn.IsChecked)
            {
                DrawTimeDomain(false);
            }
            else if ((bool)frequencyRadioBtn.IsChecked)
            {
                DrawFrequencyDomain(false);
            }
        }
        private void showAMSCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.PointLineSeries[0].Visible = true;
            m_chart.ViewXY.LineSeriesCursors[0].Visible = true;
        }
        private void showAMSCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.PointLineSeries[0].Visible = false;
            m_chart.ViewXY.LineSeriesCursors[0].Visible = false;
        }
        private void timeRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            DrawTimeDomain(false);
        }
        private void frequencyRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            DrawFrequencyDomain(false);
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_chart.BeginUpdate();
            foreach(var series in m_chart.ViewXY.PointLineSeries)
            {
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
