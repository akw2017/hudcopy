using System.Collections;
using System.Collections.Generic;

namespace AIC.Database
{
    public interface IContractSet<TEntity> : IEnumerable<TEntity>, IEnumerable, IContractSetAdapter where TEntity : class
    {
        IEnumerable<TEntity> Query(params object[] args);
        int Add(TEntity entity);
        TEntity Attach(TEntity entity);
        TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity;
        TEntity Create();
        TEntity Find(params object[] keyValues);
        TEntity Remove(TEntity entity, params object[] args);
        TEntity Update(TEntity entity);
        void Removes(IEnumerable<TEntity> entites, params object[] args);

       // ObservableCollection<TEntity> Local { get; }
    }

}
