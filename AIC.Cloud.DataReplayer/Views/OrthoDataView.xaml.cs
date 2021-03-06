﻿using AIC.Cloud.Applications;
using AIC.Cloud.DataReplayer.ViewModels;
using AIC.Cloud.Domain;
using AIC.CoreType;
using AIC.Server.Storage.Contract;
using Arction.WPF.LightningChartUltimate;
using Arction.WPF.LightningChartUltimate.Annotations;
using Arction.WPF.LightningChartUltimate.Axes;
using Arction.WPF.LightningChartUltimate.EventMarkers;
using Arction.WPF.LightningChartUltimate.SeriesXY;
using Arction.WPF.LightningChartUltimate.Views.ViewXY;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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
    /// Interaction logic for MultiDivFreOnLineView.xaml
    /// </summary>
    public partial class OrthoDataView : UserControl
    {
        private LightningChartUltimate m_chart;
        private VibrationSignal relativedSignal;
        private float[] relaFFTValues = new float[1024];
        private float[] relaPhases = new float[1024];
        private OrthoDataViewModel viewModel;
        private IDisposable orthoDataChangedSubscription;
        public OrthoDataView()
        {
            InitializeComponent();
            CreateChart();
            relativeCombobox.SelectionChanged += relativeCombobox_SelectionChanged;
            Loaded += OrthoDataView_Loaded;
        }

        void OrthoDataView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OrthoDataView_Loaded;
            viewModel = DataContext as OrthoDataViewModel;
            if (viewModel != null)
            {
                orthoDataChangedSubscription = viewModel.WhenOrthoDataChanged.Subscribe(OnOrthoDataChanged);
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
                m_chart.ViewXY.FitView();
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
            relativedSignal = ((ComboBox)sender).SelectedItem as VibrationSignal;
        }

        private void CreateChart()
        {
            gridChart.Children.Clear();
            if (m_chart != null)
            {
                m_chart.Dispose();
                m_chart = null;
            }

            m_chart = new LightningChartUltimate(LicenseKeyStrings.LightningChartUltimate);
            m_chart.BeginUpdate();
            m_chart.Title.Text = "";
            m_chart.ViewXY.AxisLayout.YAxisTitleAutoPlacement = false;
            m_chart.ViewXY.AxisLayout.YAxesLayout = YAxesLayout.Stacked;
            m_chart.ViewXY.LegendBox.Visible = false;

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
            m_chart.ViewXY.XAxes[0].AxisColor = Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.XAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.XAxes[0].LabelsFont = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            m_chart.ViewXY.YAxes[0].Title.Visible = false;
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].AxisThickness = 2;
            m_chart.ViewXY.YAxes[0].AxisColor = Color.FromArgb(100, 135, 205, 238);
            m_chart.ViewXY.YAxes[0].MinorGrid.Visible = false;
            m_chart.ViewXY.YAxes[0].LabelsFont = new WPFFont(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Regular);

            FreeformPointLineSeries series = new FreeformPointLineSeries(m_chart.ViewXY, m_chart.ViewXY.XAxes[0], m_chart.ViewXY.YAxes[0]);
            series.MouseInteraction = false;
            series.LineStyle.AntiAliasing = LineAntialias.None;
            series.LineStyle.Width = 1;

            m_chart.ViewXY.FreeformPointLineSeries.Add(series);

            m_chart.ViewXY.FitView();
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
