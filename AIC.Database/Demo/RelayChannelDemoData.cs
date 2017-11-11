

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class RelayChannelDemoData : IRepository<RelayChannel>
    {
        public List<RelayChannel> list;
        public RelayChannelDemoData()
        {
            list = new List<RelayChannel>();
            list.Add(new RelayChannel()
            {
                IP = "192.168.1.210",
                CardNum = "7",
                ChannelNum = "1"
            });
        }

        public void Add(RelayChannel newEntity)
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

        public IEnumerable<RelayChannel> Find(Func<RelayChannel, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<RelayChannel> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<RelayChannel> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RelayChannel> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<RelayChannel> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Remove(RelayChannel entity)
        {
            if(list.Contains(entity))
            {
                list.Remove(entity);
            }
        }

        public void Update(int id, RelayChannel entity)
        {
            throw new NotImplementedException();
        }
    }
}
