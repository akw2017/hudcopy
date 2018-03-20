using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using AIC.CoreType;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using AIC.HistoryDataPage.Models;

namespace AIC.HistoryDataPage.ViewModels
{
    
    public class TrackChangedEventArgs:EventArgs
    {
        private IEnumerable<BaseWaveChannelToken> _tokens;

        public TrackChangedEventArgs(IEnumerable<BaseWaveChannelToken> tokens)
        {
            _tokens = tokens;
        }
        public IEnumerable<BaseWaveChannelToken> Tokens { get { return _tokens; } }
    }
    public class ChannelDataChangedEventArgs:EventArgs
    {
        private IEnumerable<ChannelToken> _tokens;

        public ChannelDataChangedEventArgs(IEnumerable<ChannelToken> tokens)
        {
            _tokens = tokens;
        }
        public IEnumerable<ChannelToken> Tokens { get { return _tokens; } }
    }
    public class ChannelChangedEventArgs : EventArgs
    {
        private ChannelToken _token;

        public ChannelChangedEventArgs(ChannelToken token)
        {
            _token = token;
        }
        public ChannelToken Token { get { return _token; } }
    }

    public class RMSReplayDataViewModel : HistoricalDataViewModel
    {
        private event EventHandler<ChannelChangedEventArgs> channelAdded;
        private event EventHandler<ChannelChangedEventArgs> channelRemoved;
        private event EventHandler<ChannelDataChangedEventArgs> channelDataChanged;
        private event EventHandler<TrackChangedEventArgs> trackChanged;
        private event EventHandler<TrackChangedEventArgs> track2Changed;
        private event EventHandler<bool> multicursorlChannged;
        private ObservableCollection<BaseWaveChannelToken> contractsCollection;
        public bool AddAlarmMarker = false;

        public RMSReplayDataViewModel(bool addAlarmMarker = false)
        {
            DisplayMode = SignalDisplayType.RMSTrend;
            contractsCollection = new ObservableCollection<BaseWaveChannelToken>();
            allGraphTypes = new Dictionary<string, Object>();
            allGraphTypes.Add("转速三维谱", "RPM3D");
            allGraphTypes.Add("阶次分析", "OrderAnalysis");

            selectedGraphTypes = new Dictionary<string, Object>();
            selectedGraphTypes.Add("转速三维谱", "RPM3D");
            selectedGraphTypes.Add("阶次分析", "OrderAnalysis");

            AddAlarmMarker = addAlarmMarker;
        }

        public void AddChannel(ChannelToken token)
        {
            if ((token is BaseWaveChannelToken) && !contractsCollection.Contains(token))
            {
                contractsCollection.Add(token as BaseWaveChannelToken);
            }
            RaiseChannelAdded(token);
        }
        public void RemoveChannel(ChannelToken token)
        {
            if ((token is BaseWaveChannelToken) && contractsCollection.Contains(token))
            {
                contractsCollection.Remove(token as BaseWaveChannelToken);
            }
            RaiseChannelRemoved(token);
        }
        public void ChannelDataChanged(IEnumerable<ChannelToken> tokens)
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
        public void RaiseTrackChanged(IEnumerable<BaseWaveChannelToken> tokens)
        {
            if (trackChanged != null)
            {
                trackChanged(this, new TrackChangedEventArgs(tokens));
            }
        }
        public void RaiseTrack2Changed(IEnumerable<BaseWaveChannelToken> tokens)
        {
            if (track2Changed != null)
            {
                track2Changed(this, new TrackChangedEventArgs(tokens));
            }
        }
        public void CursorlChannged(bool ismulticursor)
        {
            if (multicursorlChannged != null)
            {
                multicursorlChannged(this, ismulticursor);
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
                    .Select(x => x.EventArgs.Tokens);
            }
        }
        public IObservable<IEnumerable<BaseWaveChannelToken>> WhenTrackChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<TrackChangedEventArgs>(
                        h => this.trackChanged += h,
                        h => this.trackChanged -= h)
                   .Select(x => x.EventArgs.Tokens);// .Select(x => x.EventArgs.Tokens).Throttle(TimeSpan.FromMilliseconds(500));
            }
        }

        public IObservable<IEnumerable<BaseWaveChannelToken>> WhenTrack2Changed
        {
            get
            {
                return Observable
                    .FromEventPattern<TrackChangedEventArgs>(
                        h => this.track2Changed += h,
                        h => this.track2Changed -= h)
                   .Select(x => x.EventArgs.Tokens);// .Select(x => x.EventArgs.Tokens).Throttle(TimeSpan.FromMilliseconds(500));
            }
        }

        public IObservable<bool> WhenCursorlChannged
        {
            get
            {
                return Observable
                    .FromEventPattern<bool>(
                        h => this.multicursorlChannged += h,
                        h => this.multicursorlChannged -= h)
                        .Select(x => x.EventArgs);// .Select(x => x.EventArgs.Tokens).Throttle(TimeSpan.FromMilliseconds(500));
            }
        }

        private Dictionary<string, Object> allGraphTypes;
        public Dictionary<string, Object> AllGraphTypes
        {
            get { return allGraphTypes; }
            set
            {
                if (allGraphTypes != value)
                {
                    allGraphTypes = value;
                    OnPropertyChanged("AllGraphTypes");
                }
            }
        }

        private Dictionary<string, Object> selectedGraphTypes;
        public Dictionary<string, Object> SelectedGraphTypes
        {
            get { return selectedGraphTypes; }
            set
            {
                if (selectedGraphTypes != value)
                {
                    selectedGraphTypes = value;
                    OnPropertyChanged("SelectedGraphTypes");
                }
            }
        }

        #region Property StartTime
        private DateTime? startTime = DateTime.Now.Subtract(TimeSpan.FromHours(1));
        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                if (value != startTime)
                {
                    startTime = value;
                    this.OnPropertyChanged("StartTime");
                }
            }
        }
        #endregion

        #region Property EndTime
        private DateTime? endTime = DateTime.Now;
        public DateTime? EndTime
        {
            get { return endTime; }
            set
            {
                if (value != endTime)
                {
                    endTime = value;
                    this.OnPropertyChanged("EndTime");
                }
            }
        }
        #endregion

        private int advancedCount;
        public int AdvancedCount
        {
            get { return advancedCount; }
            set
            {
                if (advancedCount != value)
                {
                    advancedCount = value;
                    OnPropertyChanged("AdvancedCount");
                }
            }
        }

        private int allCount;
        public int AllCount
        {
            get { return allCount; }
            set {
                if (allCount != value)
                {
                    allCount = value;
                    OnPropertyChanged("AllCount");
                }
            }
        }     

        private ChannelToken selecetedSeriesToken;
        public ChannelToken SelecetedSeriesToken
        {
            get { return selecetedSeriesToken; }
            set {
                if (selecetedSeriesToken != value)
                {
                    selecetedSeriesToken = value;
                    OnPropertyChanged(() => SelecetedSeriesToken);
                }
            }
        }

        public IAsyncCommand SearchAdvancedCommand { get; set; }
        public IEnumerable<BaseWaveChannelToken> Channels { get { return contractsCollection; } }
    }

}
