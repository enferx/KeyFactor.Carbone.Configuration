using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace KeyFactor.Carbone.Configuration.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<Product> FindByNumberAsync(string name);

        Task<List<Product>> GetListAsync(
           int skipCount,
           int maxResultCount,
           string sorting,
           string filter = null
       );

        Task<ProductProperty> GetProductPropertyAsync(Guid id);

        Task<ProductProperty> CreateProductPropertyAsync(ProductProperty productProperty);

        Task<ProductProperty> UpdateProductPropertyAsync(ProductProperty productProperty);
    }
}
