using AIC.Core.Models;
using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_MainControlCard : T_MainControlCard, Iid//主控板
    {
        //public int id { get; set; }//自增ID
        //public string CommunicationCategory { get; set; }//传输种类
        //public int CommunicationCode { get; set; }//传输种类代码
        //public string Identifier { get; set; }//标识符
        //public string AliasName { get; set; }//别名
        //public int ACQ_Unit_Type { get; set; }//数采器类型
        //public string DataSourceCategory { get; set; }//数据来源
        //public int DataSourceCode { get; set; }//数据来源代码
        //public bool IsAlarmLatch { get; set; }//报警自锁
        //public bool IsConfiguration { get; set; }//软件允许配置
        //public bool IsHdBypass { get; set; }//硬件旁路
        //public bool IsHdConfiguration { get; set; }//硬件允许配置
        //public bool IsHdMultiplication { get; set; }//硬件倍增
        //public bool IsListen { get; set; }//监听
        //public int AsySyn { get; set; }//异步/同步
        //public string LanguageCategory { get; set; }//语言种类
        //public int LanguageCode { get; set; }//语言种类代码
        //public string MainCardCategory { get; set; }//主板种类
        //public int MainCardCode { get; set; }//主板代码
        //public string SampleMode { get; set; }//采样方式
        //public string ServerIP { get; set; }//服务器IP
        //public string WaveCategory { get; set; }//波形种类
        //public int SynWaveCode { get; set; }//波形代码
        //public string Version { get; set; }//版本
        //public float ScaleDataRange { get; set; }//比例数据量程
        //public string IP { get; set; }//主板IP

        public T1_MainControlCard TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_MainControlCard();
            TempData.id = this.id;
            TempData.CommunicationCategory = this.CommunicationCategory;
            TempData.CommunicationCode = this.CommunicationCode;
            TempData.Identifier = this.Identifier;
            TempData.AliasName = this.AliasName;
            TempData.ACQ_Unit_Type = this.ACQ_Unit_Type;
            TempData.DataSourceCategory = this.DataSourceCategory;
            TempData.DataSourceCode = this.DataSourceCode;
            TempData.IsAlarmLatch = this.IsAlarmLatch;
            TempData.IsConfiguration = this.IsConfiguration;
            TempData.IsHdBypass = this.IsHdBypass;
            TempData.IsHdConfiguration = this.IsHdConfiguration;
            TempData.IsHdMultiplication = this.IsHdMultiplication;
            TempData.IsListen = this.IsListen;
            TempData.AsySyn = this.AsySyn;
            TempData.LanguageCategory = this.LanguageCategory;
            TempData.LanguageCode = this.LanguageCode;
            TempData.MainCardCategory = this.MainCardCategory;
            TempData.MainCardCode = this.MainCardCode;
            TempData.SampleMode = this.SampleMode;
            TempData.ServerIP = this.ServerIP;
            TempData.WaveCategory = this.WaveCategory;
            TempData.SynWaveCode = this.SynWaveCode;
            TempData.Version = this.Version;
            TempData.ScaleDataRange = this.ScaleDataRange;
            TempData.IP = this.IP;           
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.CommunicationCategory = TempData.CommunicationCategory;
            this.CommunicationCode = TempData.CommunicationCode;
            this.Identifier = TempData.Identifier;
            this.AliasName = TempData.AliasName;
            this.ACQ_Unit_Type = TempData.ACQ_Unit_Type;
            this.DataSourceCategory = TempData.DataSourceCategory;
            this.DataSourceCode = TempData.DataSourceCode;
            this.IsAlarmLatch = TempData.IsAlarmLatch;
            this.IsConfiguration = TempData.IsConfiguration;
            this.IsHdBypass = TempData.IsHdBypass;
            this.IsHdConfiguration = TempData.IsHdConfiguration;
            this.IsHdMultiplication = TempData.IsHdMultiplication;
            this.IsListen = TempData.IsListen;
            this.AsySyn = TempData.AsySyn;
            this.LanguageCategory = TempData.LanguageCategory;
            this.LanguageCode = TempData.LanguageCode;
            this.MainCardCategory = TempData.MainCardCategory;
            this.MainCardCode = TempData.MainCardCode;
            this.SampleMode = TempData.SampleMode;
            this.ServerIP = TempData.ServerIP;
            this.WaveCategory = TempData.WaveCategory;
            this.SynWaveCode = TempData.SynWaveCode;
            this.Version = TempData.Version;
            this.ScaleDataRange = TempData.ScaleDataRange;
            this.IP = TempData.IP;
            TempData = null;
        }
    }
}
