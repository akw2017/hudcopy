namespace AIC.Domain
{
    public partial class BandPassFilterWrapper : ModelWrapper<BandPassFilter>
  {
    public BandPassFilterWrapper(BandPassFilter model) : base(model)
    {
    }

        public System.Double PassbandAttenuationDB
        {
            get { return GetValue<System.Double>(); }
            set { SetValue(value); }
        }

        public System.Double PassbandAttenuationDBOriginalValue => GetOriginalValue<System.Double>(nameof(PassbandAttenuationDB));

        public bool PassbandAttenuationDBIsChanged => GetIsChanged(nameof(PassbandAttenuationDB));

        public System.Double StopbandAttenuationDB
        {
            get { return GetValue<System.Double>(); }
            set { SetValue(value); }
        }

        public System.Double StopbandAttenuationDBOriginalValue => GetOriginalValue<System.Double>(nameof(StopbandAttenuationDB));

        public bool StopbandAttenuationDBIsChanged => GetIsChanged(nameof(StopbandAttenuationDB));

        public System.Double BPPassbandFreLow
        {
            get { return GetValue<System.Double>(); }
            set { SetValue(value); }
        }

        public System.Double BPPassbandFreLowOriginalValue => GetOriginalValue<System.Double>(nameof(BPPassbandFreLow));

        public bool BPPassbandFreLowIsChanged => GetIsChanged(nameof(BPPassbandFreLow));

        public System.Double BPPassbandFreHigh
        {
            get { return GetValue<System.Double>(); }
            set { SetValue(value); }
        }

        public System.Double BPPassbandFreHighOriginalValue => GetOriginalValue<System.Double>(nameof(BPPassbandFreHigh));

        public bool BPPassbandFreHighIsChanged => GetIsChanged(nameof(BPPassbandFreHigh));

        public System.Double TransitionBandwidth
        {
            get { return GetValue<System.Double>(); }
            set { SetValue(value); }
        }

        public System.Double TransitionBandwidthOriginalValue => GetOriginalValue<System.Double>(nameof(TransitionBandwidth));
    }
}
