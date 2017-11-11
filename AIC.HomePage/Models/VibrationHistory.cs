using AIC.Core.Events;
using AIC.CoreType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HomePage.Models
{
    public class VibrationHistory : INotifyPropertyChanged
    {
        public string EquipmentGlobalID { get; set; }
        public string ChannelGlobalID { get; set; }
        public int ChannelGlobalIndex { get; set; }
        public string GroupCOName { get; set; }
        public string CorporationName { get; set; }
        public string WorkShopName { get; set; }
        public string DevName { get; set; }
        public string DevSN { get; set; }
        public string Name { get; set; }
        public string MSSN { get; set; }
        public IEnumerable<string> DivFreDescriptions { get; set; }
        public SignalType SignalType { get; set; }

        //private IEnumerable<VInfoTableAMSContract> historyDatas;
        private int historyCount;

        public VibrationHistory()
        {
            SignalType = SignalType.Vibration;
        }

        public async Task InitializeAsync()
        {
            try
            {
                //string ipAddress = ServerAddress.CTLAddress.Split('/')[2].Split(':')[0];
                //LinqWhereHelper helper = new LinqWhereHelper();
                //helper.AddCondition("STIME", ">", StartTime);
                //helper.AddCondition("STIME", "<=", EndTime);
                //helper.AddCondition("AlarmGrade", "in", "2,3");
                //var response = await Task.Run(() => SocketCaller.ExecuteMethod(ipAddress, 39997, "VInfoTable_QueryAMSByTimeDivided", 1000, true, null, new object[] { ChannelGlobalIndex, helper.ToString(), StartTime, EndTime, helper.Values, 1000000 }));
                //if (!response[0].StartsWith("#"))
                //{
                //    List<VInfoTableAMSContract> amsContractList = new List<VInfoTableAMSContract>();
                //    for (int i = 0; i < response.Length; i++)
                //    {
                //        TimeDividedVInfoTableAMSContract contract = JsonConvert.DeserializeObject<TimeDividedVInfoTableAMSContract>(response[i]);
                //        amsContractList.AddRange(contract.Items);
                //    }
                //    HistoryCount = amsContractList.Count;
                //    HistoryDatas = amsContractList.OrderBy(o => o.Date).ToArray();
                //}
                //else
                //{
                //    Exception e = new Exception(response[0]);
                //    e.Data.Add("通道", string.Format("{0}.{1}.{2}.{3}.{4}.{5}.{6}", GroupCOName, CorporationName, WorkShopName, DevName, DevSN, Name, MSSN));
                //    throw e;
                //}
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("主程序-历史报警-加载数据", ex));
            }
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public IEnumerable<VInfoTableAMSContract> HistoryDatas
        //{
        //    get { return historyDatas; }
        //    set
        //    {
        //        if (historyDatas != value)
        //        {
        //            historyDatas = value;
        //            NotifyPropertyChanged("HistoryDatas");
        //        }
        //    }
        //}
        public int HistoryCount
        {
            get { return historyCount; }
            set
            {
                if (historyCount != value)
                {
                    historyCount = value;
                    NotifyPropertyChanged("HistoryCount");
                }
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify using pre-made PropertyChangedEventArgs
        /// </summary>
        /// <param name="args"></param>
        protected void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        /// <summary>
        /// Notify using String property name
        /// </summary>
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
