using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Database
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Query();
        IEnumerable<T> Query(string condition, params object[] args);
        void Add(T newEntity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
