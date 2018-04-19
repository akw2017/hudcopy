
using AIC.CoreType;
using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public partial class MotorClass : IMach
    {
        public MotorClass()
        {
            LineFrequency = 50;
            Poles = 2;
            SCRs = 6;
            MotorType = MotorType.AC;
        }
        public long id { get; set; } = -1; //新增为-1
        public string Name { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        //电网工频,单位HZ,默认值50
        public double LineFrequency { get; set; }
        //磁极数,必须为大于等于2的偶数，如2,4,6,8，… 默认值2
        public int Poles { get; set; }
        //转子条数
        public int RotorBars { get; set; }
        //定子线圈数(同步电机)
        public int StatorCoils { get; set; }
        //绕组槽数
        public int WindingSlots { get; set; }
        //可控硅整流器数,只有3与6两种选择。默认值6
        public int SCRs { get; set; }
        //交流电机 默认值true
        public MotorType MotorType { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public MotorClass DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as MotorClass;
            }
        }

        public MotorClass ShallowClone()
        {
            return Clone() as MotorClass;
        }

        public static MotorClass ConvertFromDB(T_Motor t_motor)
        {
            MotorClass motor = new MotorClass();
            motor.id = t_motor.id;
            motor.Name = t_motor.Name;
            motor.Guid = t_motor.Guid ?? new Guid();
            motor.LineFrequency = t_motor.LineFrequency ?? 0;
            motor.Poles = t_motor.Poles ?? 0;
            motor.RotorBars = t_motor.RotorBars ?? 0;
            motor.StatorCoils = t_motor.StatorCoils ?? 0;
            motor.WindingSlots = t_motor.WindingSlots ?? 0;
            motor.SCRs = t_motor.SCRs ?? 0;
            motor.MotorType = (MotorType)(t_motor.MotorType ?? 0);
          
            return motor;
        }

        public static T_Motor ConvertToDB(MotorClass motor)
        {
            T_Motor t_motor = new T_Motor();
            t_motor.id = motor.id;
            t_motor.Name = motor.Name;
            t_motor.Guid = motor.Guid;
            t_motor.LineFrequency = motor.LineFrequency;
            t_motor.Poles = motor.Poles;
            t_motor.RotorBars = motor.RotorBars;
            t_motor.StatorCoils = motor.StatorCoils;
            t_motor.WindingSlots = motor.WindingSlots;
            t_motor.SCRs = motor.SCRs;
            t_motor.MotorType = (int)(motor.MotorType);
         
            return t_motor;
        }
    }
}
