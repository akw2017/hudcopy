using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AIC.MatlabMath
{
    public partial class Algorithm
    {
        //SampleFre  采样频率   ,输入参数
        // StopBandFre1 低阻带频率 ,输入参数
        // PassBandFre1 低逼近带通频率 ,输入参数, 
        // PassBandFre2 高逼近带通频率 ,输入参数,
        // StopBandFre2高阻带频率 ,输入参数,
        //必须满足StopBandFre1< PassBandFre1< PassBandFre2< StopBandFre2
        //PassbandAttenuationDB  通带衰减 ,输入参数，建议值0.2
        //StopbandAttenuationDB  阻带衰减 ,输入参数，建议值60
        //ErrorMessage 错误信息，输出参数
        //MessageLen 指定ErrorMessage长度，输入参数 。
        //返回参数：<=0失败，失败原因填充ErrorMessage;>0成功，冲击响应长度（阶数加1）。
        delegate int GetBandPasshnLen(double SampleFre, double StopBandFre1, double PassBandFre1, double PassBandFre2, double StopBandFre2, double PassbandAttenuationDB, double StopbandAttenuationDB, StringBuilder ErrorMessage, int MessageLen);

        //SampleFre  采样频率   ,输入参数
        // StopBandFre1 低阻带频率 ,输入参数
        // PassBandFre1 低逼近带通频率 ,输入参数, 
        // PassBandFre2 高逼近带通频率 ,输入参数,
        // StopBandFre2高阻带频率 ,输入参数,
        //必须满足StopBandFre1< PassBandFre1< PassBandFre2< StopBandFre2
        //PassbandAttenuationDB  通带衰减 ,输入参数，建议值0.2
        //StopbandAttenuationDB  阻带衰减 ,输入参数，建议值60
        //ErrorMessage 错误信息，输出参数
        //MessageLen 指定ErrorMessage长度，输入参数 。
        // hn 冲击响应数组，由开发者提供数组大小，输出参数。
        // hnLen冲击响应数组hn长度，输入参数，参数由GetBandPasshnLen得到
        //返回参数：=FALSE失败，失败原因填充ErrorMessage;=TRUE成功。
        delegate bool GetBandPasshn(double SampleFre, double StopBandFre1, double PassBandFre1, double PassBandFre2, double StopBandFre2, double PassbandAttenuationDB, double StopbandAttenuationDB, double[] hn, UInt32 hnLen, StringBuilder ErrorMessage, int MessageLen);

        private GetBandPasshnLen getBandPasshnLen;
        private GetBandPasshn getBandPasshn;

        private void InitializeBandPass(int hModule)
        {
            IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetBandPasshnLen");
            getBandPasshnLen = (GetBandPasshnLen)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetBandPasshnLen));

            intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetBandPasshn");
            getBandPasshn = (GetBandPasshn)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetBandPasshn));
        }

        private int GetBandPasshnLenAction(double SampleFre, double StopBandFre1, double PassBandFre1, double PassBandFre2, double StopBandFre2, double PassbandAttenuationDB, double StopbandAttenuationDB, StringBuilder ErrorMessage, int MessageLen)
        {
            return getBandPasshnLen(SampleFre, StopBandFre1, PassBandFre1, PassBandFre2, StopBandFre2, PassbandAttenuationDB, StopbandAttenuationDB, ErrorMessage, MessageLen);
        }
        private bool GetBandPasshnAction(double SampleFre, double StopBandFre1, double PassBandFre1, double PassBandFre2, double StopBandFre2, double PassbandAttenuationDB, double StopbandAttenuationDB, double[] hn, UInt32 hnLen, StringBuilder ErrorMessage, int MessageLen)
        {
            return getBandPasshn(SampleFre, StopBandFre1, PassBandFre1, PassBandFre2, StopBandFre2, PassbandAttenuationDB, StopbandAttenuationDB, hn, hnLen, ErrorMessage, MessageLen);
        }
        public double[] BandPassFilter(double[] input, int samplePoint, double sampleFre, double passbandAttenuationDB, double stopbandAttenuationDB, double bpStopBandFreLow, double bpPassBandFreLow, double bpPassBandFreHigh, double bpStopBandFreHigh)
        {
            StringBuilder sb = new StringBuilder(1000);
            int len = GetBandPasshnLenAction(sampleFre, bpStopBandFreLow, bpPassBandFreLow, bpPassBandFreHigh, bpStopBandFreHigh, passbandAttenuationDB, stopbandAttenuationDB, sb, 1000);
            if (len > 0)
            {
                double[] hn = new double[len];
                bool result = GetBandPasshnAction(sampleFre, bpStopBandFreLow, bpPassBandFreLow, bpPassBandFreHigh, bpStopBandFreHigh, passbandAttenuationDB, stopbandAttenuationDB, hn, (UInt32)len, sb, 1000);
                if (result)
                {
                    double[] output = new double[samplePoint];
                    if (!FilterAction(input, output, (UInt32)samplePoint, hn, (UInt32)len, sb, 1000))
                    {
                        throw new Exception(sb.ToString());
                    }
                    return output;
                }
                else
                {
                    throw new Exception(sb.ToString());
                }
            }
            else
            {
                throw new Exception(sb.ToString());
            }
        }
    }
}
