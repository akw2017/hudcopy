using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Database
{
    //public class ContractContext
    //{
    //    private Dictionary<Type, IContractSetAdapter> _genericSets;
    //    public ContractContext()
    //    {
    //        _genericSets = new Dictionary<Type, IContractSetAdapter>();
    //        //if(databaseName=="AIC9000")
    //        //{
    //        //    LMCommands = new List<LMCommandParaTableContract>();
    //        //    LMHDParas = new List<LMHDParaTableContract>();
    //        //    LMVInfos = new List<LMVInfoTableContract>();
    //        //    LMAnInfos = new List<LMAnInfoTableContract>();
    //        //    LMDivFres = new List<LMDivFreTableContract>();

    //        //    // var vedioTask = Task.Run<List<LMVideoTableContract>>(() => WCFCaller<IVideoManagement>.ExecuteMethod(ServerAddress.VIDEOADDRESS, "LMVideo_Query", string.Empty, null) as List<LMVideoTableContract>);
    //        //    var commandTask = Task.Run(() => DatabaseComponent.Instance.QueryLMCommandParaTable());
    //        //    var hdParaTask = Task.Run(() => DatabaseComponent.Instance.QueryLMHDParaTable());
    //        //    var vInfoTask = Task.Run(() => DatabaseComponent.Instance.QueryLMVInfoTable());
    //        //    var anInfoTask = Task.Run(() => DatabaseComponent.Instance.QueryLMAnInfoTable());
    //        //    var divFreTask = Task.Run(() => DatabaseComponent.Instance.QueryLMDivFreTable());
    //        //    await Task.WhenAll(commandTask, hdParaTask, vInfoTask, anInfoTask, divFreTask);
    //        //}
    //    }

    //    public ContractSet<TEntity> Set<TEntity>(IEqualityComparer<TEntity> compare = null) where TEntity : class
    //    {
    //        if (_genericSets.ContainsKey(typeof(TEntity)))
    //        {
    //            return (ContractSet<TEntity>)_genericSets[(typeof(TEntity))];
    //        }
    //        else
    //        {
    //            var set = new ContractSet<TEntity>();
    //            _genericSets.Add((typeof(TEntity)), set);
    //            return set;
    //        }
    //        // return (DbSet<TEntity>)this.InternalContext.Set<TEntity>();
    //    }
    //}
}
