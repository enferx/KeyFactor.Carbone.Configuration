using KeyFactor.Carbone.Configuration.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductRepository : EfCoreRepository<ConfigurationDbContext, Product, Guid>,
        IProductRepository
    {
        public ProductRepository(
            IDbContextProvider<ConfigurationDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public async Task<Product> FindByNumberAsync(string number)
        {
            return await DbSet.FirstOrDefaultAsync(product => product.Number == number);

        }

        public async Task<List<Product>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null) => await DbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    product => product.Number.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
    }
}
