using AIC.Cloud.Applications;
using AIC.Cloud.Applications.Events;
using AIC.Cloud.Applications.Services;
using AIC.Cloud.DataReplayer.ViewModels;
using Arction.WPF.LightningChartUltimate;
using Arction.WPF.LightningChartUltimate.Series3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIC.Cloud.DataReplayer.Views
{
    /// <summary>
    /// Interaction logic for RPM3DSpectrumDataView.xaml
    /// </summary>
    public partial class RPM3DSpectrumDataView : UserControl
    {
        private SynchronizationContext uiContext = SynchronizationContext.Current; 
        private LightningChartUltimate rpm3Dchart;
        private SurfaceGridSeries3D m_surface;
        private WaterfallSeries3D m_waterfallFront;

        private IDisposable rpm3DSpectrumSubscrible = null;

        public RPM3DSpectrumDataView()
        {
            InitializeComponent();
            CreateRPM3DChart();
            Loaded += RPM3DSpectrumView_Loaded;
            Unloaded += RPM3DSpectrumDataView_Unloaded;
        }

        private RPM3DSpectrumDataViewModel ViewModel { get; set; }

        private　void RPM3DSpectrumView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = DataContext as RPM3DSpectrumDataViewModel;
            if (ViewModel != null)
            {
                rpm3Dchart.BeginUpdate();
                m_surface.SetSize(ViewModel.SizeX, ViewModel.SizeY);
                rpm3Dchart.EndUpdate();
                if (rpm3DSpectrumSubscrible != null)
                {
                    rpm3DSpectrumSubscrible.Dispose();
                }
                rpm3DSpectrumSubscrible = ViewModel.WhenPropertyChanged.Where(o => o.ToString() == "RPM3DSpectrumData").ObserveOn(uiContext).Subscribe(OnRPM3DSpectrumDataChanged);
            }
        }

        private void RPM3DSpectrumDataView_Unloaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                if (rpm3DSpectrumSubscrible != null)
                {
                    rpm3DSpectrumSubscrible.Dispose();
                    rpm3DSpectrumSubscrible = null;
                }
                ViewModel = null;
            }
        }

        private void OnRPM3DSpectrumDataChanged(string propertyName)
        {
            UpdateChart(ViewModel.RPM3DSpectrumData);
        }

        private void UpdateChart(RPM3DSpectrumObject rpm3DObj)
        {
            try
            {
                rpm3Dchart.BeginUpdate();

                m_surface.Clear();
                RPM3DSpectrumObject rpm3DSpectrum = rpm3DObj;
                if (rpm3DSpectrum != null)
                {
                    Dictionary<Tuple<double, double>, double[]> dataSource = rpm3DSpectrum.DataSource;

                    rpm3Dchart.View3D.XAxisPrimary3D.SetRange(rpm3DSpectrum.RangeMinX, rpm3DSpectrum.RangeMaxX);
                    rpm3Dchart.View3D.YAxisPrimary3D.SetRange(rpm3DSpectrum.RangeMinY, rpm3DSpectrum.RangeMaxY * 1.2);
                    rpm3Dchart.View3D.ZAxisPrimary3D.SetRange(rpm3DSpectrum.RangeMinZ, rpm3DSpectrum.RangeMaxZ);

                    m_surface.SetRangesXZ(rpm3Dchart.View3D.XAxisPrimary3D.Minimum, rpm3Dchart.View3D.XAxisPrimary3D.Maximum,
                        rpm3Dchart.View3D.ZAxisPrimary3D.Minimum, rpm3Dchart.View3D.ZAxisPrimary3D.Maximum);

                    m_surface.ContourPalette = CreatePalette(m_surface, rpm3DSpectrum.RangeMaxY);

                    double dXAxisMin = rpm3Dchart.View3D.XAxisPrimary3D.Minimum;
                    double dXAxisMax = rpm3Dchart.View3D.XAxisPrimary3D.Maximum;
                    double dZAxisMin = rpm3Dchart.View3D.ZAxisPrimary3D.Minimum;
                    double dZAxisMax = rpm3Dchart.View3D.ZAxisPrimary3D.Maximum;
                    double dStepX = (dXAxisMax - dXAxisMin) / (m_surface.SizeX - 1);
                    double dStepZ = (dZAxisMax - dZAxisMin) / (m_surface.SizeZ - 1);

                    foreach (var item in dataSource)
                    {
                        double z = item.Key.Item1;
                        int nodeZ = (int)Math.Round((z - dZAxisMin) / dStepZ);
                        for (int i = 0; i < item.Value.Length; i++)
                        {
                            double x = item.Key.Item2 * i;
                            int nodeX = (int)Math.Round((x - dXAxisMin) / dStepX);

                            if (m_surface.Data[nodeX, nodeZ].Y < item.Value[i])
                            {
                                m_surface.Data[nodeX, nodeZ].Y = item.Value[i];
                            }
                        }
                    }
                }
                rpm3Dchart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-转速三维谱", ex));
                rpm3Dchart.EndUpdate();
            }
        }

        private void CreateRPM3DChart()
        {
            // Clear any timeGrid3DChart's children.
            rpmGrid3DChart.Children.Clear();
            if (rpm3Dchart != null)
            {
                // If a chart is already created, dispose it.
                rpm3Dchart.Dispose();
                rpm3Dchart = null;
            }

            // Create a new chart.
            rpm3Dchart = new LightningChartUltimate(LicenseKeyStrings.LightningChartUltimate);
            rpm3Dchart.ChartName = "Spectrum 3D chart";
            rpm3Dchart.Title.Text = string.Empty;

            //Disable rendering, strongly recommended before updating chart properties
            rpm3Dchart.BeginUpdate();

            //Set 3D as active view
            rpm3Dchart.ActiveView = ActiveView.View3D;

            //Setup background
            rpm3Dchart.ChartBackground.GradientColor = Colors.Black;
            rpm3Dchart.ChartBackground.Color = Colors.DimGray;
            rpm3Dchart.ChartBackground.GradientFill = GradientFill.Radial;

            //Setup LegendBox
            rpm3Dchart.View3D.LegendBox.Layout = LegendBoxLayout.VerticalColumnSpan;
            rpm3Dchart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
            rpm3Dchart.View3D.LegendBox.SurfaceScales.ScaleSizeDim1 = 150;
            rpm3Dchart.View3D.LegendBox.SurfaceScales.ScaleSizeDim2 = 10;
            rpm3Dchart.View3D.LegendBox.ShowCheckboxes = false;

            //Hide all walls but bottom
            rpm3Dchart.View3D.WallOnBack.Visible = false;
            rpm3Dchart.View3D.WallOnLeft.Visible = false;
            rpm3Dchart.View3D.WallOnRight.Visible = false;
            rpm3Dchart.View3D.WallOnTop.Visible = false;
            rpm3Dchart.View3D.WallOnFront.Visible = false;
            rpm3Dchart.View3D.WallOnBottom.Visible = true;

            //Setup primary x-axis
            rpm3Dchart.View3D.XAxisPrimary3D.Orientation = PlaneXAxis3D.XY;
            rpm3Dchart.View3D.XAxisPrimary3D.CornerAlignment = AxisAlignment3D.Outside;
            rpm3Dchart.View3D.XAxisPrimary3D.MajorDivTickStyle.Alignment = Alignment.Far;
            rpm3Dchart.View3D.XAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            rpm3Dchart.View3D.XAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            rpm3Dchart.View3D.XAxisPrimary3D.Title.Text = "频率 (Hz)";
            rpm3Dchart.View3D.XAxisPrimary3D.Title.Color = Colors.Yellow;
            rpm3Dchart.View3D.XAxisPrimary3D.SetRange(0, 0);

            //Setup primary y-axis
            rpm3Dchart.View3D.YAxisPrimary3D.Orientation = PlaneYAxis3D.XY;
            rpm3Dchart.View3D.YAxisPrimary3D.CornerAlignment = AxisAlignment3D.Outside;
            rpm3Dchart.View3D.YAxisPrimary3D.MajorDivTickStyle.Alignment = Alignment.Far;
            rpm3Dchart.View3D.YAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            rpm3Dchart.View3D.YAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            rpm3Dchart.View3D.YAxisPrimary3D.Title.Text = "幅值 P(f)";
            rpm3Dchart.View3D.YAxisPrimary3D.Title.Color = Colors.Yellow;
            rpm3Dchart.View3D.YAxisPrimary3D.SetRange(0, 0);
            rpm3Dchart.View3D.YAxisPrimary3D.LabelsNumberFormat = "0.00";

            //Setup primary z-axis
            rpm3Dchart.View3D.ZAxisPrimary3D.Reversed = true;
            rpm3Dchart.View3D.ZAxisPrimary3D.LabelsColor = Color.FromArgb(200, 255, 255, 255);
            rpm3Dchart.View3D.ZAxisPrimary3D.Title.Text = "转速";
            rpm3Dchart.View3D.ZAxisPrimary3D.Title.Color = Colors.Yellow;
            rpm3Dchart.View3D.ZAxisPrimary3D.ValueType = AxisValueType.Number;
            rpm3Dchart.View3D.ZAxisPrimary3D.MajorDivTickStyle.Color = Colors.Orange;
            rpm3Dchart.View3D.WallOnBottom.GridStrips = WallGridStripXZ.X;

            //Setup legend box
            rpm3Dchart.View3D.LegendBox.SeriesTitleColor = Colors.White;
            rpm3Dchart.View3D.LegendBox.ValueLabelColor = Colors.White;
            rpm3Dchart.View3D.LegendBox.SurfaceScales.ScaleBorderColor = Colors.White;
            rpm3Dchart.View3D.LegendBox.Position = LegendBoxPosition.TopRight;
            rpm3Dchart.View3D.LegendBox.Offset.SetValues(0, 0);
            rpm3Dchart.View3D.LegendBox.Fill.Style = RectFillStyle.None;
            rpm3Dchart.View3D.LegendBox.Shadow.Visible = false;
            rpm3Dchart.View3D.LegendBox.BorderWidth = 0;

            //Setup camera            
            rpm3Dchart.View3D.Camera.RotationX = 18.6;
            rpm3Dchart.View3D.Camera.RotationY = -23.6;
            rpm3Dchart.View3D.Camera.RotationZ = 0;
            rpm3Dchart.View3D.Camera.Target.SetValues(-9.5f, -10f, -5.8f);
            rpm3Dchart.View3D.Camera.ViewDistance = 163;

            //double dAxisZMin = updateRate / 1000.0;
            //double dAxisZMax = 100 * updateRate / 1000.0;
            //m_dStepZ = updateRate / 1000.0;

            //rpm3Dchart.View3D.ZAxisPrimary3D.SetRange(dAxisZMin, dAxisZMax);
            //m_dCurrentZ = dAxisZMax;

            //Add Surface
            CreateSurface();
            //Add Waterfall
            //CreateWaterfall();
            //Add WaterFall Front

          //  CreateWaterfallFront();

            //Allow chart rendering
            rpm3Dchart.EndUpdate();

            rpmGrid3DChart.Children.Add(rpm3Dchart);
        }

        private void CreateSurface()
        {
            if (m_surface == null)
            {
                m_surface = new SurfaceGridSeries3D(rpm3Dchart.View3D, Axis3DBinding.Primary, Axis3DBinding.Primary, Axis3DBinding.Primary);
                rpm3Dchart.View3D.SurfaceGridSeries3D.Add(m_surface);
            }
            m_surface.InitialValue = 0;
            m_surface.Title.Text = string.Empty;
            m_surface.WireframeType = SurfaceWireframeType.None;
            m_surface.ContourLineType = ContourLineType.None;
            m_surface.FadeAway = 0;
            m_surface.SuppressLighting = false;
            m_surface.BaseColor = Colors.White;
            m_surface.ContourPalette = CreatePalette(m_surface, 10);
        }

        private void CreateWaterfallFront()
        {
            if (m_waterfallFront == null)
            {
                m_waterfallFront = new WaterfallSeries3D(rpm3Dchart.View3D, Axis3DBinding.Primary, Axis3DBinding.Primary, Axis3DBinding.Primary);
                rpm3Dchart.View3D.WaterfallSeries3D.Add(m_waterfallFront);
            }

            // m_waterfallFront.SetSize(ViewModel.SgProcessor.FFTLength, 1);
            m_waterfallFront.BaseLevel = rpm3Dchart.View3D.YAxisPrimary3D.Minimum;
            m_waterfallFront.FadeAway = 0;
            m_waterfallFront.SuppressLighting = false;
            m_waterfallFront.BaseColor = Colors.White;
            m_waterfallFront.ShowInLegendBox = false;

            //Init one row
            SurfacePoint[,] areaData = m_waterfallFront.Data;
            int iColCount = m_waterfallFront.SizeX;
            double dX = rpm3Dchart.View3D.XAxisPrimary3D.Minimum;
            double dStepX;
            if (iColCount > 1)
                dStepX = (rpm3Dchart.View3D.XAxisPrimary3D.Maximum - rpm3Dchart.View3D.XAxisPrimary3D.Minimum) / (double)(iColCount - 1);
            else
                dStepX = 0;
            double dZ = rpm3Dchart.View3D.ZAxisSecondary3D.Maximum;
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

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            rpm3Dchart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
