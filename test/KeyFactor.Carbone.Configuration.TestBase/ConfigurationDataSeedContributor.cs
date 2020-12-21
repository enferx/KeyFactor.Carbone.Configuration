using KeyFactor.Carbone.Configuration.Products;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace KeyFactor.Carbone.Configuration
{
    public class ConfigurationDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IProductRepository _productRepository;
        private readonly ProductManager _productManager;
        
        public ConfigurationDataSeedContributor(
            IGuidGenerator guidGenerator,
            IProductRepository productRepository,
            ProductManager productManager)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _guidGenerator = guidGenerator ?? throw new ArgumentNullException(nameof(guidGenerator));
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
        }
        
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _productRepository.GetCountAsync() <= 0)
            {
                await _productRepository.InsertAsync(
                  await _productManager.CreateAsync
                   (
                       number: "PROD-10010",
                       name: "PROD-10010",
                       fieldServiceProductType: FieldServiceProductType.Inventory,
                       productStructure: ProductStructure.Product,
                       decimalPlaces: 2
                   ),
                   autoSave: true
                );
                await _productRepository.InsertAsync(
                  await _productManager.CreateAsync
                   (
                       number: "PROD-110010",
                       name: "PROD-110010",
                       fieldServiceProductType: FieldServiceProductType.Inventory,
                       productStructure: ProductStructure.Product,
                       decimalPlaces: 1
                   ),
                   autoSave: true
               );
               
            }
        }
    }
}