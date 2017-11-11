using AIC.Core;
using AIC.CoreType;

using System;
using System.ComponentModel;

namespace AIC.Domain
{
    public class TestPointTreeModel : TreeViewItemModel
    {
        //public TestPointTreeModel(string name, string sn, Guid id)
        //    : base(name)
        //{
        //    TestPointId = id;
        //    IsExpanded = false;
        //    MSSN = new BindableValue<string>(sn);
        //    InitializeLocation();
        //}

        //public TestPointTreeModel(string name, string sn)
        //    : this(name, sn, Guid.NewGuid())
        //{

        //}

        public TestPointTreeModel(TestPoint tp)
            :base(tp.Name)
        {
            TestPoint = tp;
            IsExpanded = false;
            MSSN = new BindableValue<string>(TestPoint.MSSN);
            if (!string.IsNullOrEmpty(TestPoint.IP) && !string.IsNullOrEmpty(TestPoint.CardNum) && !string.IsNullOrEmpty(TestPoint.ChannelNum))
            {
                ChannelId = ChannelIdentity.Create(TestPoint.IP, TestPoint.CardNum, TestPoint.ChannelNum).Value;
            }
            InitializeLocation();
        }

        protected override void NameChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        public TestPoint TestPoint { get; }

        protected override void InitializeLocation()
        {
            Location = new TreeItemLocation(Name, MSSN);
        }

        public void Bind(ChannelIdentity id)
        {
            ChannelId = id;
            OnPropertyChanged(nameof(IsPaired));
        }

        public void UnBind()
        {
            SignalType = SignalType.None;
            Alarm = AlarmGrade.HighNormal;
            ChannelId = null;
            OnPropertyChanged(nameof(IsPaired));
            Clear();
        }

        public void BindMiss()
        {
            IsBindMiss = true;
        }

        private bool isBindMiss;
        public bool IsBindMiss
        {
            get { return isBindMiss; }
            private set { SetProperty(ref isBindMiss, value); }
        }

        public BindableValue<string> MSSN { get; }

        public Guid TestPointId => TestPoint.TestPointId.Value;

        public Maybe<ChannelIdentity> ChannelId { get; private set; }

        public bool IsPaired => ChannelId.HasValue;

        private AlarmGrade alarm = AlarmGrade.HighNormal;
        public override AlarmGrade Alarm
        {
            get { return alarm; }
            set
            {
                if (alarm != value)
                {
                    alarm = value;
                    if (Parent != null)
                    {
                        Parent.Alarm = alarm;
                    }
                    OnPropertyChanged("Alarm");
                }
            }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    OnPropertyChanged("IsConnected");
                }
            }
        }

        private SignalType signalType;
        public SignalType SignalType
        {
            get { return signalType; }
            set
            {
                if (signalType != value)
                {
                    signalType = value;
                    OnPropertyChanged("SignalType");
                }
            }
        }

        //public void Bind(ChannelIdentity id)
        //{
        //    ChannelId = id;
        //    OnPropertyChanged(nameof(IsPaired));
        //}

        //public void UnBind()
        //{
        //    SignalType = SignalType.None;   
        //    Alarm = AlarmGrade.HighNormal;    
        //    ChannelId = null;
        //    OnPropertyChanged(nameof(IsPaired));
        //    Clear();
        //}

        private bool hasData;
        public bool HasData
        {
            get { return hasData; }
            set
            {
                if (hasData != value)
                {
                    hasData = value;
                    OnPropertyChanged("HasData");
                }
            }

        }
    }
}
