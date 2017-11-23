using AIC.Core.Filters;
using AIC.CoreType;
using AIC.HistoryDataPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace AIC.HistoryDataPage.ViewModels
{
    public class TimeDomainDataViewModel : HistoricalDataViewModel
    {
        private event EventHandler<ChannelChangedEventArgs> channelAdded;
        private event EventHandler<ChannelChangedEventArgs> channelRemoved;
        private event EventHandler<ChannelDataChangedEventArgs> channelDataChanged;
        private FilterType filterType = FilterType.BandPass;
        public TimeDomainDataViewModel()
        {
            DisplayMode = SignalDisplayType.TimeDomain;
        }

        public FilterType FilterType
        {
            get { return filterType; }
            set { SetProperty(ref filterType, value); }
        }

        private BandPassFilter bpFilter;
        public BandPassFilter BPFilter
        {
            get
            {
                if (bpFilter == null)
                {
                    bpFilter = new BandPassFilter();
                }
                return bpFilter;
            }
        }

        private HighPassFilter hpFilter;
        public HighPassFilter HPFilter
        {
            get
            {
                if (hpFilter == null)
                {
                    hpFilter = new HighPassFilter();
                }
                return hpFilter;
            }
        }

        private LowPassFilter lpFilter;
        public LowPassFilter LPFilter
        {
            get
            {
                if (lpFilter == null)
                {
                    lpFilter = new LowPassFilter();
                }
                return lpFilter;
            }
        }

        public double[] Filter(double[] input, int samplePoint, double sampleFre, double rpm)
        {
            if (FilterType == FilterType.BandPass)
            {
                return BPFilter.Filter(input, samplePoint, sampleFre, rpm);
            }
            else if (FilterType == FilterType.HighPass)
            {
                return HPFilter.Filter(input, samplePoint, sampleFre, rpm);
            }
            else if (FilterType == FilterType.LowPass)
            {
                return LPFilter.Filter(input, samplePoint, sampleFre, rpm);
            }
            return input;
        }

        public void AddChannel(ChannelToken token)
        {          
            RaiseChannelAdded(token);
        }
        public void RemoveChannel(ChannelToken token)
        {
            RaiseChannelRemoved(token);
        }
        public void ChangeChannelData(IEnumerable<BaseWaveChannelToken> tokens)
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
        public IObservable<IEnumerable<BaseWaveChannelToken>> WhenChannelDataChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<ChannelDataChangedEventArgs>(
                        h => this.channelDataChanged += h,
                        h => this.channelDataChanged -= h)
                    .Select(x => x.EventArgs.Tokens as IEnumerable<BaseWaveChannelToken>);
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
