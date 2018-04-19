//using NullGuard;
using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public partial class BeltClass : IMach
    {
        public long id { get; set; } = -1; //新增为-1
        public string Name { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        //皮带轮直径
        public double PulleyDiameter { get; set; }
        //皮带长度
        public double BeltLength { get; set; }

        public double Frequency => BeltLength != 0 ? (Math.PI * PulleyDiameter) / BeltLength : 0;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public BeltClass DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as BeltClass;
            }
        }

        public BeltClass ShallowClone()
        {
            return Clone() as BeltClass;
        }

        public static BeltClass ConvertFromDB(T_Belt t_belt)
        {
            BeltClass belt = new BeltClass();
            belt.Name = t_belt.Name;
            belt.id = t_belt.id;
            belt.Guid = t_belt.Guid ?? new Guid();
            belt.PulleyDiameter = t_belt.PulleyDiameter ?? 0;
            belt.BeltLength = t_belt.BeltLength ?? 0;
          
            return belt;
        }

        public static T_Belt ConvertToDB(BeltClass belt)
        {
            T_Belt t_belt = new T_Belt();
            t_belt.Name = belt.Name;
            t_belt.id = belt.id;
            t_belt.Guid = belt.Guid;
            t_belt.PulleyDiameter = belt.PulleyDiameter;
            t_belt.BeltLength = belt.BeltLength;
          
            return t_belt;
        }
    }
}
