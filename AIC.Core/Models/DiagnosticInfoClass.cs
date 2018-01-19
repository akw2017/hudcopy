using AIC.Core.Events;
using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.M9600.Common.DTO.Device;
using AIC.M9600.Common.SlaveDB.Generated;
using AIC.MatlabMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Models
{
    public class DiagnosticInfoClass
    {
        public static void GetDiagnosticInfo(BaseWaveSignal vSg)
        {
            vSg.DiagnosticGrade = vSg.AlarmGrade;
            vSg.DiagnosticResult = vSg.Result;
            vSg.DiagnosticTime = vSg.ACQDatetime;
            vSg.DiagnosticUnit = vSg.Unit;

            if (vSg.Waveform == null || vSg.SampleFre == 0 || vSg.SamplePoint == 0)
            {
                vSg.DiagnosticInfo = "没有诊断波形";
                vSg.DiagnosticAdvice = null;         
                return;
            }

            if (vSg.RPM == 0)
            {
                vSg.DiagnosticInfo = "没有上传转速";
                vSg.DiagnosticAdvice = null;
                return;
            }

            if (vSg.AlarmGrade == AlarmGrade.Invalid || vSg.AlarmGrade == AlarmGrade.Normal || vSg.AlarmGrade == AlarmGrade.DisConnect)
            {
                vSg.DiagnosticInfo = "没有发现故障";
                vSg.DiagnosticAdvice = null;
                return;
            }

            try
            {
                double[] Frequency;
                double[] Amplitude;

                //频率间隔
                double frequencyInterval = vSg.SampleFre / vSg.SamplePoint;
                int length = (int)(vSg.SamplePoint / 2.56) + 1;
                Frequency = new double[length];

                for (int i = 0; i < length; i++)
                {
                    Frequency[i] = frequencyInterval * i;
                }

                var output = Algorithm.Instance.FFT2AndPhaseAction(vSg.Waveform, vSg.SamplePoint);
                if (output != null)
                {
                    Amplitude = output[0].Take(length).ToArray();
                }
                else
                {
                    vSg.DiagnosticInfo = "没有频域波形";
                    vSg.DiagnosticAdvice = null;
                    return;
                }
                //频率
                double frequency = vSg.RPM / 60;

                double e1, e2, e3, e4, e5;
                double m1, m2, m3, m4, m5;
                GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 1, out e1, out m1);
                GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 2, out e2, out m2);
                GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 3, out e3, out m3);
                GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 4, out e4, out m4);
                GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 5, out e5, out m5);

                //求时域有效值
                var paras = Algorithm.CalculatePara(vSg.Waveform);
                var rms = paras[0];

                //最大幅值
                var mMax = Amplitude.Max();

                if (m1 >= mMax && m1 >= (rms * 0.1 * Math.Sqrt(2)) && m1 > m2 && m3 <= (rms * 0.1 * Math.Sqrt(2)))
                {
                    vSg.DiagnosticInfo = "不平衡";
                    vSg.DiagnosticAdvice = "建议：怀疑轴系的动平衡情况，请检查转子是否存在弯曲或临时弯曲、转子零部件掉块、转子偏磨或结垢、轴承间隙大、设备底板结构松动等故障，如是皮带驱动，可能是皮带轮偏心引起。\r\n";
                    double probability = e1 * 100.0 / rms;
                    vSg.DiagnosticAdvice += "概率：" + probability.ToString("0") + "%";
                }
                else if (m2 >= mMax && m2 >= (rms * 0.1 * Math.Sqrt(2)) && m1 >= (rms * 0.1 * Math.Sqrt(2)) && m3 >= (rms * 0.1 * Math.Sqrt(2)))
                {
                    vSg.DiagnosticInfo = "不对中";
                    vSg.DiagnosticAdvice = "建议：(1)请检查相关轴系的对中情况，如轴与轴的对中不良、同一轴的几个轴承安装同心或轴承间隙不等、 轴承座热膨胀不均、机壳变形或移位、 地基不均匀下沉。\r\n";
                    vSg.DiagnosticAdvice += "          (2)另外，可能以下原因也表现为不对中现象：I.由于质量偏心而引起的动平衡不良(此时转子呈弓形弯曲),也可能造成对中不良，如果此时静态条件下对中良好，可以先解决动平衡不良故障，对中不良故障将随即消除. II.由于受热、负载过大等原因导致轴弯曲;\r\n";
                    vSg.DiagnosticAdvice += "          (3).由于内环或外环安装不合适，导致轴承偏翘（翘曲）. III.轴承座松动。\r\n";
                    double probability = e2 * 100.0 / rms;
                    vSg.DiagnosticAdvice += "概率：" + probability.ToString("0") + "%";
                }
                else if (m1 >= (rms * 0.1 * Math.Sqrt(2)) && m2 >= (rms * 0.1 * Math.Sqrt(2)) && m3 >= (rms * 0.1 * Math.Sqrt(2)) && m4 >= (rms * 0.1 * Math.Sqrt(2)) && m5 >= (rms * 0.1 * Math.Sqrt(2)))
                {
                    vSg.DiagnosticInfo = "轴承座松动、轴承松动、（基础）松动等";
                    vSg.DiagnosticAdvice = "建议：(1)请检查相关轴系的润滑、基础的紧固、相关轴系的轴承、轴承座安装情况.\r\n";
                    vSg.DiagnosticAdvice += "          (2)受热、负载过大等原因导致轴弯曲；\r\n";
                    vSg.DiagnosticAdvice += "          (3)内环或外环安装不合适，导致轴承偏翘（翘曲）。\r\n";
                }
                else if (mMax >= (rms * 0.3 * Math.Sqrt(2)))
                {
                    double fMax = 0;
                    for (int i = 0; i < Amplitude.Length; i++)
                    {
                        if (Amplitude[i] == mMax)
                        {
                            fMax = Frequency[i];
                            break;
                        }
                    }
                    vSg.DiagnosticInfo = "频谱上有占能量很大比率的频率" + fMax.ToString("0.000") + "没有找到合适的故障原因";
                    vSg.DiagnosticAdvice = "建议：(1)没有设备结构模型，比如齿轮齿数、设置轴承型号等，不能确定故障的部位。\r\n";
                    vSg.DiagnosticAdvice += "          (2)转速设置是否正确。\r\n";
                }
                else
                {
                    vSg.DiagnosticInfo = "没有发现故障";
                    vSg.DiagnosticAdvice = null;
                }
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("诊断异常", ex));
            }
        }

        public static string GetDiagnosticInfo(int AlarmGrade, Guid Guid, D_WirelessVibrationSlot_Waveform data, float rpm, out string DiagnosticInfo, out string DiagnosticAdvice)
        {
            if ((AlarmGrade & 0xff) == 0x00 || (AlarmGrade & 0xff) == 0x01)
            {
                DiagnosticInfo = "没有发现故障";
                DiagnosticAdvice = null;
                return null;
            }
            if (data != null && data.WaveData != null)
            {
                return GetDiagnosticInfo(data.WaveData, data.SampleFre.Value, data.SamplePoint.Value, rpm, out DiagnosticInfo, out DiagnosticAdvice);
            }
            else
            {
                DiagnosticInfo = null;
                DiagnosticAdvice = null;
                return null;
            }
        }

        public static string GetDiagnosticInfo(byte[] bytes, double SampleFre, int SamplePoint, float RPM, out string DiagnosticInfo, out string DiagnosticAdvice)
        {
            double[] Waveform = ByteToSingle(bytes);
            if (Waveform == null || SampleFre == 0 || SamplePoint == 0)
            {
                DiagnosticInfo = "没有诊断波形";
                DiagnosticAdvice = null;
                return DiagnosticInfo + "\r\n" + DiagnosticAdvice;
            }

            if (RPM == 0)
            {
                DiagnosticInfo = "没有上传转速";
                DiagnosticAdvice = null;
                return DiagnosticInfo + "\r\n" + DiagnosticAdvice;
            }


            double[] Frequency;
            double[] Amplitude;

            //频率间隔
            double frequencyInterval = SampleFre / SamplePoint;
            int length = (int)(SamplePoint / 2.56) + 1;
            Frequency = new double[length];

            for (int i = 0; i < length; i++)
            {
                Frequency[i] = frequencyInterval * i;
            }

            var output = D2FFT(Waveform, SamplePoint);
            if (output != null)
            {
                Amplitude = output.Take(length).ToArray();
            }
            else
            {
                DiagnosticInfo = "没有频域波形";
                DiagnosticAdvice = null;
                return DiagnosticInfo + "\r\n" + DiagnosticAdvice;
            }
            //频率
            double frequency = RPM / 60;

            double e1, e2, e3, e4, e5;
            double m1, m2, m3, m4, m5;
            GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 1, out e1, out m1);
            GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 2, out e2, out m2);
            GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 3, out e3, out m3);
            GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 4, out e4, out m4);
            GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 5, out e5, out m5);

            //求时域有效值
            var paras = CalculatePara(Waveform);
            var rms = paras[0];

            //最大幅值
            var mMax = Amplitude.Max();

            if (m1 >= mMax && m1 >= (rms * 0.1 * Math.Sqrt(2)) && m1 > m2 && m3 <= (rms * 0.1 * Math.Sqrt(2)))
            {
                DiagnosticInfo =  "不平衡";
                DiagnosticAdvice = "建议：怀疑轴系的动平衡情况，请检查转子是否存在弯曲或临时弯曲、转子零部件掉块、转子偏磨或结垢、轴承间隙大、设备底板结构松动等故障，如是皮带驱动，可能是皮带轮偏心引起。\r\n";
                double probability = e1 * 100.0 / rms;
                DiagnosticAdvice += "概率：" + probability.ToString("0") + "%";
            }
            else if (m2 >= mMax && m2 >= (rms * 0.1 * Math.Sqrt(2)) && m1 >= (rms * 0.1 * Math.Sqrt(2)) && m3 >= (rms * 0.1 * Math.Sqrt(2)))
            {
                DiagnosticInfo = "不对中";
                DiagnosticAdvice = "建议：(1)请检查相关轴系的对中情况，如轴与轴的对中不良、同一轴的几个轴承安装同心或轴承间隙不等、 轴承座热膨胀不均、机壳变形或移位、 地基不均匀下沉。\r\n";
                DiagnosticAdvice += "          (2)另外，可能以下原因也表现为不对中现象：I.由于质量偏心而引起的动平衡不良(此时转子呈弓形弯曲),也可能造成对中不良，如果此时静态条件下对中良好，可以先解决动平衡不良故障，对中不良故障将随即消除. II.由于受热、负载过大等原因导致轴弯曲;\r\n";
                DiagnosticAdvice += "          (3).由于内环或外环安装不合适，导致轴承偏翘（翘曲）. III.轴承座松动。\r\n";
                double probability = e2 * 100.0 / rms;
                DiagnosticAdvice += "概率：" + probability.ToString("0") + "%";
            }
            else if (m1 >= (rms * 0.1 * Math.Sqrt(2)) && m2 >= (rms * 0.1 * Math.Sqrt(2)) && m3 >= (rms * 0.1 * Math.Sqrt(2)) && m4 >= (rms * 0.1 * Math.Sqrt(2)) && m5 >= (rms * 0.1 * Math.Sqrt(2)))
            {
                DiagnosticInfo = "轴承座松动、轴承松动、（基础）松动等\r\n";
                DiagnosticAdvice = "建议：(1)请检查相关轴系的润滑、基础的紧固、相关轴系的轴承、轴承座安装情况.\r\n";
                DiagnosticAdvice += "          (2)受热、负载过大等原因导致轴弯曲；\r\n";
                DiagnosticAdvice += "          (3)内环或外环安装不合适，导致轴承偏翘（翘曲）。\r\n";
            }
            else if (mMax >= (rms * 0.3 * Math.Sqrt(2)))
            {
                double fMax = 0;
                for (int i = 0; i < Amplitude.Length; i++)
                {
                    if (Amplitude[i] == mMax)
                    {
                        fMax = Frequency[i];
                        break;
                    }
                }
                DiagnosticInfo = "频谱上有占能量很大比率的频率" + fMax.ToString("0.000") + "没有找到合适的故障原因\r\n";
                DiagnosticAdvice = "建议：(1)没有设备结构模型，比如齿轮齿数、设置轴承型号等，不能确定故障的部位。\r\n";
                DiagnosticAdvice += "          (2)转速设置是否正确。\r\n";
            }
            else
            {
                DiagnosticInfo = "没有发现故障";
                DiagnosticAdvice = null;
            }
            return DiagnosticInfo + "\r\n" + DiagnosticAdvice;
        }

        //以下不为我用

        public static string GetDiagnosticInfo(int AlarmGrade, string devicename, Guid Guid, D_WirelessVibrationSlot_Waveform[] datas, float rpm, out string DiagnosticInfo, out string DiagnosticAdvice)
        {
            if ((AlarmGrade & 0xff) == 0x00 || (AlarmGrade & 0xff) == 0x01)
            {
                DiagnosticInfo = null;
                DiagnosticAdvice = null;
                return null;
            }
            var data = datas.FirstOrDefault();
            if (data != null && data.WaveData != null)
            {
                return GetDiagnosticInfo(devicename, data.WaveData, data.SampleFre.Value, data.SamplePoint.Value, rpm, out DiagnosticInfo, out DiagnosticAdvice);
            }
            else
            {
                DiagnosticInfo = null;
                DiagnosticAdvice = null;
                return null;
            }
        }
        public static string GetDiagnosticInfo(int AlarmGrade, string devicename, Guid Guid, WirelessVibrationSlotData[] datas, out string DiagnosticInfo, out string DiagnosticAdvice)
        {
            if ((AlarmGrade & 0xff) == 0x00 || (AlarmGrade & 0xff) == 0x01)
            {
                DiagnosticInfo = null;
                DiagnosticAdvice = null;
                return null;
            }
            var data = datas.Where(p => p.T_Item_Guid == Guid).FirstOrDefault();
            if (data != null && data.Waveform != null && data.Waveform.WaveData != null)
            {
                return GetDiagnosticInfo(devicename, data.Waveform.WaveData, data.SampleFre.Value, data.SamplePoint.Value, (float)data.RPM.Value, out DiagnosticInfo, out DiagnosticAdvice);
            }
            else
            {
                DiagnosticInfo = null;
                DiagnosticAdvice = null;
                return null;
            }
        }

        public static string GetDiagnosticInfo(string devicename, byte[] bytes, double SampleFre, int SamplePoint, float RPM, out string DiagnosticInfo, out string DiagnosticAdvice)
        {
            double[] Waveform = ByteToSingle(bytes);
            if (Waveform == null || SampleFre == 0 || SamplePoint == 0)
            {
                DiagnosticInfo = devicename + "没有诊断波形";
                DiagnosticAdvice = null;
                return DiagnosticInfo + "\r\n" + DiagnosticAdvice;
            }

            if (RPM == 0)
            {
                DiagnosticInfo = devicename + "没有上传转速";
                DiagnosticAdvice = null;
                return DiagnosticInfo + "\r\n" + DiagnosticAdvice;
            }


            double[] Frequency;
            double[] Amplitude;

            //频率间隔
            double frequencyInterval = SampleFre / SamplePoint;
            int length = (int)(SamplePoint / 2.56) + 1;
            Frequency = new double[length];

            for (int i = 0; i < length; i++)
            {
                Frequency[i] = frequencyInterval * i;
            }

            var output = D2FFT(Waveform, SamplePoint);
            if (output != null)
            {
                Amplitude = output.Take(length).ToArray();
            }
            else
            {
                DiagnosticInfo = devicename + "没有频域波形";
                DiagnosticAdvice = null;
                return DiagnosticInfo + "\r\n" + DiagnosticAdvice;
            }
            //频率
            double frequency = RPM / 60;

            double e1, e2, e3, e4, e5;
            double m1, m2, m3, m4, m5;
            GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 1, out e1, out m1);
            GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 2, out e2, out m2);
            GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 3, out e3, out m3);
            GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 4, out e4, out m4);
            GetEnergy(Frequency, Amplitude, frequency, frequencyInterval, 5, out e5, out m5);

            //求时域有效值
            var paras = CalculatePara(Waveform);
            var rms = paras[0];

            //最大幅值
            var mMax = Amplitude.Max();

            if (m1 >= mMax && m1 >= (rms * 0.1 * Math.Sqrt(2)) && m1 > m2 && m3 <= (rms * 0.1 * Math.Sqrt(2)))
            {
                DiagnosticInfo = devicename + "不平衡";
                DiagnosticAdvice = "建议：怀疑轴系的动平衡情况，请检查转子是否存在弯曲或临时弯曲、转子零部件掉块、转子偏磨或结垢、轴承间隙大、设备底板结构松动等故障，如是皮带驱动，可能是皮带轮偏心引起。\r\n";
                double probability = e1 * 100.0 / rms;
                DiagnosticAdvice += "概率：" + probability.ToString("0") + "%";
            }
            else if (m2 >= mMax && m2 >= (rms * 0.1 * Math.Sqrt(2)) && m1 >= (rms * 0.1 * Math.Sqrt(2)) && m3 >= (rms * 0.1 * Math.Sqrt(2)))
            {
                DiagnosticInfo = devicename + "不对中";
                DiagnosticAdvice = "建议：(1)请检查相关轴系的对中情况，如轴与轴的对中不良、同一轴的几个轴承安装同心或轴承间隙不等、 轴承座热膨胀不均、机壳变形或移位、 地基不均匀下沉。\r\n";
                DiagnosticAdvice += "          (2)另外，可能以下原因也表现为不对中现象：I.由于质量偏心而引起的动平衡不良(此时转子呈弓形弯曲),也可能造成对中不良，如果此时静态条件下对中良好，可以先解决动平衡不良故障，对中不良故障将随即消除. II.由于受热、负载过大等原因导致轴弯曲;\r\n";
                DiagnosticAdvice += "          (3).由于内环或外环安装不合适，导致轴承偏翘（翘曲）. III.轴承座松动。\r\n";
                double probability = e2 * 100.0 / rms;
                DiagnosticAdvice += "概率：" + probability.ToString("0") + "%";
            }
            else if (m1 >= (rms * 0.1 * Math.Sqrt(2)) && m2 >= (rms * 0.1 * Math.Sqrt(2)) && m3 >= (rms * 0.1 * Math.Sqrt(2)) && m4 >= (rms * 0.1 * Math.Sqrt(2)) && m5 >= (rms * 0.1 * Math.Sqrt(2)))
            {
                DiagnosticInfo = devicename + "轴承座松动、轴承松动、（基础）松动等\r\n";
                DiagnosticAdvice = "建议：(1)请检查相关轴系的润滑、基础的紧固、相关轴系的轴承、轴承座安装情况.\r\n";
                DiagnosticAdvice += "          (2)受热、负载过大等原因导致轴弯曲；\r\n";
                DiagnosticAdvice += "          (3)内环或外环安装不合适，导致轴承偏翘（翘曲）。\r\n";
            }
            else if (mMax >= (rms * 0.3 * Math.Sqrt(2)))
            {
                double fMax = 0;
                for (int i = 0; i < Amplitude.Length; i++)
                {
                    if (Amplitude[i] == mMax)
                    {
                        fMax = Frequency[i];
                        break;
                    }
                }
                DiagnosticInfo = devicename + "频谱上有占能量很大比率的频率" + fMax.ToString("0.000") + "没有找到合适的故障原因\r\n";
                DiagnosticAdvice = "建议：(1)没有设备结构模型，比如齿轮齿数、设置轴承型号等，不能确定故障的部位。\r\n";
                DiagnosticAdvice += "          (2)转速设置是否正确。\r\n";
            }
            else
            {
                DiagnosticInfo = devicename + "没有发现故障";
                DiagnosticAdvice = null;
            }
            return DiagnosticInfo + "\r\n" + DiagnosticAdvice;
        }

        public static double[] ByteToSingle(byte[] bytes)
        {
            int length = bytes.Length / 4;
            double[] data = new double[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = BitConverter.ToSingle(bytes, i * 4);
            }
            return data;
        }

        public static bool GetEnergy(double[] Frequency, double[] Amplitude, double frequency, double frequencyInterval, int number, out double energy, out double m)
        {
            //List<int> keys = new List<int>();
            //for (int i = 0; i < Frequency.Length; i++)
            //{
            //    if (Frequency[i] <= (frequency * number + frequencyInterval)  && Frequency[i] >= (frequency * number - frequencyInterval) )
            //    {
            //        keys.Add(i);
            //    }
            //}

            var keys = Frequency.Select((s, index) => new { Index = index, Value = s }).Where(p => p.Value <= (frequency * number + frequencyInterval)  && p.Value >= (frequency * number - frequencyInterval) ).ToList();
            
            double eng1 = 0;
            foreach (var key in keys)
            {
                eng1 += Math.Pow(Amplitude[key.Index], 2);
            }
            energy = Math.Sqrt(eng1 / 2);
            List<double> m1list = new List<double>();
            foreach (var key in keys)
            {
                m1list.Add(Amplitude[key.Index]);
            }
            if (m1list.Count > 0)
            {
                m = m1list.Max();
                return true;
            }
            else
            {
                m = 0;
                return false;
            }
        }

        //旋转因子法求FFT
        //对原数据组进行重排
        private static void DataSort(ref double[] data_r, ref double[] data_i)
        {
            if (data_r.Length == 0 || data_i.Length == 0 || data_r.Length != data_i.Length)
                return;
            int len = data_r.Length;
            int[] count = new int[len];
            int M = (int)(Math.Log(len) / Math.Log(2));
            double[] temp_r = new double[len];
            double[] temp_i = new double[len];

            for (int i = 0; i < len; i++)
            {
                temp_r[i] = data_r[i];
                temp_i[i] = data_i[i];
            }
            for (int l = 0; l < M; l++)
            {
                int space = (int)Math.Pow(2, l);
                int add = (int)Math.Pow(2, M - l - 1);
                for (int i = 0; i < len; i++)
                {
                    if ((i / space) % 2 != 0)
                        count[i] += add;
                }
            }
            for (int i = 0; i < len; i++)
            {
                data_r[i] = temp_r[count[i]];
                data_i[i] = temp_i[count[i]];
            }
        }
        //FFT算法
        private static void Dit2_FFT(ref double[] data_r, ref double[] data_i, ref double[] result_r, ref double[] result_i)
        {
            if (data_r.Length == 0 || data_i.Length == 0 || data_r.Length != data_i.Length)
                return;
            int len = data_r.Length;
            double[] X_r = new double[len];
            double[] X_i = new double[len];
            for (int i = 0; i < len; i++)//将源数据复制副本，避免影响源数据的安全性
            {
                X_r[i] = data_r[i];
                X_i[i] = data_i[i];
            }
            DataSort(ref X_r, ref X_i);//位置重排
            double WN_r, WN_i;//旋转因子
            int M = (int)(Math.Log(len) / Math.Log(2));//蝶形图级数
            for (int l = 0; l < M; l++)
            {
                int space = (int)Math.Pow(2, l);
                int num = space;//旋转因子个数
                double temp1_r, temp1_i, temp2_r, temp2_i;
                for (int i = 0; i < num; i++)
                {
                    int p = (int)Math.Pow(2, M - 1 - l);//同一旋转因子有p个蝶
                    WN_r = Math.Cos(2 * Math.PI / len * p * i);
                    WN_i = -Math.Sin(2 * Math.PI / len * p * i);
                    for (int j = 0, n = i; j < p; j++, n += (int)Math.Pow(2, l + 1))
                    {
                        temp1_r = X_r[n];
                        temp1_i = X_i[n];
                        temp2_r = X_r[n + space];
                        temp2_i = X_i[n + space];//为蝶形的两个输入数据作副本，对副本进行计算，避免数据被修改后参加下一次计算
                        X_r[n] = temp1_r + temp2_r * WN_r - temp2_i * WN_i;
                        X_i[n] = temp1_i + temp2_i * WN_r + temp2_r * WN_i;
                        X_r[n + space] = temp1_r - temp2_r * WN_r + temp2_i * WN_i;
                        X_i[n + space] = temp1_i - temp2_i * WN_r - temp2_r * WN_i;
                    }
                }
            }
            //for (int i = 0; i < len; i++)//将源数据复制副本，避免影响源数据的安全性
            //{
            //    result_r[i] = X_r[i];
            //    result_i[i] = X_i[i];
            //}
            result_r = X_r;
            result_i = X_i;
        }

        private static void GetMod(ref double[] complex_r, ref double[] complex_i, ref double[] mod, int samplePoint)
        {
            if (complex_r.Length == 0 || complex_i.Length == 0 || complex_r.Length != complex_i.Length)
                return;
            for (int i = 0; i < complex_r.Length; i++)
                mod[i] = (Math.Sqrt(complex_r[i] * complex_r[i] + complex_i[i] * complex_i[i]) * 2) / samplePoint;
            //去直流
            mod[0] = 0;
        }

        public static double[] D2FFT(double[] data_r, int samplePoint)
        {
            double[] data_i;
            double[] fft_r;
            double[] fft_i;

            data_i = new double[samplePoint];

            fft_r = new double[data_r.Length];
            fft_i = new double[data_r.Length];
            double[] result = new double[data_r.Length];
            Dit2_FFT(ref data_r, ref data_i, ref fft_r, ref fft_i);
            GetMod(ref fft_r, ref fft_i, ref result, samplePoint);

            return result;
        }

        public static double[] CalculatePara(double[] input)
        {
            if (input == null || input.Length == 0) return null;
            double[] result = new double[11];
            //开根号
            double sqrtSum = 0;
            //1次方
            double singleSum = 0;
            //2次方
            double squareSum = 0;
            //3次方
            double cubeSum = 0;
            //4次方
            double fouthSum = 0;
            int n = input.Length;
            for (int i = 0; i < n; i++)
            {
                sqrtSum += Math.Sqrt(Math.Abs(input[i])) / n;
                singleSum += Math.Abs(input[i]) / n;
                squareSum += Math.Pow(Math.Abs(input[i]), 2) / n;
                cubeSum += Math.Pow(input[i], 3) / n;
                fouthSum += Math.Pow(input[i], 4) / n;
            }
            //最大值
            double max = Math.Max(Math.Abs(input.Max()), Math.Abs(input.Min()));
            //平均值
            double mean = singleSum;

            //有效值
            double rms = Math.Pow(squareSum, 0.5);
            result[0] = rms;
            //峰值
            result[1] = Math.Max(Math.Abs(input.Min()), Math.Abs(input.Max()));
            //峰峰值
            result[2] = input.Max() - input.Min();
            //斜度
            result[3] = cubeSum;
            //峭度
            result[4] = fouthSum;
            //峭度指标
            result[5] = fouthSum / Math.Pow(rms, 4);
            //波形指标
            result[6] = rms / mean;
            //峰值指标
            result[7] = max / rms;
            //脉冲指标
            result[8] = max / mean;
            //方根幅值
            result[9] = Math.Pow(sqrtSum, 2);
            //裕度指标
            result[10] = max / result[8];

            return result;
        }
    }
}
