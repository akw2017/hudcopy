using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AIC.Cloud.DataReplayer.Views
{
    public static class ReplaySDK
    {
        public delegate void DisplayCallback(long nPort, string pBuf, long nSize, long nWidth, long nHeight, long nStamp, long nType, long nReserved);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_SetDisplayCallBack(uint nPort, DisplayCallback callback);

        public delegate void FileEndCallback(uint nPort, IntPtr ptr);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_SetFileEndCallback(uint nPort, FileEndCallback callback, IntPtr ptr);

        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_GetPort(ref uint nPort);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_FreePort(uint nPort);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_OpenFile(uint nPort, string sFileName);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_CloseFile(uint nPort);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_Play(uint nPort, IntPtr hWnd);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_Stop(uint nPort);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_Pause(uint nPort, bool nPause);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_Fast(uint nPort);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_Slow(uint nPort);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern uint PlayM4_GetFileTime(uint nPort);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_SetPlayedTimeEx(uint nPort, uint nTime);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern uint PlayM4_GetPlayedTimeEx(uint nPort);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern uint PlayM4_GetPlayedTime(uint nPort);  
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern float PlayM4_GetPlayPos(uint nPort);
        [DllImport(@"Resources\PlayCtrl.dll")]
        public static extern bool PlayM4_GetJPEG(uint nPort, byte[] pJpeg, uint nBufSize, ref uint pJpegSize);
    }
}

