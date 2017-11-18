using AIC.Core;
using AIC.Core.ControlModels;
using AIC.Core.Events;
using AIC.Core.Models;
using AIC.OnLineDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Annotations;
using Arction.Wpf.Charting.Axes;
using Arction.Wpf.Charting.SeriesXY;
using Arction.Wpf.Charting.Views.ViewXY;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using Wpf.CloseTabControl;

namespace AIC.OnLineDataPage.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class OnlineDataTrendView : UserControl, ICloseable
    {
        public OnlineDataTrendView()
        {
            InitializeComponent();

            this.Closer = new CloseableHeader((string)Application.Current.Resources["menuOnlineDataTrend"], true);

            viewModel = this.DataContext as OnlineDataTrendViewModel;

            CreateChart();
        }

        public CloseableHeader Closer { get; private set; }

        private LightningChartUltimate m_chart;
        OnlineDataTrendViewModel viewModel;

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
            m_chart.Title.Text = ""; ;
            m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Layered;
            m_chart.ViewXY.AxisLayout.YAxisAutoPlacement = YAxisAutoPlacement.LeftThenRight;

            // m_chart.ViewXY.AxisLayout.AutoAdjustAxisGap = 0;
            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.DateTime;
            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            //m_chart.ViewXY.XAxes[0].MinimumDateTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5));
            //m_chart.ViewXY.XAxes[0].MaximumDateTime = DateTime.Now;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            m_chart.ViewXY.YAxes.Clear();
            m_chart.ViewXY.AxisLayout.Segments.Clear();
            m_chart.ViewXY.AxisLayout.Segments.Add(new YAxisSegment(m_chart.ViewXY.AxisLayout) { Height = 0 });
            m_chart.ViewXY.AxisLayout.Segments.Add(new YAxisSegment(m_chart.ViewXY.AxisLayout) { Height = 0 });

            m_chart.ViewXY.LegendBoxes[0].Visible = true;
            m_chart.ViewXY.LegendBoxes[0].Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.ViewXY.LegendBoxes[0].Fill.Style = RectFillStyle.None;
            m_chart.ViewXY.LegendBoxes[0].Shadow.Visible = false;
            m_chart.ViewXY.LegendBoxes[0].BorderWidth = 0;
            m_chart.ViewXY.LegendBoxes[0].Position = LegendBoxPositionXY.TopRight;
            m_chart.ViewXY.LegendBoxes[0].Offset.SetValues(-80, 5);
            m_chart.ViewXY.LegendBoxes[0].SeriesTitleFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.LegendBoxes[0].SeriesTitleMouseMoveOverOn += LegendBox_SeriesTitleMouseMoveOverOn;

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

        private void LegendBox_SeriesTitleMouseMoveOverOn(Object sender, Arction.Wpf.Charting.Views.ViewXY.SeriesTitleMouseMovedEventArgs e)
        {
            if (e.Series is PointLineSeries)
            {
                //viewModel.AllCount = ((PointLineSeries)e.Series).PointCount;
            }
        }
        private void CreateAnnotation()
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

        private void cursor_PositionChanged(Object sender, PositionChangedEventArgs e)
        {
            e.CancelRendering = true;
            //UpdateCursorResult(e.Cursor.ValueAtXAxis);
        }

        //private void UpdateCursorResult(double xValue)
        //{
        //    try
        //    {
        //        m_chart.BeginUpdate();
        //        List<BaseWaveChannelToken> channelList = new List<BaseWaveChannelToken>();
        //        AnnotationXY cursorValueDisplay = m_chart.ViewXY.Annotations[0];
        //        float fTargetYCoord = m_chart.ViewXY.GetMarginsRect().Bottom;
        //        double dY;
        //        m_chart.ViewXY.YAxes[0].CoordToValue(fTargetYCoord, out dY);
        //        cursorValueDisplay.TargetAxisValues.X = xValue;
        //        cursorValueDisplay.TargetAxisValues.Y = dY;

        //        StringBuilder sb = new StringBuilder();
        //        int iSeriesNumber = 1;

        //        string strChannelStringFormat = "{0}: {1}({2})";
        //        string strValue = "";
        //        bool bLabelVisible = false;

        //        foreach (PointLineSeries series in m_chart.ViewXY.PointLineSeries)
        //        {
        //            strValue = "";
        //            int index = GetNearestIndex(series, xValue);
        //            bLabelVisible = true;
        //            if (series.Tag is BaseDivfreChannelToken)
        //            {
        //                BaseDivfreChannelToken token = series.Tag as BaseDivfreChannelToken;
        //                token.CurrentIndex = index;
        //                channelList.Add(token);
        //                if (index != -1)
        //                {
        //                    var contract = token.DataContracts[index];  //. series.Points[index].Tag as VInfoTableAMSContract;
        //                    string unit = contract.Unit;
        //                    strValue = string.Format("{0}: {1}({2})|{3}", token.DisplayName, Math.Round(contract.Result ?? 0.0, 3), unit, Math.Round(contract.RPM ?? 0.0, 3));
        //                }
        //                else
        //                {
        //                    strValue = string.Format(strChannelStringFormat, token.DisplayName, "---", "Unit");
        //                }
        //            }
        //            else if (series.Tag is BaseWaveChannelToken)
        //            {
        //                BaseWaveChannelToken token = series.Tag as BaseWaveChannelToken;
        //                token.CurrentIndex = index;
        //                channelList.Add(token);
        //                if (index != -1)
        //                {
        //                    var contract = token.DataContracts[index];  //. series.Points[index].Tag as VInfoTableAMSContract;
        //                    string unit = contract.Unit;
        //                    strValue = string.Format("{0}: {1}({2})", token.DisplayName, Math.Round(contract.Result ?? 0.0, 3), unit);
        //                }
        //                else
        //                {
        //                    strValue = string.Format(strChannelStringFormat, token.DisplayName, "---", "Unit");
        //                }
        //            }
        //            else if (series.Tag is BaseAlarmChannelToken)
        //            {
        //                BaseAlarmChannelToken token = series.Tag as BaseAlarmChannelToken;
        //                if (index != -1)
        //                {
        //                    var contract = token.DataContracts[index]; //series.Points[index].Tag as AnInfoTableAMSContract;
        //                    string unit = contract.Unit;
        //                    strValue = string.Format(strChannelStringFormat, token.DisplayName, Math.Round(contract.Result ?? 0.0, 3), unit);
        //                }
        //                else
        //                {
        //                    strValue = string.Format(strChannelStringFormat, token.DisplayName, "---", "Unit");
        //                }
        //            }
        //            else if (series.Tag is DivFreChannelToken)
        //            {
        //                DivFreChannelToken token = series.Tag as DivFreChannelToken;
        //                if (index != -1)
        //                {
        //                    var contract = token.DataContracts[index];
        //                    string unit = (from p in token.SlotDataContracts where p.RecordLab == contract.RecordLab select p.Unit).FirstOrDefault();

        //                    strValue = string.Format(strChannelStringFormat, token.DataContracts[index].DescriptionFre, Math.Round(contract.Result ?? 0.0, 3), unit);
        //                }
        //                else
        //                {
        //                    strValue = string.Format(strChannelStringFormat, token.DataContracts[index].DescriptionFre, "---", "Unit");
        //                }
        //            }

        //            sb.AppendLine(strValue);
        //            // series.Title.Text = strValue;
        //            iSeriesNumber++;
        //        }
        //        sb.AppendLine("Time: " + m_chart.ViewXY.XAxes[0].TimeString(xValue, "yyyy/MM/dd HH:mm:ss"));
        //        ////Set text
        //        cursorValueDisplay.Text = sb.ToString().Trim();
        //        cursorValueDisplay.Visible = bLabelVisible;
        //        //Allow chart rendering
        //        m_chart.EndUpdate();

        //        if (channelList.Count > 0)
        //        {
        //            viewModel.RaiseTrackChanged(channelList);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-趋势趋势-Track", ex));
        //        m_chart.EndUpdate();
        //    }
        //}

        private bool SolveValueAccurate(PointLineSeries series, double xValue, out double yValue)
        {
            AxisY axisY = m_chart.ViewXY.YAxes[series.AssignYAxisIndex];
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
        private int GetNearestIndex(PointLineSeries series, double xValue)
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



    }
}