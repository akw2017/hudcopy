

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class IEPECardDemoData : IRepository<IEPECard>
    {
        public List<IEPECard> list;
        public IEPECardDemoData()
        {
            list = new List<IEPECard>();
            list.Add(new IEPECard()
            {
                IP = "192.168.1.210",
                CardNum = "6",
            });
        }

        public void Add(IEPECard newEntity)
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

        public IEnumerable<IEPECard> Find(Func<IEPECard, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<IEPECard> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<IEPECard> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEPECard> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<IEPECard> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Remove(IEPECard entity)
        {
            if(list.Contains(entity))
            {
                list.Remove(entity);
            }
        }

        public void Update(int id, IEPECard entity)
        {
            throw new NotImplementedException();
        }
    }
}
