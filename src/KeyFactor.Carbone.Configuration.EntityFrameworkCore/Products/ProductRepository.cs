using KeyFactor.Carbone.Configuration.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductRepository : CarboneRepository<ConfigurationDbContext, Product, Guid>,
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

        public async Task<ProductProperty> GetProductPropertyAsync(Guid id) =>  
            await DbContext.ProductProperties.FirstOrDefaultAsync(property => property.Id == id);

        public async Task<ProductProperty> CreateProductProperty(ProductProperty productProperty)
        {
            await DbContext.ProductProperties.AddAsync(productProperty);
            await DbContext.SaveChangesAsync();
            return productProperty;
        }


    }
}
