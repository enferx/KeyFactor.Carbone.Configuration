using KeyFactor.Carbone.Configuration.EntityFrameworkCore;
using KeyFactor.Carbone.Configuration.Units;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class UnitRepository : CarboneRepository<ConfigurationDbContext, Unit, Guid>, IUnitRepository
    {
        public UnitRepository(IDbContextProvider<ConfigurationDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<Unit> FindByNameAsync(string name)
        => await DbSet.FirstOrDefaultAsync(unit => unit.Name == name);
        

        public async Task<List<Unit>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
            => await DbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    unit => unit.Name.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
    }
}
