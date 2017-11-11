using AIC.CoreType;

using Newtonsoft.Json;
using Prism.Mvvm;
using System.Collections.Generic;

namespace AIC.Domain
{
    public class PDABaseModel : BindableBase
    {
        private List<BaseCardModel> cardList = new List<BaseCardModel>();
        public PDABaseModel(PDABase pda)
        {
            PDA = pda;
            IP = PDA.PDAIP;
            Is4G = pda.Is4G ?? false;
            IsSyn = pda.IsSyn ?? false;
            IsTotalBypass = pda.IsTotalBypass ?? false;
            IsZipDownload = pda.IsZipDownload ?? false;
            IsZipUpload = pda.IsZipUpload ?? false;
            Language = pda.Language ?? 0;
            MasterWirelessMAC = pda.MasterWirelessMAC ?? string.Empty;
            PDAAliasName = pda.PDAAliasName ?? string.Empty;
            PDAMAC = pda.PDAMAC ?? string.Empty;
            SampleFre = pda.SampleFre ?? 0;
            SamplePoint = pda.SamplePoint ?? 0;
            SampleType = pda.SampleType == null ? SampleType.Transient : (SampleType)pda.SampleType.Value;  //pda.SampleType ?? 0;
            ServerIP = pda.ServerIP ?? string.Empty;
            ServerPort = pda.ServerPort ?? 0;
            TriggerCardNum = pda.TriggerCardNum ?? 0;
            TriggerChannelNum = pda.TriggerChannelNum ?? 0;
            TriggerType = pda.TriggerType == null ? TriggerType.Auto: (TriggerType)pda.TriggerType.Value;
            UploadMode = pda.UploadMode ?? 0;
            Count = pda.Count ?? 0;
            TriggerCount = pda.TriggerCount ?? 0;
            List<TriggerChannel> list = new List<TriggerChannel>();
            list.Add(new TriggerChannel() { IP = "192.168.1.112", CardNum = "0", ChannelNum = "0" });
            list.Add(new TriggerChannel() { IP = "192.168.1.112", CardNum = "0", ChannelNum = "1" });
            list.Add(new TriggerChannel() { IP = "192.168.1.112", CardNum = "1", ChannelNum = "0" });
            list.Add(new TriggerChannel() { IP = "192.168.1.112", CardNum = "1", ChannelNum = "1" });

           // var json = JsonConvert.SerializeObject(list);

            TriggerChannels = !string.IsNullOrEmpty(pda.TriggerChannels)? JsonConvert.DeserializeObject<IEnumerable<TriggerChannel>>(pda.TriggerChannels): list;
            
        }

        public void SerializeTriggerChannel()
        {
            PDA.TriggerChannels = JsonConvert.SerializeObject(TriggerChannels);
        }

        public void AddCard(BaseCardModel model)
        {
            cardList.Add(model);
        }

        public PDABase PDA { get; }

        public IEnumerable<TriggerChannel> TriggerChannels { get; }

        public IEnumerable<BaseCardModel> Cards => cardList;

        private string ip;
        public string IP
        {
            get { return ip; }
            set
            {
                if (SetProperty(ref ip, value))
                    PDA.PDAIP = value;
            }
        }

        private bool is4G;
        public bool Is4G
        {
            get { return is4G; }
            set
            {
                if (SetProperty(ref is4G, value))
                    PDA.Is4G = value;
            }
        }

        private bool isSyn;
        public bool IsSyn
        {
            get { return isSyn; }
            set
            {
                if (SetProperty(ref isSyn, value))
                    PDA.IsSyn = value;
            }
        }

        private bool isTotalBypass;
        public bool IsTotalBypass
        {
            get { return isTotalBypass; }
            set
            {
                if (SetProperty(ref isTotalBypass, value))
                    PDA.IsTotalBypass = value;
            }
        }

        private bool isZipDownload;
        public bool IsZipDownload
        {
            get { return isZipDownload; }
            set
            {
                if (SetProperty(ref isZipDownload, value))
                    PDA.IsZipDownload = value;
            }
        }

        private bool isZipUpload;
        public bool IsZipUpload
        {
            get { return isZipUpload; }
            set
            {
                if (SetProperty(ref isZipUpload, value))
                    PDA.IsZipUpload = value;
            }
        }

        private int language;
        public int Language
        {
            get { return language; }
            set
            {
                if (SetProperty(ref language, value))
                    PDA.Language = value;
            }
        }

        private string masterWirelessMAC;
        public string MasterWirelessMAC
        {
            get { return masterWirelessMAC; }
            set
            {
                if (SetProperty(ref masterWirelessMAC, value))
                    PDA.MasterWirelessMAC = value;
            }
        }

        private string pdaAliasName;
        public string PDAAliasName
        {
            get { return pdaAliasName; }
            set
            {
                if (SetProperty(ref pdaAliasName, value))
                    PDA.PDAAliasName = value;
            }
        }

        private string pdaMAC;
        public string PDAMAC
        {
            get { return pdaMAC; }
            set
            {
                if (SetProperty(ref pdaMAC, value))
                    PDA.PDAMAC = value;
            }
        }

        private double sampleFre;
        public double SampleFre
        {
            get { return sampleFre; }
            set
            {
                if (SetProperty(ref sampleFre, value))
                    PDA.SampleFre = value;
            }
        }

        private int samplePoint;
        public int SamplePoint
        {
            get { return samplePoint; }
            set
            {
                if (SetProperty(ref samplePoint, value))
                    PDA.SamplePoint = value;
            }
        }

        private SampleType sampleType;
        public SampleType SampleType
        {
            get { return sampleType; }
            set
            {
                if (SetProperty(ref sampleType, value))
                    PDA.SampleType = (int)value;
            }
        }

        private string serverIP;
        public string ServerIP
        {
            get { return serverIP; }
            set
            {
                if (SetProperty(ref serverIP, value))
                    PDA.ServerIP = value;
            }
        }

        private int serverPort;
        public int ServerPort
        {
            get { return serverPort; }
            set
            {
                if (SetProperty(ref serverPort, value))
                    PDA.ServerPort = value;
            }
        }

        private int triggerCardNum;
        public int TriggerCardNum
        {
            get { return triggerCardNum; }
            set
            {
                if (SetProperty(ref triggerCardNum, value))
                    PDA.TriggerCardNum = value;
            }
        }

        private int triggerChannelNum;
        public int TriggerChannelNum
        {
            get { return triggerChannelNum; }
            set
            {
                if (SetProperty(ref triggerChannelNum, value))
                    PDA.TriggerChannelNum = value;
            }
        }

        private TriggerType triggerType;
        public TriggerType TriggerType
        {
            get { return triggerType; }
            set
            {
                if (SetProperty(ref triggerType, value))
                    PDA.TriggerType = (int)value;
            }
        }

        private int uploadMode;
        public int UploadMode
        {
            get { return uploadMode; }
            set
            {
                if (SetProperty(ref uploadMode, value))
                    PDA.UploadMode = value;
            }
        }
        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                if (SetProperty(ref count, value))
                    PDA.Count = value;
            }
        }

        private int triggerCount;
        public int TriggerCount
        {
            get { return triggerCount; }
            set
            {
                if (SetProperty(ref triggerCount, value))
                    PDA.TriggerCount = value;
            }
        }
    }
}
