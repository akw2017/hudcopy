using AIC.AkkaSystem.Messages;
using Akka.Actor;
using Arction.Wpf.Charting;
using Arction.Wpf.Charting.Annotations;
using Arction.Wpf.Charting.SeriesXY;
using Arction.Wpf.Charting.Views.ViewXY;
using System;
using System.Linq;
using System.Text;

namespace AIC.AkkaSystem.Actors
{
    public class ChartFrequencyActor : ReceiveActor
    {
        private readonly LightningChartUltimate m_chart;

        public ChartFrequencyActor(LightningChartUltimate chart)
        {
            m_chart = chart;
            Receive<VibrationMessage>(message => HandleVibrationMessage(message));
        }

        private void HandleVibrationMessage(VibrationMessage message)
        {
            try
            {
                m_chart.BeginUpdate();

                var fftLength = message.Frequency.Length;
                var series = m_chart.ViewXY.PointLineSeries[0];
                var phaseSeries = m_chart.ViewXY.PointLineSeries[1];

                if (series.Points == null || series.Points.Length != fftLength)
                {
                    series.Points = new SeriesPoint[fftLength];
                }
                if (phaseSeries.Points == null || phaseSeries.Points.Length != fftLength)
                {
                    phaseSeries.Points = new SeriesPoint[fftLength];
                }
                for (int i = 0; i < fftLength; i++)
                {
                    series.Points[i].X = message.Frequency[i];
                    series.Points[i].Y = message.Amplitude[i];

                    phaseSeries.Points[i].X = message.Frequency[i];
                    phaseSeries.Points[i].Y = message.Phase[i];
                }

                if (m_chart.ViewXY.Annotations[1].Visible)
                {
                    LineSeriesCursor lineSeriesCursor = m_chart.ViewXY.LineSeriesCursors[0];
                    int index = GetNearestPointIndex(series, lineSeriesCursor.ValueAtXAxis);
                    if (index == -1)
                    {
                        m_chart.ViewXY.Annotations[1].Text = string.Empty;
                    }
                    else
                    {
                        SeriesPoint point = series.Points[index];
                        m_chart.ViewXY.Annotations[1].Text = string.Format("幅值:{0}", Math.Round(point.Y, 3)) + "\r\n" + string.Format("频率:{0}", Math.Round(point.X, 3));
                    }
                }

                m_chart.ViewXY.PointLineSeries[0].InvalidateData();
                m_chart.ViewXY.PointLineSeries[1].InvalidateData();
                AnnotationXY spectrumAnnotation = m_chart.ViewXY.Annotations[0];
                StringBuilder spectrumSB = new StringBuilder();
                spectrumSB.AppendLine("频率" + "  " + "幅值");

                var fftValuesDict = message.Amplitude.Select((s, i) => new { Key = i, Value = s }).OrderByDescending(o => o.Value).Take(6);
                foreach (var item in fftValuesDict)
                {
                    spectrumSB.AppendLine(message.Frequency[item.Key].ToString("0.00") + "; " + item.Value.ToString("0.00"));
                }

                spectrumAnnotation.Text = spectrumSB.ToString().Trim();

                //if (fitViewCheckBox.IsChecked == true)
                //{
                    m_chart.ViewXY.ZoomToFit();
               // }
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                m_chart.EndUpdate();
            }
        }

        private int GetNearestPointIndex(PointLineSeries series, double xValue)
        {
            double x = 0;
            double y = 0;
            double yValue = 0;
            int index = 0;
            LineSeriesValueSolveResult lssvs = series.SolveYValueAtXValue(xValue);
            if (lssvs.SolveStatus == LineSeriesSolveStatus.OK)
            {
                //PointLineSeries may have two or more points at same X value. If so, center it between min and max 
                yValue = (lssvs.YMax + lssvs.YMin) / 2.0;
                series.SolveNearestDataPointByValue(xValue, yValue, out x, out y, out index);
                return index;
            }
            else
            {
                return -1;
            }
        }
    }
}
