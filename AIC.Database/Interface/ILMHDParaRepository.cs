
using System.Collections.Generic;

namespace AIC.Database
{
    public interface ILMHDParaRepository : IRepository<LMHDParaTableContract>
    {
        IEnumerable<LMHDParaTableContract> QueryByIP(string ip);
        IEnumerable<LMHDParaTableContract> FindByIP(string ip);
        void DeleteByIP(string ip);
    }
}
