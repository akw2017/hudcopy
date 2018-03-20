using AIC.Core.Filters;
using AIC.CoreType;
using AIC.HistoryDataPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace AIC.HistoryDataPage.ViewModels
{
    public class DistributionMapViewModel : HistoricalDataViewModel
    {
        private event EventHandler<ChannelChangedEventArgs> channelAdded;
        private event EventHandler<ChannelChangedEventArgs> channelRemoved;
        private event EventHandler<ChannelDataChangedEventArgs> channelDataChanged;
        public DistributionMapViewModel()
        {
            DisplayMode = SignalDisplayType.DistributionMap;
        }      

        public void AddChannel(ChannelToken token)
        {          
            RaiseChannelAdded(token);
        }
        public void RemoveChannel(ChannelToken token)
        {
            RaiseChannelRemoved(token);
        }
        public void ChangeChannelData(IEnumerable<ChannelToken> tokens)
        {
            RaiseChannelDataChanged(tokens);
        }

        public void RaiseChannelAdded(ChannelToken token)
        {
            if (channelAdded != null)
            {
                channelAdded(this, new ChannelChangedEventArgs(token));
            }
        }
        public void RaiseChannelRemoved(ChannelToken token)
        {
            if (channelRemoved != null)
            {
                channelRemoved(this, new ChannelChangedEventArgs(token));
            }
        }
        public void RaiseChannelDataChanged(IEnumerable<ChannelToken> tokens)
        {
            if (channelDataChanged != null)
            {
                channelDataChanged(this, new ChannelDataChangedEventArgs(tokens));
            }
        }

        public IObservable<ChannelToken> WhenChannelAdded
        {
            get
            {
                return Observable
                    .FromEventPattern<ChannelChangedEventArgs>(
                        h => this.channelAdded += h,
                        h => this.channelAdded -= h)
                    .Select(x => x.EventArgs.Token);
            }
        }
        public IObservable<ChannelToken> WhenChannelRemoved
        {
            get
            {
                return Observable
                    .FromEventPattern<ChannelChangedEventArgs>(
                        h => this.channelRemoved += h,
                        h => this.channelRemoved -= h)
                    .Select(x => x.EventArgs.Token);
            }
        }
        public IObservable<IEnumerable<ChannelToken>> WhenChannelDataChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<ChannelDataChangedEventArgs>(
                        h => this.channelDataChanged += h,
                        h => this.channelDataChanged -= h)
                    .Select(x => x.EventArgs.Tokens as IEnumerable<ChannelToken>);
            }
        }

        #region Property ShowDetail
        private bool showDetail = true;
        public bool ShowDetail
        {
            get { return showDetail; }
            set
            {
                if (showDetail != value)
                {
                    showDetail = value;
                    this.OnPropertyChanged(() => ShowDetail);
                }
            }
        }
        #endregion
    }
}
