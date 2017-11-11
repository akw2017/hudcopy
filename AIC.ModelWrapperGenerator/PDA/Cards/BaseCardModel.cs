using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AIC.ModelWrapperGenerator
{
    public abstract class BaseCardModel : BindableBase
    {
        private static NullCard nullCard = new NullCard();

        private List<BaseChannelModel> channelList = new List<BaseChannelModel>();

        public BaseCardModel(CardIdentity id, int count)
        {
            CardId = id;
            Count = count;
        }

        public virtual void AddChannel(BaseChannelModel channel)
        {
            channelList.Add(channel);
        }

        public IEnumerable<BaseChannelModel> Channels => channelList;

        public CardIdentity CardId { get; protected set; }

        public int Count { get; }

        public static NullCard Null { get { return nullCard; } }
    }

    public class NullCard : BaseCardModel
    {
        public NullCard() : base(null, 0)
        {

        }
    }
}
