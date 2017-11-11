

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class IEPEChannelDemoData : IRepository<IEPEChannel>
    {
        public List<IEPEChannel> list;
        public IEPEChannelDemoData()
        {
            list = new List<IEPEChannel>();
            list.Add(new IEPEChannel()
            {
                IP = "192.168.1.210",
                CardNum = "6",
                ChannelNum = "1"
            });
        }

        public void Add(IEPEChannel newEntity)
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

        public IEnumerable<IEPEChannel> Find(Func<IEPEChannel, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<IEPEChannel> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<IEPEChannel> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEPEChannel> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<IEPEChannel> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }


        public void Update(int id, IEPEChannel entity)
        {
            throw new NotImplementedException();
        }


    }
}
