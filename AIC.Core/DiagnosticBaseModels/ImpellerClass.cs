//using NullGuard;

using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public partial class ImpellerClass : IMach
    {
        public long id { get; set; } = -1; //新增为-1
        public string Name { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
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

        public static ImpellerClass ConvertFromDB(T_Impeller t_impeller)
        {
            ImpellerClass impeller = new ImpellerClass();
            impeller.id = t_impeller.id;
            impeller.Name = t_impeller.Name;
            impeller.Guid = t_impeller.Guid ?? new Guid();
            impeller.NumberOfBlades = t_impeller.NumberOfBlades ?? 0;
           
            return impeller;
        }

        public static T_Impeller ConvertToDB(ImpellerClass impeller)
        {
            T_Impeller t_impeller = new T_Impeller();
            t_impeller.id = impeller.id;
            t_impeller.Name = impeller.Name;
            t_impeller.Guid = impeller.Guid;
            t_impeller.NumberOfBlades = impeller.NumberOfBlades;
           
            return t_impeller;
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
