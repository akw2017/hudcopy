

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class AnalogInCardDemoData : IRepository<AnalogInCard>
    {
        public List<AnalogInCard> list;
        public AnalogInCardDemoData()
        {
            list = new List<AnalogInCard>();
            list.Add(new AnalogInCard()
            {
                IP = "192.168.1.210",
                CardNum = "1",
                Count = 4
            });
        }

        public void Add(AnalogInCard newEntity)
        {
            list.Add(newEntity);
        }

        public void Delete(int id)
        {
            var item = list.Where(o => o.id == id).SingleOrDefault();
            if (list.Contains(item))
            {
                list.Remove(item);
            }
        }

        public IEnumerable<AnalogInCard> Find(Func<AnalogInCard, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<AnalogInCard> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<AnalogInCard> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnalogInCard> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<AnalogInCard> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, AnalogInCard entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<AnalogInCard>.Add(AnalogInCard newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
