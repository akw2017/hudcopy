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
        // InputSignalArray  变换前信号数组，  输入参数
        // OutSignalArray 变换后信号数组  输出参数，长度等于SamplePoint，由开发者分配。
        // SamplePoint 采样点数，输入参数
        // hn 冲击响应数组，输入参数。
        // hnLen冲击响应数组hn长度，输入参数，
        //由低通GetLowPasshn或高通GetHighPasshn或带通GetBandPasshn 获得hn冲击响应数组
        //ErrorMessage 错误信息，输出参数
        //MessageLen 指定ErrorMessage长度，输入参数 。
        //返回参数：=FALSE失败，失败原因填充ErrorMessage;=TRUE成功。
        delegate bool Filter(double[] InputSignalArray, double[] OutSignalArray, UInt32 SamplePoint, double[] hn, UInt32 hnLen, StringBuilder ErrorMessage, int MessageLen);

        private Filter filter;

        private void InitializeFilter(int hModule)
        {
            IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "Filter");
            filter = (Filter)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(Filter));
        }

        private bool FilterAction(double[] inputSignalArray, double[] outSignalArray, UInt32 samplePoint, double[] hn, UInt32 hnLen, StringBuilder errorMessage, int messageLen)
        {
            return filter(inputSignalArray, outSignalArray, samplePoint, hn, hnLen, errorMessage, messageLen);
        }
    }
}
