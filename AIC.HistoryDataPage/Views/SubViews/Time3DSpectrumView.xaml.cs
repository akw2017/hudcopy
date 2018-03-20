using AIC.Core;
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
    public partial class Time3DSpectrumView : DisposableUserControl
    {
        private LightningChartUltimate m_chart;
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

        private Time3DSpectrumDataViewModel ViewModel
        {
            get { return DataContext as Time3DSpectrumDataViewModel; }
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

        private　void Time3DSpectrumView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Time3DSpectrumView_Loaded;
            if (ViewModel!=null)
            {
                ViewModel.Closed += ViewModel_Closed;
            }
        }

        private void DrawTime3D(Tuple<float[], float[]> turple)
        {
            if (turple!=null)
            {
                m_chart.BeginUpdate();
                m_dCurrentZ += 1;
                SurfacePoint[,] surfaceData = m_surface.Data;
                double dZMin = m_dCurrentZ - (m_chart.View3D.ZAxisPrimary3D.Maximum - m_chart.View3D.ZAxisPrimary3D.Minimum);
                double dZMax = m_dCurrentZ;
                var datas = turple.Item2.Select(o => (double)o).ToArray();

                double yMax = datas.Max();
                if (m_chart.View3D.YAxisPrimary3D.Maximum < yMax)
                {
                    m_surface.ContourPalette = CreatePalette(m_surface, yMax);
                    m_chart.View3D.YAxisPrimary3D.SetRange(0, yMax * 1.2);
                }

                var xMax = turple.Item1.Max();
                if (xMax != m_chart.View3D.XAxisPrimary3D.Maximum)
                {
                    m_chart.View3D.XAxisPrimary3D.SetRange(0, xMax);
                    m_surface.SetRangesXZ(m_chart.View3D.XAxisPrimary3D.Minimum, m_chart.View3D.XAxisPrimary3D.Maximum,
                        m_chart.View3D.ZAxisPrimary3D.Minimum, m_chart.View3D.ZAxisPrimary3D.Maximum);
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
                m_chart.EndUpdate();
            }
        }

        private void CreateTime3DChart()
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
            m_chart.ChartName = "Spectrum 3D chart";
            m_chart.Title.Text = string.Empty;

            //Disable rendering, strongly recommended before updating chart properties
            m_chart.BeginUpdate();

            //Set 3D as active view
            m_chart.ActiveView = ActiveView.View3D;

            //Setup background
            m_chart.ChartBackground.GradientColor = Colors.Black;
            m_chart.ChartBackground.Color = Colors.DimGray;
            m_chart.ChartBackground.GradientFill = GradientFill.Radial;

            //Setup LegendBox
            m_chart.View3D.LegendBox.Layout = LegendBoxLayout.VerticalColumnSpan;
            m_chart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
            m_chart.View3D.LegendBox.SurfaceScales.ScaleSizeDim1 = 150;
            m_chart.View3D.LegendBox.SurfaceScales.ScaleSizeDim2 = 10;
            m_chart.View3D.LegendBox.ShowCheckboxes = false;

            //Hide all walls but bottom
            m_chart.View3D.WallOnBack.Visible = false;
            m_chart.View3D.WallOnLeft.Visible = false;
            m_chart.View3D.WallOnRight.Visible = false;
            m_chart.View3D.WallOnTop.Visible = false;
            m_chart.View3D.WallOnFront.Visible = false;
            m_chart.View3D.WallOnBottom.Visible = true;

            //Setup primary x-axis
            m_chart.View3D.XAxisPrimary3D.Orientation = PlaneXAxis3D.XY;
            m_chart.View3D.XAxisPrimary3D.CornerAlignment = AxisAlignment3D.Outside;
            m_chart.View3D.XAxisPrimary3D.MajorDivTickStyle.Alignment = Alignment.Far;
            m_chart.View3D.XAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            m_chart.View3D.XAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            m_chart.View3D.XAxisPrimary3D.Title.Text = "频率 (Hz)";
            m_chart.View3D.XAxisPrimary3D.Title.Color = Colors.Yellow;

            //Setup primary y-axis
            m_chart.View3D.YAxisPrimary3D.Orientation = PlaneYAxis3D.XY;
            m_chart.View3D.YAxisPrimary3D.CornerAlignment = AxisAlignment3D.Outside;
            m_chart.View3D.YAxisPrimary3D.MajorDivTickStyle.Alignment = Alignment.Far;
            m_chart.View3D.YAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            m_chart.View3D.YAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            m_chart.View3D.YAxisPrimary3D.Title.Text = "幅值 P(f)";
            m_chart.View3D.YAxisPrimary3D.Title.Color = Colors.Yellow;
            m_chart.View3D.YAxisPrimary3D.SetRange(0, 0);
            m_chart.View3D.YAxisPrimary3D.LabelsNumberFormat = "0.00";

            //Setup primary z-axis
            m_chart.View3D.ZAxisPrimary3D.Reversed = true;
            m_chart.View3D.ZAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            m_chart.View3D.ZAxisPrimary3D.Title.Text = "时间";
            m_chart.View3D.ZAxisPrimary3D.Title.Color = Colors.Yellow;
            m_chart.View3D.ZAxisPrimary3D.ValueType = AxisValueType.DateTime;
            m_chart.View3D.ZAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            m_chart.View3D.ZAxisPrimary3D.SetRange(1, 100);
            m_chart.View3D.ZAxisPrimary3D.LabelsVisible = false;
            m_chart.View3D.WallOnBottom.GridStrips = WallGridStripXZ.X;

            //Setup legend box
            m_chart.View3D.LegendBox.SeriesTitleColor = Colors.White;
            m_chart.View3D.LegendBox.ValueLabelColor = Colors.White;
            m_chart.View3D.LegendBox.SurfaceScales.ScaleBorderColor = Colors.White;
            m_chart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
            m_chart.View3D.LegendBox.Offset.SetValues(0, 0);
            m_chart.View3D.LegendBox.Fill.Style = RectFillStyle.None;
            m_chart.View3D.LegendBox.Shadow.Visible = false;
            m_chart.View3D.LegendBox.BorderWidth = 0;

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

          //  CreateWaterfallFront();

            //Allow chart rendering
            m_chart.EndUpdate();

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
            // m_surface.SetSize(150, 150);
            m_surface.FadeAway = 0;
            m_surface.SuppressLighting = false;
            m_surface.BaseColor = Colors.White;
            m_surface.SetSize(400, 100);
            m_surface.SetRangesXZ(m_chart.View3D.XAxisPrimary3D.Minimum, m_chart.View3D.XAxisPrimary3D.Maximum,
                m_chart.View3D.ZAxisPrimary3D.Minimum, m_chart.View3D.ZAxisPrimary3D.Maximum);
            m_surface.ContourPalette = CreatePalette(m_surface, 10);
            m_surface.TraceMouseCell = true;
        }

        private void CreateWaterfallFront()
        {
            if (m_waterfallFront == null)
            {
                m_waterfallFront = new WaterfallSeries3D(m_chart.View3D, Axis3DBinding.Primary, Axis3DBinding.Primary, Axis3DBinding.Primary);
                m_chart.View3D.WaterfallSeries3D.Add(m_waterfallFront);
            }

            // m_waterfallFront.SetSize(ViewModel.SgProcessor.FFTLength, 1);
            m_waterfallFront.BaseLevel = m_chart.View3D.YAxisPrimary3D.Minimum;
            m_waterfallFront.FadeAway = 0;
            m_waterfallFront.SuppressLighting = false;
            m_waterfallFront.BaseColor = Colors.White;
            m_waterfallFront.ShowInLegendBox = false;

            //Init one row
            SurfacePoint[,] areaData = m_waterfallFront.Data;
            int iColCount = m_waterfallFront.SizeX;
            double dX = m_chart.View3D.XAxisPrimary3D.Minimum;
            double dStepX;
            if (iColCount > 1)
                dStepX = (m_chart.View3D.XAxisPrimary3D.Maximum - m_chart.View3D.XAxisPrimary3D.Minimum) / (double)(iColCount - 1);
            else
                dStepX = 0;
            double dZ = m_chart.View3D.ZAxisSecondary3D.Maximum;
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
            m_chart.BeginUpdate();
            foreach (var series in m_chart.View3D.SurfaceGridSeries3D)
            {
                series.Clear();
            }
            m_chart.EndUpdate();
        }

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }

        private void clearChartBtn_Click(object sender, RoutedEventArgs e)
        {
            m_chart.BeginUpdate();
            foreach (var series in m_chart.View3D.SurfaceGridSeries3D)
            {
                series.Clear();
            }
            m_chart.EndUpdate();
        }
    }
}
