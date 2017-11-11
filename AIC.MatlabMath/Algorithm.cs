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
        // 返回值=TRUE,初始化成功；=FALSE 初始化失败，失败后下面的函数都不能执行，否则程序崩溃。
        delegate bool AICMathInit();

        //去初始化，程序退出之前调用，否则存在内存泄露。
        delegate void AICMathUnInit();

        private AICMathInit aICMathInit;
        private AICMathUnInit aICMathUnInit;

        private static Algorithm instance = new Algorithm();

        private string filePath;

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "LoadLibrary")]
        public static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        private Algorithm()
        {
            string matlabPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\AICMatlabDLL.dll";          
            int matlabModule = LoadLibrary(matlabPath);
            if (matlabModule > 0)
            {
                filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\AICMath.dll";               
                int hModule = LoadLibrary(filePath);
                if (hModule > 0)
                {
                    if (Initialize(hModule))
                    {
                        InitializeLowPass(hModule);
                        InitializeBandPass(hModule);
                        InitializeHighPass(hModule);
                        InitializeFilter(hModule);
                        InitializeTFF(hModule);
                        InitializeEnvelop(hModule);
                        InitializeCepstrum(hModule);
                        InitializeFFT(hModule);
                        InitializePowerSpectrum(hModule);
                        InitializePowerSpectrumDensity(hModule);
                    }
                    else
                    {
                        throw new Exception("算法初始化失败" + "Path:" + matlabPath);
                    }
                }
                else
                {
                    throw new Exception("无法加载AICMath模块" + "Path:" + matlabPath);
                }
            }
            else
            {
                throw new Exception("无法加载AICMatlabDLL模块" + "Path:" + matlabPath);
            }
        }

        private bool Initialize(int hModule)
        {
            IntPtr intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "AICMathInit");
            aICMathInit = (AICMathInit)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(AICMathInit));
            intPtr = CPlusPlusDLLDynamicInvoker.GetProcAddress(hModule, "AICMathUnInit");
            aICMathUnInit = (AICMathUnInit)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(AICMathUnInit));
            if (aICMathInit != null)
            {
                bool result = aICMathInit();
                if(result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception(string.Format("不能从路径{0}中提取算法", filePath));
            }
        }

        public static Algorithm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Algorithm();
                }
                return instance;
            }
        }

        public static double[] CalculatePara(double[] input)
        {
            if (input == null || input.Length == 0) return null;
            double[] result = new double[11];
            //开根号
            double sqrtSum = 0;
            //1次方
            double singleSum = 0;
            //2次方
            double squareSum = 0;
            //3次方
            double cubeSum = 0;
            //4次方
            double fouthSum = 0;
            int n = input.Length;
            for (int i = 0; i < n; i++)
            {
                sqrtSum += Math.Sqrt(Math.Abs(input[i])) / n;
                singleSum += Math.Abs(input[i]) / n;
                squareSum += Math.Pow(Math.Abs(input[i]), 2) / n;
                cubeSum += Math.Pow(input[i], 3) / n;
                fouthSum += Math.Pow(input[i], 4) / n;
            }
            //最大值
            double max = Math.Max(Math.Abs(input.Max()), Math.Abs(input.Min()));
            //平均值
            double mean = singleSum;

            //有效值
            double rms = Math.Pow(squareSum, 0.5);
            result[0] = rms;
            //峰值
            result[1] = Math.Max(Math.Abs(input.Min()), Math.Abs(input.Max()));
            //峰峰值
            result[2] = input.Max() - input.Min();
            //斜度
            result[3] = cubeSum;
            //峭度
            result[4] = fouthSum;
            //峭度指标
            result[5] = fouthSum / Math.Pow(rms, 4);
            //波形指标
            result[6] = rms / mean;
            //峰值指标
            result[7] = max / rms;
            //脉冲指标
            result[8] = max / mean;
            //方根幅值
            result[9] = Math.Pow(sqrtSum, 2);
            //裕度指标
            result[10] = max / result[8];

            return result;
        }

        public static double[] ByteToSingle(byte[] bytes)
        {
            int length = bytes.Length / 4;
            double[] data = new double[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = BitConverter.ToSingle(bytes, i * 4);
            }
            return data;
        }


        //旋转因子法求FFT
        //对原数据组进行重排
        private static void DataSort(ref double[] data_r, ref double[] data_i)
        {
            if (data_r.Length == 0 || data_i.Length == 0 || data_r.Length != data_i.Length)
                return;
            int len = data_r.Length;
            int[] count = new int[len];
            int M = (int)(Math.Log(len) / Math.Log(2));
            double[] temp_r = new double[len];
            double[] temp_i = new double[len];

            for (int i = 0; i < len; i++)
            {
                temp_r[i] = data_r[i];
                temp_i[i] = data_i[i];
            }
            for (int l = 0; l < M; l++)
            {
                int space = (int)Math.Pow(2, l);
                int add = (int)Math.Pow(2, M - l - 1);
                for (int i = 0; i < len; i++)
                {
                    if ((i / space) % 2 != 0)
                        count[i] += add;
                }
            }
            for (int i = 0; i < len; i++)
            {
                data_r[i] = temp_r[count[i]];
                data_i[i] = temp_i[count[i]];
            }
        }
        //FFT算法
        private static void Dit2_FFT(ref double[] data_r, ref double[] data_i, ref double[] result_r, ref double[] result_i)
        {
            if (data_r.Length == 0 || data_i.Length == 0 || data_r.Length != data_i.Length)
                return;
            int len = data_r.Length;
            double[] X_r = new double[len];
            double[] X_i = new double[len];
            for (int i = 0; i < len; i++)//将源数据复制副本，避免影响源数据的安全性
            {
                X_r[i] = data_r[i];
                X_i[i] = data_i[i];
            }
            DataSort(ref X_r, ref X_i);//位置重排
            double WN_r, WN_i;//旋转因子
            int M = (int)(Math.Log(len) / Math.Log(2));//蝶形图级数
            for (int l = 0; l < M; l++)
            {
                int space = (int)Math.Pow(2, l);
                int num = space;//旋转因子个数
                double temp1_r, temp1_i, temp2_r, temp2_i;
                for (int i = 0; i < num; i++)
                {
                    int p = (int)Math.Pow(2, M - 1 - l);//同一旋转因子有p个蝶
                    WN_r = Math.Cos(2 * Math.PI / len * p * i);
                    WN_i = -Math.Sin(2 * Math.PI / len * p * i);
                    for (int j = 0, n = i; j < p; j++, n += (int)Math.Pow(2, l + 1))
                    {
                        temp1_r = X_r[n];
                        temp1_i = X_i[n];
                        temp2_r = X_r[n + space];
                        temp2_i = X_i[n + space];//为蝶形的两个输入数据作副本，对副本进行计算，避免数据被修改后参加下一次计算
                        X_r[n] = temp1_r + temp2_r * WN_r - temp2_i * WN_i;
                        X_i[n] = temp1_i + temp2_i * WN_r + temp2_r * WN_i;
                        X_r[n + space] = temp1_r - temp2_r * WN_r + temp2_i * WN_i;
                        X_i[n + space] = temp1_i - temp2_i * WN_r - temp2_r * WN_i;
                    }
                }
            }
            //for (int i = 0; i < len; i++)//将源数据复制副本，避免影响源数据的安全性
            //{
            //    result_r[i] = X_r[i];
            //    result_i[i] = X_i[i];
            //}
            result_r = X_r;
            result_i = X_i;
        }

        private static void GetMod(ref double[] complex_r, ref double[] complex_i, ref double[] mod, int samplePoint)
        {
            if (complex_r.Length == 0 || complex_i.Length == 0 || complex_r.Length != complex_i.Length)
                return;
            for (int i = 0; i < complex_r.Length; i++)
                mod[i] = (Math.Sqrt(complex_r[i] * complex_r[i] + complex_i[i] * complex_i[i]) * 2) / samplePoint;
            //去直流
            mod[0] = 0;
        }

        public static double[] D2FFT(double[] data_r, int samplePoint)
        {
            double[] data_i;
            double[] fft_r;
            double[] fft_i;

            data_i = new double[samplePoint];
         
            fft_r = new double[data_r.Length];
            fft_i = new double[data_r.Length];
            double[] result = new double[data_r.Length];
            Dit2_FFT(ref data_r, ref data_i, ref fft_r, ref fft_i);
            GetMod(ref fft_r, ref fft_i, ref result, samplePoint);

            return result;
        }

    }
}
