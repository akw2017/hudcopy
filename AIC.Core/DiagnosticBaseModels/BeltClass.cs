//using NullGuard;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public partial class BeltClass : IMach
    {
        public int ID { get; set; } = -1;//新增为-1
        public string Name { get; set; }
        public Guid BeltID { get; set; }
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
    }
}
