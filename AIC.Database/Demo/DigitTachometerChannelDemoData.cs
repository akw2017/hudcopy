

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class DigitTachometerChannelDemoData : IRepository<DigitTachometerChannel>
    {
        public List<DigitTachometerChannel> list;
        public DigitTachometerChannelDemoData()
        {
            list = new List<DigitTachometerChannel>();
            list.Add(new DigitTachometerChannel()
            {
                IP = "192.168.1.210",
                CardNum = "2",
                ChannelNum = "1"
            });
        }

        public void Add(DigitTachometerChannel newEntity)
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

        public IEnumerable<DigitTachometerChannel> Find(Func<DigitTachometerChannel, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<DigitTachometerChannel> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<DigitTachometerChannel> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DigitTachometerChannel> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<DigitTachometerChannel> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(DigitTachometerChannel entity)
        {
            
        }

        public void Update(int id, DigitTachometerChannel entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<DigitTachometerChannel>.Add(DigitTachometerChannel newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
