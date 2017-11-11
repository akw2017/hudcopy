
using System.Collections.Generic;

namespace AIC.Database
{
    public interface ILMVInfoRepository:IRepository<LMVInfoTableContract>
    {
        IEnumerable<LMVInfoTableContract> QueryByIP(string ip);
        IEnumerable<LMVInfoTableContract> FindByIP(string ip);
        void DeleteByIP(string ip);
    }
}
