using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_WirelessReceiveCard : T_WirelessReceiveCard, Iid//无线接受卡
    {
        //public int id { get; set; }//自增ID
        //public string MasterIdentifier { get; set; }//标识符
        //public string ReceiveCardName { get; set; }//名称
        //public string Code { get; set; }//代号
        //public string T_MainControlCard_IP { get; set; }//主板IP

        public T1_WirelessReceiveCard TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_WirelessReceiveCard();
            TempData.id = this.id;
            TempData.MasterIdentifier = this.MasterIdentifier;
            TempData.ReceiveCardName = this.ReceiveCardName;
            TempData.Code = this.Code;
            TempData.T_MainControlCard_IP = this.T_MainControlCard_IP;          
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.MasterIdentifier = TempData.MasterIdentifier;
            this.ReceiveCardName = TempData.ReceiveCardName;
            this.Code = TempData.Code;
            this.T_MainControlCard_IP = TempData.T_MainControlCard_IP;           
            TempData = null;
        }
    }
}
