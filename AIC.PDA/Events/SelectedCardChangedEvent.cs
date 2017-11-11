using AIC.Domain;
using Prism.Events;
using System;

namespace AIC.PDA.Events
{
    public class SelectedCardChangedEvent : PubSubEvent<Tuple<string,string>>
    {
    }
}
