//using NullGuard;
using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public partial class GearClass : IMach
    {
        public long id { get; set; } = -1; //新增为-1
        public string Name { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int TeethNumber { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public GearClass DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as GearClass;
            }
        }

        public GearClass ShallowClone()
        {
            return Clone() as GearClass;
        }

        public static GearClass ConvertFromDB(T_Gear t_gear)
        {
            GearClass gear = new GearClass();
            gear.id = t_gear.id;
            gear.Name = t_gear.Name;
            gear.Guid = t_gear.Guid ?? new Guid();
            gear.TeethNumber = t_gear.TeethNumber ?? 0;
            
            return gear;
        }

        public static T_Gear ConvertToDB(GearClass gear)
        {
            T_Gear t_gear = new T_Gear();
            t_gear.id = gear.id;
            t_gear.Name = gear.Name;
            t_gear.Guid = gear.Guid;
            t_gear.TeethNumber = gear.TeethNumber;
         
            return t_gear;
        }

        //public Gear Clone()
        //{
        //    return new Gear()
        //    {
        //        Name = this.Name,
        //        TeethNumber = this.TeethNumber,
        //    };
        //}

        //IMach IMach.Clone()
        //{
        //    return Clone();
        //}
    }
}
