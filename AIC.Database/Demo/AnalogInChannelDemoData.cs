

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class AnalogInChannelDemoData : IRepository<AnalogInChannel>
    {
        public List<AnalogInChannel> list;
        public AnalogInChannelDemoData()
        {
            list = new List<AnalogInChannel>();
            list.Add(new AnalogInChannel()
            {
                IP = "192.168.1.210",
                CardNum = "1",
                ChannelNum = "1"
            });
        }

        public void Add(AnalogInChannel newEntity)
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

        public IEnumerable<AnalogInChannel> Find(Func<AnalogInChannel, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<AnalogInChannel> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<AnalogInChannel> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnalogInChannel> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<AnalogInChannel> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(AnalogInChannel entity)
        {
            
        }

        public void Update(int id, AnalogInChannel entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<AnalogInChannel>.Add(AnalogInChannel newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
