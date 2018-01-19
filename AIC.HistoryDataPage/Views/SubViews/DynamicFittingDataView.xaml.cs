using AIC.Core.Events;
using AIC.Core.SignalModels;
using AIC.HistoryDataPage.Models;
using AIC.HistoryDataPage.ViewModels;
using AIC.ServiceInterface;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.EventMarkers;
using Arction.Wpf.Charting.SeriesXY;
using MathNet.Numerics.Interpolation;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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

namespace AIC.HistoryDataPage.Views
{
    /// <summary>
    /// Interaction logic for DynamicFittingCurveView.xaml
    /// </summary>
    public partial class DynamicFittingDataView : UserControl
    {        
        private LightningChartUltimate m_chart;

        private IDisposable dataChangedSubscription;
        private IDisposable disposedSubscription;

        public DynamicFittingDataView()
        {
            InitializeComponent();
            CreateChart();
            Loaded += DynamicFittingDataView_Loaded;
        }

        private ChannelToken ViewModel { get; set; }

        private void DynamicFittingDataView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= DynamicFittingDataView_Loaded;
            ViewModel = DataContext as ChannelToken;

            if (ViewModel != null)
            {
                DrawingChart();
                if (ViewModel is BaseWaveChannelToken)
                {
                    dataChangedSubscription = ((BaseWaveChannelToken)ViewModel).WhenDataChanged.Subscribe(OnDataChanged);
                    disposedSubscription = ((BaseWaveChannelToken)ViewModel).WhenDisposed.Subscribe(OnDisposed);
                }
                else if (ViewModel is DivFreChannelToken)
                {
                    dataChangedSubscription = ((DivFreChannelToken)ViewModel).WhenDataChanged.Subscribe(OnDataChanged);
                    disposedSubscription = ((DivFreChannelToken)ViewModel).WhenDisposed.Subscribe(OnDisposed);
                }
            }
        }

        private void OnDisposed(object args)
        {
            gridChart.Children.Clear();
            if (m_chart != null)
            {
                m_chart.Dispose();
                m_chart = null;
            }
            dataChangedSubscription.Dispose();
            disposedSubscription.Dispose();
        }
        private void OnDataChanged(object args)
        {
            m_chart.BeginUpdate();
            FreeformPointLineSeries ps = m_chart.ViewXY.FreeformPointLineSeries[0];
            if (ViewModel is BaseDivfreChannelToken)
            {
                var vChannel = ViewModel as BaseDivfreChannelToken;
                if (vChannel.DataContracts != null)
                {
                    SeriesPoint[] points = new SeriesPoint[vChannel.DataContracts.Count];
                    for (int i = 0; i < vChannel.DataContracts.Count; i++)
                    {                       
                        points[i].X = vChannel.DataContracts[i].RPM ?? 0;
                        points[i].Y = vChannel.DataContracts[i].Result?? 0;
                    }
                    ps.Points = points;
                }
            }
            else if (ViewModel is DivFreChannelToken)
            {
                var divChannel = ViewModel as DivFreChannelToken;
                if (divChannel.DataContracts != null)
                {
                    SeriesPoint[] points = new SeriesPoint[divChannel.DataContracts.Count];
                    for (int i = 0; i < divChannel.DataContracts.Count; i++)
                    {  
                        var rpm = (from p in divChannel.SlotDataContracts where p.RecordLab == divChannel.DataContracts[i].RecordLab select p.RPM).FirstOrDefault();
                        if (rpm == null)
                        {
                            continue;
                        }
                        points[i].X = rpm??0;
                        points[i].Y = divChannel.DataContracts[i].Result??0;
                    }
                    ps.Points = points;
                }
            }
            m_chart.ViewXY.ZoomToFit();
            m_chart.EndUpdate();
        }
        private void DrawingChart()
        {
            m_chart.BeginUpdate();
            FreeformPointLineSeries ps = m_chart.ViewXY.FreeformPointLineSeries[0];
            if (ViewModel is BaseDivfreChannelToken)
            {
                var vChannel = ViewModel as BaseDivfreChannelToken;
                if (vChannel.DataContracts != null)
                {
                    SeriesPoint[] points = new SeriesPoint[vChannel.DataContracts.Count];
                    for (int i = 0; i < vChannel.DataContracts.Count; i++)
                    {                        
                        points[i].X = vChannel.DataContracts[i].RPM ?? 0;
                        points[i].Y = vChannel.DataContracts[i].Result?? 0;
                    }
                    ps.Points = points;
                }
            }
            else if (ViewModel is DivFreChannelToken)
            {
                var divChannel = ViewModel as DivFreChannelToken;
                if (divChannel.DataContracts != null)
                {
                    SeriesPoint[] points = new SeriesPoint[divChannel.DataContracts.Count];
                    for (int i = 0; i < divChannel.DataContracts.Count; i++)
                    {
                        var rpm = (from p in divChannel.SlotDataContracts where p.RecordLab == divChannel.DataContracts[i].RecordLab select p.RPM).FirstOrDefault();
                        if (rpm == null)
                        {
                            continue;
                        }
                        points[i].X = rpm ?? 0;
                        points[i].Y = divChannel.DataContracts[i].Result??0;
                    }
                    ps.Points = points;
                }
            }

            if (m_chart.ViewXY.FreeformPointLineSeries.Count == 1)
            {
                FreeformPointLineSeries fpls = m_chart.ViewXY.FreeformPointLineSeries[0];
                double minY = fpls.Points.Take(fpls.PointCount).Select(o => o.Y).Min();
                double maxY = fpls.Points.Take(fpls.PointCount).Select(o => o.Y).Max();
                double minX = fpls.Points.Take(fpls.PointCount).Select(o => o.X).Min();
                double maxX = fpls.Points.Take(fpls.PointCount).Select(o => o.X).Max();
                double middleY = minY + (maxY - minY) / 2;

                string[] seriesTitles = new string[] { "高危", "高警", "正常(高)", "正常(低)", "低警", "低危" };
                foreach (var title in seriesTitles)
                {
                    FreeformPointLineSeries alarmFpls = new FreeformPointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
                    alarmFpls.Title.Text = title;
                    // alarmFpls.PointStyle.Shape = Arction.WPF.LightningChartUltimate.Shape.Rectangle;
                    alarmFpls.PointStyle.BorderWidth = 0;
                    alarmFpls.PointStyle.GradientFill = GradientFillPoint.Edge;
                    alarmFpls.PointStyle.Color1 = Color.FromArgb(200, 0, 0, 255);
                    alarmFpls.PointStyle.Color2 = Color.FromArgb(100, 0, 0, 0);
                    alarmFpls.MouseDoubleClick += FplsMouseDoubleClick;
                    m_chart.ViewXY.FreeformPointLineSeries.Add(alarmFpls);

                    double y = 0;
                    if (title == "高危")
                    {
                        alarmFpls.LineStyle.Color = Colors.Red;
                        y = maxY;
                    }
                    else if (title == "高警")
                    {
                        alarmFpls.LineStyle.Color = Colors.Yellow;
                        y = middleY;
                    }
                    else if (title == "正常(高)")
                    {
                        alarmFpls.LineStyle.Color = Colors.Green;
                        y = minY;
                    }
                    else if (title == "正常(低)")
                    {
                        alarmFpls.Visible = false;
                        alarmFpls.LineStyle.Color = Colors.Green;
                        y = -minY;
                    }
                    else if (title == "低警")
                    {
                        alarmFpls.Visible = false;
                        alarmFpls.LineStyle.Color = Colors.Yellow;
                        y = -middleY;
                    }
                    else if (title == "低危")
                    {
                        alarmFpls.Visible = false;
                        alarmFpls.LineStyle.Color = Colors.Red;
                        y = -maxY;
                    }

                    double step = (maxX - minX) / 99;
                    SeriesPoint[] seriesPoints = new SeriesPoint[100];
                    for (int i = 0; i < 100; i++)
                    {
                        seriesPoints[i].X = minX + step * i;
                        seriesPoints[i].Y = y;
                    }
                    alarmFpls.Points = seriesPoints;


                    for (int i = 0; i < 3; i++)
                    {
                        SeriesEventMarker marker = new SeriesEventMarker(alarmFpls);
                        marker.Label.Text = (i + 1).ToString();
                        marker.Label.HorizontalAlign = AlignmentHorizontal.Center;
                        marker.Label.Shadow.Style = TextShadowStyle.Off;
                        marker.Label.Shadow.ContrastColor = Color.FromArgb(150, 0, 0, 0);

                        marker.Symbol.BorderWidth = 0;

                        marker.Symbol.GradientFill = GradientFillPoint.Edge;
                        marker.Symbol.Color1 = Color.FromArgb(200, 0, 0, 255);
                        marker.Symbol.Color2 = Color.FromArgb(100, 0, 0, 0);

                        if (i == 0)
                        {
                            marker.XValue = seriesPoints[0].X;
                            marker.YValue = seriesPoints[0].Y;
                        }
                        else if (i == 1)
                        {
                            marker.XValue = seriesPoints[50].X;
                            marker.YValue = seriesPoints[50].Y;
                        }
                        else if (i == 2)
                        {
                            marker.XValue = seriesPoints[99].X;
                            marker.YValue = seriesPoints[99].Y;
                        }

                        marker.VerticalPosition = SeriesEventMarkerVerticalPosition.AtYValue;
                        marker.PositionChanged += marker_PositionChanged;
                        marker.MouseUp += marker_MouseUp;
                        alarmFpls.SeriesEventMarkers.Add(marker);
                    }
                }
            }
            m_chart.ViewXY.ZoomToFit();
            m_chart.EndUpdate();
        }
        private void CreateChart()
        {
            // Clear any gridChart's children.
            gridChart.Children.Clear();

            if (m_chart != null)
            {
                // If a chart is already created, dispose it.
                m_chart.Dispose();
                m_chart = null;
            }

            // Create a new chart.
            m_chart = new LightningChartUltimate();
            // m_chart.ChartName = "Scatter chart";

            //Disable rendering, strongly recommended before updating chart properties
            m_chart.BeginUpdate();

            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            //Remove existing PointLineSeries
            m_chart.ViewXY.PointLineSeries.Clear();

            //Set antialiasing level
            m_chart.ChartRenderOptions.AntiAliasLevel = 4;

            //Update title text
            m_chart.Title.Text = "";

            m_chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;
            //Setup x-axis
            AxisX axisX = m_chart.ViewXY.XAxes[0];
            axisX.AxisThickness = 2;
            axisX.AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            axisX.ValueType = AxisValueType.Number;
            axisX.SweepingGap = 0;
            axisX.ScrollMode = XAxisScrollMode.None;
            axisX.MouseScaling = true;
            axisX.MouseScrolling = true;
            axisX.MajorDivTickStyle.Visible = true;
            axisX.MinorDivTickStyle.Visible = true;
            axisX.Title.Visible = false;

            m_chart.ViewXY.YAxes[0].Title.Visible = false;
            m_chart.ViewXY.YAxes[0].AxisThickness = 2;
            m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);

            //Create some series
            FreeformPointLineSeries flsRectangles = new FreeformPointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            flsRectangles.Title.Visible = false;
            flsRectangles.Title.Color = flsRectangles.LineStyle.Color;
            flsRectangles.Title.Text = "元数据";
            flsRectangles.LineVisible = false;
            flsRectangles.PointsVisible = true;
            flsRectangles.PointStyle.Shape = Arction.Wpf.Charting.Shape.Rectangle;
            flsRectangles.PointStyle.Color1 = Color.FromArgb(200, 255, 0, 0);
            flsRectangles.PointStyle.BorderWidth = 0;
            flsRectangles.PointStyle.GradientFill = GradientFillPoint.Edge;
            flsRectangles.PointStyle.Color2 = Color.FromArgb(100, 0, 0, 0);

            m_chart.ViewXY.FreeformPointLineSeries.Add(flsRectangles);
            flsRectangles.MouseInteraction = false;

            m_chart.ViewXY.LegendBoxes[0].Visible = true;
            m_chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.ViewXY.LegendBoxes[0].Fill.Style = RectFillStyle.None;
            m_chart.ViewXY.LegendBoxes[0].Shadow.Visible = false;
            m_chart.ViewXY.LegendBoxes[0].BorderWidth = 0;
            m_chart.ViewXY.LegendBoxes[0].Position = LegendBoxPositionXY.TopRight;
            m_chart.ViewXY.LegendBoxes[0].Offset.SetValues(0, 15);
            m_chart.ViewXY.LegendBoxes[0].SeriesTitleFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);
        }
        private void FplsMouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                MouseButtonEventArgs arg = e as MouseButtonEventArgs;
                if (arg.ChangedButton == MouseButton.Left)
                {
                    m_chart.BeginUpdate();
                    FreeformPointLineSeries fpls = sender as FreeformPointLineSeries;
                    double dXValue, dYValue;
                    double dXValue1, dYValue1;
                    int iNearestIndex;
                    Point p = e.GetPosition(m_chart);
                    fpls.SolveNearestDataPointByCoord((Int32)(p.X), (Int32)(p.Y), out dXValue1, out dYValue1, out iNearestIndex);
                    m_chart.ViewXY.XAxes[0].CoordToValue((Int32)(p.X), out dXValue, false);
                    m_chart.ViewXY.YAxes[0].CoordToValue((Int32)(p.Y), out dYValue);
                    p.X = dXValue;
                    p.Y = dYValue;
                    List<SeriesPoint> points = new List<SeriesPoint>();
                    points.AddRange(fpls.Points);
                    SeriesPoint point = new SeriesPoint(p.X, p.Y);

                    int index = fpls.SeriesEventMarkers.IndexOf(fpls.SeriesEventMarkers.Where(o => o.XValue >= p.X).First());
                    for (int i = index; i < fpls.SeriesEventMarkers.Count; i++)
                    {
                        fpls.SeriesEventMarkers[i].Label.Text = (i + 2).ToString();
                    }

                    SeriesEventMarker marker = new SeriesEventMarker(fpls);
                    marker.Label.Text = (index + 1).ToString();
                    marker.Label.HorizontalAlign = AlignmentHorizontal.Center;
                    marker.Label.Shadow.Style = TextShadowStyle.Off;
                    marker.Label.Shadow.ContrastColor = Color.FromArgb(150, 0, 0, 0);
                    marker.XValue = p.X;
                    marker.YValue = p.Y;
                    marker.Symbol.BorderWidth = 0;
                    marker.Symbol.GradientFill = GradientFillPoint.Edge;
                    marker.Symbol.Color1 = Color.FromArgb(200, 0, 0, 255);
                    marker.Symbol.Color2 = Color.FromArgb(100, 0, 0, 0);
                    marker.VerticalPosition = SeriesEventMarkerVerticalPosition.AtYValue;
                    marker.PositionChanged += marker_PositionChanged;
                    marker.MouseUp += marker_MouseUp;
                    fpls.SeriesEventMarkers.Insert(index, marker);

                    m_chart.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-动态拟合-添加点", ex));
            }
        }
        private void marker_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                MouseButtonEventArgs arg = e as MouseButtonEventArgs;
                if (arg.ChangedButton == MouseButton.Right)
                {
                    SeriesEventMarker marker = sender as SeriesEventMarker;
                    marker.MouseUp -= marker_MouseUp;
                    FreeformPointLineSeries fpls = marker.GetOwnerSeries() as FreeformPointLineSeries;
                    int index = fpls.SeriesEventMarkers.IndexOf(marker);
                    if (index == 0 || index == fpls.SeriesEventMarkers.Count - 1) return;

                    m_chart.BeginUpdate();
                    fpls.SeriesEventMarkers.Remove(marker);
                    FitSpline(fpls);
                    m_chart.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-动态拟合-移除点", ex));
            }
        }
        private void marker_PositionChanged(Object sender, PositionChangedSeriesEventMarkerEventArgs e)
        {
            try
            {
                //Set new value to point having same index than the marker
                FreeformPointLineSeries fpls = ((SeriesEventMarker)sender).GetOwnerSeries() as FreeformPointLineSeries;
                int iPointIndex = fpls.SeriesEventMarkers.IndexOf((SeriesEventMarker)sender);
                if (iPointIndex >= 0)
                {
                    e.CancelRendering = true;

                    FitSpline(fpls);
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-动态拟合-移动点", ex));
            }
        }

        private void FitSpline(FreeformPointLineSeries fpls)
        {
            m_chart.BeginUpdate();

            int iMarkerCount = fpls.SeriesEventMarkers.Count;

            double[] aMarkerValuesX = new double[iMarkerCount];
            double[] aMarkerValuesY = new double[iMarkerCount];

            for (int i = 0; i < iMarkerCount; i++)
            {
                SeriesEventMarker marker = fpls.SeriesEventMarkers[i];
                aMarkerValuesX[i] = marker.XValue;
                aMarkerValuesY[i] = marker.YValue;
            }

            //One solved point for each pixel in X-dimension

            double[] aXValues = new double[100];
            double dXMin = aMarkerValuesX[0];
            double dXMax = aMarkerValuesX[iMarkerCount - 1];
            double dXStep = (dXMax - dXMin) / (double)(100 - 1);

            for (int i = 0; i < 100; i++)
            {
                aXValues[i] = dXMin + dXStep * (double)i;
            }
            int iOrder = iMarkerCount - 1;
            //double[] aYValues = MathRoutines.PolynomialRegression(aMarkerValuesX, aMarkerValuesY, aXValues, iOrder);


            double[] aYValues = new double[100];
            CubicSpline naturalSpline = CubicSpline.InterpolateNatural(aMarkerValuesX, aMarkerValuesY);
            for (int i = 0; i < 100; i++)
            {
                aYValues[i] = naturalSpline.Interpolate(aXValues[i]);
            }
            if (aYValues != null)
            {
                fpls.Clear();
                fpls.AddPoints(aXValues, aYValues, false);
            }
            m_chart.EndUpdate();
        }


        private async void MediatorButton_Click(object sender, RoutedEventArgs e)
        {
            await MediatorSamplePoints();
        }
        private async Task MediatorSamplePoints()
        {
            try
            {
                if (ViewModel is BaseWaveChannelToken)
                {
                    var vChannelToken = ViewModel as BaseWaveChannelToken;

                    var _organizationService = ServiceLocator.Current.GetInstance<IOrganizationService>();
                    if (_organizationService != null)
                    {
                        var vInfo = _organizationService.ItemTreeItems.Where(p => p.T_Item.Guid == vChannelToken.Guid).Select(p => p.BaseAlarmSignal as BaseAlarmSignal).SingleOrDefault();
                        if (vInfo != null)
                        {
                            // "高危", "高警", "正常(高)", "正常(低)", "低警", "低危"
                            FreeformPointLineSeries fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "高危").Single();
                            vInfo.FormulaHighDanger = string.Empty;
                            vInfo.FormulaHighDanger += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            vInfo.FormulaHighDanger += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "高警").Single();
                            vInfo.FormulaHighAlert = string.Empty;
                            vInfo.FormulaHighAlert += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            vInfo.FormulaHighAlert += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "正常(高)").Single();
                            vInfo.FormulaHighNormal = string.Empty;
                            vInfo.FormulaHighNormal += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            vInfo.FormulaHighNormal += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "正常(低)").Single();
                            vInfo.FormulaLowNormal = string.Empty;
                            vInfo.FormulaLowNormal += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            vInfo.FormulaLowNormal += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "低警").Single();
                            vInfo.FormulaLowAlert = string.Empty;
                            vInfo.FormulaLowAlert += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            vInfo.FormulaLowAlert += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "低危").Single();
                            vInfo.FormulaLowDanger = string.Empty;
                            vInfo.FormulaLowDanger += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            vInfo.FormulaLowDanger += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            //await Task.Run(() => WCFCaller<IStorageManagement>.ExecuteMethod(ServerAddress.CTLAddress, "LMVInfoTable_Update", vInfo.Contract)); //htzk123方程式报警数据提交
                        }
                    }
                }
                else if (ViewModel is DivFreChannelToken)
                {
                    var divChannelToken = ViewModel as DivFreChannelToken;
                    //htzk123
                    var _organizationService = ServiceLocator.Current.GetInstance<IOrganizationService>();
                    if (_organizationService != null)
                    {
                        var divFre = _organizationService.DivFreTreeItems.Where(p => p.T_DivFreInfo.Guid == divChannelToken.DivFreChannel.Guid).Select(p => p.DivFreSignal).SingleOrDefault();

                        if (divFre != null)
                        {
                            FreeformPointLineSeries fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "高危").Single();
                            divFre.FormulaHighDanger = string.Empty;
                            divFre.FormulaHighDanger += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            divFre.FormulaHighDanger += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "高警").Single();
                            divFre.FormulaHighAlert = string.Empty;
                            divFre.FormulaHighAlert += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            divFre.FormulaHighAlert += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "正常(高)").Single();
                            divFre.FormulaHighNormal = string.Empty;
                            divFre.FormulaHighNormal += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            divFre.FormulaHighNormal += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "正常(低)").Single();
                            divFre.FormulaLowNormal = string.Empty;
                            divFre.FormulaLowNormal += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            divFre.FormulaLowNormal += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "低警").Single();
                            divFre.FormulaLowAlert = string.Empty;
                            divFre.FormulaLowAlert += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            divFre.FormulaLowAlert += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            fls = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Title.Text == "低危").Single();
                            divFre.FormulaLowDanger = string.Empty;
                            divFre.FormulaLowDanger += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.XValue)).Replace("-", null)));
                            divFre.FormulaLowDanger += string.Concat(fls.SeriesEventMarkers.Select(o => BitConverter.ToString(BitConverter.GetBytes(o.YValue)).Replace("-", null)));

                            //await Task.Run(() => WCFCaller<IStorageManagement>.ExecuteMethod(ServerAddress.CTLAddress, "LMDivFreTable_Update", divFre.Contract)); //htzk123方程式报警数据提交
                        }

                    }
                }

                EventAggregatorService.Instance.EventAggregator.GetEvent<FittingCoeffEvent>().Publish(null);
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-传递方程式", ex));
            }
        }
        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
