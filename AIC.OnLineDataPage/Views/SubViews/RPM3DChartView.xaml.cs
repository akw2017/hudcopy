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
using Arction.Wpf.Charting.Series3D;
using System.Configuration;
using AIC.Core.Events;

namespace AIC.OnLineDataPage.Views.SubViews
{
    /// <summary>
    /// Interaction logic for TimeDomainOnLineView.xaml
    /// </summary>
    public partial class RPM3DChartView : ChartViewBase
    {
        private LightningChartUltimate m_chart;
        // Surface.
        private SurfaceGridSeries3D m_surface;

        public RPM3DChartView()
        {
            InitializeComponent();
            CreateRPM3DChart();     
        }

        protected override void ChartViewBase_Unloaded(object sender, RoutedEventArgs e)
        {
            base.ChartViewBase_Unloaded(sender, e);
            if (m_chart != null)
            {
                m_chart.ChartRenderOptions.DeviceType = RendererDeviceType.None;
            }
        }

        protected override void ChartViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            base.ChartViewBase_Loaded(sender, e);
            if (m_chart != null)
            {
                m_chart.ChartRenderOptions.DeviceType = RendererDeviceType.SoftwareOnlyD11;
            }
        }

        protected override void ViewModel_Disposed(object sender, EventArgs e)
        {
            base.ViewModel_Disposed(sender, e);
            // Don't forget to clear chart grid child list.
            gridChart.Children.Clear();
            if (m_chart != null)
            {
                m_chart.Dispose();
                m_chart = null;
            }
            base.Dispose();
        }

        protected override void ViewModel_Closed(object sender, EventArgs e)
        {
            this.ViewModel_Disposed(sender, e);
            base.GCCollect();
        }


        protected override void ViewModel_SignalChanged()
        {
            try
            {
                m_chart.BeginUpdate();
                m_surface.Clear();
                
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-转速三维谱-信号变换", ex));
            }
            finally
            {
                m_chart.EndUpdate();
            }
        }
        protected override void UpdateChart(object args)
        {
            try
            {
                if (ViewModel == null || !(ViewModel.Signal is BaseWaveSignal))
                {
                    return;
                }
                BaseWaveSignal signal = (BaseWaveSignal)ViewModel.Signal;
                if (signal.FFTLength == 0)
                {
                    return;
                }

                m_chart.BeginUpdate();

                if (signal.TriggerN == AIC.CoreType.TriggerType.Angle)
                {
                    if (signal.RPM <= 0)
                    {
                        Exception exception = new Exception("等角度触发模式下转速应大于0");
                        exception.Data.Add("RPM", signal.RPM);
                        throw exception;
                    }
                    if (signal.TeethNumber <= 0)
                    {
                        Exception exception = new Exception("等角度触发模式下齿轮齿数应大于0");
                        exception.Data.Add("TeethNumber", signal.TeethNumber);
                        throw exception;
                    }
                }
                double resolution = signal.SampleFre / signal.SamplePoint;
                double[] data = signal.Amplitude.Select(o => (double)o).ToArray();
                double maxValue = data.Max();

                bool reset = false;

                if (m_chart.View3D.YAxisPrimary3D.Maximum < maxValue)
                {
                    m_chart.View3D.YAxisPrimary3D.SetRange(0, maxValue * 1.2);
                    m_surface.ContourPalette = CreatePalette(m_surface, maxValue * 1.2);
                    reset = true;
                }
                if (signal.SampleFre / 2.56 != m_chart.View3D.XAxisPrimary3D.Maximum)
                {
                    m_chart.View3D.XAxisPrimary3D.SetRange(0, signal.SampleFre / 2.56);
                    reset = true;
                }
                if (signal.RPM > m_chart.View3D.ZAxisPrimary3D.Maximum)
                {
                    double maxZ = (int)Math.Ceiling(signal.RPM / 10) * 10;
                    m_chart.View3D.ZAxisPrimary3D.SetRange(0, maxZ);
                    reset = true;
                }

                if (m_surface.SizeX != data.Length)
                {
                    m_surface.Clear();
                    if (m_surface.CheckSurfaceSizeForGPU(data.Length, 100))
                    {
                        m_surface.SetSize(data.Length, 100);
                    }
                    else
                    {
                        throw new Exception(string.Format("当前显卡不支持点数:{0}X{1}", data.Length, 100));
                    }
                    reset = true;
                }

                double dXAxisMin = m_chart.View3D.XAxisPrimary3D.Minimum;
                double dXAxisMax = m_chart.View3D.XAxisPrimary3D.Maximum;
                double dZAxisMin = m_chart.View3D.ZAxisPrimary3D.Minimum;
                double dZAxisMax = m_chart.View3D.ZAxisPrimary3D.Maximum;
                double dStepX = (dXAxisMax - dXAxisMin) / (m_surface.SizeX - 1);
                double dStepZ = (dZAxisMax - dZAxisMin) / (m_surface.SizeZ - 1);

                if (reset)
                {
                    int xs = m_surface.SizeX;
                    int zs = m_surface.SizeZ;
                    SurfacePoint[,] surfacePoints = new SurfacePoint[xs, zs];
                    Array.Copy(m_surface.Data, surfacePoints, m_surface.Data.Length);
                    for (int z = 0; z < m_surface.SizeZ; z++)
                    {
                        for (int x = 0; x < m_surface.SizeX; x++)
                        {
                            m_surface.Data[x, z].Y = 0;
                        }
                    }

                    for (int z = 0; z < m_surface.SizeZ; z++)
                    {
                        for (int x = 0; x < m_surface.SizeX; x++)
                        {
                            int nodeX = (int)Math.Round((surfacePoints[x, z].X) / dStepX);
                            int nodeZ = (int)Math.Round((surfacePoints[x, z].Z) / dStepZ);
                            if (m_surface.Data[nodeX, nodeZ].Y < surfacePoints[x, z].Y)
                            {
                                m_surface.Data[nodeX, nodeZ].Y = surfacePoints[x, z].Y;
                            }
                        }
                    }
                    m_surface.SetRangesXZ(m_chart.View3D.XAxisPrimary3D.Minimum, m_chart.View3D.XAxisPrimary3D.Maximum, m_chart.View3D.ZAxisPrimary3D.Minimum, m_chart.View3D.ZAxisPrimary3D.Maximum);
                }

                int nodeZ2 = (int)Math.Round(((double)signal.RPM - dZAxisMin) / dStepZ);
                for (int i = 0; i < data.Length; i++)
                {
                    double x = resolution * i;
                    int nodeX = (int)Math.Round((x - dXAxisMin) / dStepX);
                    if (m_surface.Data[nodeX, nodeZ2].Y < data[i])
                    {
                        m_surface.Data[nodeX, nodeZ2].Y = data[i];
                    }
                }
                m_surface.InvalidateData();
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-转速三维谱", ex));
                m_chart.EndUpdate();
            }
            finally
            {
                
            }
        }

        private void CreateRPM3DChart()
        {
            // Clear any timeGrid3DChart's children.
            gridChart.Children.Clear();
            if (m_chart != null)
            {
                // If a chart is already created, dispose it.
                m_chart.Dispose();
                m_chart = null;
            }

            try
            {
                // Create a new chart.
                m_chart = new LightningChartUltimate();
                m_chart.Title.Text = string.Empty;

                //Disable rendering, strongly recommended before updating chart properties
                m_chart.BeginUpdate();

                //Set 3D as active view
                m_chart.ActiveView = ActiveView.View3D;

                //Setup background
                //m_chart.ChartBackground.GradientColor = Colors.Black;
                //m_chart.ChartBackground.Color = Colors.DimGray;
                //m_chart.ChartBackground.GradientFill = GradientFill.Radial;
                m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
                m_chart.ChartBackground.GradientFill = GradientFill.Solid;


                //Setup LegendBox
                m_chart.View3D.LegendBox.Layout = LegendBoxLayout.VerticalColumnSpan;
                m_chart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
                m_chart.View3D.LegendBox.SurfaceScales.ScaleSizeDim1 = 150;
                m_chart.View3D.LegendBox.SurfaceScales.ScaleSizeDim2 = 10;
                m_chart.View3D.LegendBox.ShowCheckboxes = false;
                m_chart.View3D.LegendBox.SeriesTitleColor = Colors.White;
                m_chart.View3D.LegendBox.ValueLabelColor = Colors.White;
                m_chart.View3D.LegendBox.SurfaceScales.ScaleBorderColor = Colors.White;
                m_chart.View3D.LegendBox.Offset.SetValues(0, 0);
                m_chart.View3D.LegendBox.Fill.Style = RectFillStyle.None;
                m_chart.View3D.LegendBox.Shadow.Visible = false;
                m_chart.View3D.LegendBox.BorderWidth = 0;

                //Hide all walls but bottom
                m_chart.View3D.WallOnBack.Visible = false;
                m_chart.View3D.WallOnLeft.Visible = false;
                m_chart.View3D.WallOnRight.Visible = false;
                m_chart.View3D.WallOnTop.Visible = false;
                m_chart.View3D.WallOnFront.Visible = false;
                m_chart.View3D.WallOnBottom.Visible = true;

                //Setup primary x-axis
                m_chart.View3D.XAxisPrimary3D.Maximum = 100;
                m_chart.View3D.XAxisPrimary3D.Orientation = PlaneXAxis3D.XY;
                m_chart.View3D.XAxisPrimary3D.CornerAlignment = AxisAlignment3D.Outside;
                m_chart.View3D.XAxisPrimary3D.MajorDivTickStyle.Alignment = Alignment.Far;
                m_chart.View3D.XAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
                m_chart.View3D.XAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
                m_chart.View3D.XAxisPrimary3D.Title.Text = "频率 (Hz)";
                m_chart.View3D.XAxisPrimary3D.Title.Color = Colors.Yellow;
                m_chart.View3D.XAxisPrimary3D.MouseScrolling = false;

                //Setup primary y-axis
                m_chart.View3D.YAxisPrimary3D.Maximum = 10;
                m_chart.View3D.YAxisPrimary3D.Orientation = PlaneYAxis3D.XY;
                m_chart.View3D.YAxisPrimary3D.CornerAlignment = AxisAlignment3D.Outside;
                m_chart.View3D.YAxisPrimary3D.MajorDivTickStyle.Alignment = Alignment.Far;
                m_chart.View3D.YAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
                m_chart.View3D.YAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
                m_chart.View3D.YAxisPrimary3D.Title.Text = "幅值 P(f)";
                m_chart.View3D.YAxisPrimary3D.Title.Color = Colors.Yellow;
                m_chart.View3D.YAxisPrimary3D.SetRange(0, 0);
                m_chart.View3D.YAxisPrimary3D.LabelsNumberFormat = "0.00";
                m_chart.View3D.YAxisPrimary3D.MouseScrolling = false;

                //Setup primary z-axis
                m_chart.View3D.ZAxisPrimary3D.Maximum = 100;
                m_chart.View3D.ZAxisPrimary3D.Reversed = true;
                m_chart.View3D.ZAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
                m_chart.View3D.ZAxisPrimary3D.Title.Text = "转速";
                m_chart.View3D.ZAxisPrimary3D.Title.Color = Colors.Yellow;
                m_chart.View3D.ZAxisPrimary3D.ValueType = AxisValueType.Number;
                m_chart.View3D.ZAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
                m_chart.View3D.WallOnBottom.GridStrips = WallGridStripXZ.X;
                m_chart.View3D.ZAxisPrimary3D.MouseScrolling = false;

                //Setup camera            
                m_chart.View3D.Camera.RotationX = 18.6;
                m_chart.View3D.Camera.RotationY = -23.6;
                m_chart.View3D.Camera.RotationZ = 0;
                m_chart.View3D.Camera.Target.SetValues(-9.5f, -10f, -5.8f);
                m_chart.View3D.Camera.ViewDistance = 163;

                //double dAxisZMin = updateRate / 1000.0;
                //double dAxisZMax = 100 * updateRate / 1000.0;
                //m_dStepZ = updateRate / 1000.0;

                //m_chart.View3D.ZAxisPrimary3D.SetRange(dAxisZMin, dAxisZMax);
                //m_dCurrentZ = dAxisZMax;

                //Add Surface
                CreateSurface();
                //Add Waterfall
                //CreateWaterfall();
                //Add WaterFall Front
                // CreateWaterfallFront();
                //Allow chart rendering
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-转速三维谱-初始化", ex));
                if (m_chart != null)
                {
                    m_chart.EndUpdate();
                }
            }
            finally
            {

            }
            gridChart.Children.Add(m_chart);
        }

        private void CreateSurface()
        {
            if (m_surface == null)
            {
                m_surface = new SurfaceGridSeries3D(m_chart.View3D, Axis3DBinding.Primary, Axis3DBinding.Primary, Axis3DBinding.Primary);
                m_chart.View3D.SurfaceGridSeries3D.Add(m_surface);
            }
            m_surface.InitialValue = 0;
            m_surface.Title.Text = string.Empty;
            m_surface.WireframeType = SurfaceWireframeType3D.None;
            m_surface.ContourLineType = ContourLineType3D.None;
            m_surface.FadeAway = 0;
            m_surface.SuppressLighting = false;
            m_surface.BaseColor = Colors.White;
            m_surface.ContourPalette = CreatePalette(m_surface, 10);
            m_surface.SetSize(100, 100);
            m_surface.SetRangesXZ(m_chart.View3D.XAxisPrimary3D.Minimum, m_chart.View3D.XAxisPrimary3D.Maximum, m_chart.View3D.ZAxisPrimary3D.Minimum, m_chart.View3D.ZAxisPrimary3D.Maximum);
        }

        private ValueRangePalette CreatePalette(SeriesBase3D ownerSeries, double valueMax)
        {
            ValueRangePalette palette = new ValueRangePalette(ownerSeries);
            palette.Steps.Clear();
            palette.MinValue = 0;
            palette.Type = PaletteType.Gradient;
            palette.Steps.Add(new PaletteStep(palette, Colors.Blue, 0));
            palette.Steps.Add(new PaletteStep(palette, Colors.Green, valueMax * 0.3));
            palette.Steps.Add(new PaletteStep(palette, Colors.Yellow, valueMax * 0.6));
            palette.Steps.Add(new PaletteStep(palette, Colors.Red, valueMax));
            return palette;
        }
        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
