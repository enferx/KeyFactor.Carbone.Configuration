using KeyFactor.Carbone.Configuration.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace KeyFactor.Carbone.Configuration.Domain
{
    public class ConfigurationDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductManager _productManager;

        public ConfigurationDataSeederContributor(IProductRepository productRepository, ProductManager productManager)
        {
            _productRepository = productRepository;
            _productManager = productManager;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _productRepository.GetCountAsync() <= 0)
            {

                await _productRepository.InsertAsync(
                   await _productManager.CreateAsync
                    (
                        number: "PROD-100",
                        name: "PROD-100",
                        fieldServiceProductType: FieldServiceProductType.Inventory,
                        productStructure: ProductStructure.Product,
                        decimalPlaces: 2
                    ),
                    autoSave: true
                );
            }

        }
    }
}
