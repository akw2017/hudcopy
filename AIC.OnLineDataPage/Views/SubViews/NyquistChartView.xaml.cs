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
using AIC.Core.Events;

namespace AIC.OnLineDataPage.Views.SubViews
{
    /// <summary>
    /// Interaction logic for TimeDomainOnLineView.xaml
    /// </summary>
    public partial class NyquistChartView : ChartViewBase
    {
        private LightningChartUltimate m_chart;
        private const int iCount = 100;
        private bool resetPointSeries;

        public NyquistChartView()
        {
            m_chart = null;
            InitializeComponent();
            //CreateChart();
            divCombobox.SelectionChanged += divCombobox_SelectionChanged;
            divCheckBox.Checked += divCheckBox_Checked;
            divCheckBox.Unchecked += divCheckBox_Unchecked;
            showRPMCheckBox.Checked += showRPMCheckBox_Checked;
            showRPMCheckBox.Unchecked += showRPMCheckBox_Unchecked;
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

        protected override void ViewModel_Opened(object sender, EventArgs e)
        {
            base.ViewModel_Opened(sender, e);
            CreateChart();
        }

        protected override void ViewModel_SignalChanged()
        {
            try
            {
                m_chart.BeginUpdate();
                m_chart.ViewPolar.Markers.Clear();
                if (ViewModel.Signal is BaseDivfreSignal)
                {
                    divCombobox.ItemsSource = ((BaseDivfreSignal)ViewModel.Signal).DivFres;
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-伯德-信号变换", ex));
            }
            finally
            {
                m_chart.EndUpdate();
            }
        }

        void showRPMCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            m_chart.BeginUpdate();
            foreach (var marker in m_chart.ViewPolar.Markers)
            {
                marker.Label.Visible = false;
            }
            m_chart.EndUpdate();
        }

        void showRPMCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            m_chart.BeginUpdate();
            foreach (var marker in m_chart.ViewPolar.Markers)
            {
                marker.Label.Visible = true;
            }
            m_chart.EndUpdate();
        }

        void divCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            resetPointSeries = true;
        }

        void divCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            resetPointSeries = true;
        }

        void divCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetPointSeries = true;
        }

        protected override void UpdateChart(object args)
        {
            try
            {
                if (ViewModel == null || ViewModel.Signal == null)
                {
                    return;
                }

                m_chart.BeginUpdate();
                BaseWaveSignal signal = (BaseWaveSignal)ViewModel.Signal;
                PolarEventMarker marker = new PolarEventMarker();
                marker.Symbol.Shape = Shape.Circle;
                marker.Label.Text = signal.RPM.ToString("0");
                if ((bool)showRPMCheckBox.IsChecked)
                {
                    marker.Label.Visible = true;
                }
                else
                {
                    marker.Label.Visible = false;
                }
                marker.MouseOverOn += new MouseEventHandler(marker_MouseOverOn);
                marker.MouseOverOff += new MouseEventHandler(marker_MouseOverOff);
                if ((bool)divCheckBox.IsChecked && (double)multiplierUpDown.Value > 0)
                {
                    double divx = (signal.RPM / 60) * (double)multiplierUpDown.Value;
                    double intervalx = signal.SampleFre / signal.SamplePoint;
                    double indexx = divx / intervalx;
                    double minx = Math.Floor(indexx);
                    double maxx = Math.Ceiling(indexx);
                    int index;
                    if (signal.Amplitude[(int)maxx] > signal.Amplitude[(int)minx])
                    {
                        index = (int)maxx;
                    }
                    else
                    {
                        index = (int)minx;
                    }
                    marker.Amplitude = signal.Amplitude[index];
                    marker.AngleValue = signal.Phase[index];
                    m_chart.ViewPolar.Markers.Add(marker);
                }
                else
                {
                    DivFreSignal divFre = divCombobox.SelectedItem as DivFreSignal;
                    if (divFre != null)
                    {
                        marker.Amplitude = divFre.FreMV;
                        marker.AngleValue = divFre.Phase;
                        m_chart.ViewPolar.Markers.Add(marker);
                    }
                }

                AxisPolar axis = m_chart.ViewPolar.Axes[0];
                if (marker.Amplitude > axis.MaxAmplitude)
                {
                    axis.MaxAmplitude = marker.Amplitude * 1.5;
                };

                if (resetPointSeries)
                {
                    m_chart.ViewPolar.Markers.Clear();
                    resetPointSeries = false;
                }

                if (m_chart.ViewPolar.Markers.Count > iCount)
                {
                    marker = m_chart.ViewPolar.Markers[0];
                    marker.MouseOverOff -= marker_MouseOverOff;
                    marker.MouseOverOn -= marker_MouseOverOn;
                    m_chart.ViewPolar.Markers.Remove(marker);
                }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-奈奎斯特", ex));
                m_chart.EndUpdate();
            }
            finally
            {
                
            }
        }

        private void marker_MouseOverOff(object sender, MouseEventArgs e)
        {
            if (!(bool)showRPMCheckBox.IsChecked)
            {
                ((PolarEventMarker)sender).Label.Visible = false;
            }
        }

        private void marker_MouseOverOn(object sender, MouseEventArgs e)
        {
            if (!(bool)showRPMCheckBox.IsChecked)
            {
                ((PolarEventMarker)sender).Label.Visible = true;
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
            m_chart.Title.Text = string.Empty;

            //Sisable rendering, strongly recommended before updating chart properties
            m_chart.BeginUpdate();

            //Set active view to Polar
            m_chart.ActiveView = ActiveView.ViewPolar;
            m_chart.ViewPolar.LegendBox.Visible = false;

            m_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            m_chart.ChartBackground.Color = Color.FromArgb(0, 0, 0, 0);
            m_chart.ChartBackground.GradientFill = GradientFill.Solid;
            //m_chart.ViewXY.GraphBackground.Color = Color.FromArgb(0, 0, 0, 0);
            //m_chart.ViewXY.GraphBackground.GradientFill = GradientFill.Solid;
            //m_chart.ViewXY.GraphBorderColor = Color.FromArgb(0, 0, 0, 0);

            //Create polar axis
            AxisPolar axis = m_chart.ViewPolar.Axes[0];
            axis.MajorGrid.Visible = true;
            axis.MinorGrid.Visible = true;
            axis.Title.Text = string.Empty;
            axis.TickMarkLocation = RoundGridTickmarkLocation.Outside;
            axis.Reversed = true;
            axis.InnerCircleRadiusPercentage = 10;
            axis.MajorDivCount = 4;
            axis.MinAmplitude = 0;
            axis.MaxAmplitude = 1;
            axis.Visible = true;
            //Empty polar line series
            // m_chart.ViewPolar.PointLineSeries.Clear();
            m_chart.ViewPolar.Markers.Clear();

            //PointLineSeriesPolar plsp = new PointLineSeriesPolar(m_chart.ViewPolar, axis);
            //plsp.PointsVisible = true;
            //plsp.LineVisible = false;
            //plsp.PointStyle.Width = 5;
            //plsp.PointStyle.Height = 5;

            ////Add series to view
            //m_chart.ViewPolar.PointLineSeries.Add(plsp);

            //Allow chart rendering
            m_chart.EndUpdate();

            gridChart.Children.Add(m_chart);
        }

        private void clearBufferBtn_Click(object sender, RoutedEventArgs e)
        {
            m_chart.BeginUpdate();
            m_chart.ViewPolar.Markers.Clear();
            m_chart.EndUpdate();
        }
        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            m_chart.CopyToClipboard(ClipboardImageFormat.Jpg);
        }
    }
}
