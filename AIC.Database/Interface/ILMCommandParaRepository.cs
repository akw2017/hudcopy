
using System.Collections.Generic;

namespace AIC.Database
{
    public interface ILMCommandParaRepository : IRepository<LMCommandParaTableContract>
    {
        IEnumerable<LMCommandParaTableContract> QueryByIP(string ip);
        IEnumerable<LMCommandParaTableContract> FindByIP(string ip);
        void DeleteByIP(string ip);
    }
}
