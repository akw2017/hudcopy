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
        // InputSignalArray  变换前信号数组， 输入参数
        // OutSignalArray 变换后信号数组  输出参数，长度等于SamplePoint，由开发者分配。
        // SamplePoint 采样点数
        //ErrorMessage 错误信息，输出参数
        //MessageLen 指定ErrorMessage长度，输入参数 。
        //返回参数：=FALSE失败，失败原因填充ErrorMessage;=TRUE成功。
        delegate bool AMHilbert(double[] InputSignalArray, double[] OutSignalArray, UInt32 SamplePoint, StringBuilder ErrorMessage, int MessageLen);

        private AMHilbert amHilbert;

        private void InitializeEnvelop(int hModule)
        {
            IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "AMHilbert");
            amHilbert = (AMHilbert)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(AMHilbert));
        }
        private bool AMHilbertAction(double[] inputSignalArray, double[] outSignalArray, UInt32 samplePoint, StringBuilder errorMessage, int messageLen)
        {
            return amHilbert(inputSignalArray, outSignalArray, samplePoint, errorMessage, messageLen);
        }
        public double[] Envelope(double[] input, int samplePoint)
        {
            StringBuilder sb = new StringBuilder(1000);
            double[] output = new double[samplePoint];
            if (!AMHilbertAction(input, output, (UInt32)samplePoint, sb, 1000))
            {
                throw new Exception(sb.ToString());
            }
            return output;
        }
    }
}
