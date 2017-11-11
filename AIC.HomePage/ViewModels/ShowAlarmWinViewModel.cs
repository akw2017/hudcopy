using AIC.Core.Events;
using AIC.HomePage.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIC.HomePage.ViewModels
{
    public class ShowAlarmWinViewModel : BindableBase
    {
        //private DatabaseComponent database;
        private ObservableCollection<VibrationHistory> vibrationHistoryCollection = new ObservableCollection<VibrationHistory>();

        private bool openHistory = false;
        private VibrationHistory selectedVibrationHistory;
        private int historyCount;

        public ShowAlarmWinViewModel()
        {
            //RefreshHistoryCommand = AsyncCommand.Create(RefreshHistory);
        }

        public async Task InitializeAsync()
        {
            try
            {
                //database = await DatabaseComponent.Instance;
                DateTime vhEndTime = DateTime.Now;
                DateTime vhStartTime = DateTime.Now.Subtract(TimeSpan.FromHours(24));
                //var channels = database.ChannelContracts;
                //var dict = await database.CheckAlarmChannel(vhStartTime, vhEndTime, channels.Select(o => o.ChannelGlobalIndex).ToArray());
                //if (dict != null)
                //{
                //    foreach (var item in dict)
                //    {
                //        if (item.Value > 0)
                //        {
                //            var c = channels.Where(o => o.ChannelGlobalIndex == item.Key).Single();
                //            VibrationHistory vh = new VibrationHistory();
                //            vh.ChannelGlobalID = c.ChannelGlobalID;
                //            vh.ChannelGlobalIndex = c.ChannelGlobalIndex;
                //            vh.GroupCOName = c.GroupCOName;
                //            vh.CorporationName = c.CorporationName;
                //            vh.WorkShopName = c.WorkShopName;
                //            vh.DevName = c.DevName;
                //            vh.DevSN = c.DevSN;
                //            vh.Name = c.Name;
                //            vh.MSSN = c.MSSN;
                //            vh.StartTime = vhStartTime;
                //            vh.EndTime = vhEndTime;
                //            await vh.InitializeAsync();
                //            vibrationHistoryCollection.Add(vh);
                //            OpenHistory = true;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("历史报警-初始化失败", ex));
            }
        }

        private async Task RefreshHistory()
        {
            try
            {
                vibrationHistoryCollection.Clear();
                DateTime vhEndTime = DateTime.Now;
                DateTime vhStartTime = DateTime.Now.Subtract(TimeSpan.FromHours(24));
                //var channels = database.ChannelContracts;
                //var dict = await database.CheckAlarmChannel(vhStartTime, vhEndTime, channels.Select(o => o.ChannelGlobalIndex).ToArray());
                //if (dict != null)
                //{
                //    foreach (var item in dict)
                //    {
                //        if (item.Value > 0)
                //        {
                //            var c = channels.Where(o => o.ChannelGlobalIndex == item.Key).Single();
                //            VibrationHistory vh = new VibrationHistory();
                //            vh.ChannelGlobalID = c.ChannelGlobalID;
                //            vh.ChannelGlobalIndex = c.ChannelGlobalIndex;
                //            vh.GroupCOName = c.GroupCOName;
                //            vh.CorporationName = c.CorporationName;
                //            vh.WorkShopName = c.WorkShopName;
                //            vh.DevName = c.DevName;
                //            vh.DevSN = c.DevSN;
                //            vh.Name = c.Name;
                //            vh.MSSN = c.MSSN;
                //            vh.StartTime = vhStartTime;
                //            vh.EndTime = vhEndTime;
                //            await vh.InitializeAsync();
                //            vibrationHistoryCollection.Add(vh);
                //            OpenHistory = true;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("历史报警-刷新记录", ex));
            }
        }

        public VibrationHistory SelectedVibrationHistory
        {
            get { return selectedVibrationHistory; }
            set
            {
                if (selectedVibrationHistory != value)
                {
                    selectedVibrationHistory = value;
                    OnPropertyChanged(() => SelectedVibrationHistory);
                    if (selectedVibrationHistory != null)
                    {
                        HistoryCount = selectedVibrationHistory.HistoryCount;
                    }
                }
            }
        }

        public bool OpenHistory
        {
            get { return openHistory; }
            set
            {
                if (openHistory != value)
                {
                    openHistory = value;
                    OnPropertyChanged(() => this.OpenHistory);
                }
            }
        }

        public int HistoryCount
        {
            get { return historyCount; }
            set
            {
                if (historyCount != value)
                {
                    historyCount = value;
                    OnPropertyChanged("HistoryCount");
                }
            }
        }

        private ICommand refreshHistoryCommand;
        public ICommand RefreshHistoryCommand
        {
            get
            {
                return this.refreshHistoryCommand ?? (this.refreshHistoryCommand = new DelegateCommand(() => this.RefreshHistory()));
            }
        }
       
        public IEnumerable<VibrationHistory> VibrationHistories { get { return vibrationHistoryCollection; } }
    }
}
