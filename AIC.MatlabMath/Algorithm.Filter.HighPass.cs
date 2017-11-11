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
        /// 获得高通滤波器的长度（阶数+1）
        /// </summary>
        /// <param name="SampleFre">采样频率</param>
        /// <param name="StopbandFre">通带频率,必须大于阻带频率</param>
        /// <param name="PassbandFre">阻带频率,必须小于通带频率</param>
        /// <param name="StopbandAttenuationDB">通带衰减,建议值0.2</param>
        /// <param name="PassbandAttenuationDB">阻带衰减,建议值60</param>
        /// <param name="ErrorMessage">错误信息</param>
        /// <param name="MessageLen"><指定ErrorMessage长度/param>
        /// <returns>小于等于0失败，失败原因填充ErrorMessage;大于0成功，冲击响应长度(阶数加1)</returns>
        delegate int GetHighPasshnLen(double SampleFre, double StopbandFre, double PassbandFre, double StopbandAttenuationDB, double PassbandAttenuationDB, StringBuilder ErrorMessage, int MessageLen);

        /// <summary>
        /// 获得低通滤波器冲击响应函数
        /// </summary>
        /// <param name="SampleFre">采样频率</param>
        /// <param name="PassbandFre">通带频率,必须大于阻带频率</param>
        /// <param name="StopbandFre">阻带频率,必须小于通带频率</param>
        /// <param name="PassbandAttenuationDB">通带衰减,建议值0.2</param>
        /// <param name="StopbandAttenuationDB">阻带衰减,建议值60</param>
        /// <param name="hn"></param>
        /// <param name="hnLen"></param>
        /// <param name="ErrorMessage">错误信息</param>
        /// <param name="MessageLen">指定ErrorMessage长度</param>
        /// <returns>=FALSE失败，失败原因填充ErrorMessage;=TRUE成功</returns>
        delegate bool GetHighPasshn(double SampleFre, double StopbandFre, double PassbandFre, double StopbandAttenuationDB, double PassbandAttenuationDB, double[] hn, UInt32 hnLen, StringBuilder ErrorMessage, int MessageLen);

        private GetHighPasshnLen getHighPasshnLen;
        private GetHighPasshn getHighPasshn;

        private void InitializeHighPass(int hModule)
        {
            IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetHighPasshnLen");
            getHighPasshnLen = (GetHighPasshnLen)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetHighPasshnLen));

            intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetHighPasshn");
            getHighPasshn = (GetHighPasshn)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetHighPasshn));
        }

        public double[] HighPassFilter(double[] input, int samplePoint, double sampleFre, double passbandFre, double stopbandFre, double passbandAttenuationDB, double stopbandAttenuationDB)
        {
            StringBuilder sb = new StringBuilder(1000);
            int len = GetHighPasshnLenAction(sampleFre, passbandFre, stopbandFre, passbandAttenuationDB, stopbandAttenuationDB, sb, 1000);
            if (len > 0)
            {
                double[] hn = new double[len];
                bool result = GetHighPasshnAction(sampleFre, passbandFre, stopbandFre, passbandAttenuationDB, stopbandAttenuationDB, hn, (UInt32)len, sb, 1000);
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

        private int GetHighPasshnLenAction(double sampleFre, double passbandFre, double stopbandFre, double passbandAttenuationDB, double stopbandAttenuationDB, StringBuilder errorMessage, int messageLen)
        {
            return getHighPasshnLen(sampleFre, stopbandFre, passbandFre, stopbandAttenuationDB, passbandAttenuationDB, errorMessage, messageLen);
        }
        private bool GetHighPasshnAction(double SampleFre, double PassbandFre, double StopbandFre, double PassbandAttenuationDB, double StopbandAttenuationDB, double[] hn, UInt32 hnLen, StringBuilder ErrorMessage, int MessageLen)
        {
            return getHighPasshn(SampleFre, StopbandFre, PassbandFre, StopbandAttenuationDB, PassbandAttenuationDB, hn, hnLen, ErrorMessage, MessageLen);
        }
    }
}
