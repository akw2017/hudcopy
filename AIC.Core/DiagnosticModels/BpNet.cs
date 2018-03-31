using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticModels
{
    /// <summary>  
    /// BpNet 的摘要说明。  
    /// </summary>  
    public class BPNet
    {
        public int inNum;//输入节点数  
        int hideNum;//隐层节点数  
        public int outNum;//输出层节点数  
        public int sampleNum;//样本总数  

        Random R;
        public NodeDisplay[] input;//输入节点的输入数据  
        public NodeDisplay[] hide;//隐层节点的输出  
        public NodeDisplay[] output;//输出节点的输出  

        //double[] hide_addition;//隐层的输入  
        //double[] oupput_addition;//输出层的输入  
        public LineDisplay[,] w;//权值矩阵w  
        public LineDisplay[,] v;//权值矩阵V  
        //public double[,] dw;//权值矩阵w  
        //public double[,] dv;//权值矩阵V  


        public double rate;//学习率  
        public double[] q;//隐层阈值矩阵  
        public double[] r;//输出层阈值矩阵  
        public double[] dq;//隐层阈值矩阵  
        public double[] dr;//输出层阈值矩阵  

        double[] hide_err;//输出层的误差  
        double[] output_err;//隐层的误差  
        double[] teacher;//输出层的教师数据  
        public double e;//均方误差  
        double in_rate;//归一化比例系数  
        double out_rate;//归一化比例系数  

        public int computeHideNum(int m, int n)
        {
            double s = Math.Sqrt(0.43 * m * n + 0.12 * n * n + 2.54 * m + 0.77 * n + 0.35) + 0.51;
            int ss = Convert.ToInt32(s);
            return ((s - (double)ss) > 0.5) ? ss + 1 : ss;

        }

        public BPNet() //仿真函数 用的构造函数  
        {

        }

        public BPNet(double[,] p, double[,] t)
        {

            // 构造函数逻辑  
            R = new Random(32); //加了一个参数，使产生的伪随机序列相同  

            this.inNum = p.GetLength(1); //数组第二维大小为 输入节点数  
            this.outNum = t.GetLength(1); //输出节点数  
            this.hideNum = computeHideNum(inNum, outNum); //隐藏节点数，不知其原理  
            //      this.hideNum=18;  
            this.sampleNum = p.GetLength(0); //数组第一维大小 为  

            Console.WriteLine("输入节点数目：" + inNum);
            Console.WriteLine("隐层节点数目：" + hideNum);
            Console.WriteLine("输出层节点数目：" + outNum);

            Console.ReadLine();

            input = new NodeDisplay[inNum];
            hide = new NodeDisplay[hideNum];
            output = new NodeDisplay[outNum];

            //hide_addition = new double[hideNum];
            //oupput_addition = new double[outNum];

            w = new LineDisplay[inNum, hideNum];
            v = new LineDisplay[hideNum, outNum];
            //dw = new double[inNum, hideNum];
            //dv = new double[hideNum, outNum];

            q = new double[hideNum];
            r = new double[outNum];
            dq = new double[hideNum];
            dr = new double[outNum];

            hide_err = new double[hideNum];
            output_err = new double[outNum];
            teacher = new double[outNum];

            //初始化w  
            for (int i = 0; i < inNum; i++)
            {
                for (int j = 0; j < hideNum; j++)
                {
                    w[i, j].LineValue = (R.NextDouble() * 2 - 1.0) / 2;
                }
            }

            //初始化v  
            for (int i = 0; i < hideNum; i++)
            {
                for (int j = 0; j < outNum; j++)
                {
                    v[i, j].LineValue = (R.NextDouble() * 2 - 1.0) / 2;
                }
            }

            rate = 0.8;
            e = 0.0;
            in_rate = 1.0;
        }

        //训练函数  
        public void train(double[,] p, double[,] t)
        {
            e = 0.0;
            //求p中的最大值  
            double pMax = 0.0;
            this.sampleNum = p.GetLength(0); //数组第一维大小

            for (int isamp = 0; isamp < sampleNum; isamp++)
            {
                for (int i = 0; i < inNum; i++)
                {
                    if (Math.Abs(p[isamp, i]) > pMax)
                    {
                        pMax = Math.Abs(p[isamp, i]);
                    }
                }

                for (int j = 0; j < outNum; j++)
                {
                    if (Math.Abs(t[isamp, j]) > pMax)
                    {
                        pMax = Math.Abs(t[isamp, j]);
                    }
                }

                in_rate = pMax;
            }//end isamp  
             //求t中的最大值  
            pMax = 0.0;
            for (int isamp = 0; isamp < sampleNum; isamp++)
            {
                for (int j = 0; j < outNum; j++)
                {
                    if (Math.Abs(t[isamp, j]) > pMax)
                    {
                        pMax = Math.Abs(t[isamp, j]);
                    }
                }

                out_rate = pMax;
            }//end isamp  

            for (int isamp = 0; isamp < sampleNum; isamp++)
            {
                //数据归一化  
                for (int i = 0; i < inNum; i++)
                {
                    input[i].InputValue = p[isamp, i] / in_rate;
                }
                for (int i = 0; i < outNum; i++)
                {
                    teacher[i] = t[isamp, i] / out_rate;
                }

                //计算隐层的输入和输出  

                for (int j = 0; j < hideNum; j++)
                {
                    hide[j].InputValue = 0.0;
                    for (int i = 0; i < inNum; i++)
                    {
                        hide[j].InputValue += w[i, j].LineValue * input[i].InputValue;
                    }
                    hide[j].OutputValue = 1.0 / (1.0 + Math.Exp(-hide[j].InputValue - q[j]));
                }

                //计算输出层的输入和输出  
                for (int k = 0; k < outNum; k++)
                {
                    output[k].InputValue = 0.0;
                    for (int j = 0; j < hideNum; j++)
                    {
                        output[k].InputValue += v[j, k].LineValue * hide[j].OutputValue;
                    }
                    output[k].OutputValue = 1.0 / (1.0 + Math.Exp(-output[k].InputValue - r[k]));//是否需要改变输出的s函数
                }

                //计算输出层误差和均方差  

                for (int k = 0; k < outNum; k++)
                {
                    output_err[k] = (teacher[k] - output[k].OutputValue) * output[k].OutputValue * (1.0 - output[k].OutputValue);
                    e += (teacher[k] - output[k].OutputValue) * (teacher[k] - output[k].OutputValue);//学习误差曲线
                    //更新V  
                    for (int j = 0; j < hideNum; j++)
                    {
                        v[j, k].LineValue += rate * output_err[k] * hide[j].OutputValue;
                    }
                }

                //计算隐层误差  

                for (int j = 0; j < hideNum; j++)
                {
                    hide_err[j] = 0.0;
                    for (int k = 0; k < outNum; k++)
                    {
                        hide_err[j] += output_err[k] * v[j, k].LineValue;
                    }
                    hide_err[j] = hide_err[j] * hide[j].OutputValue * (1 - hide[j].OutputValue);

                    //更新W  

                    for (int i = 0; i < inNum; i++)
                    {
                        w[i, j].LineValue += rate * hide_err[j] * input[i].InputValue;
                    }
                }

                //更新b2  
                for (int k = 0; k < outNum; k++)
                {
                    r[k] += rate * output_err[k];
                }

                //更新b1  
                for (int j = 0; j < hideNum; j++)
                {
                    q[j] += rate * hide_err[j];
                }

            }//end isamp  
            e = Math.Sqrt(e);
            //      adjustWV(w,dw);  
            //      adjustWV(v,dv);  


        }//end train  

        public void adjustWV(double[,] w, double[,] dw)
        {
            for (int i = 0; i < w.GetLength(0); i++)
            {
                for (int j = 0; j < w.GetLength(1); j++)
                {
                    w[i, j] += dw[i, j];
                }
            }

        }

        public void adjustWV(double[] w, double[] dw)
        {
            for (int i = 0; i < w.Length; i++)
            {

                w[i] += dw[i];

            }

        }

        //数据仿真函数  
        public double[] sim(double[] psim) //in_rate inNum HideNum outNum   
        {
            for (int i = 0; i < inNum; i++)
                input[i].InputValue = psim[i] / in_rate;

            for (int j = 0; j < hideNum; j++)
            {
                hide[j].InputValue = 0.0;
                for (int i = 0; i < inNum; i++)
                    hide[j].InputValue = hide[j].InputValue + w[i, j].LineValue * input[i].InputValue;
                hide[j].OutputValue = 1.0 / (1.0 + Math.Exp(-hide[j].InputValue - q[j]));
            }
            for (int k = 0; k < outNum; k++)
            {
                output[k].InputValue = 0.0;
                for (int j = 0; j < hideNum; j++)
                    output[k].InputValue = output[k].InputValue + v[j, k].LineValue * hide[j].OutputValue;
                output[k].OutputValue = 1.0 / (1.0 + Math.Exp(-output[k].InputValue - r[k]));

                output[k].OutputValue = out_rate * output[k].OutputValue;

            }

            return output.Select(p => p.OutputValue).ToArray();
        } //end sim  

        //保存矩阵w,v  
        public void saveMatrix(LineDisplay[,] w, string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            for (int i = 0; i < w.GetLength(0); i++)
            {
                for (int j = 0; j < w.GetLength(1); j++)
                {
                    sw.Write(w[i, j].LineValue.ToString() + " ");
                }
                sw.WriteLine();
            }
            sw.Close();

        }

        //保存矩阵q,b2  
        public void saveMatrix(double[] b, string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            for (int i = 0; i < b.Length; i++)
            {
                sw.Write(b[i] + " ");
            }
            sw.Close();
        }

        //保存参数 in_rate inNum HideNum outNum   
        public void saveParas(string filename)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filename);
                string str = inNum.ToString() + " "
                + hideNum.ToString() + " "
                + outNum.ToString() + " "
                + in_rate.ToString() + " "
                + out_rate.ToString();
                sw.WriteLine(str);
                sw.Close();
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.  
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        //读回参数 in_rate inNum HideNum outNum, tjt 预测新数据   
        public void readParas(string filename)
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader(filename);
                String line;
                if ((line = sr.ReadLine()) != null)
                {
                    string[] strArr = line.Split(' ');
                    this.inNum = Convert.ToInt32(strArr[0]);
                    this.hideNum = Convert.ToInt32(strArr[1]);
                    this.outNum = Convert.ToInt32(strArr[2]);
                    this.in_rate = Convert.ToDouble(strArr[3]);
                    this.out_rate = Convert.ToDouble(strArr[4]);
                }
                sr.Close();

            }
            catch (Exception e)
            {
                // Let the user know what went wrong.  
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void initial() // 建立一些中间数组 tjt 预测新数据  
        {
            input = new NodeDisplay[inNum];
            hide = new NodeDisplay[hideNum];
            output = new NodeDisplay[outNum];

            //hide_addition = new double[hideNum];
            //oupput_addition = new double[outNum];

            w = new LineDisplay[inNum, hideNum];
            v = new LineDisplay[hideNum, outNum];
            //dw = new double[inNum, hideNum];
            //dv = new double[hideNum, outNum];

            q = new double[hideNum];
            r = new double[outNum];
            dq = new double[hideNum];
            dr = new double[outNum];

            hide_err = new double[hideNum];
            output_err = new double[outNum];
            teacher = new double[outNum];
        }

        //读取矩阵W,V  
        public void readMatrixW(LineDisplay[,] w, string filename)
        {

            StreamReader sr;
            try
            {

                sr = new StreamReader(filename);

                String line;
                int i = 0;

                while ((line = sr.ReadLine()) != null)
                {

                    string[] s1 = line.Trim().Split(' ');
                    for (int j = 0; j < s1.Length; j++)
                    {
                        w[i, j].LineValue = Convert.ToDouble(s1[j]);
                    }
                    i++;
                }
                sr.Close();

            }
            catch (Exception e)
            {
                // Let the user know what went wrong.  
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }


        //读取矩阵q,b2  
        public void readMatrixB(double[] b, string filename)
        {

            StreamReader sr;
            try
            {
                sr = new StreamReader(filename);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    int i = 0;
                    string[] s1 = line.Trim().Split(' ');
                    for (int j = 0; j < s1.Length; j++)
                    {
                        b[i] = Convert.ToDouble(s1[j]);
                        i++;
                    }
                }
                sr.Close();

            }
            catch (Exception e)
            {
                // Let the user know what went wrong.  
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

    }//end bpnet 
}
