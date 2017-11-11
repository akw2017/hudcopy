using AIC.HistoryDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Series3D;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Interaction logic for Time3DSpectrum.xaml
    /// </summary>
    public partial class Time3DSpectrumView : UserControl
    {
        private LightningChartUltimate time3Dchart;
        private SurfaceGridSeries3D m_surface;
        private WaterfallSeries3D m_waterfallFront;
        private Double m_dCurrentZ = 0;
        private IDisposable dataLink;
        public Time3DSpectrumView()
        {
            InitializeComponent();
            CreateTime3DChart();
            Loaded += Time3DSpectrumView_Loaded;
        }

        private Time3DSpectrumDataViewModel ViewModel { get; set; }

        private　void Time3DSpectrumView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = DataContext as Time3DSpectrumDataViewModel;
            if (ViewModel!=null)
            {

            }
        }

        private void DrawTime3D(Tuple<float[], float[]> turple)
        {
            if (turple!=null)
            {
                time3Dchart.BeginUpdate();
                m_dCurrentZ += 1;
                SurfacePoint[,] surfaceData = m_surface.Data;
                double dZMin = m_dCurrentZ - (time3Dchart.View3D.ZAxisPrimary3D.Maximum - time3Dchart.View3D.ZAxisPrimary3D.Minimum);
                double dZMax = m_dCurrentZ;
                var datas = turple.Item2.Select(o => (double)o).ToArray();

                double yMax = datas.Max();
                if (time3Dchart.View3D.YAxisPrimary3D.Maximum < yMax)
                {
                    m_surface.ContourPalette = CreatePalette(m_surface, yMax);
                    time3Dchart.View3D.YAxisPrimary3D.SetRange(0, yMax * 1.2);
                }

                var xMax = turple.Item1.Max();
                if (xMax != time3Dchart.View3D.XAxisPrimary3D.Maximum)
                {
                    time3Dchart.View3D.XAxisPrimary3D.SetRange(0, xMax);
                    m_surface.SetRangesXZ(time3Dchart.View3D.XAxisPrimary3D.Minimum, time3Dchart.View3D.XAxisPrimary3D.Maximum,
                        time3Dchart.View3D.ZAxisPrimary3D.Minimum, time3Dchart.View3D.ZAxisPrimary3D.Maximum);
                }

                if (m_surface.SizeX != datas.Length)
                {
                    if (m_surface.CheckSurfaceSizeForGPU(datas.Length, 100))
                    {
                        m_surface.SetSize(datas.Length, 100);
                        messageTxt.Text = string.Empty;
                    }
                    else
                    {
                        messageTxt.Text = string.Format("当前显卡不支持点数:{0}X{1}", datas.Length, 100);
                    }
                }
                m_surface.InsertRowBackAndScroll(datas, dZMin, dZMax, dZMin, dZMax);
                time3Dchart.EndUpdate();
            }
        }

        private void CreateTime3DChart()
        {
            // Clear any timeGrid3DChart's children.
            timeGrid3DChart.Children.Clear();
            if (time3Dchart != null)
            {
                // If a chart is already created, dispose it.
                time3Dchart.Dispose();
                time3Dchart = null;
            }

            // Create a new chart.
            time3Dchart = new LightningChartUltimate();
            time3Dchart.ChartName = "Spectrum 3D chart";
            time3Dchart.Title.Text = string.Empty;

            //Disable rendering, strongly recommended before updating chart properties
            time3Dchart.BeginUpdate();

            //Set 3D as active view
            time3Dchart.ActiveView = ActiveView.View3D;

            //Setup background
            time3Dchart.ChartBackground.GradientColor = Colors.Black;
            time3Dchart.ChartBackground.Color = Colors.DimGray;
            time3Dchart.ChartBackground.GradientFill = GradientFill.Radial;

            //Setup LegendBox
            time3Dchart.View3D.LegendBox.Layout = LegendBoxLayout.VerticalColumnSpan;
            time3Dchart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
            time3Dchart.View3D.LegendBox.SurfaceScales.ScaleSizeDim1 = 150;
            time3Dchart.View3D.LegendBox.SurfaceScales.ScaleSizeDim2 = 10;
            time3Dchart.View3D.LegendBox.ShowCheckboxes = false;

            //Hide all walls but bottom
            time3Dchart.View3D.WallOnBack.Visible = false;
            time3Dchart.View3D.WallOnLeft.Visible = false;
            time3Dchart.View3D.WallOnRight.Visible = false;
            time3Dchart.View3D.WallOnTop.Visible = false;
            time3Dchart.View3D.WallOnFront.Visible = false;
            time3Dchart.View3D.WallOnBottom.Visible = true;

            //Setup primary x-axis
            time3Dchart.View3D.XAxisPrimary3D.Orientation = PlaneXAxis3D.XY;
            time3Dchart.View3D.XAxisPrimary3D.CornerAlignment = AxisAlignment3D.Outside;
            time3Dchart.View3D.XAxisPrimary3D.MajorDivTickStyle.Alignment = Alignment.Far;
            time3Dchart.View3D.XAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            time3Dchart.View3D.XAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            time3Dchart.View3D.XAxisPrimary3D.Title.Text = "频率 (Hz)";
            time3Dchart.View3D.XAxisPrimary3D.Title.Color = Colors.Yellow;

            //Setup primary y-axis
            time3Dchart.View3D.YAxisPrimary3D.Orientation = PlaneYAxis3D.XY;
            time3Dchart.View3D.YAxisPrimary3D.CornerAlignment = AxisAlignment3D.Outside;
            time3Dchart.View3D.YAxisPrimary3D.MajorDivTickStyle.Alignment = Alignment.Far;
            time3Dchart.View3D.YAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            time3Dchart.View3D.YAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            time3Dchart.View3D.YAxisPrimary3D.Title.Text = "幅值 P(f)";
            time3Dchart.View3D.YAxisPrimary3D.Title.Color = Colors.Yellow;
            time3Dchart.View3D.YAxisPrimary3D.SetRange(0, 0);
            time3Dchart.View3D.YAxisPrimary3D.LabelsNumberFormat = "0.00";

            //Setup primary z-axis
            time3Dchart.View3D.ZAxisPrimary3D.Reversed = true;
            time3Dchart.View3D.ZAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            time3Dchart.View3D.ZAxisPrimary3D.Title.Text = "时间";
            time3Dchart.View3D.ZAxisPrimary3D.Title.Color = Colors.Yellow;
            time3Dchart.View3D.ZAxisPrimary3D.ValueType = AxisValueType.DateTime;
            time3Dchart.View3D.ZAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            time3Dchart.View3D.ZAxisPrimary3D.SetRange(1, 100);
            time3Dchart.View3D.ZAxisPrimary3D.LabelsVisible = false;
            time3Dchart.View3D.WallOnBottom.GridStrips = WallGridStripXZ.X;

            //Setup legend box
            time3Dchart.View3D.LegendBox.SeriesTitleColor = Colors.White;
            time3Dchart.View3D.LegendBox.ValueLabelColor = Colors.White;
            time3Dchart.View3D.LegendBox.SurfaceScales.ScaleBorderColor = Colors.White;
            time3Dchart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
            time3Dchart.View3D.LegendBox.Offset.SetValues(0, 0);
            time3Dchart.View3D.LegendBox.Fill.Style = RectFillStyle.None;
            time3Dchart.View3D.LegendBox.Shadow.Visible = false;
            time3Dchart.View3D.LegendBox.BorderWidth = 0;

            //Setup camera            
            time3Dchart.View3D.Camera.RotationX = 18.6;
            time3Dchart.View3D.Camera.RotationY = -23.6;
            time3Dchart.View3D.Camera.RotationZ = 0;
            time3Dchart.View3D.Camera.Target.SetValues(-9.5f, -10f, -5.8f);
            time3Dchart.View3D.Camera.ViewDistance = 163;

            //double dAxisZMin = updateRate / 1000.0;
            //double dAxisZMax = 100 * updateRate / 1000.0;
            //m_dStepZ = updateRate / 1000.0;

            //time3Dchart.View3D.ZAxisPrimary3D.SetRange(dAxisZMin, dAxisZMax);
            //m_dCurrentZ = dAxisZMax;

            //Add Surface
            CreateSurface();
            //Add Waterfall
            //CreateWaterfall();
            //Add WaterFall Front

          //  CreateWaterfallFront();

            //Allow chart rendering
            time3Dchart.EndUpdate();

            timeGrid3DChart.Children.Add(time3Dchart);
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
            // m_surface.SetSize(150, 150);
            m_surface.FadeAway = 0;
            m_surface.SuppressLighting = false;
            m_surface.BaseColor = Colors.White;
            m_surface.SetSize(400, 100);
            m_surface.SetRangesXZ(time3Dchart.View3D.XAxisPrimary3D.Minimum, time3Dchart.View3D.XAxisPrimary3D.Maximum,
                time3Dchart.View3D.ZAxisPrimary3D.Minimum, time3Dchart.View3D.ZAxisPrimary3D.Maximum);
            m_surface.ContourPalette = CreatePalette(m_surface, 10);
            m_surface.TraceMouseCell = true;
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
            m_waterfallFront.ShowInLegendBox = false;

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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            time3Dchart.BeginUpdate();
            foreach (var series in time3Dchart.View3D.SurfaceGridSeries3D)
            {
                series.Clear();
            }
            time3Dchart.EndUpdate();
        }

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            time3Dchart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }

        private void clearChartBtn_Click(object sender, RoutedEventArgs e)
        {
            time3Dchart.BeginUpdate();
            foreach (var series in time3Dchart.View3D.SurfaceGridSeries3D)
            {
                series.Clear();
            }
            time3Dchart.EndUpdate();
        }
    }
}
