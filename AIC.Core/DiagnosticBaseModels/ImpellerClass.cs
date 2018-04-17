//using NullGuard;

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public partial class ImpellerClass : IMach
    {
        public int ID { get; set; } = -1;//新增为-1
        public string Name { get; set; }
        public Guid ImpellerID { get; set; }
        //叶片数
        public int NumberOfBlades { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public ImpellerClass DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as ImpellerClass;
            }
        }

        public ImpellerClass ShallowClone()
        {
            return Clone() as ImpellerClass;
        }

        //public Impeller Clone()
        //{
        //    return new Impeller()
        //    {
        //        Name = this.Name,
        //        NumberOfBlades = this.NumberOfBlades,
        //    };
        //}

        //IMach IMach.Clone()
        //{
        //    return Clone();
        //}
    }
}
