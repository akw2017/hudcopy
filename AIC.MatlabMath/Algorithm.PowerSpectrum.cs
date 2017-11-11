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
        //IsdB=FALSE，纵坐标单位为unit^2，其中unit为m/s^2或mm/s或um；=TRUE,单位为dB。默认IsdB=TRUE, 也即纵坐标为dB,输入参数
        //ErrorMessage 错误信息，输出参数
        //MessageLen 指定ErrorMessage长度，输入参数 。
        //返回参数：=FALSE失败，失败原因填充ErrorMessage;=TRUE成功。
        // ps数组ps[n]对应的横坐标为n*SampleFre/ SamplePoint,单位HZ,其中n小于等于SamplePoint/2.56,与FFT横坐标相同。
        delegate bool PowerSpectrum(double[] InputSignalArray, double[] ps, double SampleFre, UInt32 SamplePoint, bool IsdB, StringBuilder ErrorMessage, int MessageLen);

        private PowerSpectrum powerSpectrum;

        private void InitializePowerSpectrum(int hModule)
        {
            IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "PowerSpectrum");
            powerSpectrum = (PowerSpectrum)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(PowerSpectrum));
        }

        public double[] PowerSpectrumAction(double[] InputSignalArray, double sampleFre, int samplePoint, bool isdB)
        {
            StringBuilder sb = new StringBuilder(1000);
            double[] output = new double[samplePoint];
            if (!powerSpectrum(InputSignalArray, output, sampleFre, (UInt32)samplePoint, isdB, sb, 1000))
            {
                throw new Exception(sb.ToString());
            }
            return output;
        }
    }
}
