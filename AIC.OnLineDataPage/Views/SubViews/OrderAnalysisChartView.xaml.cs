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
using System.Collections.Generic;
using Arction.Wpf.Charting.EventMarkers;
using System.Windows.Input;
using Arction.Wpf.Charting.Views;
using Arction.Wpf.Charting.Series3D;
using Arction.Wpf.Charting.Views.View3D;
using AIC.Core.Events;

namespace AIC.OnLineDataPage.Views.SubViews
{
    /// <summary>
    /// Interaction logic for TimeDomainOnLineView.xaml
    /// </summary>
    public partial class OrderAnalysisChartView : ChartViewBase
    {
        int iColumnCount = 60;
        int iRowCount = 100;
        private SurfaceMeshSeries3D m_series;
        private LightningChartUltimate m_chart;
        private IntensityPoint[] points = new IntensityPoint[1024];
        private IntensityPoint[] lohPoints = new IntensityPoint[32768];

        public OrderAnalysisChartView()
        {
            InitializeComponent();
            CreateChart();
        }

        protected override void ViewModel_Closed(object sender, EventArgs e)
        {
            base.ViewModel_Closed(sender, e);
            // Don't forget to clear chart grid child list.
            gridChart.Children.Clear();
            if (m_chart != null)
            {
                m_chart.Dispose();
                m_chart = null;
            }
        }

        protected override void ViewModel_SignalChanged()
        {
            try
            {
                m_chart.BeginUpdate();
                m_chart.View3D.XAxisPrimary3D.SetRange(0, 1);
                m_chart.View3D.YAxisPrimary3D.SetRange(0, 1);
                m_chart.View3D.ZAxisPrimary3D.SetRange(0, 1);
                SurfacePoint invalidPoint = new SurfacePoint(0, -100, 0, Colors.White);

                for (int i = 0; i < iColumnCount; i++)
                {
                    for (int j = 0; j < iRowCount; j++)
                    {
                        m_series.Data[i, j] = invalidPoint;
                    }
                }
                m_series.ContourPalette = CreatePalette(m_series, 1);               
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-阶次分析-信号变换", ex));
                
            }
            finally
            {
                m_chart.EndUpdate();
            }
        }

        private int currentIndex = 0;
        protected override void UpdateChart(object args)
        {
            try
            {
                if (ViewModel == null || !(ViewModel.Signal is BaseWaveSignal))
                {
                    return;
                }

                BaseWaveSignal signal = (BaseWaveSignal)ViewModel.Signal;
                m_chart.BeginUpdate();

                if (signal.RPM <= 0)
                {
                    Exception exception = new Exception("转速小于等于0");
                    exception.Data.Add("RPM", signal.RPM);
                    throw exception;
                }

                if (signal.TriggerN == AIC.CoreType.TriggerType.Angle)
                {
                    if (signal.TeethNumber <= 0)
                    {
                        Exception exception = new Exception("等角度触发模式下齿轮齿数应大于0");
                        exception.Data.Add("TeethNumber", signal.TeethNumber);
                        throw exception;
                    }
                }

                double rpm = signal.RPM;

                var indexArray = signal.Amplitude.Select((s, k) => new { Key = k, Value = s }).OrderByDescending(o => o.Value).Take(100).Select(o => o.Key).ToArray();

                double maxX = m_chart.View3D.XAxisPrimary3D.Maximum;
                double maxY = m_chart.View3D.YAxisPrimary3D.Maximum;
                double maxZ = m_chart.View3D.ZAxisPrimary3D.Maximum;
                for (int i = 0; i < indexArray.Length; i++)
                {
                    SurfacePoint point = m_series.Data[currentIndex, i];
                    if (signal.TriggerN == AIC.CoreType.TriggerType.Angle)
                    {
                        point.X = i / (signal.SamplePoint / signal.TeethNumber);
                        point.Y = signal.Amplitude[indexArray[i]];
                        point.Z = rpm;
                    }
                    else
                    {
                        point.X = (signal.Frequency[indexArray[i]] * 60) / rpm;
                        point.Y = signal.Amplitude[indexArray[i]];
                        point.Z = rpm;
                    }
                    if (point.X > maxX)
                    {
                        maxX = point.X * 1.2;
                    }
                    if (point.Y > maxY)
                    {
                        maxY = point.Y * 1.2;
                    }
                    if (point.Z > maxZ)
                    {
                        maxZ = point.Z * 1.2;
                    }
                    m_series.Data[currentIndex, i] = point;
                }

                if (maxX != m_chart.View3D.XAxisPrimary3D.Maximum)
                {
                    m_chart.View3D.XAxisPrimary3D.SetRange(0, maxX);
                }
                if (maxY != m_chart.View3D.YAxisPrimary3D.Maximum)
                {
                    m_chart.View3D.YAxisPrimary3D.SetRange(0, maxY);
                    m_series.ContourPalette = CreatePalette(m_series, maxY);
                }
                if (maxZ != m_chart.View3D.ZAxisPrimary3D.Maximum)
                {
                    m_chart.View3D.ZAxisPrimary3D.SetRange(0, maxZ);
                }

                currentIndex++;
                if (currentIndex == 60)
                {
                    currentIndex = 0;
                }
                m_series.InvalidateData();
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-阶次分析", ex));
                m_chart.EndUpdate();
            }
            finally
            {
                
            }
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
            m_chart.Title.Text = "";

            //Disable rendering, strongly recommended before updating chart properties
            m_chart.BeginUpdate();

            //Set active view
            m_chart.ActiveView = ActiveView.View3D;

            //Chart name
            m_chart.ChartName = "Point cloud chart";
            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;

            //Setup LegendBox
            m_chart.View3D.LegendBox.Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
            m_chart.View3D.LegendBox.SurfaceScales.ScaleSizeDim1 = 150;
            m_chart.View3D.LegendBox.SurfaceScales.ScaleSizeDim2 = 20;
            m_chart.View3D.LegendBox.ShowCheckboxes = false;
            m_chart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
            m_chart.View3D.LegendBox.Offset.SetValues(0, 0);
            m_chart.View3D.LegendBox.Fill.Style = RectFillStyle.None;
            m_chart.View3D.LegendBox.Shadow.Visible = false;
            m_chart.View3D.LegendBox.BorderWidth = 0;
            m_chart.View3D.LegendBox.SeriesTitleColor = Colors.White;
            m_chart.View3D.LegendBox.ValueLabelColor = Colors.White;
            m_chart.View3D.LegendBox.SurfaceScales.ScaleBorderColor = Colors.White;

            m_series = new SurfaceMeshSeries3D(m_chart.View3D, Axis3DBinding.Primary, Axis3DBinding.Primary, Axis3DBinding.Primary);
            m_chart.View3D.SurfaceMeshSeries3D.Add(m_series);
            m_series.Title.Text = "";

            m_series.ColorSaturation = 100;
            m_series.WireframeType = SurfaceWireframeType3D.DotsPalettedByValue;
            m_series.ContourLineType = ContourLineType3D.None;
            m_series.Fill = SurfaceFillStyle.None;
            m_series.InitialValue = -10;
            m_series.ContourPalette = CreatePalette(m_series, 1);
            m_series.WireframeLineStyle.Width = 3;

            SurfacePoint[,] seriesData = new SurfacePoint[iColumnCount, iRowCount];
            SurfacePoint invalidPoint = new SurfacePoint(0, -100, 0, Colors.White);

            for (int i = 0; i < iColumnCount; i++)
            {
                for (int j = 0; j < iRowCount; j++)
                {
                    seriesData[i, j] = invalidPoint;
                }
            }
            m_series.Data = seriesData;

            //Hide walls 
            foreach (WallBase wall in m_chart.View3D.GetWalls())
            {
                wall.Visible = false;
            }

            //Set camera 
            m_chart.View3D.Camera.MinimumViewDistance = 30;
            m_chart.View3D.Camera.RotationX += 10;

            //Set Y dimensions a little bit higher than default
            m_chart.View3D.Dimensions.Y = 80;

            //Set axis ranges
            m_chart.View3D.XAxisPrimary3D.SetRange(0, 1);
            m_chart.View3D.XAxisPrimary3D.Title.Text = "倍频";
            m_chart.View3D.YAxisPrimary3D.SetRange(0, 1);
            m_chart.View3D.YAxisPrimary3D.Title.Text = "幅值";
            m_chart.View3D.ZAxisPrimary3D.SetRange(0, 1);
            m_chart.View3D.ZAxisPrimary3D.Title.Text = "转速";

            // m_chart.MouseMove += m_chart_MouseMove;

            //Allow chart rendering
            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);
        }

        private void m_chart_MouseMove(object sender, MouseEventArgs e)
        {
            int hitColumnIndex = -1;
            int hitRowIndex = -1;
            // var p=e.GetPosition(m_chart);
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            var result = m_series.IsMouseOver(w32Mouse.X, w32Mouse.Y, out hitColumnIndex, out hitRowIndex);
            if (result)
            {
                SurfacePoint point = m_series.Data[hitColumnIndex, hitRowIndex];
                textX.Text = "X:" + point.X;
                textY.Text = "Y:" + point.Y;
                textZ.Text = "Z:" + point.Z;
            }
        }

        private ValueRangePalette CreatePalette(SeriesBase3D ownerSeries, double max)
        {
            ValueRangePalette palette = new ValueRangePalette(ownerSeries);

            palette.Steps.Clear();

            //Transparent value for 'invalid' points. They are way below this value and thus become transparent
            palette.Steps.Add(new PaletteStep(palette, Color.FromArgb(0, 0, 0, 0), -0.01));

            //Actual steps 
            palette.Steps.Add(new PaletteStep(palette, Colors.Blue, 0));
            palette.Steps.Add(new PaletteStep(palette, Colors.Green, max * 0.3));
            palette.Steps.Add(new PaletteStep(palette, Colors.Yellow, max * 0.6));
            palette.Steps.Add(new PaletteStep(palette, Colors.Red, max));
            palette.Type = PaletteType.Gradient;

            return palette;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            checkBoxOrthographic.IsChecked = true;
            m_chart.View3D.Camera.RotationX = 90;
            m_chart.View3D.Camera.RotationY = 0;
            m_chart.View3D.Camera.RotationZ = 0;
            m_chart.View3D.Camera.OrthographicCamera = checkBoxOrthographic.IsChecked.Value;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            checkBoxOrthographic.IsChecked = true;
            m_chart.View3D.Camera.RotationX = 0;
            m_chart.View3D.Camera.RotationY = 90;
            m_chart.View3D.Camera.RotationZ = 0;
            m_chart.View3D.Camera.OrthographicCamera = checkBoxOrthographic.IsChecked.Value;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            checkBoxOrthographic.IsChecked = true;
            m_chart.View3D.Camera.RotationX = 0;
            m_chart.View3D.Camera.RotationY = 0;
            m_chart.View3D.Camera.RotationZ = 90;
            m_chart.View3D.Camera.OrthographicCamera = checkBoxOrthographic.IsChecked.Value;
        }

        private void checkBoxOrthographic_Checked(object sender, RoutedEventArgs e)
        {
            m_chart.View3D.Camera.OrthographicCamera = checkBoxOrthographic.IsChecked.Value;
        }

        private void checkBoxOrthographic_Unchecked(object sender, RoutedEventArgs e)
        {
            m_chart.View3D.Camera.OrthographicCamera = checkBoxOrthographic.IsChecked.Value;

        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }
        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
