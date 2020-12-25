using KeyFactor.Carbone.Configuration.Products;
using KeyFactor.Carbone.Configuration.Units;
using System;
using System.Threading.Tasks;
using Volo.Abp;
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
        private readonly IUnitRepository _unitRepository;
        private readonly UnitManager _unitManager;
        
        public ConfigurationDataSeedContributor(
            IGuidGenerator guidGenerator,
            IProductRepository productRepository,
            ProductManager productManager,
            IUnitRepository unitRepository,
            UnitManager unitManager)
        {
            _productRepository = Check.NotNull(productRepository, nameof(productRepository));
            _guidGenerator = Check.NotNull(guidGenerator, nameof(guidGenerator));
            _productManager = Check.NotNull(productManager, nameof(productManager));
            _unitRepository = Check.NotNull(unitRepository, nameof(unitRepository));
            _unitManager = Check.NotNull(unitManager, nameof(unitManager));
        }
        
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _productRepository.GetCountAsync() <= 0)
            {
                var units = await _unitRepository.InsertAsync(
                await _unitManager.CreateAsync(
                    "Units"
                ));

                var packs = await _unitRepository.InsertAsync(
                await _unitManager.CreateAsync(
                    "Packs"
                ));

                await _productRepository.InsertAsync(
                  await _productManager.CreateAsync
                   (
                       number: "PROD-10010",
                       name: "PROD-10010",
                       fieldServiceProductType: FieldServiceProductType.Inventory,
                       productStructure: ProductStructure.Product,
                       decimalPlaces: 2,
                       unitId: units.Id
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
                       decimalPlaces: 1,
                       unitId: packs.Id
                   ),
                   autoSave: true
               );
            }
        }
    }
}