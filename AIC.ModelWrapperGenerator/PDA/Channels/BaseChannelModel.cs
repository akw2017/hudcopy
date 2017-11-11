using Prism.Mvvm;

namespace AIC.ModelWrapperGenerator
{
    public class BaseChannelModel : BindableBase
    {
        public ChannelIdentity ChannelId => ChannelIdentity.Create(IP, CardNum, ChannelNum).Value;

        public string IP { get; set; }
        public int CardNum { get; set; }
        public int ChannelNum { get; set; }

        private string unit;
        public string Unit
        {
            get { return unit; }
            set { SetProperty(ref unit, value); }
        }

        private bool isBypass;
        public bool IsBypass
        {
            get { return isBypass; }
            set { SetProperty(ref isBypass, value); }
        }

        private bool isUpload;
        public bool IsUpload
        {
            get { return isUpload; }
            set { SetProperty(ref isUpload, value); }
        }
    }
}
