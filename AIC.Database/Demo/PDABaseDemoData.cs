

using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class PDABaseDemoData : IRepository<PDABase>
    {
        public List<PDABase> list;
        public PDABaseDemoData()
        {
            list = new List<PDABase>();
            list.Add(new PDABase()
            {
                IP="192.168.1.210",
                Count=10,
            });
        }

        public void Add(PDABase newEntity)
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

        public IEnumerable<PDABase> Find(Func<PDABase, bool> predicate)
        {
            return list.Where(predicate);
        }

        public IEnumerable<PDABase> FindAll()
        {
            return list.ToArray();
        }

        public IEnumerable<PDABase> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PDABase> Query(params object[] args)
        {
            return list.ToArray();
        }

        public IEnumerable<PDABase> Query(string condition, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, PDABase entity)
        {
            throw new NotImplementedException();
        }

    }
}
