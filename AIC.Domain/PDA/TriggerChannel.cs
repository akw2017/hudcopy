using Newtonsoft.Json;
using Prism.Mvvm;

namespace AIC.Domain
{
    public class TriggerChannel: BindableBase
    {
        public TriggerChannel(){ }


        private string ip;
        public string IP
        {
            get { return ip; }
            set {
                if (SetProperty(ref ip, value))
                    OnPropertyChanged(nameof(IsEmpty));
            }
        }

        private string _cardNum;
        public string CardNum
        {
            get { return _cardNum; }
            set {
                if(SetProperty(ref _cardNum, value))
                    OnPropertyChanged(nameof(IsEmpty));
            }
        }

        private string _channelNum;
        public string ChannelNum
        {
            get { return _channelNum; }
            set {
                if(SetProperty(ref _channelNum, value))
                    OnPropertyChanged(nameof(IsEmpty));
            }
        }

        public void Clear()
        {
            IP = string.Empty;
            CardNum = string.Empty;
            ChannelNum = string.Empty;
        }

        [JsonIgnore]
        public bool IsEmpty => string.IsNullOrEmpty(IP) && string.IsNullOrEmpty(CardNum) && string.IsNullOrEmpty(ChannelNum);
    }
}
