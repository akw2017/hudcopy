using System;
using System.Runtime.InteropServices;


namespace AIC.MatlabMath
{
    public class CPlusPlusDLLDynamicInvoker
    {
        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "LoadLibrary")]
        public static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetProcAddress")]
        public static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "FreeLibrary")]
        public static extern bool FreeLibrary(int hModule);
    }
}
