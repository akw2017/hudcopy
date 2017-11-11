using AIC.M9600.Common.MasterDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.LMModels
{
    public class T1_WireMatchingCard : T_WireMatchingCard, Iid
    {
        //public int id { get; set; }//自增ID
        //public string CardName { get; set; }//别名
        //public int CardNum { get; set; }//卡号
        //public string T_MainControlCard_IP { get; set; }//主板IP
        //public string Code { get; set; }//配板代号

        public T1_WireMatchingCard TempData { get; set; }

        public void SaveTempData()
        {
            TempData = new T1_WireMatchingCard();
            TempData.id = this.id;
            TempData.CardName = this.CardName;
            TempData.CardNum = this.CardNum;
            TempData.T_MainControlCard_IP = this.T_MainControlCard_IP;
            TempData.Code = this.Code;
        }

        public void GetTempData()
        {
            this.id = TempData.id;
            this.CardName = TempData.CardName;
            this.CardNum = TempData.CardNum;           
            this.T_MainControlCard_IP = TempData.T_MainControlCard_IP;
            this.Code = TempData.Code;
            TempData = null;
        }
    }
}
