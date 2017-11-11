

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class EddyCurrentTachometerCardDemoData : IRepository<EddyCurrentTachometerCard>
    {
        public List<EddyCurrentTachometerCard> list;
        public EddyCurrentTachometerCardDemoData()
        {
            list = new List<EddyCurrentTachometerCard>();
            list.Add(new EddyCurrentTachometerCard()
            {
                IP = "192.168.1.210",
                CardNum = "5",
                Count = 4
            });
        }

        public void Add(EddyCurrentTachometerCard newEntity)
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

        public IEnumerable<EddyCurrentTachometerCard> Find(Func<EddyCurrentTachometerCard, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<EddyCurrentTachometerCard> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentTachometerCard> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EddyCurrentTachometerCard> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentTachometerCard> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, EddyCurrentTachometerCard entity)
        {
            throw new NotImplementedException();
        }
    }
}
