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
using AIC.Core.DataModels;
using Arction.Wpf.Charting.EventMarkers;
using AIC.Core;

namespace AIC.HistoryDataPage.Views
{
    /// <summary>
    /// Interaction logic for TimeDomainDataView.xaml
    /// </summary>
    public partial class DistributionMapView : DisposableUserControl
    {
        private LightningChartUltimate m_chart;
        private IDisposable channelDataChangedSubscription;
        private IDisposable channelAddedSubscription;
        private IDisposable channelRemovedSubscription;

        public DistributionMapView()
        {
            InitializeComponent();
            CreateChart();
            Loaded += DistributionMapView_Loaded;
        }

        private DistributionMapViewModel ViewModel
        {
            get { return DataContext as DistributionMapViewModel; }
            set { this.DataContext = value; }
        }

        protected void ViewModel_Closed(object sender, EventArgs e)
        {
            // Don't forget to clear chart grid child list.
            gridChart.Children.Clear();
            if (m_chart != null)
            {
                m_chart.Dispose();
                m_chart = null;
            }
            base.Dispose();
            base.GCCollect();
        }

        private void DistributionMapView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= DistributionMapView_Loaded;
            if (ViewModel != null)
            {
                channelAddedSubscription = ViewModel.WhenChannelAdded.Subscribe(OnChannelAdded);
                channelRemovedSubscription = ViewModel.WhenChannelRemoved.Subscribe(OnChannelRemoved);
                channelDataChangedSubscription = ViewModel.WhenChannelDataChanged.Subscribe(OnChannelDataChanged);
                ViewModel.Closed += ViewModel_Closed;
            }
        }

        private void OnChannelAdded(ChannelToken token)
        {
            try
            {
                var sameseries = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Tag == token).SingleOrDefault();
                if (sameseries != null)
                {
                    return;
                }

                m_chart.BeginUpdate();

                #region
                if (token is BaseAlarmChannelToken)
                {
                    var anToken = token as BaseAlarmChannelToken;
                    string unit = (anToken.DataContracts[0].Unit == null) ? "" : anToken.DataContracts[0].Unit;
                    FreeformPointLineSeries series = new FreeformPointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
                    //Make points visible
                    series.PointsVisible = true;
                    //Don't show line
                    series.LineVisible = false;  
                    //Set indidividual point color
                    series.IndividualPointColoring = PointColoringTarget.Color1;
                    //Set other point style options 
                    series.PointStyle.Shape = Arction.Wpf.Charting.Shape.Circle;
                    series.PointStyle.Height = series.PointStyle.Width = 1;
                    series.PointStyle.BorderColor = anToken.SolidColorBrush.Color;
                    series.PointStyle.Color1 = anToken.SolidColorBrush.Color; //color;
                    series.PointStyle.Color2 = anToken.SolidColorBrush.Color; //color;
                    series.PointStyle.Color3 = anToken.SolidColorBrush.Color; //color;
                    series.PointStyle.BorderWidth = 0f;
                    series.PointStyle.GradientFill = GradientFillPoint.Solid;
                    //Disable mouse interaction from series 
                    series.MouseInteraction = false;
                    series.Tag = token;

                    //series.LineStyle.Color = anToken.SolidColorBrush.Color; //color;
                    series.Title.Text = anToken.DisplayName;//htzk123
                    series.Title.Font = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 10f, System.Drawing.FontStyle.Bold);
                    series.Title.Color = ChartTools.CalcGradient(Colors.White, Colors.White, 50);

                    SeriesPoint[] points = new SeriesPoint[anToken.DataContracts.Count];
                    for (int i = 0; i < points.Length; i++)
                    {
                        if (anToken.DataContracts[i] is IBaseDivfreSlot)
                        {
                            points[i].X = (anToken.DataContracts[i] as IBaseDivfreSlot).RPM.Value;
                        }
                        else
                        {
                            points[i].X = 0;
                        }
                        points[i].Y = anToken.DataContracts[i].Result.Value;
                    }
                    series.Points = points;

                    SeriesEventMarker marker = new SeriesEventMarker(); // Create marker for point size change illusion.
                    marker.Symbol.Height = marker.Symbol.Width = 5;
                    marker.Symbol.BorderColor = anToken.SolidColorBrush.Color; //Colors.Orange;
                    marker.Symbol.BorderWidth = 0;
                    marker.Symbol.Shape = Arction.Wpf.Charting.Shape.Circle;
                    marker.Symbol.GradientFill = GradientFillPoint.Solid;
                    marker.Symbol.Color1 = marker.Symbol.Color2 = marker.Symbol.Color3 = anToken.SolidColorBrush.Color; //Colors.Transparent;
                    marker.MouseInteraction = false;
                    marker.Label.Visible = false;
                    marker.Visible = false;
                    series.SeriesEventMarkers.Add(marker);

                    m_chart.ViewXY.FreeformPointLineSeries.Add(series);

                    double minX = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Min();
                    double maxX = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Max();
                    double minY = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.Y).Min();
                    double maxY = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.Y).Max();
                    m_chart.ViewXY.XAxes[0].SetRange(minX - 20, maxX + 20);
                    m_chart.ViewXY.YAxes[0].SetRange(minY, maxY);
                }
                #endregion
                UpdateBox();
                UpdatePointColors();
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-分布图-添加通道", ex));
                m_chart.EndUpdate();
            }
        }
        private void OnChannelRemoved(ChannelToken token)
        {
            try
            {
                m_chart.BeginUpdate();
                var series = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Tag == token).SingleOrDefault();
                if (series != null)
                {
                    series.SeriesEventMarkers.Clear();
                    series.Clear();
                    m_chart.ViewXY.FreeformPointLineSeries.Remove(series);    
                }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-分布图-移除通道", ex));
                m_chart.EndUpdate();
            }
        }
        private void OnChannelDataChanged(IEnumerable<ChannelToken> tokens)
        {
            if (tokens == null || tokens.Count() == 0)
            {
                return;
            }
            foreach (var token in tokens)//修复隐藏时候没有添加成功
            {
                OnChannelAdded(token);
            }
            try
            {
                m_chart.BeginUpdate();
                foreach (var token in tokens)
                {
                    #region
                    if (token is BaseAlarmChannelToken)
                    {                       
                        FreeformPointLineSeries series = m_chart.ViewXY.FreeformPointLineSeries.Where(o => o.Tag == token).SingleOrDefault();

                        if (series == null)
                        {
                            continue;
                        }
                        var anToken = token as BaseAlarmChannelToken;

                        SeriesPoint[] points = new SeriesPoint[anToken.DataContracts.Count];
                        for (int i = 0; i < points.Length; i++)
                        {
                            if (anToken.DataContracts[i] is IBaseDivfreSlot)
                            {
                                points[i].X = (anToken.DataContracts[i] as IBaseDivfreSlot).RPM.Value;
                            }
                            else
                            {
                                points[i].X = 0;
                            }
                            points[i].Y = anToken.DataContracts[i].Result.Value;
                        }
                        series.Points = points;
                    }
                    #endregion
                }
                double minX = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Min();
                double maxX = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Max();
                double minY = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.Y).Min();
                double maxY = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.Y).Max();
                m_chart.ViewXY.XAxes[0].SetRange(minX - 20, maxX + 20);
                m_chart.ViewXY.YAxes[0].SetRange(minY, maxY);
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-分布图-数据更新", ex));
                m_chart.EndUpdate();
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

            //Create new chart 
            m_chart = new LightningChartUltimate();

            //Disable rendering, strongly recommended before updating chart properties
            m_chart.BeginUpdate();
            m_chart.Title.Visible = false;

            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            m_chart.ViewXY.YAxes[0].Title.Visible = false;

            // Configure x-axis.
            AxisX axisX = m_chart.ViewXY.XAxes[0];
            axisX.ScrollMode = XAxisScrollMode.None;
            axisX.ValueType = AxisValueType.Number;

            //Add annotation as selection box 
            AnnotationXY box = new AnnotationXY(m_chart.ViewXY, axisX, m_chart.ViewXY.YAxes[0]);
            box.Style = AnnotationStyle.Rectangle;
            box.RotateByMouse = false;
            box.AnchorAdjustByMouse = false;
            box.TargetMoveByMouse = false;
            box.Behind = false;
            box.Fill.Color = Color.FromArgb(50, 255, 165, 0);
            box.Fill.GradientColor = Color.FromArgb(50, 255, 140, 0);
            box.Shadow.Visible = false;
            box.TextStyle.Visible = false;
            box.Sizing = AnnotationXYSizing.AxisValuesBoundaries;
            box.ResizedByMouse += box_ResizedByMouse;
            box.MovedByMouse += box_MovedByMouse;
            box.AxisValuesBoundaries.SetValues(0, 100, 0, 100);
            box.NibStyle.Color = Colors.Yellow;
            m_chart.ViewXY.Annotations.Add(box);

            //Create annotation for showing the nearest point info
            AnnotationXY annot = new AnnotationXY(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            annot.Fill.Color = Color.FromArgb(180, 30, 30, 30);
            annot.Fill.GradientFill = GradientFill.Solid;
            annot.BorderLineStyle.Color = Color.FromArgb(50, 255, 255, 255);
            annot.Shadow.Visible = false;
            annot.TargetCoordinateSystem = AnnotationTargetCoordinates.AxisValues;
            annot.Visible = false; //Don't show before the data point has been found
            annot.MouseInteraction = false;
            annot.LocationCoordinateSystem = CoordinateSystem.RelativeCoordinatesToTarget;
            annot.LocationRelativeOffset.X = 0;
            annot.LocationRelativeOffset.Y = -40;
            annot.TextStyle.Color = Colors.White;
            annot.Style = AnnotationStyle.Rectangle;
            m_chart.ViewXY.Annotations.Add(annot);

            m_chart.ViewXY.LegendBoxes[0].Visible = true;
            m_chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.ViewXY.LegendBoxes[0].Fill.Style = RectFillStyle.None;
            m_chart.ViewXY.LegendBoxes[0].Shadow.Visible = false;
            m_chart.ViewXY.LegendBoxes[0].BorderWidth = 0;
            m_chart.ViewXY.LegendBoxes[0].Position = LegendBoxPositionXY.TopRight;
            m_chart.ViewXY.LegendBoxes[0].Offset.SetValues(-80, 10);
            m_chart.ViewXY.LegendBoxes[0].SeriesTitleFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            //Add a series
            //AddSeries();

            //UpdatePointColors();

            m_chart.ViewXY.ZoomToFit();

            m_chart.MouseMove += new MouseEventHandler(m_chart_MouseMove);

            //Allow chart rendering
            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);

            showCheckBox.Checked += showCheckBox_Checked;
            showCheckBox.Unchecked += showCheckBox_Checked;
        }


        private void m_chart_MouseMove(object sender, MouseEventArgs e)
        {
            //Find nearest point and show its values 
            double xValue, yValue;
            int nearestIndex;

            Point p = Mouse.GetPosition(m_chart);

            foreach (var series in m_chart.ViewXY.FreeformPointLineSeries)
            {
                if (series.SolveNearestDataPointByCoord((int)p.X, (int)p.Y, out xValue, out yValue, out nearestIndex))
                {
                    double xCoord = m_chart.ViewXY.XAxes[0].ValueToCoord(xValue);
                    double yCoord = m_chart.ViewXY.YAxes[0].ValueToCoord(yValue);
                    double dist  = Math.Sqrt((xCoord - p.X) * (xCoord - p.X) + (yCoord - p.Y) * (yCoord - p.Y));
                    if (dist < 20)
                    {
                        m_chart.BeginUpdate();

                        AnnotationXY annot = m_chart.ViewXY.Annotations[1];
                        annot.TargetAxisValues.X = xValue;
                        annot.TargetAxisValues.Y = yValue;
                        annot.Text = "X=" + xValue.ToString("f3") + "\n" + "Y=" + yValue.ToString("f3");
                        annot.Visible = true;

                        SeriesEventMarker marker = series.SeriesEventMarkers[0];
                        marker.XValue = xValue;
                        marker.YValue = yValue;
                        marker.Visible = true;

                        m_chart.EndUpdate();
                    }
                    else
                    {
                        series.SeriesEventMarkers[0].Visible = false;
                    }
                }
            }
        }
        private void box_MovedByMouse(object sender, MouseEventArgs e)
        {
            UpdatePointColors();
        }

        /// <summary>
        /// Selection box resized or moved
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void box_ResizedByMouse(object sender, AnnotationResizedByMouseXYEventArgs e)
        {
            UpdatePointColors();
        }

        #region 测试用
        /// <summary>
        /// Add one scatter series
        /// </summary>
        private void AddSeries()
        {
            //Add freeform point line series 
            FreeformPointLineSeries pls = new FreeformPointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);

            //Make points visible
            pls.PointsVisible = true;

            //Don't show line
            pls.LineVisible = false;

            //Set indidividual point color
            pls.IndividualPointColoring = PointColoringTarget.Color1;

            //Set other point style options 
            pls.PointStyle.Shape = Arction.Wpf.Charting.Shape.Circle;
            pls.PointStyle.Height = pls.PointStyle.Width = 15;
            pls.PointStyle.BorderColor = Colors.White;
            pls.PointStyle.BorderWidth = 1.5f;
            pls.PointStyle.GradientFill = GradientFillPoint.Solid;

            //Disable mouse interaction from series 
            pls.MouseInteraction = false;

            //Assign the data for the point line series 
            pls.Points = GenerateScatterClusters(10000, 0, 1000, 0, 1000);

            UpdatePointColors();

            //Add the created point line series into PointLineSeries list 
            m_chart.ViewXY.FreeformPointLineSeries.Add(pls);
        }

        private void UpdateBox()
        {
            AnnotationXY box = m_chart.ViewXY.Annotations[0];
            double minX = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Min();
            double maxX = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.X).Max();
            double minY = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.Y).Min();
            double maxY = m_chart.ViewXY.FreeformPointLineSeries.SelectMany(o => o.Points).Select(o => o.Y).Max();

            box.AxisValuesBoundaries.XMin = minX - 5;           
            box.AxisValuesBoundaries.XMax = maxX + 5;           
            box.AxisValuesBoundaries.YMin = minY;          
            box.AxisValuesBoundaries.YMax = maxY;           
        }
        /// <summary>
        /// Update point colors, apply the box limits 
        /// </summary>
        private void UpdatePointColors()
        {
            m_chart.BeginUpdate();

            if (m_chart.ViewXY.FreeformPointLineSeries.Count > 0)
            {
                string info = "选中比例：";
                foreach (var series in m_chart.ViewXY.FreeformPointLineSeries)
                {
                    SeriesPoint[] points = series.Points;
                    AnnotationXY box = m_chart.ViewXY.Annotations[0];
                    double boxMinX = box.AxisValuesBoundaries.XMin;
                    double boxMaxX = box.AxisValuesBoundaries.XMax;
                    double boxMinY = box.AxisValuesBoundaries.YMin;
                    double boxMaxY = box.AxisValuesBoundaries.YMax;
                    int pointCount = series.PointCount;
                    Color colorSelected = Color.FromArgb(200, 255, 69, 0);
                    Color colorNotSelected = Color.FromArgb(100, 100, 100, 100);
                    if (series.Tag is BaseAlarmChannelToken)
                    {
                        colorNotSelected = (series.Tag as BaseAlarmChannelToken).SolidColorBrush.Color;
                    }
                  
                    int selectedPointCount = 0;

                    double x, y;
                    for (int i = 0; i < pointCount; i++)
                    {
                        x = points[i].X;
                        y = points[i].Y;

                        if (x >= boxMinX && x <= boxMaxX && y >= boxMinY && y <= boxMaxY)
                        {
                            points[i].PointColor = colorSelected;
                            selectedPointCount++;
                        }
                        else
                        {
                            points[i].PointColor = colorNotSelected;
                        }
                    }
                    series.InvalidateData();

                    info += selectedPointCount.ToString("0") + "/" + pointCount.ToString("0") + " ";
                }
                txtInfo.Text = info;
            }

            m_chart.EndUpdate();
        }

        /// <summary>
        /// Create clustered scatter data
        /// </summary>
        /// <param name="pointCount">Point count to generate</param>
        /// <param name="seedMinX">X min of cluster center</param>
        /// <param name="seedMaxX">X max of cluster center</param>
        /// <param name="seedMinY">Y min of cluster center</param>
        /// <param name="seedMaxY">Y max of cluster center</param>
        /// <returns></returns>
        SeriesPoint[] GenerateScatterClusters(int pointCount, double seedMinX, double seedMaxX, double seedMinY, double seedMaxY)
        {
            Random rand = new Random();

            int pointsGenerated = 0;
            double maxRadius = (seedMaxX - seedMinX) / 10.0;

            SeriesPoint[] points = new SeriesPoint[pointCount];

            while (pointsGenerated < pointCount)
            {
                double centerX = seedMinX + rand.NextDouble() * (seedMaxX - seedMinX);
                double centerY = seedMinY + rand.NextDouble() * (seedMaxY - seedMinY);

                int pointsInCluster = rand.Next(200, 500);
                if (pointsGenerated + pointsInCluster > pointCount)
                    pointsInCluster = pointCount - pointsGenerated;

                for (int i = 0; i < pointsInCluster; i++)
                {
                    double angleRad = Math.PI * 2.0 * rand.NextDouble();
                    double radius = maxRadius * rand.NextDouble();
                    points[pointsGenerated + i].X = centerX + radius * Math.Cos(angleRad);
                    points[pointsGenerated + i].Y = centerY + radius * Math.Sin(angleRad);
                }
                pointsGenerated += pointsInCluster;
            }
            return points;
        }
        #endregion

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }

        private void showCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (showCheckBox.IsChecked == true)
            {
                m_chart.ViewXY.Annotations[0].Visible = true;
                m_chart.ViewXY.LegendBoxes[0].Visible = true;
                txtInfo.Visibility = Visibility.Visible;
            }
            else
            {
                m_chart.ViewXY.Annotations[0].Visible = false;
                m_chart.ViewXY.LegendBoxes[0].Visible = false;
                txtInfo.Visibility = Visibility.Collapsed;
            }
        }
    }
}
