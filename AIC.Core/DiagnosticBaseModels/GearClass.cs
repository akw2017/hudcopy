//using NullGuard;
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
        public int ID { get; set; } = -1;//新增为-1
        public string Name { get; set; }
        public Guid BeltID { get; set; }
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
