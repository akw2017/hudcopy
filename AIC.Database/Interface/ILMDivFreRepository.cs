
using System.Collections.Generic;

namespace AIC.Database
{
    public interface ILMDivFreRepository : IRepository<LMDivFreTableContract>
    {
        IEnumerable<LMDivFreTableContract> QueryByIP(string ip);
        IEnumerable<LMDivFreTableContract> FindByIP(string ip);
        void DeleteByIP(string ip);
    }
}
