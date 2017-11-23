using AIC.Core.Events;
using AIC.Core.Models;
using AIC.HomePage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Annotations;
using Arction.Wpf.Charting.SeriesXY;
using Arction.Wpf.Charting.Views.ViewXY;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Deployment.Application;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIC.HomePage.Views
{
    /// <summary>
    /// Interaction logic for LoginWin.xaml
    /// </summary>
    public partial class ShowAlarmWin : MetroWindow
    {
        private LightningChartUltimate m_chart;
        public ShowAlarmWin()
        {
            InitializeComponent();
            CreateChart();
            Loaded += HistoryAlarmView_Loaded;
        }

        private void HistoryAlarmView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = DataContext as ShowAlarmWinViewModel;
        }

        private ShowAlarmWinViewModel ViewModel { get; set; }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel != null && ViewModel.SelectedVibrationHistory != null)
            {
                UpdateChart();
            }
        }

        private void UpdateChart()
        {
            try
            {
                m_chart.BeginUpdate();
                m_chart.ViewXY.PointLineSeries[0].Clear();
                //htzk123
                //if (ViewModel.SelectedVibrationHistory.HistoryDatas != null)
                //{
                //    var datas = ViewModel.SelectedVibrationHistory.HistoryDatas.ToArray();
                //    SeriesPoint[] points = new SeriesPoint[datas.Length];                 
                //    for (int i = 0; i < datas.Length; i++)
                //    {
                //        double x = m_chart.ViewXY.XAxes[0].DateTimeToAxisValue(datas[i].Date);
                //        points[i].X = x;
                //        points[i].Y = datas[i].Value;
                //        points[i].Tag = datas[i].id;                       
                //    }
                //    if (datas.Length > 0)
                //    {
                //        m_chart.ViewXY.LineSeriesCursors[0].ValueAtXAxis = points[datas.Length / 2].X;
                //    }
                //    m_chart.ViewXY.PointLineSeries[0].Points = points;

                //    m_chart.ViewXY.XAxes[0].SetRange(points.First().X - 5, points.Last().X + 5);
                //    m_chart.ViewXY.YAxes[0].SetRange(points.Select(o => o.Y).Min() - 5, points.Select(o => o.Y).Max() + 5);
                //}
                
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("历史报警-更新图形", ex));
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

            m_chart = new LightningChartUltimate();          
            m_chart.Loaded += m_chart_Loaded;
            m_chart.Unloaded += m_chart_Unloaded;
            m_chart.BeginUpdate();
            m_chart.Title.Text = "";

            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            //m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Stacked;
            m_chart.ViewXY.LegendBoxes[0].Visible = false;

            //Disable automatic axis layouts 
            m_chart.ViewXY.AxisLayout.AutoAdjustMargins = false;
            m_chart.ViewXY.AxisLayout.XAxisAutoPlacement = XAxisAutoPlacement.Off;
            m_chart.ViewXY.AxisLayout.YAxisAutoPlacement = YAxisAutoPlacement.Off;
            m_chart.ViewXY.AxisLayout.XAxisTitleAutoPlacement = false;
            m_chart.ViewXY.AxisLayout.YAxisTitleAutoPlacement = false;

            //Almost zero margins, bottom is 3u
            m_chart.ViewXY.Margins = new Thickness(0, 3, 0, 3);

            //Setup x-axis
            m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.DateTime;
            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.XAxes[0].LabelsPosition = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MajorDivTickStyle.Alignment = Alignment.Near;
            m_chart.ViewXY.XAxes[0].MinorDivTickStyle.Alignment = Alignment.Near;
            //m_chart.ViewXY.XAxes[0].MajorGrid.Visible = false;
            //m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;  

            //Setup y-axis
            m_chart.ViewXY.YAxes[0].Title.Visible = false;
            m_chart.ViewXY.YAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);
            m_chart.ViewXY.YAxes[0].MiniScale.Visible = false;
            m_chart.ViewXY.YAxes[0].AxisThickness = 2;
            m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Alignment = Alignment.Far;
            m_chart.ViewXY.YAxes[0].MajorDivTickStyle.Color = Colors.Gray;
            m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Alignment = Alignment.Far;
            m_chart.ViewXY.YAxes[0].MinorDivTickStyle.Color = Colors.Gray;
            //m_chart.ViewXY.YAxes[0].MajorGrid.Visible = false;
            //m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].Alignment = AlignmentHorizontal.Right;

            PointLineSeries series = new PointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            series.MouseInteraction = false;
            series.LineStyle.AntiAliasing = LineAntialias.None;
            series.LineStyle.Width = 1;
            series.LineStyle.Color = DefaultColors.SeriesForBlackBackgroundWpf[0];          

            m_chart.ViewXY.PointLineSeries.Add(series);          

            //Add cursor
            LineSeriesCursor cursor = new LineSeriesCursor(m_chart.ViewXY, m_chart.ViewXY.XAxes[0]);
            m_chart.ViewXY.LineSeriesCursors.Add(cursor);
            cursor.LineStyle.Color = System.Windows.Media.Color.FromArgb(150, 255, 0, 0);
            cursor.LineStyle.Width = 2;
            cursor.SnapToPoints = true;
            cursor.TrackPoint.Color1 = Colors.White;

            CreateAnnotation();

            // m_chart.ViewXY.FitView();
            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);
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

        private void m_chart_Loaded(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.LineSeriesCursors[0].PositionChanged += VibrationHistoryView_PositionChanged;
        }

        private void m_chart_Unloaded(object sender, RoutedEventArgs e)
        {
            m_chart.ViewXY.LineSeriesCursors[0].PositionChanged -= VibrationHistoryView_PositionChanged;
        }

        private void VibrationHistoryView_PositionChanged(Object sender, PositionChangedEventArgs e)
        {
            e.CancelRendering = true;
            UpdateCursorResult(e.Cursor.ValueAtXAxis);
        }

        private void UpdateCursorResult(double xValue)
        {
            try
            {
                m_chart.BeginUpdate();

                //Get cursor
                LineSeriesCursor cursor = m_chart.ViewXY.LineSeriesCursors[0];

                AnnotationXY cursorValueDisplay = m_chart.ViewXY.Annotations[0];
                float fTargetYCoord = m_chart.ViewXY.GetMarginsRect().Bottom;
                double dY;
                m_chart.ViewXY.YAxes[0].CoordToValue(fTargetYCoord, out dY);
                cursorValueDisplay.TargetAxisValues.X = xValue;
                cursorValueDisplay.TargetAxisValues.Y = dY;

                var series = m_chart.ViewXY.PointLineSeries[0];
                LineSeriesCursor lineSeriesCursor = m_chart.ViewXY.LineSeriesCursors[0];
                int index = GetNearestPointIndex(series, lineSeriesCursor.ValueAtXAxis);
                if (index == -1)
                {
                    cursorValueDisplay.Text = string.Empty;
                    cursorValueDisplay.Visible = false;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    SeriesPoint point = series.Points[index];
                    int id = (int)point.Tag;
                    //htzk123
                    //var contract = ViewModel.SelectedVibrationHistory.HistoryDatas.Where(o => o.id == id).Single();
                    //string unit = string.Empty;
                    //switch (contract.Unit)
                    //{
                    //    case 0:
                    //        unit = "m/s^2";
                    //        break;
                    //    case 1:
                    //        unit = "mm/s";
                    //        break;
                    //    case 2:
                    //        unit = "mm";
                    //        break;
                    //    default:
                    //        unit = "---";
                    //        break;
                    //}
                    //sb.AppendLine("有效值:" + Math.Round(contract.Value, 3) + "(" + unit + ")");
                    //sb.AppendLine("时间: " + m_chart.ViewXY.XAxes[0].TimeString(xValue, "yyyy-MM-dd HH:mm:ss"));
                    //string alarmTypeStr = JudgeAlarmType(contract.AlarmType);
                    //if (!string.IsNullOrEmpty(alarmTypeStr))
                    //{
                    //    sb.AppendLine(alarmTypeStr);
                    //}
                    cursorValueDisplay.Text = sb.ToString().Trim();
                    cursorValueDisplay.Visible = true;
                }

                //Allow chart rendering
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("历史报警-更新坐标", ex));
            }

        }

        private string JudgeAlarmType(int alarmType)
        {
            string alarmTypeStr = string.Empty;
            int consts = alarmType & 0X03;
            int dynamic = alarmType & 0X0C;
            int comparative = alarmType & 0X30;
            switch (consts)
            {
                case 2:
                    alarmTypeStr += "常数报警：警告";
                    break;
                case 3:
                    alarmTypeStr += "常数报警：危险";
                    break;
            }
            switch (dynamic)
            {
                case 8:
                    alarmTypeStr += "曲线报警：警告";
                    break;
                case 12:
                    alarmTypeStr += "曲线报警：危险";
                    break;
            }
            switch (comparative)
            {
                case 32:
                    alarmTypeStr += "相对报警：警告";
                    break;
                case 48:
                    alarmTypeStr += "相对报警：危险";
                    break;
            }
            return alarmTypeStr;
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

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
