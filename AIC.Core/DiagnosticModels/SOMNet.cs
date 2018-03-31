using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticModels
{
    public class SOMNet
    {
        public int I_num;                                                                     //输入层节点个数
        public int O_num;                                                                     //输出层二维网络边长
        public double speed;                                                                  //初始学习效率
        public int radius;                                                                    //初始优胜邻域半径
        public int times;                                                                     //最大学习次数
        public NodeDisplay[] I_layer;                                                              //输入层数据
        public NodeDisplay[,] O_layer;                                                             //输出层数据
        public LineDisplay[,,] IO_send; //权值矩阵
        public double now_speed; 
        public int now_time;                                                                         //当前学习次数
        public int now_radius;                                                                         //当前优胜邻域半径
        Random Ran;                                                                           //产生一个随机数
        public struct index                                                                   //保存欧氏距离最小点坐标
        {
            public int x;
            public int y;
        }
        public index myindex;

        public SOMNet() //仿真函数 用的构造函数  
        {

        }
        //SOM类初始化
        public SOMNet(int input_num, int output_length, double study_speed = 0.5, int field_radius = 2, int train_times = 10000)
        {
            I_num = input_num;
            O_num = output_length;
            speed = study_speed;
            radius = field_radius;
            times = train_times;
            I_layer = new NodeDisplay[input_num];
            O_layer = new NodeDisplay[output_length, output_length];
            IO_send = new LineDisplay[output_length, output_length, input_num];
            now_time = 0;
            now_radius = field_radius;
            now_speed = study_speed;
            Ran = new Random();
            myindex.x = 0;
            myindex.y = 0;

            //权值矩阵初始化
            for (int i = 0; i < output_length; i++)
            {
                for (int j = 0; j < output_length; j++)
                {
                    for (int k = 0; k < input_num; k++)
                    {
                        IO_send[i, j, k].LineValue = Ran.NextDouble() * 20 - 10;
                    }
                    IO_send = Normalization(IO_send, i, j);
                }
            }
        }

        //归一化处理
        public NodeDisplay[] Normalization(NodeDisplay[] array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i].InputValue * array[i].InputValue;
            }
            for (int i = 0; i < array.Length; i++)
            {
                array[i].InputValue = array[i].InputValue / Math.Sqrt(sum);
            }
            return array;
        }
        public LineDisplay[,,] Normalization(LineDisplay[,,] array, int a, int b)
        {
            double sum = 0;
            for (int i = 0; i < I_num; i++)
            {
                sum += array[a, b, i].LineValue * array[a, b, i].LineValue;
            }
            for (int i = 0; i < I_num; i++)
            {
                array[a, b, i].LineValue = array[a, b, i].LineValue / Math.Sqrt(sum);
            }
            return array;
        }

        //SOM网络训练
        public void train(double[] input)
        {
            if (input.Length == I_num)
            {
                I_layer = new NodeDisplay[I_num];
                for (int i = 0; i < I_num; i++)
                {
                    I_layer[i].InputValue = input[i];
                }
                I_layer = Normalization(I_layer);

                //获得欧氏距离最小点
                double dis = 100000;
                for (int i = 0; i < O_num; i++)
                {
                    for (int j = 0; j < O_num; j++)
                    {
                        //求欧氏距离
                        double distance = 0;
                        for (int k = 0; k < I_num; k++)
                        {
                            distance += (IO_send[i, j, k].LineValue - I_layer[k].InputValue) * (IO_send[i, j, k].LineValue - I_layer[k].InputValue);
                        }
                        if (dis > distance)
                        {
                            dis = distance;
                            myindex.x = i;
                            myindex.y = j;
                        }
                    }
                }

                //在优胜邻域内更新权值向量
                /*for (int i = myindex.x - now_radius; i <= myindex.x + now_radius; i++)                //厨师帽
                {
                    for (int j = myindex.y - now_radius; j <= myindex.y + now_radius; j++)
                    {
                        if (i >= 0 && i <= 4 && j >= 0 && j <= 4)
                        {
                            for (int k = 0; k < I_num; k++)
                            {
                                IO_send[i, j, k] += now_speed * (I_layer[k] - IO_send[i, j, k]); 
                            }
                        }
                    }
                }*/
                for (int i = 0; i < O_num; i++)                                                         //墨西哥草帽
                {
                    for (int j = 0; j < O_num; j++)
                    {
                        double distance = (i - myindex.x) * (i - myindex.x) + (j - myindex.y) * (j - myindex.y);
                        for (int k = 0; k < I_num; k++)
                        {
                            IO_send[i, j, k].LineValue += now_speed * (I_layer[k].InputValue - IO_send[i, j, k].LineValue) * Math.Exp(-distance / ((now_radius + 0.00001) * (now_radius + 0.00001)));
                        }
                    }
                }
                //学习速率、邻域半径随训练次数的增加而减小
                now_time = now_time + 1;
                if (now_time <= 1000)
                {
                    now_speed = now_speed * (1 - (double)(2 * now_time) / (double)times);
                    if (now_time > 500)
                    {
                        now_radius = radius - 1;
                    }
                }
                else
                {
                    now_speed = 0.4 * (1 - (double)(now_time - 1000) / (double)(times - 1000));
                    now_radius = radius - 2;
                }
            }
            else
            {
                Console.WriteLine("SOM输入数据错误！");
            }
        }

        public void train(double[,] input)
        {
            if (input.GetLength(1) == I_num)
            {
                var sampleNum = input.GetLength(0); //数组第一维大小 为  
                for (int isamp = 0; isamp < sampleNum; isamp++)
                {
                    I_layer = new NodeDisplay[I_num];
                    for (int i = 0; i < I_num; i++)
                    {
                        I_layer[i].InputValue = input[isamp, i];
                    }
                    I_layer = Normalization(I_layer);

                    //获得欧氏距离最小点
                    double dis = 100000;
                    for (int i = 0; i < O_num; i++)
                    {
                        for (int j = 0; j < O_num; j++)
                        {
                            //求欧氏距离
                            double distance = 0;
                            for (int k = 0; k < I_num; k++)
                            {
                                distance += (IO_send[i, j, k].LineValue - I_layer[k].InputValue) * (IO_send[i, j, k].LineValue - I_layer[k].InputValue);
                            }
                            if (dis > distance)
                            {
                                dis = distance;
                                myindex.x = i;
                                myindex.y = j;
                            }
                        }
                    }

                    //在优胜邻域内更新权值向量
                    //for (int i = myindex.x - now_radius; i <= myindex.x + now_radius; i++)                //厨师帽
                    //{
                    //    for (int j = myindex.y - now_radius; j <= myindex.y + now_radius; j++)
                    //    {
                    //        if (i >= 0 && i <= 4 && j >= 0 && j <= 4)
                    //        {
                    //            for (int k = 0; k < I_num; k++)
                    //            {
                    //                IO_send[i, j, k] += now_speed * (I_layer[k].InputValue - IO_send[i, j, k]); 
                    //            }
                    //        }
                    //    }
                    //}
                    for (int i = 0; i < O_num; i++)                                                         //墨西哥草帽
                    {
                        for (int j = 0; j < O_num; j++)
                        {
                            double distance = (i - myindex.x) * (i - myindex.x) + (j - myindex.y) * (j - myindex.y);
                            for (int k = 0; k < I_num; k++)
                            {
                                IO_send[i, j, k].LineValue += now_speed * (I_layer[k].InputValue - IO_send[i, j, k].LineValue) * Math.Exp(-distance / ((now_radius + 0.00001) * (now_radius + 0.00001)));
                            }
                        }
                    }
                    //学习速率、邻域半径随训练次数的增加而减小
                    now_time = now_time + 1;
                    if (now_time <= 1000)
                    {
                        now_speed = now_speed * (1 - (double)(2 * now_time) / (double)times);
                        if (now_time > 500)
                        {
                            now_radius = radius - 1;
                        }
                    }
                    else
                    {
                        now_speed = 0.4 * (1 - (double)(now_time - 1000) / (double)(times - 1000));
                        now_radius = radius - 2;
                    }
                }
            }
            else
            {
                Console.WriteLine("SOM输入数据错误！");
            }
        }

        //测试
        public index sim(double[] psim)
        {
            for (int i = 0; i < I_num; i++)
                I_layer[i].InputValue = psim[i];
            I_layer = Normalization(I_layer);

            //获得欧氏距离最小点
            double dis = 100000;
            for (int i = 0; i < O_num; i++)
            {
                for (int j = 0; j < O_num; j++)
                {
                    //求欧氏距离
                    double distance = 0;
                    for (int k = 0; k < I_num; k++)
                    {
                        distance += (IO_send[i, j, k].LineValue - I_layer[k].InputValue) * (IO_send[i, j, k].LineValue - I_layer[k].InputValue);
                    }
                    if (dis > distance)
                    {
                        dis = distance;
                        myindex.x = i;
                        myindex.y = j;
                    }
                }
            }
            return myindex;
        }

        //获得权值矩阵
        public LineDisplay[,,] get_IO_send
        {
            get
            {
                return IO_send;
            }
        }

        //获得输出节点
        public int get_Result_x
        {
            get
            {
                return myindex.x;
            }
        }
        public int get_Result_y
        {
            get
            {
                return myindex.y;
            }
        }

        //保存参数
        public void saveParas(string filename)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filename);
                string str = I_num.ToString() + " "
                + O_num.ToString() + " "
                + speed.ToString() + " "
                + radius.ToString() + " "
                + times.ToString();
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

        //读回参数
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
                    this.I_num = Convert.ToInt32(strArr[0]);
                    this.O_num = Convert.ToInt32(strArr[1]);
                    this.speed = Convert.ToDouble(strArr[2]);
                    this.radius = Convert.ToInt32(strArr[3]);
                    this.times = Convert.ToInt32(strArr[4]);
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

        public void saveMatrix(LineDisplay[,,] w, string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            for (int i = 0; i < w.GetLength(0); i++)
            {
                for (int j = 0; j < w.GetLength(1); j++)
                {
                    sw.Write("(");
                    for (int k = 0; k < w.GetLength(2); k++)
                    {
                        sw.Write(w[i, j, k].LineValue.ToString() + " ");
                    }
                    sw.Write(")");
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        public void readMatrixW(LineDisplay[,,] w, string filename)
        {

            StreamReader sr;
            try
            {

                sr = new StreamReader(filename);

                String line;
                int i = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] s1 = line.Trim().Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < s1.Length; j++)
                    {
                        string[] s2 = s1[j].Trim().Split(' ');
                        for (int k = 0; k < s2.Length; k++)
                        {
                            w[i, j, k].LineValue = Convert.ToDouble(s2[k]);
                        }
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

        public void initial() // 建立一些中间数组 tjt 预测新数据  
        {
            I_layer = new NodeDisplay[I_num];
            O_layer = new NodeDisplay[O_num, O_num];
            IO_send = new LineDisplay[O_num, O_num, I_num];
            now_time = 0;
            now_radius = radius;
            now_speed = speed;
            myindex.x = 0;
            myindex.y = 0;
        }
    }
}
