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
        // InputSignalArray  时域信号数组，  输入参数
        // ps变换后信号数组  输出参数，长度等于SamplePoint，由开发者分配。
        // SampleFre采样频率，输入参数
        // SamplePoint 采样点数，输入参数
        //IsdB=FALSE，纵坐标单位为unit^2/Hz，其中unit为m/s^2或mm/s或um；=TRUE,单位为dB/Hz。默认IsdB=TRUE, 也即纵坐标单位为dB/Hz, 输入参数。
        //ErrorMessage 错误信息，输出参数
        //MessageLen 指定ErrorMessage长度，输入参数 。
        //返回参数：=FALSE失败，失败原因填充ErrorMessage;=TRUE成功。
        // psd数组ps[n]对应的横坐标为n*SampleFre/ SamplePoint, 单位HZ,其中n小于等于SamplePoint/2.56,与FFT横坐标相同。
        delegate bool PowerSpectrumDensity(double[] InputSignalArray, double[] ps, double SampleFre, UInt32 SamplePoint, bool IsdB, StringBuilder ErrorMessage, int MessageLen);

        private PowerSpectrumDensity powerSpectrumDensity;

        private void InitializePowerSpectrumDensity(int hModule)
        {
            IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "PowerSpectrumDensity");
            powerSpectrumDensity = (PowerSpectrumDensity)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(PowerSpectrumDensity));
        }

        public double[] PowerSpectrumDensityAction(double[] InputSignalArray, double sampleFre, int samplePoint, bool isdB)
        {
            StringBuilder sb = new StringBuilder(1000);
            double[] output = new double[samplePoint];
            if (!powerSpectrumDensity(InputSignalArray, output, sampleFre, (UInt32)samplePoint, isdB, sb, 1000))
            {
                throw new Exception(sb.ToString());
            }
            return output;
        }
    }
}
