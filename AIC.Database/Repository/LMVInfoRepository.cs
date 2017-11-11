
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIC.Database
{
    //public class LMVInfoRepository : Repository<LMVInfoTableContract> ,ILMVInfoRepository
    //{
    //    public LMVInfoRepository(ContractContext context):base(context,new LMVInfoCompare())
    //    {

    //    }

    //    public IEnumerable<LMVInfoTableContract> QueryByIP(string ip)
    //    {
    //        LinqWhereHelper helper = new LinqWhereHelper();
    //        helper.AddCondition("IP", "=", ip);
    //        return _contractSet.Query(helper.ToString(), helper.Values);
    //    }

    //    public void DeleteByIP(string ip)
    //    {
    //        var cntracts = _contractSet.Where(o => o.IP == ip);
    //        _contractSet.Removes(cntracts, cntracts.Select(o=>o.id).ToList());
    //    }

    //    public IEnumerable<LMVInfoTableContract> FindByIP(string ip)
    //    {
    //        return _contractSet.Where(o => o.IP == ip);
    //    }
    //}

    //public class LMVInfoCompare : IEqualityComparer<LMVInfoTableContract>
    //{
    //    public bool Equals(LMVInfoTableContract x, LMVInfoTableContract y)
    //    {
    //        if (Object.ReferenceEquals(x, y)) return true;

    //        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
    //            return false;

    //        return x.IP == y.IP && x.SlotNum == y.SlotNum && x.ChaN == y.ChaN;
    //    }

    //    public int GetHashCode(LMVInfoTableContract obj)
    //    {
    //        unchecked
    //        {
    //            int hashCode = obj.IP.GetHashCode();
    //            hashCode = (hashCode * 397) ^ obj.SlotNum.GetHashCode();
    //            hashCode = (hashCode * 397) ^ obj.ChaN.GetHashCode();
    //            return hashCode;
    //        }
    //    }
    //}
}
