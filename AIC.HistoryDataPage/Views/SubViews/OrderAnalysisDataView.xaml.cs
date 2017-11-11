using AIC.Core.Events;
using AIC.HistoryDataPage.ViewModels;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Series3D;
using Arction.Wpf.Charting.Views.View3D;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace AIC.HistoryDataPage.Views
{
    /// <summary>
    /// Interaction logic for OrderAnalysisDataView.xaml
    /// </summary>
    public partial class OrderAnalysisDataView : UserControl
    {
        private SynchronizationContext uiContext = SynchronizationContext.Current; 
        int iColumnCount = 60;
        int iRowCount = 100;
        private SurfaceMeshSeries3D m_series;
        private LightningChartUltimate m_chart;
        private OrderAnalysisDataViewModel ViewModel;
        private IDisposable orderAnalysisDataSubscrible;
        

        public OrderAnalysisDataView()
        {
            InitializeComponent();
            CreateChart();
            Loaded += OrderAnalysisDataView_Loaded;
        }

        void OrderAnalysisDataView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OrderAnalysisDataView_Loaded;
            ViewModel = DataContext as OrderAnalysisDataViewModel;
            if (ViewModel != null)
            {
                if (orderAnalysisDataSubscrible != null)
                {
                    orderAnalysisDataSubscrible.Dispose();
                }
                orderAnalysisDataSubscrible = ViewModel.WhenPropertyChanged.Where(o => o.ToString() == "OrderAnalysisData").ObserveOn(uiContext).Subscribe(OnOrderAnalysisDataChanged);
            }
        }

        private void OnOrderAnalysisDataChanged(string propertyName)
        {
            UpdateChart(ViewModel.OrderAnalysisData);
        }

        private void UpdateChart(OrderAnalysisObject order)
        {
            try
            {
                if (order == null) return;
                m_chart.BeginUpdate();

                m_chart.View3D.XAxisPrimary3D.SetRange(0, order.MaxXValue);
                m_chart.View3D.YAxisPrimary3D.SetRange(0, order.MaxYValue);
                m_series.ContourPalette = CreatePalette(m_series, order.MaxYValue);
                m_chart.View3D.ZAxisPrimary3D.SetRange(0, order.MaxZValue);

                //m_series.Data = order.SurfacePointArray;//htzk123           
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据回放-阶次分析", ex));
                m_chart.EndUpdate();
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

            //Allow chart rendering
            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);
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
            m_chart.View3D.Camera.RotationY = 0;
            m_chart.View3D.Camera.OrthographicCamera = checkBoxOrthographic.IsChecked.Value;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            checkBoxOrthographic.IsChecked = true;
            m_chart.View3D.Camera.RotationX = 0;
            m_chart.View3D.Camera.RotationY = 90;
            m_chart.View3D.Camera.RotationY = 0;
            m_chart.View3D.Camera.OrthographicCamera = checkBoxOrthographic.IsChecked.Value;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            checkBoxOrthographic.IsChecked = true;
            m_chart.View3D.Camera.RotationX = 0;
            m_chart.View3D.Camera.RotationY = 0;
            m_chart.View3D.Camera.RotationY = 90;
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

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
