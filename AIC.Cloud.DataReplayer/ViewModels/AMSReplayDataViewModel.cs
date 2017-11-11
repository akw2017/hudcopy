using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

using Microsoft.Practices.Prism.ViewModel;

using AIC.CoreType;
using AIC.Cloud.Domain;
using System.Collections.ObjectModel;
using AIC.Server.Storage.Contract;
using Microsoft.Practices.Prism.Commands;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks;
using System.Threading;
using AIC.Cloud.Applications;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    
    public class TrackChangedEventArgs:EventArgs
    {
        private IEnumerable<VibrationChannelToken> _tokens;

        public TrackChangedEventArgs(IEnumerable<VibrationChannelToken> tokens)
        {
            _tokens = tokens;
        }
        public IEnumerable<VibrationChannelToken> Tokens { get { return _tokens; } }
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

    public class AMSReplayDataViewModel : HistoricalDataViewModel
    {
        private event EventHandler<ChannelChangedEventArgs> channelAdded;
        private event EventHandler<ChannelChangedEventArgs> channelRemoved;
        private event EventHandler<ChannelDataChangedEventArgs> channelDataChanged;
        private event EventHandler<TrackChangedEventArgs> trackChanged;
        private ObservableCollection<VibrationChannelToken> contractsCollection;

        public AMSReplayDataViewModel()
        {
            DisplayMode = SignalDisplayType.AMSTrend;
            contractsCollection = new ObservableCollection<VibrationChannelToken>();
            allGraphTypes = new Dictionary<string, Object>();
            allGraphTypes.Add("转速三维谱", "RPM3D");
            allGraphTypes.Add("阶次分析", "OrderAnalysis");

            selectedGraphTypes = new Dictionary<string, Object>();
            selectedGraphTypes.Add("转速三维谱", "RPM3D");
            selectedGraphTypes.Add("阶次分析", "OrderAnalysis");
        }

        public void AddChannel(ChannelToken token)
        {
            if ((token is VibrationChannelToken) && !contractsCollection.Contains(token))
            {
                contractsCollection.Add(token as VibrationChannelToken);
            }
            RaiseChannelAdded(token);
        }
        public void RemoveChannel(ChannelToken token)
        {
            if ((token is VibrationChannelToken) && contractsCollection.Contains(token))
            {
                contractsCollection.Remove(token as VibrationChannelToken);
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
        public void RaiseTrackChanged(IEnumerable<VibrationChannelToken> tokens)
        {
            if (trackChanged != null)
            {
                trackChanged(this, new TrackChangedEventArgs(tokens));
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
        public IObservable<IEnumerable<VibrationChannelToken>> WhenTrackChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<TrackChangedEventArgs>(
                        h => this.trackChanged += h,
                        h => this.trackChanged -= h)
                    .Select(x => x.EventArgs.Tokens);
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
        public IEnumerable<VibrationChannelToken> Channels { get { return contractsCollection; } }
    }

}
