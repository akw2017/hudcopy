
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.PDA.Models
{
    public class DownloadingData
    {
        PDABase _pda;

        List<AnalogInCard> analogInCardList;
        List<AnalogInChannel> analogInChannelList;

        List<DigitTachometerCard> digitTachometerCardList;
        List<DigitTachometerChannel> digitTachometerChannelList;

        List<EddyCurrentDisplacementCard> eddyCurrentDisplacementCardList;
        List<EddyCurrentDisplacementChannel> eddyCurrentDisplacementChannelList;

        List<EddyCurrentKeyPhaseCard> eddyCurrentKeyPhaseCardList;
        List<EddyCurrentKeyPhaseChannel> eddyCurrentKeyPhaseChannelList;

        List<EddyCurrentTachometerCard> eddyCurrentTachometerCardList;
        List<EddyCurrentTachometerChannel> eddyCurrentTachometerChannelList;

        List<IEPECard> iepeCardList;
        List<IEPEChannel> iepeChannelList;

        List<RelayCard> relayCardList;
        List<RelayChannel> relayChannelList;

        public DownloadingData()
        {
            analogInCardList = new List<AnalogInCard>();
            analogInChannelList = new List<AnalogInChannel>();

            digitTachometerCardList = new List<DigitTachometerCard>();
            digitTachometerChannelList = new List<DigitTachometerChannel>();

            eddyCurrentDisplacementCardList = new List<EddyCurrentDisplacementCard>();
            eddyCurrentDisplacementChannelList = new List<EddyCurrentDisplacementChannel>();

            eddyCurrentKeyPhaseCardList = new List<EddyCurrentKeyPhaseCard>();
            eddyCurrentKeyPhaseChannelList = new List<EddyCurrentKeyPhaseChannel>();

            eddyCurrentTachometerCardList = new List<EddyCurrentTachometerCard>();
            eddyCurrentTachometerChannelList = new List<EddyCurrentTachometerChannel>();

            iepeCardList = new List<IEPECard>();
            iepeChannelList = new List<IEPEChannel>();

            relayCardList = new List<RelayCard>();
            relayChannelList = new List<RelayChannel>();
        }

        public PDABase PDA { get { return _pda; } }

        public IEnumerable<AnalogInCard> AnalogInCards { get { return analogInCardList; } }
        public IEnumerable<AnalogInChannel> AnalogInChannels { get { return analogInChannelList; } }

        public IEnumerable<DigitTachometerCard> DigitTachometerCards { get { return digitTachometerCardList; } }
        public IEnumerable<DigitTachometerChannel> DigitTachometerChannels { get { return digitTachometerChannelList; } }

        public IEnumerable<EddyCurrentDisplacementCard> EddyCurrentDisplacementCards { get { return eddyCurrentDisplacementCardList; } }
        public IEnumerable<EddyCurrentDisplacementChannel> EddyCurrentDisplacementChannels { get { return eddyCurrentDisplacementChannelList; } }

        public IEnumerable<EddyCurrentKeyPhaseCard> EddyCurrentKeyPhaseCards { get { return eddyCurrentKeyPhaseCardList; } }
        public IEnumerable<EddyCurrentKeyPhaseChannel> EddyCurrentKeyPhaseChannels { get { return eddyCurrentKeyPhaseChannelList; } }

        public IEnumerable<EddyCurrentTachometerCard> EddyCurrentTachometerCards { get { return eddyCurrentTachometerCardList; } }
        public IEnumerable<EddyCurrentTachometerChannel> EddyCurrentTachometerChannels { get { return eddyCurrentTachometerChannelList; } }

        public IEnumerable<IEPECard> IEPECards { get { return iepeCardList; } }
        public IEnumerable<IEPEChannel> IEPEChannels { get { return iepeChannelList; } }

        public IEnumerable<RelayCard> RelayCards { get { return relayCardList; } }
        public IEnumerable<RelayChannel> RelayChannels { get { return relayChannelList; } }

        public void Add<T>(T data)
        {
            if (data is AnalogInCard)
            {
                analogInCardList.Add(data as AnalogInCard);
            }
            else if (data is AnalogInChannel)
            {
                analogInChannelList.Add(data as AnalogInChannel);
            }
            else if (data is DigitTachometerCard)
            {
                digitTachometerCardList.Add(data as DigitTachometerCard);
            }
            else if (data is DigitTachometerChannel)
            {
                digitTachometerChannelList.Add(data as DigitTachometerChannel);
            }
            else if (data is EddyCurrentDisplacementCard)
            {
                eddyCurrentDisplacementCardList.Add(data as EddyCurrentDisplacementCard);
            }
            else if (data is EddyCurrentDisplacementChannel)
            {
                eddyCurrentDisplacementChannelList.Add(data as EddyCurrentDisplacementChannel);
            }
            else if (data is EddyCurrentKeyPhaseCard)
            {
                eddyCurrentKeyPhaseCardList.Add(data as EddyCurrentKeyPhaseCard);
            }
            else if (data is EddyCurrentKeyPhaseChannel)
            {
                eddyCurrentKeyPhaseChannelList.Add(data as EddyCurrentKeyPhaseChannel);
            }
            else if (data is EddyCurrentTachometerCard)
            {
                eddyCurrentTachometerCardList.Add(data as EddyCurrentTachometerCard);
            }
            else if (data is EddyCurrentTachometerChannel)
            {
                eddyCurrentTachometerChannelList.Add(data as EddyCurrentTachometerChannel);
            }
            else if (data is IEPECard)
            {
                iepeCardList.Add(data as IEPECard);
            }
            else if (data is IEPEChannel)
            {
                iepeChannelList.Add(data as IEPEChannel);
            }
            else if (data is RelayCard)
            {
                relayCardList.Add(data as RelayCard);
            }
            else if (data is RelayChannel)
            {
                relayChannelList.Add(data as RelayChannel);
            }
        }

        public void SetPDA(PDABase pda)
        {
            _pda = pda;
        }

        public T GetCard<T>(string ip, string cardNum)
        {
            if (typeof(T) == typeof(AnalogInCard))
            {
                var data = analogInCardList.Where(o => o.IP == ip && o.CardNum == cardNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(DigitTachometerCard))
            {
                var data = digitTachometerCardList.Where(o => o.IP == ip && o.CardNum == cardNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(EddyCurrentDisplacementCard))
            {
                var data = eddyCurrentDisplacementCardList.Where(o => o.IP == ip && o.CardNum == cardNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(EddyCurrentKeyPhaseCard))
            {
                var data = eddyCurrentKeyPhaseCardList.Where(o => o.IP == ip && o.CardNum == cardNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(EddyCurrentTachometerCard))
            {
                var data = eddyCurrentTachometerCardList.Where(o => o.IP == ip && o.CardNum == cardNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(IEPECard))
            {
                var data = iepeCardList.Where(o => o.IP == ip && o.CardNum == cardNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(RelayCard))
            {
                var data = relayCardList.Where(o => o.IP == ip && o.CardNum == cardNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            return default(T);
        }
        public T GetChannel<T>(string ip, string cardNum, string chnNum)
        {
            if (typeof(T) == typeof(AnalogInChannel))
            {
                var data = analogInChannelList.Where(o => o.IP == ip && o.CardNum == cardNum && o.ChannelNum == chnNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(DigitTachometerChannel))
            {
                var data = digitTachometerChannelList.Where(o => o.IP == ip && o.CardNum == cardNum && o.ChannelNum == chnNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(EddyCurrentDisplacementChannel))
            {
                var data = eddyCurrentDisplacementChannelList.Where(o => o.IP == ip && o.CardNum == cardNum && o.ChannelNum == chnNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(EddyCurrentKeyPhaseChannel))
            {
                var data = eddyCurrentKeyPhaseChannelList.Where(o => o.IP == ip && o.CardNum == cardNum && o.ChannelNum == chnNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(EddyCurrentTachometerChannel))
            {
                var data = eddyCurrentTachometerChannelList.Where(o => o.IP == ip && o.CardNum == cardNum && o.ChannelNum == chnNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(IEPEChannel))
            {
                var data = iepeChannelList.Where(o => o.IP == ip && o.CardNum == cardNum && o.ChannelNum == chnNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (typeof(T) == typeof(RelayChannel))
            {
                var data = relayChannelList.Where(o => o.IP == ip && o.CardNum == cardNum && o.ChannelNum == chnNum).SingleOrDefault();
                if (data == null) return default(T);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            return default(T);
        }
    }
}
