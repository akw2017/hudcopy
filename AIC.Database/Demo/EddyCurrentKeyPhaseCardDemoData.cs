

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class EddyCurrentKeyPhaseCardDemoData : IRepository<EddyCurrentKeyPhaseCard>
    {
        public List<EddyCurrentKeyPhaseCard> list;
        public EddyCurrentKeyPhaseCardDemoData()
        {
            list = new List<EddyCurrentKeyPhaseCard>();
            list.Add(new EddyCurrentKeyPhaseCard()
            {
                IP = "192.168.1.210",
                CardNum = "4",
                Count = 4
            });
        }

        public void Add(EddyCurrentKeyPhaseCard newEntity)
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

        public IEnumerable<EddyCurrentKeyPhaseCard> Find(Func<EddyCurrentKeyPhaseCard, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<EddyCurrentKeyPhaseCard> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentKeyPhaseCard> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EddyCurrentKeyPhaseCard> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentKeyPhaseCard> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(EddyCurrentKeyPhaseCard entity)
        {
            
        }

        public void Update(int id, EddyCurrentKeyPhaseCard entity)
        {
            throw new NotImplementedException();
        }

    }
}
