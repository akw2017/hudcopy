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
        /// <summary>
        /// 获得低通滤波器的长度（阶数+1）
        /// </summary>
        /// <param name="SampleFre">采样频率</param>
        /// <param name="PassbandFre">通带频率,必须小于阻带频率</param>
        /// <param name="StopbandFre">阻带频率,必须大于通带频率</param>
        /// <param name="PassbandAttenuationDB">通带衰减,建议值0.2</param>
        /// <param name="StopbandAttenuationDB">阻带衰减,建议值60</param>
        /// <param name="ErrorMessage">错误信息</param>
        /// <param name="MessageLen">指定ErrorMessage长度</param>
        /// <returns>小于等于0失败，失败原因填充ErrorMessage;大于0成功，冲击响应长度(阶数加1)</returns>
        delegate int GetLowPasshnLen(double SampleFre, double PassbandFre, double StopbandFre, double PassbandAttenuationDB, double StopbandAttenuationDB, StringBuilder ErrorMessage, int MessageLen);


        /// <summary>
        /// 获得低通滤波器冲击响应函数
        /// </summary>
        /// <param name="SampleFre">采样频率</param>
        /// <param name="PassbandFre">通带频率,必须小于阻带频率</param>
        /// <param name="StopbandFre">阻带频率,必须大于通带频率</param>
        /// <param name="PassbandAttenuationDB">通带衰减,建议值0.2</param>
        /// <param name="StopbandAttenuationDB">阻带衰减,建议值60</param>
        /// <param name="hn"></param>
        /// <param name="hnLen"></param>
        /// <param name="ErrorMessage">错误信息</param>
        /// <param name="MessageLen">指定ErrorMessage长度</param>
        /// <returns>=FALSE失败，失败原因填充ErrorMessage;=TRUE成功</returns>
        delegate bool GetLowPasshn(double SampleFre, double PassbandFre, double StopbandFre, double PassbandAttenuationDB, double StopbandAttenuationDB, double[] hn, UInt32 hnLen, StringBuilder ErrorMessage, int MessageLen);

        private GetLowPasshnLen getLowPasshnLen;
        private GetLowPasshn getLowPasshn;

        private void InitializeLowPass(int hModule)
        {
            IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetLowPasshnLen");
            getLowPasshnLen = (GetLowPasshnLen)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetLowPasshnLen));

            intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetLowPasshn");
            getLowPasshn = (GetLowPasshn)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetLowPasshn));
        }


        public int GetLowPasshnLenAction(double sampleFre, double passbandFre, double stopbandFre, double passbandAttenuationDB, double stopbandAttenuationDB, StringBuilder errorMessage, int messageLen)
        {
            return getLowPasshnLen(sampleFre, passbandFre, stopbandFre, passbandAttenuationDB, stopbandAttenuationDB, errorMessage, messageLen);
        }
        public bool GetLowPasshnAction(double SampleFre, double PassbandFre, double StopbandFre, double PassbandAttenuationDB, double StopbandAttenuationDB, double[] hn, UInt32 hnLen, StringBuilder ErrorMessage, int MessageLen)
        {
            return getLowPasshn(SampleFre, PassbandFre, StopbandFre, PassbandAttenuationDB, StopbandAttenuationDB, hn, hnLen, ErrorMessage, MessageLen);
        }

        public double[] LowPassFilter(double[] input, int samplePoint, double sampleFre, double passbandFre, double stopbandFre, double passbandAttenuationDB, double stopbandAttenuationDB)
        {
            StringBuilder sb = new StringBuilder(1000);
            int len = GetLowPasshnLenAction(sampleFre, passbandFre, stopbandFre, passbandAttenuationDB, stopbandAttenuationDB, sb, 1000);
            if (len > 0)
            {
                double[] hn = new double[len];
                bool result = GetLowPasshnAction(sampleFre, passbandFre, stopbandFre, passbandAttenuationDB, stopbandAttenuationDB, hn, (UInt32)len, sb, 1000);
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
