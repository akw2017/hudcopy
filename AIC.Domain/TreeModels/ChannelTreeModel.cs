using AIC.CoreType;
using System;

namespace AIC.Domain
{
    public class ChannelTreeModel : TreeViewItemModel, IPairable
    {
        public ChannelTreeModel(ChannelIdentity channelId) : base("Channel")
        {
            IsExpanded = true;
            ChannelId = channelId;
        }
      //  public Guid Id { get; private set; }

        public ChannelIdentity ChannelId { get; }

        private bool isPaired;
        public bool IsPaired
        {
            get { return isPaired; }
            private set
            {
                if (isPaired != value)
                {
                    isPaired = value;
                    OnPropertyChanged("IsPaired");
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

        public void Bind()
        {
           // Id = id;
            IsPaired = true;
        }

        public void UnBind()
        {
          //  Id = Guid.Empty;
            isPaired = false;
        }
    }
}