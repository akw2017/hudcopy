using AIC.OnlineSystem.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Database
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ContractContext _context;
        public Repository(ContractContext context)
        {
            _context = context;
        }

        public IEnumerable<T> Query()
        {
            return Query(string.Empty, null);
        }

        public IEnumerable<T> Query(string condition,params object[] args)
        {
            return _context.Query<T>(condition, args);
        }


        public void Add(T newEntity)
        {
            _context.Add<T>(newEntity);
        }

        public void Update(int id, T entity)
        {
            _context.Modify<T>(id,entity);
        }

        public void Delete(int id)
        {
            _context.Delete<T>(id);
        }
    }
}
