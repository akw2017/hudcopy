using AIC.Core;
using AIC.Server.Common;
using AIC.Server.Storage.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    public class ContractSet<TEntity> :IContractSet<TEntity>,  IEnumerable<TEntity>, IEnumerable where TEntity :class
    {
        private HashSet<TEntity> set;
        public ContractSet(IEqualityComparer<TEntity> compare = null)
        {
            if (compare != null)
            {
                set = new HashSet<TEntity>(compare);
            }
            else
            {
                set = new HashSet<TEntity>();
            }

            foreach (var item in Query(string.Empty, null))
            {
                if (!set.Contains(item))
                {
                    set.Add(item);
                }
            }
        }
        public IEnumerable<TEntity> Query(params object[] args)
        {
            var entities = Excute<IEnumerable<TEntity>>(MethodType.Query, args);
            foreach(var entity in entities)
            {
                if(!set.Contains(entity))
                {
                    set.Add(entity);
                }       
            }
            return entities;
        }

        public int Add(TEntity entity)
        {
            var id = Excute<int>(MethodType.Add, entity);
            if (!set.Contains(entity))
            {
                set.Add(entity);
            }
            return id;
        }

        public TEntity Update(TEntity entity)
        {
            Excute(MethodType.Update, entity);
            return entity;
        }

        public void Updates(IEnumerable<TEntity> entities)
        {
            Excute(MethodType.Updates, entities.ToList());
        }

        public TEntity Attach(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Create()
        {
            throw new NotImplementedException();
        }

        public TEntity Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public TEntity Remove(TEntity entity, params object[] args)
        {
            Excute(MethodType.Delete, args);
            if (set.Contains(entity))
            {
                set.Remove(entity);
            }  
            return entity;
        }

        public void Removes(IEnumerable<TEntity> entites, params object[] args)
        {
            Excute(MethodType.Deletes, args);
            foreach (var entity in entites.ToArray())
            {
                if (set.Contains(entity))
                {
                    set.Remove(entity);
                }
            }
        }

        TDerivedEntity IContractSet<TEntity>.Create<TDerivedEntity>()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            foreach (TEntity entity in set)
            {
                yield return entity;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        private void Excute(MethodType methodType, params object[] args)
        {
            string methodName = typeof(TEntity).Name.Replace("Contract", "") + "_" + methodType.ToString();
            WCFCaller<IStorageManagement>.ExecuteMethod(ServerAddress.CTLAddress, methodName, args);// as List<LMCommandParaTableContract>;
        }

        private TDerivedEntity Excute<TDerivedEntity>(MethodType methodType, params object[] args) 
        {
            string methodName = typeof(TEntity).Name.Replace("Contract", "") + "_" + methodType.ToString();
            return (TDerivedEntity)WCFCaller<IStorageManagement>.ExecuteMethod(ServerAddress.CTLAddress, methodName, args) ;
        }

    }

    public enum MethodType
    {
        Query,
        Add,
        Delete,
        Update,
        Updates,
        Deletes,
    }
}
