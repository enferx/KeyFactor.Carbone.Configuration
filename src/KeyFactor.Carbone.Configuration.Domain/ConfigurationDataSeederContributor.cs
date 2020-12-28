using KeyFactor.Carbone.Configuration.Products;
using KeyFactor.Carbone.Configuration.Units;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace KeyFactor.Carbone.Configuration.Domain
{
    public class ConfigurationDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly ProductManager _productManager;
        private readonly UnitManager _unitManager;

        public ConfigurationDataSeederContributor
        (
            IProductRepository productRepository,
            IUnitRepository unitRepository,
            ProductManager productManager,
            UnitManager unitManager)
        {
            _productRepository = Check.NotNull(productRepository, nameof(productRepository));
            _unitRepository = Check.NotNull(unitRepository, nameof(unitRepository));
            _productManager = Check.NotNull(productManager, nameof(productManager));
            _unitManager = Check.NotNull(unitManager, nameof(unitManager));
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _unitRepository.GetCountAsync() == 0)
            {
                await _unitRepository.InsertAsync(
                    await _unitManager.CreateAsync(
                        "Units"
                    ), autoSave: true);

                await _unitRepository.InsertAsync(
                await _unitManager.CreateAsync(
                    "Packs"
                ), autoSave: true);
            }

            var units = await _unitRepository.FindByNameAsync("Units");

            if (await _productRepository.GetCountAsync() == 0)
            {
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
                       unitId: units.Id
                   ),
                   autoSave: true
               );
            }
        }
    }
}
