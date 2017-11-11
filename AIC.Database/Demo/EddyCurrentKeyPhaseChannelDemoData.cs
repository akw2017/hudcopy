

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class EddyCurrentKeyPhaseChannelDemoData : IRepository<EddyCurrentKeyPhaseChannel>
    {
        public List<EddyCurrentKeyPhaseChannel> list;
        public EddyCurrentKeyPhaseChannelDemoData()
        {
            list = new List<EddyCurrentKeyPhaseChannel>();
            list.Add(new EddyCurrentKeyPhaseChannel()
            {
                IP = "192.168.1.210",
                CardNum = "4",
                ChannelNum = "1"
            });
        }

        public void Add(EddyCurrentKeyPhaseChannel newEntity)
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

        public IEnumerable<EddyCurrentKeyPhaseChannel> Find(Func<EddyCurrentKeyPhaseChannel, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<EddyCurrentKeyPhaseChannel> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentKeyPhaseChannel> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EddyCurrentKeyPhaseChannel> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentKeyPhaseChannel> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, EddyCurrentKeyPhaseChannel entity)
        {
            throw new NotImplementedException();
        }

    }
}
