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
        // OutFFT2Array变换后频谱信号数组  输出参数，长度等于SamplePoint，由开发者分配。
        // SamplePoint 采样点数，输入参数
        //ErrorMessage 错误信息，输出参数
        //MessageLen 指定ErrorMessage长度，输入参数 。
        //返回参数：=FALSE失败，失败原因填充ErrorMessage;=TRUE成功。
        delegate bool FFT2(double[] InputSignalArray, double[] OutFFT2Array, UInt32 SamplePoint, StringBuilder ErrorMessage, int MessageLen);

        // InputSignalArray  时域信号数组，  输入参数
        // OutFFT2Array变换后频谱信号数组  输出参数，长度等于SamplePoint，由开发者分配。
        // OutFFT2PhaseArray变换后相位信号数组  输出参数，长度等于SamplePoint，由开发者分配。
        // SamplePoint 采样点数，输入参数
        //ErrorMessage 错误信息，输出参数
        //MessageLen 指定ErrorMessage长度，输入参数 。
        //返回参数：=FALSE失败，失败原因填充ErrorMessage;=TRUE成功。
        delegate bool FFT2AndPhase(double[] InputSignalArray, double[] OutFFT2Array, double[] OutFFT2PhaseArray, UInt32 SamplePoint, StringBuilder ErrorMessage, int MessageLen);

        private FFT2 fft2;
        private FFT2AndPhase fft2AndPhase;

        private void InitializeFFT(int hModule)
        {
            IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "FFT2");
            fft2 = (FFT2)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(FFT2));

            intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "FFT2AndPhase");
            fft2AndPhase = (FFT2AndPhase)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(FFT2AndPhase));
        }

        public double[] FFT2Action(double[] input, int samplePoint)
        {
            StringBuilder sb = new StringBuilder(1000);
            double[] output = new double[samplePoint];
            if (!fft2(input, output, (UInt32)samplePoint, sb, 1000))
            {
                throw new Exception(sb.ToString());
            }
            return output;
        }

        public double[][] FFT2AndPhaseAction(double[] input, int samplePoint)
        {
            if (input == null || samplePoint == 0)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder(1000);
            double[][] output = new double[2][];
            output[0] = new double[samplePoint];
            output[1] = new double[samplePoint];
            if (!fft2AndPhase(input, output[0], output[1], (UInt32)samplePoint, sb, 1000))
            {
                throw new Exception(sb.ToString());
            }
            return output;
        }
    }
}
