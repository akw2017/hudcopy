using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AIC.Core.DiagnosticBaseModels
{
    [Serializable]
    public partial class BearingClass : IMach
    {
        public long id { get; set; } = -1; //新增为-1
        public Guid Guid { get; set; } = Guid.NewGuid();
        //轴承型号
        public string Designation { get; set; }
        //节圆直径
        public double PitchDiameter { get; set; }
        //滚子直径
        public double RollerDiameter { get; set; }
        //接触角
        public double ContactAngle { get; set; }
        //滚子个数
        public int NumberOfRoller { get; set; }
        //外圈直径
        public double OuterRingDiameter { get; set; }
        //内圈直径
        public double InnerRingDiameter { get; set; }
        //列数
        public int NumberOfColumns { get; set; }
        //内环特征频率
        public double InnerRingFrequency { get; set; }
        //外环特征频率
        public double OuterRingFrequency { get; set; }
        //滚动体特征频率
        public double RollerFrequency { get; set; }
        //保持架特征频率
        public double MaintainsFrequency { get; set; }
        //轴承系列
        public string BearingSeries { get; set; }
        public int BearingSeriesID { get; set; }
        //转速
        public double RPM { get; set; }
        //名字
        public string Name { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public BearingClass DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as BearingClass;
            }
        }

        public BearingClass ShallowClone()
        {
            return Clone() as BearingClass;
        }

        public static BearingClass ConvertFromDB(T_Bearing t_bear)
        {
            BearingClass bear = new BearingClass();
            bear.id = t_bear.id;
            bear.Name = t_bear.Name;
            bear.Guid = t_bear.Guid ?? new Guid();
            bear.Designation = t_bear.Designation;
            bear.PitchDiameter = t_bear.PitchDiameter ?? 0;
            bear.RollerDiameter = t_bear.RollerDiameter ?? 0;
            bear.ContactAngle = t_bear.ContactAngle ?? 0;
            bear.NumberOfRoller = t_bear.NumberOfRoller ?? 0;
            bear.OuterRingDiameter = t_bear.OuterRingDiameter ?? 0;
            bear.InnerRingDiameter = t_bear.InnerRingDiameter ?? 0;
            bear.NumberOfColumns = t_bear.NumberOfColumns ?? 0;
            bear.InnerRingFrequency = t_bear.InnerRingFrequency ?? 0;
            bear.OuterRingFrequency = t_bear.OuterRingFrequency ?? 0;
            bear.RollerFrequency = t_bear.RollerFrequency ?? 0;
            bear.MaintainsFrequency = t_bear.MaintainsFrequency ?? 0;
            bear.BearingSeries = t_bear.BearingSeries;
            bear.BearingSeriesID = t_bear.BearingSeriesID ?? 0;
            bear.RPM = t_bear.RPM ?? 0;
         
            return bear;
        }

        public static T_Bearing ConvertToDB(BearingClass bear)
        {
            T_Bearing t_bear = new T_Bearing();
            t_bear.id = bear.id;
            t_bear.Name = bear.Name;
            t_bear.Guid = bear.Guid;
            t_bear.Designation = bear.Designation;
            t_bear.PitchDiameter = bear.PitchDiameter;
            t_bear.RollerDiameter = bear.RollerDiameter;
            t_bear.ContactAngle = bear.ContactAngle;
            t_bear.NumberOfRoller = bear.NumberOfRoller;
            t_bear.OuterRingDiameter = bear.OuterRingDiameter;
            t_bear.InnerRingDiameter = bear.InnerRingDiameter;
            t_bear.NumberOfColumns = bear.NumberOfColumns;
            t_bear.InnerRingFrequency = bear.InnerRingFrequency;
            t_bear.OuterRingFrequency = bear.OuterRingFrequency;
            t_bear.MaintainsFrequency = bear.MaintainsFrequency;
            t_bear.RollerFrequency = bear.RollerFrequency;
            t_bear.BearingSeries = bear.BearingSeries;
            t_bear.BearingSeriesID = bear.BearingSeriesID;
            t_bear.RPM = bear.RPM;

            return t_bear;
        }
    }
}
