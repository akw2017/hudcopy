



















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `MasterDB`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=localhost;User ID=sa;password=**zapped**;Initial Catalog=AIC9600Master`
//     Schema:                 ``
//     Include Views:          `False`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace AIC.M9600.Common.MasterDB.Generated
{

	public partial class MasterORM : Database
	{
		public MasterORM() 
			: base("MasterDB")
		{
			CommonConstruct();
		}

		public MasterORM(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			MasterORM GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static MasterORM GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new MasterORM();
        }

		[ThreadStatic] static MasterORM _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static MasterORM repo { get { return MasterORM.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    

	[TableName("dbo.S_GlobalSetting")]



	[PrimaryKey("ID", AutoIncrement=false)]


	[ExplicitColumns]

    public partial class S_GlobalSetting : MasterORM.Record<S_GlobalSetting>  
    {



		[Column] public string ID { get; set; }





		[Column] public string Type { get; set; }





		[Column] public string Value { get; set; }





		[Column] public string Description { get; set; }





		[Column] public DateTime? CreateTime { get; set; }





		[Column] public DateTime? UpdateTime { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("dbo.S_SlaveDatabase")]



	[PrimaryKey("ID")]




	[ExplicitColumns]

    public partial class S_SlaveDatabase : MasterORM.Record<S_SlaveDatabase>  
    {



		[Column] public int ID { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string DBIP { get; set; }





		[Column] public string DBName { get; set; }





		[Column] public string DBUser { get; set; }





		[Column] public string DBPassword { get; set; }





		[Column] public int WorkMode { get; set; }





		[Column] public string Description { get; set; }





		[Column] public string MutiPartition { get; set; }





		[Column] public DateTime? CreateTime { get; set; }





		[Column] public DateTime? UpdateTime { get; set; }





		[Column] public int? Status { get; set; }



	}

    

	[TableName("dbo.T_AbstractChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_AbstractChannelInfo : MasterORM.Record<T_AbstractChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string Organization { get; set; }





		[Column] public string T_Device_Name { get; set; }





		[Column] public string T_Device_Code { get; set; }





		[Column] public Guid? T_Device_Guid { get; set; }





		[Column] public string T_Item_Name { get; set; }





		[Column] public string T_Item_Code { get; set; }





		[Column] public Guid? T_Item_Guid { get; set; }





		[Column] public int CHNum { get; set; }





		[Column] public int SubCHNum { get; set; }





		[Column] public bool IsUploadData { get; set; }





		[Column] public string Unit { get; set; }





		[Column] public string SVTypeCategory { get; set; }





		[Column] public int SVTypeCode { get; set; }





		[Column] public string LocalSaveCategory { get; set; }





		[Column] public int LocalSaveCode { get; set; }





		[Column] public bool IsBypass { get; set; }





		[Column] public int DelayAlarmTime { get; set; }





		[Column] public int NotOKDelayAlarmTime { get; set; }





		[Column] public bool IsLogic { get; set; }





		[Column] public string LogicExpression { get; set; }





		[Column] public string Remarks { get; set; }





		[Column] public string Extra_Information { get; set; }





		[Column] public string AlarmStrategy { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_AbstractSlotInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_AbstractSlotInfo : MasterORM.Record<T_AbstractSlotInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string InSignalCategory { get; set; }





		[Column] public int InSignalCode { get; set; }





		[Column] public int SlotNum { get; set; }





		[Column] public string SlotName { get; set; }





		[Column] public int? UploadIntevalTime { get; set; }





		[Column] public bool? IsInput { get; set; }





		[Column] public string Unit { get; set; }





		[Column] public string Version { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_WireMatchingCard_Code { get; set; }



	}

    

	[TableName("dbo.T_AnalogRransducerInChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_AnalogRransducerInChannelInfo : MasterORM.Record<T_AnalogRransducerInChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string TransformMethod { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_AnalogRransducerInSlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_AnalogRransducerInSlot : MasterORM.Record<T_AnalogRransducerInSlot>  
    {



		[Column] public long id { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_AnalogRransducerOutChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_AnalogRransducerOutChannelInfo : MasterORM.Record<T_AnalogRransducerOutChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string TransformMethod { get; set; }





		[Column] public string SourceChannelInfo { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_AnalogRransducerOutSlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_AnalogRransducerOutSlot : MasterORM.Record<T_AnalogRransducerOutSlot>  
    {



		[Column] public long id { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_Bearing")]




	[ExplicitColumns]

    public partial class T_Bearing : MasterORM.Record<T_Bearing>  
    {



		[Column] public long id { get; set; }





		[Column] public string Vendor { get; set; }





		[Column] public Guid? Guid { get; set; }





		[Column] public string Designation { get; set; }





		[Column] public double? PitchDiameter { get; set; }





		[Column] public double? RollerDiameter { get; set; }





		[Column] public int? NumberOfRoller { get; set; }





		[Column] public double? ContactAngle { get; set; }





		[Column] public double? OuterRingDiameter { get; set; }





		[Column] public double? InnerRingDiameter { get; set; }





		[Column] public int? NumberOfColumns { get; set; }





		[Column] public double? InnerRingFrequency { get; set; }





		[Column] public double? OuterRingFrequency { get; set; }





		[Column] public double? RollerFrequency { get; set; }





		[Column] public double? MaintainsFrequency { get; set; }





		[Column] public int? BearingSeriesID { get; set; }





		[Column] public string BearingSeries { get; set; }





		[Column] public double? RPM { get; set; }



	}

    

	[TableName("dbo.T_DBExportRecord")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_DBExportRecord : MasterORM.Record<T_DBExportRecord>  
    {



		[Column] public long id { get; set; }





		[Column] public string T_User_Name { get; set; }





		[Column] public string T_User_Code { get; set; }





		[Column] public Guid T_Item_Guid { get; set; }





		[Column] public string T_Item_Code { get; set; }





		[Column] public string T_Item_FirstOperateId { get; set; }





		[Column] public string T_Item_OperateCount { get; set; }





		[Column] public DateTime OperateTime { get; set; }





		[Column] public string Remarks { get; set; }





		[Column] public string IP { get; set; }





		[Column] public string Mac { get; set; }



	}

    

	[TableName("dbo.T_DBImportRecord")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_DBImportRecord : MasterORM.Record<T_DBImportRecord>  
    {



		[Column] public long id { get; set; }





		[Column] public string T_User_Name { get; set; }





		[Column] public string T_User_Code { get; set; }





		[Column] public Guid T_Item_Guid { get; set; }





		[Column] public string T_Item_Code { get; set; }





		[Column] public string T_Item_FirstOperateId { get; set; }





		[Column] public string T_Item_OperateCount { get; set; }





		[Column] public DateTime OperateTime { get; set; }





		[Column] public string Remarks { get; set; }





		[Column] public string IP { get; set; }





		[Column] public string Mac { get; set; }



	}

    

	[TableName("dbo.T_Device")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_Device : MasterORM.Record<T_Device>  
    {



		[Column] public long id { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Code { get; set; }





		[Column] public Guid Guid { get; set; }





		[Column] public string Model { get; set; }





		[Column] public string Chart_Number { get; set; }





		[Column] public string Manufacturer { get; set; }





		[Column] public string Serial_Number { get; set; }





		[Column] public string Material { get; set; }





		[Column] public string Vendor { get; set; }





		[Column] public string Grade { get; set; }





		[Column] public string Status { get; set; }





		[Column] public string Class { get; set; }





		[Column] public string Installation_Site { get; set; }





		[Column] public string Person_In_Charge { get; set; }





		[Column] public DateTime? Date_Of_Production { get; set; }





		[Column] public DateTime? Date_Of_Entering { get; set; }





		[Column] public DateTime? Date_Of_Start { get; set; }





		[Column] public DateTime? Create_Time { get; set; }





		[Column] public DateTime? Modify_Time { get; set; }





		[Column] public int Sort_No { get; set; }





		[Column] public bool Is_Disabled { get; set; }





		[Column] public string Extend_Info { get; set; }





		[Column] public Guid? T_Diagnosis_Model_Guid { get; set; }





		[Column] public Guid? T_Organization_Guid { get; set; }





		[Column] public string T_Organization_Code { get; set; }





		[Column] public int? T_Organization_Level { get; set; }





		[Column] public string Remarks { get; set; }



	}

    

	[TableName("dbo.T_Diagnosis_Model")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_Diagnosis_Model : MasterORM.Record<T_Diagnosis_Model>  
    {



		[Column] public long id { get; set; }





		[Column] public string Name { get; set; }





		[Column] public Guid Guid { get; set; }





		[Column] public string Structure { get; set; }



	}

    

	[TableName("dbo.T_DigitRransducerInChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_DigitRransducerInChannelInfo : MasterORM.Record<T_DigitRransducerInChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string TransformMethod { get; set; }





		[Column] public string DigitRransducerInfo { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_DigitRransducerInSlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_DigitRransducerInSlot : MasterORM.Record<T_DigitRransducerInSlot>  
    {



		[Column] public long id { get; set; }





		[Column] public string ModBusTCPIP { get; set; }





		[Column] public bool? EnableModBusTCPIP { get; set; }





		[Column] public string ModBus485 { get; set; }





		[Column] public bool? EnableModBus485 { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_DigitRransducerOutChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_DigitRransducerOutChannelInfo : MasterORM.Record<T_DigitRransducerOutChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string TransformMethod { get; set; }





		[Column] public string DigitRransducerInfo { get; set; }





		[Column] public string SourceChannelInfo { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_DigitRransducerOutSlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_DigitRransducerOutSlot : MasterORM.Record<T_DigitRransducerOutSlot>  
    {



		[Column] public long id { get; set; }





		[Column] public string ModBusTCPIP { get; set; }





		[Column] public bool? EnableModBusTCPIP { get; set; }





		[Column] public string ModBus485 { get; set; }





		[Column] public bool? EnableModBus485 { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_DigitTachometerChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_DigitTachometerChannelInfo : MasterORM.Record<T_DigitTachometerChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string RPMChannelInfo { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_DigitTachometerSlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_DigitTachometerSlot : MasterORM.Record<T_DigitTachometerSlot>  
    {



		[Column] public long id { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_DivFreInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_DivFreInfo : MasterORM.Record<T_DivFreInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public Guid Guid { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string Name { get; set; }





		[Column] public DateTime? Create_Time { get; set; }





		[Column] public DateTime? Modify_Time { get; set; }





		[Column] public string Remarks { get; set; }





		[Column] public Guid T_Item_Guid { get; set; }





		[Column] public string T_Item_Name { get; set; }





		[Column] public string T_Item_Code { get; set; }





		[Column] public int DivFreCode { get; set; }





		[Column] public string BasedOnRPM { get; set; }





		[Column] public string FixedFre { get; set; }





		[Column] public string BasedOnRange { get; set; }





		[Column] public string AlarmStrategy { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_EddyCurrentDisplacementChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_EddyCurrentDisplacementChannelInfo : MasterORM.Record<T_EddyCurrentDisplacementChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string OtherInfo { get; set; }





		[Column] public string VibrationAddition { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_EddyCurrentDisplacementSlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_EddyCurrentDisplacementSlot : MasterORM.Record<T_EddyCurrentDisplacementSlot>  
    {



		[Column] public long id { get; set; }





		[Column] public string WaveInfo { get; set; }





		[Column] public string SampleMode { get; set; }





		[Column] public bool? Is24V { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_EddyCurrentKeyPhaseChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_EddyCurrentKeyPhaseChannelInfo : MasterORM.Record<T_EddyCurrentKeyPhaseChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string ThresholdInfo { get; set; }





		[Column] public string VibrationAddition { get; set; }





		[Column] public string RPMChannelInfo { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_EddyCurrentKeyPhaseSlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_EddyCurrentKeyPhaseSlot : MasterORM.Record<T_EddyCurrentKeyPhaseSlot>  
    {



		[Column] public long id { get; set; }





		[Column] public bool? Is24V { get; set; }





		[Column] public string EddyCurrentRPMSampleInfo { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_EddyCurrentTachometerChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_EddyCurrentTachometerChannelInfo : MasterORM.Record<T_EddyCurrentTachometerChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string ThresholdInfo { get; set; }





		[Column] public string VibrationAddition { get; set; }





		[Column] public string RPMChannelInfo { get; set; }





		[Column] public string RPMCouplingInfo { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_EddyCurrentTachometerSlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_EddyCurrentTachometerSlot : MasterORM.Record<T_EddyCurrentTachometerSlot>  
    {



		[Column] public long id { get; set; }





		[Column] public bool? Is24V { get; set; }





		[Column] public string EddyCurrentRPMSampleInfo { get; set; }





		[Column] public bool? IsEnableMainCH { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_IEPEChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_IEPEChannelInfo : MasterORM.Record<T_IEPEChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string CalibrationlInfo { get; set; }





		[Column] public string OtherInfo { get; set; }





		[Column] public string VibrationAddition { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_IEPESlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_IEPESlot : MasterORM.Record<T_IEPESlot>  
    {



		[Column] public long id { get; set; }





		[Column] public int? Integration { get; set; }





		[Column] public string WaveInfo { get; set; }





		[Column] public string SampleMode { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_Item")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_Item : MasterORM.Record<T_Item>  
    {



		[Column] public long id { get; set; }





		[Column] public Guid Guid { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string ChannelHDID { get; set; }





		[Column] public int? CardNum { get; set; }





		[Column] public int? SlotNum { get; set; }





		[Column] public int? CHNum { get; set; }





		[Column] public Guid T_Device_Guid { get; set; }





		[Column] public string T_Device_Code { get; set; }





		[Column] public string Remarks { get; set; }





		[Column] public DateTime? Create_Time { get; set; }





		[Column] public DateTime? Modify_Time { get; set; }





		[Column] public int Sort_No { get; set; }





		[Column] public bool Is_Disabled { get; set; }





		[Column] public string IP { get; set; }





		[Column] public string Identifier { get; set; }





		[Column] public string ServerIP { get; set; }





		[Column] public int ItemType { get; set; }





		[Column] public string SlaveIdentifier { get; set; }



	}

    

	[TableName("dbo.T_MainControlCard")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_MainControlCard : MasterORM.Record<T_MainControlCard>  
    {



		[Column] public long id { get; set; }





		[Column] public string CommunicationCategory { get; set; }





		[Column] public int CommunicationCode { get; set; }





		[Column] public string Identifier { get; set; }





		[Column] public string AliasName { get; set; }





		[Column] public int? ACQ_Unit_Type { get; set; }





		[Column] public string DataSourceCategory { get; set; }





		[Column] public int DataSourceCode { get; set; }





		[Column] public bool IsAlarmLatch { get; set; }





		[Column] public bool IsConfiguration { get; set; }





		[Column] public bool IsHdBypass { get; set; }





		[Column] public bool IsHdConfiguration { get; set; }





		[Column] public bool IsHdMultiplication { get; set; }





		[Column] public bool IsListen { get; set; }





		[Column] public int AsySyn { get; set; }





		[Column] public string LanguageCategory { get; set; }





		[Column] public int LanguageCode { get; set; }





		[Column] public string MainCardCategory { get; set; }





		[Column] public int MainCardCode { get; set; }





		[Column] public string SampleMode { get; set; }





		[Column] public string ServerIP { get; set; }





		[Column] public string WaveCategory { get; set; }





		[Column] public int SynWaveCode { get; set; }





		[Column] public string Version { get; set; }





		[Column] public double ScaleDataRange { get; set; }





		[Column] public string IP { get; set; }



	}

    

	[TableName("dbo.T_Menu")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_Menu : MasterORM.Record<T_Menu>  
    {



		[Column] public long id { get; set; }





		[Column] public string Name { get; set; }





		[Column] public Guid Guid { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string Type { get; set; }





		[Column] public int InternalNumber { get; set; }





		[Column] public string ShowText { get; set; }



	}

    

	[TableName("dbo.T_OperateRecord")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_OperateRecord : MasterORM.Record<T_OperateRecord>  
    {



		[Column] public long id { get; set; }





		[Column] public string T_User_Name { get; set; }





		[Column] public string T_User_Code { get; set; }





		[Column] public short OperateType { get; set; }





		[Column] public DateTime OperateTime { get; set; }





		[Column] public string Remarks { get; set; }



	}

    

	[TableName("dbo.T_Organization")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_Organization : MasterORM.Record<T_Organization>  
    {



		[Column] public long id { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Code { get; set; }





		[Column] public Guid Guid { get; set; }





		[Column] public int Level { get; set; }





		[Column] public int Sort_No { get; set; }





		[Column] public DateTime? Create_Time { get; set; }





		[Column] public DateTime? Modify_Time { get; set; }





		[Column] public bool Is_Disabled { get; set; }





		[Column] public string Parent_Code { get; set; }





		[Column] public Guid? Parent_Guid { get; set; }





		[Column] public int? Parent_Level { get; set; }





		[Column] public string Remarks { get; set; }





		[Column] public int NodeType { get; set; }



	}

    

	[TableName("dbo.T_OrganizationPrivilege")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_OrganizationPrivilege : MasterORM.Record<T_OrganizationPrivilege>  
    {



		[Column] public long id { get; set; }





		[Column] public string Name { get; set; }





		[Column] public Guid Guid { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_Organization_Code { get; set; }





		[Column] public Guid T_Organization_Guid { get; set; }



	}

    

	[TableName("dbo.T_RelayChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_RelayChannelInfo : MasterORM.Record<T_RelayChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_RelaySlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_RelaySlot : MasterORM.Record<T_RelaySlot>  
    {



		[Column] public long id { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractSlotInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_Role")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_Role : MasterORM.Record<T_Role>  
    {



		[Column] public long id { get; set; }





		[Column] public string Name { get; set; }





		[Column] public Guid Guid { get; set; }





		[Column] public string Code { get; set; }





		[Column] public int Sort_No { get; set; }





		[Column] public bool Is_Admin { get; set; }





		[Column] public bool Is_SuperAdmin { get; set; }





		[Column] public bool Is_Disabled { get; set; }





		[Column] public bool? Is_Cooperator { get; set; }





		[Column] public bool? Is_Opened { get; set; }





		[Column] public string Remark { get; set; }



	}

    

	[TableName("dbo.T_TransmissionCard")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_TransmissionCard : MasterORM.Record<T_TransmissionCard>  
    {



		[Column] public long id { get; set; }





		[Column] public string SlaveIdentifier { get; set; }





		[Column] public int TransmissionType { get; set; }





		[Column] public string Version { get; set; }





		[Column] public string TransmissionName { get; set; }





		[Column] public int? WorkTime { get; set; }





		[Column] public int? SleepTime { get; set; }





		[Column] public double? BatteryEnergy { get; set; }





		[Column] public string Remarks { get; set; }





		[Column] public string ExtraInfo { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_WirelessReceiveCard_Code { get; set; }



	}

    

	[TableName("dbo.T_User")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_User : MasterORM.Record<T_User>  
    {



		[Column] public long id { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string Alias_Name { get; set; }





		[Column] public string Password { get; set; }





		[Column] public string T_Role_Name { get; set; }





		[Column] public string T_Role_Code { get; set; }





		[Column] public Guid? T_Role_Guid { get; set; }





		[Column] public string T_Menu_Name { get; set; }





		[Column] public string T_Menu_Code { get; set; }





		[Column] public Guid? T_Menu_Guid { get; set; }





		[Column] public string T_OrganizationPrivilege_Name { get; set; }





		[Column] public string T_OrganizationPrivilege_Code { get; set; }





		[Column] public Guid? T_OrganizationPrivilege_Guid { get; set; }





		[Column] public string Person_Telephone { get; set; }





		[Column] public string Office_Telephone { get; set; }





		[Column] public string Email { get; set; }





		[Column] public Guid Guid { get; set; }





		[Column] public string Contact { get; set; }





		[Column] public bool Is_Disabled { get; set; }





		[Column] public bool Is_Inside { get; set; }



	}

    

	[TableName("dbo.T_WirelessReceiveCard")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_WirelessReceiveCard : MasterORM.Record<T_WirelessReceiveCard>  
    {



		[Column] public long id { get; set; }





		[Column] public string MasterIdentifier { get; set; }





		[Column] public string ReceiveCardName { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_MainControlCard_IP { get; set; }



	}

    

	[TableName("dbo.T_WirelessScalarChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_WirelessScalarChannelInfo : MasterORM.Record<T_WirelessScalarChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string TransformMethod { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_WirelessScalarSlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_WirelessScalarSlot : MasterORM.Record<T_WirelessScalarSlot>  
    {



		[Column] public long id { get; set; }





		[Column] public int SlotNum { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_TransmissionCard_Code { get; set; }



	}

    

	[TableName("dbo.T_WirelessVibrationChannelInfo")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_WirelessVibrationChannelInfo : MasterORM.Record<T_WirelessVibrationChannelInfo>  
    {



		[Column] public long id { get; set; }





		[Column] public string CalibrationlInfo { get; set; }





		[Column] public string OtherInfo { get; set; }





		[Column] public string VibrationAddition { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_AbstractChannelInfo_Code { get; set; }



	}

    

	[TableName("dbo.T_WirelessVibrationSlot")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_WirelessVibrationSlot : MasterORM.Record<T_WirelessVibrationSlot>  
    {



		[Column] public long id { get; set; }





		[Column] public int Integration { get; set; }





		[Column] public string Unit { get; set; }





		[Column] public string SampleFreCategory { get; set; }





		[Column] public int SampleFreCode { get; set; }





		[Column] public string SamplePointCategory { get; set; }





		[Column] public int SamplePointCode { get; set; }





		[Column] public int SlotNum { get; set; }





		[Column] public string Code { get; set; }





		[Column] public string T_TransmissionCard_Code { get; set; }



	}

    

	[TableName("dbo.T_WireMatchingCard")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_WireMatchingCard : MasterORM.Record<T_WireMatchingCard>  
    {



		[Column] public long id { get; set; }





		[Column] public string CardName { get; set; }





		[Column] public int CardNum { get; set; }





		[Column] public string T_MainControlCard_IP { get; set; }





		[Column] public string Code { get; set; }



	}

    

	[TableName("dbo.T_WX_Forwarding_Rule")]



	[PrimaryKey("id")]




	[ExplicitColumns]

    public partial class T_WX_Forwarding_Rule : MasterORM.Record<T_WX_Forwarding_Rule>  
    {



		[Column] public long id { get; set; }





		[Column] public Guid T_Device_Guid { get; set; }





		[Column] public Guid T_Item_Guid { get; set; }





		[Column] public string Range_Rule { get; set; }





		[Column] public string Conditional_Rule { get; set; }





		[Column] public string WX_Target_Id_Array { get; set; }





		[Column] public string WX_Target_Id_Name { get; set; }





		[Column] public string Default_Content { get; set; }





		[Column] public string Forward_Content { get; set; }





		[Column] public bool Is_Auto_Start { get; set; }





		[Column] public bool Is_Statis { get; set; }





		[Column] public double Active_Interval { get; set; }





		[Column] public double Active_Percent { get; set; }





		[Column] public string DateFormat { get; set; }



	}


}
