

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class EddyCurrentTachometerChannelDemoData : IRepository<EddyCurrentTachometerChannel>
    {
        public List<EddyCurrentTachometerChannel> list;
        public EddyCurrentTachometerChannelDemoData()
        {
            list = new List<EddyCurrentTachometerChannel>();
            list.Add(new EddyCurrentTachometerChannel()
            {
                IP = "192.168.1.210",
                CardNum = "5",
                ChannelNum = "1"
            });
        }

        public void Add(EddyCurrentTachometerChannel newEntity)
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

        public IEnumerable<EddyCurrentTachometerChannel> Find(Func<EddyCurrentTachometerChannel, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<EddyCurrentTachometerChannel> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentTachometerChannel> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EddyCurrentTachometerChannel> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentTachometerChannel> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, EddyCurrentTachometerChannel entity)
        {
            throw new NotImplementedException();
        }

    }
}
