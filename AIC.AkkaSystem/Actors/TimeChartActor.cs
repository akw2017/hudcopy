using AIC.AkkaSystem.Messages;
using Akka.Actor;
using Arction.Wpf.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.AkkaSystem.Actors
{
    public class TimeChartActor: ReceiveActor
    {

        private readonly LightningChartUltimate m_chart;
        public TimeChartActor(LightningChartUltimate chart)
        {
            m_chart = chart;
            Receive<VibrationMessage>(message => HandleVibrationMessage(message));
        }


        private void HandleVibrationMessage(VibrationMessage message)
        {
            try
            {
                m_chart.BeginUpdate();
                var vParm = message.VParm;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("有效值:{0}", vParm.AMS.ToString("0.00")));
                sb.AppendLine(string.Format("峰值:{0}", vParm.PeakValue.ToString("0.00")));
                sb.AppendLine(string.Format("峰峰值:{0}", vParm.PeakPeakValue.ToString("0.00")));
                sb.AppendLine(string.Format("斜度:{0}", vParm.Slope.ToString("0.00")));
                sb.AppendLine(string.Format("峭度:{0}", vParm.Kurtosis.ToString("0.00")));
                sb.AppendLine(string.Format("峭度指标:{0}", vParm.KurtosisIndex.ToString("0.00")));
                sb.AppendLine(string.Format("波形指标:{0}", vParm.WaveIndex.ToString("0.00")));
                sb.AppendLine(string.Format("峰值指标:{0}", vParm.PeakIndex.ToString("0.00")));
                sb.AppendLine(string.Format("脉冲指标:{0}", vParm.ImpulsionIndex.ToString("0.00")));
                sb.AppendLine(string.Format("裕度指标:{0}", vParm.ToleranceIndex.ToString("0.00")));


                if (message.IsCepstrum)
                {
                    m_chart.ViewXY.SampleDataSeries[0].SamplingFrequency = message.SampleFre / 1000;
                }
                else
                {
                    m_chart.ViewXY.SampleDataSeries[0].SamplingFrequency = 1;
                }
                m_chart.ViewXY.SampleDataSeries[0].SamplesDouble = message.VData;
                m_chart.ViewXY.SampleDataSeries[0].InvalidateData();
                m_chart.ViewXY.Annotations[0].Text = sb.ToString().Trim();
                //if (fitViewCheckBox.IsChecked == true)
                //{
                    m_chart.ViewXY.ZoomToFit();
                //}
                m_chart.EndUpdate();
            }
            catch (Exception ex)
            {
                m_chart.EndUpdate();
            }
        }
    }
}
