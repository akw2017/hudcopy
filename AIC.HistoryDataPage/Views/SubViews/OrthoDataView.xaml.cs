using AIC.HistoryDataPage.ViewModels;
using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.SeriesXY;
using AIC.Core;

namespace AIC.HistoryDataPage.Views
{
    /// <summary>
    /// Interaction logic for MultiDivFreOnLineView.xaml
    /// </summary>
    public partial class OrthoDataView : DisposableUserControl
    {
        private LightningChartUltimate m_chart;
        //private VibrationSignal relativedSignal;//htzk123
        private float[] relaFFTValues = new float[1024];
        private float[] relaPhases = new float[1024];
        private IDisposable orthoDataChangedSubscription;
        public OrthoDataView()
        {
            InitializeComponent();
            CreateChart();
            relativeCombobox.SelectionChanged += relativeCombobox_SelectionChanged;
            Loaded += OrthoDataView_Loaded;
        }

        private OrthoDataViewModel ViewModel
        {
            get { return DataContext as OrthoDataViewModel; }
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

        void OrthoDataView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OrthoDataView_Loaded;
            if (ViewModel != null)
            {
                orthoDataChangedSubscription = ViewModel.WhenOrthoDataChanged.Subscribe(OnOrthoDataChanged);
                ViewModel.Closed += ViewModel_Closed;
            }
        }

        private void OnOrthoDataChanged(OrthoDataChangedEventArgs args)
        {
            double[] xArray = args.XArray;
            double[] yArray = args.YArray;
            if (xArray.Length == yArray.Length && xArray.Length!=0)
            {
                m_chart.BeginUpdate();
                FreeformPointLineSeries series = m_chart.ViewXY.FreeformPointLineSeries[0];
                if (series.Points == null || series.Points.Length != xArray.Length)
                {
                    series.Points = new SeriesPoint[xArray.Length];
                }
                for (int i = 0; i < xArray.Length; i++)
                {
                    series.Points[i].X = xArray[i];
                    series.Points[i].Y = yArray[i];
                }
                series.InvalidateData();
                m_chart.ViewXY.ZoomToFit();
                m_chart.EndUpdate();
            }
            else
            {
                m_chart.BeginUpdate();
                m_chart.ViewXY.FreeformPointLineSeries[0].Clear();
                m_chart.EndUpdate();
            }
        }

        void relativeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //relativedSignal = ((ComboBox)sender).SelectedItem as VibrationSignal;//htzk123
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
            m_chart.BeginUpdate();
            m_chart.Title.Text = "";
            m_chart.ViewXY.AxisLayout.YAxisTitleAutoPlacement = false;
            m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Stacked;
            m_chart.ViewXY.LegendBoxes[0].Visible = false;

            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            m_chart.ViewXY.XAxes[0].ValueType = AxisValueType.Number;
            m_chart.ViewXY.XAxes[0].Title.Visible = false;
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].AxisThickness = 2;
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            m_chart.ViewXY.YAxes[0].Title.Visible = false;
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].AxisThickness = 2;
            m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);//Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].LabelsFont = new WpfFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            FreeformPointLineSeries series = new FreeformPointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            series.MouseInteraction = false;
            series.LineStyle.AntiAliasing = LineAntialias.None;
            series.LineStyle.Width = 1;

            m_chart.ViewXY.FreeformPointLineSeries.Add(series);

            m_chart.ViewXY.ZoomToFit();
            m_chart.EndUpdate();
            gridChart.Children.Add(m_chart);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_chart.BeginUpdate();
            foreach (var series in m_chart.ViewXY.FreeformPointLineSeries)
            {
                series.Clear();
            }
            m_chart.EndUpdate();
        }

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
