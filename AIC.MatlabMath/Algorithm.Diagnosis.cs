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
        //ConditonString--设备类型、测点等JSON格式信息,输入参数
        //InLen--ConditonString字符串长度，输入参数
        //ConclusionString--诊断结论等信息，JSON格式，输出参数，有开发者提供内存分配。
        //OutLen--ConclusionString长度，输入参数
        //返回参数，=0成功；=1警告，输入参数不全；=-1错误，输入参数，无法得出诊断结论。
        delegate IntPtr GetDiagnosisConclusion(StringBuilder ConditonString, int InLen);
        private GetDiagnosisConclusion getDiagnosisConclusion;

        public void InitDiagnosis()
        {
            string sfdPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\SFDAlgorithm.dll";
            int hModule = CPlusPlusDLLDynamicInvoker.LoadLibrary(sfdPath);
            if (hModule > 0)
            {
                IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "GetDiagnosisConclusion");
                getDiagnosisConclusion = (GetDiagnosisConclusion)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetDiagnosisConclusion));

                InitializeBearing(hModule);
            }
            else
            {
                throw new Exception("无法加载SFDAlgorithm模块" + "Path:" + sfdPath);
            }
        }
        public string GetDiagnosisConclusionAction(StringBuilder condition)
        {
            IntPtr pStr = getDiagnosisConclusion(condition, condition.Length);
            string ret = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(pStr);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(pStr);
            pStr = IntPtr.Zero;
            return ret;
        }
    }
}
