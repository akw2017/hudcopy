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
    public partial class Time3DChartView : ChartViewBase
    {
        private static double updateRate = Convert.ToDouble(ConfigurationManager.AppSettings["UIUPDATERATE"]);

        private LightningChartUltimate time3Dchart;
        // Surface.
        private SurfaceGridSeries3D m_surface;
        // Waterfall front.
        private WaterfallSeries3D m_waterfallFront;
        private Double m_dCurrentZ;
        private Double m_dStepZ;

        public Time3DChartView()
        {
            InitializeComponent();
            CreateTime3DChart();
        }

        protected override void ViewModel_Closed(object sender, EventArgs e)
        {
            base.ViewModel_Closed(sender, e);
            // Don't forget to clear chart grid child list.
            gridTime3DChart.Children.Clear();
            if (time3Dchart != null)
            {
                time3Dchart.Dispose();
                time3Dchart = null;
            }
        }

        protected override void ViewModel_SignalChanged()
        {
            try
            {
                time3Dchart.BeginUpdate();
                m_surface.Clear();               
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-时间三维谱-信号变换", ex));
            }
            finally
            {
                time3Dchart.EndUpdate();
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

                time3Dchart.BeginUpdate();

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

                double[] data = signal.Amplitude.Select(o => (double)o).ToArray();
                double maxValue = data.Max();

                if (time3Dchart.View3D.YAxisPrimary3D.Maximum < maxValue)
                {
                    m_surface.ContourPalette = CreatePalette(m_surface, maxValue);
                    time3Dchart.View3D.YAxisPrimary3D.SetRange(0, maxValue * 1.2);                    
                }

                if (signal.SampleFre / 2.56 != time3Dchart.View3D.XAxisPrimary3D.Maximum)
                {
                    time3Dchart.View3D.XAxisPrimary3D.SetRange(0, signal.SampleFre / 2.56);
                    m_surface.SetRangesXZ(time3Dchart.View3D.XAxisPrimary3D.Minimum, time3Dchart.View3D.XAxisPrimary3D.Maximum, time3Dchart.View3D.ZAxisPrimary3D.Minimum, time3Dchart.View3D.ZAxisPrimary3D.Maximum);
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
                }

                m_dCurrentZ += m_dStepZ;//此处有bug20170419 find by htzk123

                SetNewDataToSurface(data);
                time3Dchart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-时间三维谱", ex));
                time3Dchart.EndUpdate();
            }
            finally
            {
                
            }
        }

        private void SetNewDataToSurface(double[] data)
        {
            //Surface grid series has optimized methods for adding data to back.
            SurfacePoint[,] surfaceData = m_surface.Data;

            double dZMin = m_dCurrentZ - (time3Dchart.View3D.ZAxisPrimary3D.Maximum - time3Dchart.View3D.ZAxisPrimary3D.Minimum);
            double dZMax = m_dCurrentZ;
            m_surface.InsertRowBackAndScroll(data, dZMin, dZMax, dZMin, dZMax);
        }

        private void SetNewDataToFrontWaterfall(double[] data)
        {
            SurfacePoint[,] areaData = m_waterfallFront.Data;

            //Shift older data, drop oldest row. Set new data to back.
            int iColCount = m_waterfallFront.SizeX;
            int iNewDataCopyCount = Math.Min(data.Length, iColCount);
            for (int iCol = 0; iCol < iNewDataCopyCount; iCol++)
            {
                //No need to update X or Z values 
                areaData[iCol, 0].Y = data[iCol];
                areaData[iCol, 0].Z = m_dCurrentZ;
            }
            m_waterfallFront.InvalidateData();
        }

        private void CreateTime3DChart()
        {
            // Clear any timeGrid3DChart's children.
            gridTime3DChart.Children.Clear();
            if (time3Dchart != null)
            {
                // If a chart is already created, dispose it.
                time3Dchart.Dispose();
                time3Dchart = null;
            }

            // Create a new chart.
            time3Dchart = new LightningChartUltimate();
            time3Dchart.Title.Text = string.Empty;

            //Disable rendering, strongly recommended before updating chart properties
            time3Dchart.BeginUpdate();

            //Set 3D as active view
            time3Dchart.ActiveView = ActiveView.View3D;

            //Setup background
            //time3Dchart.ChartBackground.GradientColor = Colors.Black;
            //time3Dchart.ChartBackground.Color = Colors.DimGray;
            //time3Dchart.ChartBackground.GradientFill = GradientFill.Radial;
            time3Dchart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            time3Dchart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            time3Dchart.ChartBackground.GradientFill = GradientFill.Solid;


            //Setup LegendBox
            time3Dchart.View3D.LegendBox.Layout = LegendBoxLayout.VerticalColumnSpan;
            time3Dchart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
            time3Dchart.View3D.LegendBox.SurfaceScales.ScaleSizeDim1 = 150;
            time3Dchart.View3D.LegendBox.SurfaceScales.ScaleSizeDim2 = 10;
            time3Dchart.View3D.LegendBox.ShowCheckboxes = false;
            time3Dchart.View3D.LegendBox.SeriesTitleColor = Colors.White;
            time3Dchart.View3D.LegendBox.ValueLabelColor = Colors.White;
            time3Dchart.View3D.LegendBox.SurfaceScales.ScaleBorderColor = Colors.White;
            time3Dchart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
            time3Dchart.View3D.LegendBox.Offset.SetValues(0, 0);
            time3Dchart.View3D.LegendBox.Fill.Style = RectFillStyle.None;
            time3Dchart.View3D.LegendBox.Shadow.Visible = false;
            time3Dchart.View3D.LegendBox.BorderWidth = 0;

            //Hide all walls but bottom
            time3Dchart.View3D.WallOnBack.Visible = false;
            time3Dchart.View3D.WallOnLeft.Visible = false;
            time3Dchart.View3D.WallOnRight.Visible = false;
            time3Dchart.View3D.WallOnTop.Visible = false;
            time3Dchart.View3D.WallOnFront.Visible = false;
            time3Dchart.View3D.WallOnBottom.Visible = true;

            //Setup primary x-axis
            time3Dchart.View3D.XAxisPrimary3D.Maximum = 100;
            time3Dchart.View3D.XAxisPrimary3D.Orientation = PlaneXAxis3D.XY;
            time3Dchart.View3D.XAxisPrimary3D.CornerAlignment = AxisAlignment3D.Outside;
            time3Dchart.View3D.XAxisPrimary3D.MajorDivTickStyle.Alignment = Alignment.Far;
            time3Dchart.View3D.XAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            time3Dchart.View3D.XAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            time3Dchart.View3D.XAxisPrimary3D.Title.Text = "频率 (Hz)";
            time3Dchart.View3D.XAxisPrimary3D.Title.Color = Colors.Yellow;
            time3Dchart.View3D.XAxisPrimary3D.MouseScrolling = false;

            //Setup primary y-axis
            time3Dchart.View3D.YAxisPrimary3D.Maximum = 10;
            time3Dchart.View3D.YAxisPrimary3D.Orientation = PlaneYAxis3D.XY;
            time3Dchart.View3D.YAxisPrimary3D.CornerAlignment = AxisAlignment3D.Outside;
            time3Dchart.View3D.YAxisPrimary3D.MajorDivTickStyle.Alignment = Alignment.Far;
            time3Dchart.View3D.YAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            time3Dchart.View3D.YAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            time3Dchart.View3D.YAxisPrimary3D.Title.Text = "幅值 P(f)";
            time3Dchart.View3D.YAxisPrimary3D.Title.Color = Colors.Yellow;
            time3Dchart.View3D.YAxisPrimary3D.SetRange(0, 0);
            time3Dchart.View3D.YAxisPrimary3D.LabelsNumberFormat = "0.00";
            time3Dchart.View3D.YAxisPrimary3D.MouseScrolling = false;

            //Setup primary z-axis
            time3Dchart.View3D.ZAxisPrimary3D.Maximum = 100;
            time3Dchart.View3D.ZAxisPrimary3D.Reversed = true;
            time3Dchart.View3D.ZAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            time3Dchart.View3D.ZAxisPrimary3D.Title.Text = "时间";
            time3Dchart.View3D.ZAxisPrimary3D.Title.Color = Colors.Yellow;
            time3Dchart.View3D.ZAxisPrimary3D.ValueType = AxisValueType.Time;
            time3Dchart.View3D.ZAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            time3Dchart.View3D.WallOnBottom.GridStrips = WallGridStripXZ.X;
            time3Dchart.View3D.ZAxisPrimary3D.MouseScrolling = false;

            //Setup camera            
            time3Dchart.View3D.Camera.RotationX = 18.6;
            time3Dchart.View3D.Camera.RotationY = -23.6;
            time3Dchart.View3D.Camera.RotationZ = 0;
            time3Dchart.View3D.Camera.Target.SetValues(-9.5f, -10f, -5.8f);
            time3Dchart.View3D.Camera.ViewDistance = 163;

            double dAxisZMin = updateRate / 1000.0;
            double dAxisZMax = 100 * updateRate / 1000.0;
            m_dStepZ = updateRate / 1000.0;

            time3Dchart.View3D.ZAxisPrimary3D.SetRange(dAxisZMin, dAxisZMax);
            m_dCurrentZ = dAxisZMax;

            //Add Surface
            CreateSurface();
            //Add Waterfall
            //CreateWaterfall();
            //Add WaterFall Front
            // CreateWaterfallFront();
            //Allow chart rendering
            time3Dchart.EndUpdate();

            gridTime3DChart.Children.Add(time3Dchart);
        }

        private void CreateSurface()
        {
            if (m_surface == null)
            {
                m_surface = new SurfaceGridSeries3D(time3Dchart.View3D, Axis3DBinding.Primary, Axis3DBinding.Primary, Axis3DBinding.Primary);
                time3Dchart.View3D.SurfaceGridSeries3D.Add(m_surface);
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
            // m_surface.SetRangesXZ(time3Dchart.View3D.XAxisPrimary3D.Minimum, time3Dchart.View3D.XAxisPrimary3D.Maximum, time3Dchart.View3D.ZAxisPrimary3D.Minimum, time3Dchart.View3D.ZAxisPrimary3D.Maximum);
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

        private void CreateWaterfallFront()
        {
            if (m_waterfallFront == null)
            {
                m_waterfallFront = new WaterfallSeries3D(time3Dchart.View3D, Axis3DBinding.Primary, Axis3DBinding.Primary, Axis3DBinding.Primary);
                time3Dchart.View3D.WaterfallSeries3D.Add(m_waterfallFront);
            }
            // m_waterfallFront.SetSize(ViewModel.SgProcessor.FFTLength, 1);
            m_waterfallFront.BaseLevel = time3Dchart.View3D.YAxisPrimary3D.Minimum;
            m_waterfallFront.FadeAway = 0;
            m_waterfallFront.SuppressLighting = false;
            m_waterfallFront.BaseColor = Colors.White;
            // m_waterfallFront.Title.ShowInLegendBox = false;

            //Init one row
            SurfacePoint[,] areaData = m_waterfallFront.Data;
            int iColCount = m_waterfallFront.SizeX;
            double dX = time3Dchart.View3D.XAxisPrimary3D.Minimum;
            double dStepX;
            if (iColCount > 1)
                dStepX = (time3Dchart.View3D.XAxisPrimary3D.Maximum - time3Dchart.View3D.XAxisPrimary3D.Minimum) / (double)(iColCount - 1);
            else
                dStepX = 0;
            double dZ = time3Dchart.View3D.ZAxisSecondary3D.Maximum;
            double dY = m_waterfallFront.InitialValue;
            for (int iCol = 0; iCol < iColCount; iCol++)
            {
                areaData[iCol, 0].X = dX;
                areaData[iCol, 0].Y = dY;
                areaData[iCol, 0].Z = dZ;
                dX += dStepX;
            }
            m_waterfallFront.InvalidateData();
            m_waterfallFront.ContourLineType = WaterfallContourLineType.None;
            m_waterfallFront.WireframeType = WaterfallWireframeType.None;
            m_waterfallFront.ContourPalette = CreatePalette(m_waterfallFront, 10);
        }

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            time3Dchart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
