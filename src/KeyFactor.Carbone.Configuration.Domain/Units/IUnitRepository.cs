using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace KeyFactor.Carbone.Configuration.Units
{
    public interface IUnitRepository : IRepository<Unit, Guid>
    {
        Task<Unit> FindByNameAsync(string name);

        Task<List<Unit>> GetListAsync(
           int skipCount,
           int maxResultCount,
           string sorting,
           string filter = null
       );
    }
}
