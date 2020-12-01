using JetBrains.Annotations;
using KeyFactor.Carbone.Configuration.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Validation;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductManager : DomainService
    {
        private readonly IProductRepository _repository;
        
        public ProductManager(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> CreateAsync(
            [NotNull] string number,
            [NotNull] string name,
            FieldServiceProductType fieldServiceProductType,
            ProductStructure productStructure,
            int decimalPlaces,
            [CanBeNull] string description = null,
            decimal? currentCost = null,
            decimal? standardCost = null,
            bool isStockItem = false,
            bool convertToCustomerAsset = false,
            bool taxable = false,
            [CanBeNull] string purchaseName = null,
            DateTime? validFromDate = null,
            DateTime? validToDate = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(number, nameof(number));

            var existingProduct = await _repository.FindByNumberAsync(number);
            if (existingProduct != null)
            {
                throw new ProductNumberAlreadyExistsException(number);
            }

            return new Product
            (
                id: GuidGenerator.Create(),
                number: number,
                name: name,
                fieldServiceProductType: fieldServiceProductType,
                productStructure: productStructure,
                decimalPlaces: decimalPlaces,
                description: description,
                currentCost: currentCost,
                standardCost: standardCost,
                isStockItem: isStockItem,
                convertToCustomerAsset: convertToCustomerAsset,
                taxable: taxable,
                purchaseName: purchaseName,
                validFromDate: validFromDate,
                validToDate: validToDate
            );
        }

        public async Task ChangeNumberAsync(
            [NotNull] Product product,
            [NotNull] string newNumber)
        {
            Check.NotNull(product, nameof(product));
            Check.NotNullOrWhiteSpace(newNumber, nameof(newNumber));

            var existingProduct = await _repository.FindByNumberAsync(newNumber);
            if (existingProduct != null && existingProduct.Id != product.Id)
            {
                throw new ProductNumberAlreadyExistsException(newNumber);        
            }
            product.ChangeNumber(newNumber);
        }
    }
}
