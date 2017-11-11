using AIC.CoreType;
using AIC.Database;
using AIC.Domain;
using AIC.Server.Storage.Contract;
using AIC.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using System;
using System.Text;
using AIC.OnlineSystem.Client;
using AIC.OnlineSystem.Server.DB.Models.Generated.Master;

namespace AIC.PDAService
{
    public class PDAService : IPDAService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDictionary<string, PDABaseModel> pdaDict;
        private IDictionary<CardIdentity, BaseCardModel> cardDict;
        private IDictionary<ChannelIdentity, BaseChannelModel> channelDict;

        public PDAService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            pdaDict = new Dictionary<string, PDABaseModel>();
            cardDict = new Dictionary<CardIdentity, BaseCardModel>();
            channelDict = new Dictionary<ChannelIdentity, BaseChannelModel>();
        }

        public void Initialize()
        {
            try
            {
                CreateChannels();
                CreateCards();
                CreatePDAs();
            }
            catch { }
        }

        private void CreateChannels()
        {
            StringBuilder sb = new StringBuilder();

            _unitOfWork.AnalogInChannels.Query()
                .Select(o => new AnalogInChannelModel(o))
                .ForEach(w => AddChannel(w));

            _unitOfWork.DigitTachometerChannels.Query()
                 .Select(o => new DigitTachometerChannelModel(o))
                .ForEach(w => AddChannel(w));

            _unitOfWork.EddyCurrentDisplacementChannels.Query()
                .Select(o => new EddyCurrentDisplacementChannelModel(o))
                .ForEach(w => AddChannel(w));

            _unitOfWork.EddyCurrentKeyPhaseChannels.Query()
                .Select(o => new EddyCurrentKeyPhaseChannelModel(o))
                .ForEach(w => AddChannel(w));

            _unitOfWork.EddyCurrentTachometerChannels.Query()
                 .Select(o => new EddyCurrentTachometerChannelModel(o))
                .ForEach(w => AddChannel(w));

            _unitOfWork.IEPEChannels.Query()
                .Select(o => new IEPEChannelModel(o))
                .ForEach(w => AddChannel(w));

            _unitOfWork.RelayChannels.Query()
                .Select(o => new RelayChannelModel(o))
                .ForEach(w => AddChannel(w));
        }

        //private BaseChannelModel CreateChannelModel(object obj)
        //{
        //    BaseChannelModel model = null;
        //    if (obj is AnalogInChannel)
        //    {
        //        model = ObjectConvertor<AnalogInChannel, AnalogInChannelModel>.Convert(obj as AnalogInChannel);
        //    }
        //    else if(obj is DigitTachometerChannel)
        //    {
        //        model = ObjectConvertor<DigitTachometerChannel, DigitTachometerChannelModel>.Convert(obj as DigitTachometerChannel);
        //    }
        //    else if (obj is EddyCurrentDisplacementChannel)
        //    {
        //        model = ObjectConvertor<EddyCurrentDisplacementChannel, EddyCurrentDisplacementChannelModel>.Convert(obj as EddyCurrentDisplacementChannel);
        //    }
        //    else if (obj is EddyCurrentKeyPhaseChannel)
        //    {
        //        model = ObjectConvertor<EddyCurrentKeyPhaseChannel, EddyCurrentKeyPhaseChannelModel>.Convert(obj as EddyCurrentKeyPhaseChannel);
        //    }
        //    else if (obj is EddyCurrentTachometerChannel)
        //    {
        //        model = ObjectConvertor<EddyCurrentTachometerChannel, EddyCurrentTachometerChannelModel>.Convert(obj as EddyCurrentTachometerChannel);
        //    }
        //    else if (obj is IEPEChannel)
        //    {
        //        model = ObjectConvertor<IEPEChannel, IEPEChannelModel>.Convert(obj as IEPEChannel);
        //    }
        //    else if (obj is RelayChannel)
        //    {
        //        model = ObjectConvertor<RelayChannel, RelayChannelModel>.Convert(obj as RelayChannel);
        //    }
        //    return model;
        //}

        private void AddChannel(BaseChannelModel channel)
        {
            if (!channelDict.ContainsKey(channel.ChannelId))
            {
                channelDict.Add(channel.ChannelId, channel);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"ChannelId已被占用:{channel.ChannelId.ToString()}");
                sb.AppendLine(channelDict[channel.ChannelId].GetType().ToString());
                sb.AppendLine("AnalogInSignal");
                throw new Exception(sb.ToString());
            }
        }

        private void CreateCards()
        {
            StringBuilder sb = new StringBuilder();

            _unitOfWork.AnalogInCards.Query()
                .Select(o => new AnalogInCardModel(o))
                .ForEach(w => AddCard(w));

            _unitOfWork.DigitTachometerCards.Query()
                .Select(o => new DigitTachometerCardModel(o))
                .ForEach(w => AddCard(w));

            _unitOfWork.EddyCurrentDisplacementCards.Query()
                .Select(o => new EddyCurrentDisplacementCardModel(o))
                .ForEach(w => AddCard(w));

            _unitOfWork.EddyCurrentKeyPhaseCards.Query()
                .Select(o => new EddyCurrentKeyPhaseCardModel(o))
                .ForEach(w => AddCard(w));

            _unitOfWork.EddyCurrentTachometerCards.Query()
                .Select(o => new EddyCurrentTachometerCardModel(o))
                .ForEach(w => AddCard(w));

            _unitOfWork.IEPECards.Query()
                .Select(o => new IEPECardModel(o))
                .ForEach(w => AddCard(w));

            _unitOfWork.RelayCards.Query()
                .Select(o => new RelayCardModel(o))
                .ForEach(w => AddCard(w));
        }

        //private BaseCardModel CreateCardModel(object obj)
        //{
        //    BaseCardModel model = null;
        //    if (obj is AnalogInCard)
        //    {
        //        model = ObjectConvertor<AnalogInCard, AnalogInCardModel>.Convert(obj as AnalogInCard);
        //    }
        //    else if (obj is DigitTachometerCard)
        //    {
        //        model = ObjectConvertor<DigitTachometerCard, DigitTachometerCardModel>.Convert(obj as DigitTachometerCard);
        //    }
        //    else if (obj is EddyCurrentDisplacementCard)
        //    {
        //        model = ObjectConvertor<EddyCurrentDisplacementCard, EddyCurrentDisplacementCardModel>.Convert(obj as EddyCurrentDisplacementCard);
        //    }
        //    else if (obj is EddyCurrentKeyPhaseCard)
        //    {
        //        model = ObjectConvertor<EddyCurrentKeyPhaseCard, EddyCurrentKeyPhaseCardModel>.Convert(obj as EddyCurrentKeyPhaseCard);
        //    }
        //    else if (obj is EddyCurrentTachometerCard)
        //    {
        //        model = ObjectConvertor<EddyCurrentTachometerCard, EddyCurrentTachometerCardModel>.Convert(obj as EddyCurrentTachometerCard);
        //    }
        //    else if (obj is IEPECard)
        //    {
        //        model = ObjectConvertor<IEPECard, IEPECardModel>.Convert(obj as IEPECard);
        //    }
        //    else if (obj is RelayCard)
        //    {
        //        model = ObjectConvertor<RelayCard, RelayCardModel>.Convert(obj as RelayCard);
        //    }
        //    return model;
        //}

        private void AddCard(BaseCardModel card)
        {
            if (!cardDict.ContainsKey(card.CardId))
            {
                for (int i = 0; i < card.Count; i++)
                {
                    var key = ChannelIdentity.Create(card.CardId.IP, card.CardId.CardNum, i.ToString()).Value;
                    if (channelDict.ContainsKey(key))
                    {
                        card.AddChannel(channelDict[key]);
                    }
                }
                cardDict.Add(card.CardId, card);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"CardId已被占用:{card.CardId.ToString()}");
                sb.AppendLine(cardDict[card.CardId].GetType().ToString());
                sb.AppendLine("AnalogInSignal");
                throw new Exception(sb.ToString());
            }
        }

        private void CreatePDAs()
        {
            _unitOfWork.PDAs.Query()
                 .Select(o => new PDABaseModel(o))
                 .ForEach(w => AddPDA(w));
        }

        private PDABaseModel CreatePDAModel(PDABase obj)
        {
            return ObjectConvertor<PDABase, PDABaseModel>.Convert(obj);
        }

        private void AddPDA(PDABaseModel w)
        {
            if (!pdaDict.ContainsKey(w.IP))
            {
                for (int i = 0; i < w.Count; i++)
                {
                    var key = CardIdentity.Create(w.IP, i.ToString()).Value;
                    if (cardDict.ContainsKey(key))
                    {
                        w.AddCard(cardDict[key]);
                    }
                }
                pdaDict.Add(w.IP, w);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"IP已被占用:{w.IP}");
                sb.AppendLine(pdaDict[w.IP].GetType().ToString());
                sb.AppendLine("AnalogInSignal");
                throw new Exception(sb.ToString());
            };
        }

        public void UpdatePDA(string ip)
        {
            if(pdaDict.ContainsKey(ip))
            {
                pdaDict[ip].SerializeTriggerChannel();
                UpdatePDA(pdaDict[ip].PDA);
            }
        }

        private void UpdatePDA(PDABase pda)
        {
            _unitOfWork.PDAs.Update((pda).id, pda);
        }

        public void UpdateCard(BaseCardModel model)
        {
            if(model is AnalogInCardModel)
            {
                _unitOfWork.AnalogInCards.Update(((AnalogInCardModel)model).Card.id, ((AnalogInCardModel)model).Card);
            }
            else if(model is DigitTachometerCardModel)
            {
                _unitOfWork.DigitTachometerCards.Update(((DigitTachometerCardModel)model).Card.id, ((DigitTachometerCardModel)model).Card);
            }
            else if (model is EddyCurrentDisplacementCardModel)
            {
                _unitOfWork.EddyCurrentDisplacementCards.Update(((EddyCurrentDisplacementCardModel)model).Card.id, ((EddyCurrentDisplacementCardModel)model).Card);
            }
            else if (model is EddyCurrentKeyPhaseCardModel)
            {
                _unitOfWork.EddyCurrentKeyPhaseCards.Update(((EddyCurrentKeyPhaseCardModel)model).Card.id, ((EddyCurrentKeyPhaseCardModel)model).Card);
            }
            else if (model is EddyCurrentTachometerCardModel)
            {
                _unitOfWork.EddyCurrentTachometerCards.Update(((EddyCurrentTachometerCardModel)model).Card.id, ((EddyCurrentTachometerCardModel)model).Card);
            }
            else if (model is IEPECardModel)
            {
                _unitOfWork.IEPECards.Update(((IEPECardModel)model).Card.id, ((IEPECardModel)model).Card);
            }
            else if (model is RelayCardModel)
            {
                _unitOfWork.RelayCards.Update(((RelayCardModel)model).Card.id, ((RelayCardModel)model).Card);
            }
        }

        public void DeleteCard(object card)
        {
            if (card is AnalogInCard)
            {
                _unitOfWork.AnalogInCards.Delete(((AnalogInCard)card).id);
            }
            else if (card is DigitTachometerCard)
            {
                _unitOfWork.DigitTachometerCards.Delete(((DigitTachometerCard)card).id);
            }
            else if (card is EddyCurrentDisplacementCard)
            {
                _unitOfWork.EddyCurrentDisplacementCards.Delete(((EddyCurrentDisplacementCard)card).id);
            }
            else if (card is EddyCurrentKeyPhaseCard)
            {
                _unitOfWork.EddyCurrentKeyPhaseCards.Delete(((EddyCurrentKeyPhaseCard)card).id);
            }
            else if (card is EddyCurrentTachometerCard)
            {
                _unitOfWork.EddyCurrentTachometerCards.Delete(((EddyCurrentTachometerCard)card).id);
            }
            else if (card is IEPECard)
            {
                _unitOfWork.IEPECards.Delete(((IEPECard)card).id);
            }
            else if (card is RelayCard)
            {
                _unitOfWork.RelayCards.Delete(((RelayCard)card).id);
            }
        }

        public void UpdateChannel(BaseChannelModel model)
        {
            if (model is AnalogInChannelModel)
            {
                _unitOfWork.AnalogInChannels.Update(((AnalogInChannelModel)model).Channel.id, ((AnalogInChannelModel)model).Channel);
            }
            else if (model is DigitTachometerChannelModel)
            {
                _unitOfWork.DigitTachometerChannels.Update(((DigitTachometerChannelModel)model).Channel.id, ((DigitTachometerChannelModel)model).Channel);
            }
            else if (model is EddyCurrentDisplacementChannelModel)
            {
                _unitOfWork.EddyCurrentDisplacementChannels.Update(((EddyCurrentDisplacementChannelModel)model).Channel.id, ((EddyCurrentDisplacementChannelModel)model).Channel);
            }
            else if (model is EddyCurrentKeyPhaseChannelModel)
            {
                _unitOfWork.EddyCurrentKeyPhaseChannels.Update(((EddyCurrentKeyPhaseChannelModel)model).Channel.id, ((EddyCurrentKeyPhaseChannelModel)model).Channel);
            }
            else if (model is EddyCurrentTachometerChannelModel)
            {
                _unitOfWork.EddyCurrentTachometerChannels.Update(((EddyCurrentTachometerChannelModel)model).Channel.id, ((EddyCurrentTachometerChannelModel)model).Channel);
            }
            else if (model is IEPEChannelModel)
            {
                _unitOfWork.IEPEChannels.Update(((IEPEChannelModel)model).Channel.id, ((IEPEChannelModel)model).Channel);
            }
            else if (model is RelayChannelModel)
            {
                _unitOfWork.RelayChannels.Update(((RelayChannelModel)model).Channel.id, ((RelayChannelModel)model).Channel);
            }
        }

        public void DeleteChannel(object channel)
        {
            if (channel is AnalogInChannel)
            {
                _unitOfWork.AnalogInChannels.Delete(((AnalogInChannel)channel).id);
            }
            else if (channel is DigitTachometerChannel)
            {
                _unitOfWork.DigitTachometerChannels.Delete(((DigitTachometerChannel)channel).id);
            }
            else if (channel is EddyCurrentDisplacementChannel)
            {
                _unitOfWork.EddyCurrentDisplacementChannels.Delete(((EddyCurrentDisplacementChannel)channel).id);
            }
            else if (channel is EddyCurrentKeyPhaseChannel)
            {
                _unitOfWork.EddyCurrentKeyPhaseChannels.Delete(((EddyCurrentKeyPhaseChannel)channel).id);
            }
            else if (channel is EddyCurrentTachometerChannel)
            {
                _unitOfWork.EddyCurrentTachometerChannels.Delete(((EddyCurrentTachometerChannel)channel).id);
            }
            else if (channel is IEPEChannel)
            {
                _unitOfWork.IEPEChannels.Delete(((IEPEChannel)channel).id);
            }
            else if (channel is RelayChannel)
            {
                _unitOfWork.RelayChannels.Delete(((RelayChannel)channel).id);
            }
        }

        public IEnumerable<PDABaseModel> GetPDAs()
        {
            return pdaDict.Values;
        }

        public IEnumerable<BaseCardModel> GetCards()
        {
            return cardDict.Values;
        }

        public IEnumerable<BaseChannelModel> GetChannels()
        {
            return channelDict.Values;
        }

        public IEnumerable<TriggerChannel> GetTriggerChannels(string ip)
        {
            if(pdaDict.ContainsKey(ip))
            {
                return pdaDict[ip].TriggerChannels;
            }
            return null;
        }
    }
}
