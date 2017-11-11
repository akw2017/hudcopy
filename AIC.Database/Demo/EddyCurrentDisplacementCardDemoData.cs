

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class EddyCurrentDisplacementCardDemoData : IRepository<EddyCurrentDisplacementCard>
    {
        public List<EddyCurrentDisplacementCard> list;
        public EddyCurrentDisplacementCardDemoData()
        {
            list = new List<EddyCurrentDisplacementCard>();
            list.Add(new EddyCurrentDisplacementCard()
            {
                IP = "192.168.1.210",
                CardNum = "3",
                Count = 4
            });
        }

        public void Add(EddyCurrentDisplacementCard newEntity)
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

        public IEnumerable<EddyCurrentDisplacementCard> Find(Func<EddyCurrentDisplacementCard, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<EddyCurrentDisplacementCard> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentDisplacementCard> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EddyCurrentDisplacementCard> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentDisplacementCard> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, EddyCurrentDisplacementCard entity)
        {
            throw new NotImplementedException();
        }
    }
}
