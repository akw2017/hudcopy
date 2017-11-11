

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class RelayCardDemoData : IRepository<RelayCard>
    {
        public List<RelayCard> list;
        public RelayCardDemoData()
        {
            list = new List<RelayCard>();
            list.Add(new RelayCard()
            {
                IP = "192.168.1.210",
                CardNum = "7",
            });
        }

        public void Add(RelayCard newEntity)
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

        public IEnumerable<RelayCard> Find(Func<RelayCard, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<RelayCard> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<RelayCard> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RelayCard> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<RelayCard> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }


        public void Update(int id, RelayCard entity)
        {
            throw new NotImplementedException();
        }

    }
}
