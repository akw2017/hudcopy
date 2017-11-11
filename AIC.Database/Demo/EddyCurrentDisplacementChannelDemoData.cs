

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class EddyCurrentDisplacementChannelDemoData : IRepository<EddyCurrentDisplacementChannel>
    {
        public List<EddyCurrentDisplacementChannel> list;
        public EddyCurrentDisplacementChannelDemoData()
        {
            list = new List<EddyCurrentDisplacementChannel>();
            list.Add(new EddyCurrentDisplacementChannel()
            {
                IP = "192.168.1.210",
                CardNum = "3",
                ChannelNum = "1"
            });
        }

        public void Add(EddyCurrentDisplacementChannel newEntity)
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

        public IEnumerable<EddyCurrentDisplacementChannel> Find(Func<EddyCurrentDisplacementChannel, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<EddyCurrentDisplacementChannel> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentDisplacementChannel> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EddyCurrentDisplacementChannel> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<EddyCurrentDisplacementChannel> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(EddyCurrentDisplacementChannel entity)
        {
            
        }

        public void Update(int id, EddyCurrentDisplacementChannel entity)
        {
            throw new NotImplementedException();
        }


    }
}
