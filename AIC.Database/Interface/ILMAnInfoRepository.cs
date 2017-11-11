
using System.Collections.Generic;

namespace AIC.Database
{
    public interface ILMAnInfoRepository : IRepository<LMAnInfoTableContract>
    {
        IEnumerable<LMAnInfoTableContract> QueryByIP(string ip);
        IEnumerable<LMAnInfoTableContract> FindByIP(string ip);
        void DeleteByIP(string ip);
    }
}
