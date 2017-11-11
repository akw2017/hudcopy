

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class DigitTachometerCardDemoData : IRepository<DigitTachometerCard>
    {
        public List<DigitTachometerCard> list;
        public DigitTachometerCardDemoData()
        {
            list = new List<DigitTachometerCard>();
            list.Add(new DigitTachometerCard()
            {
                IP = "192.168.1.210",
                CardNum = "2",
                Count = 4
            });
        }

        public void Add(DigitTachometerCard newEntity)
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

        public IEnumerable<DigitTachometerCard> Find(Func<DigitTachometerCard, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<DigitTachometerCard> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<DigitTachometerCard> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DigitTachometerCard> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<DigitTachometerCard> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, DigitTachometerCard entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<DigitTachometerCard>.Add(DigitTachometerCard newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
