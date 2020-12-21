using KeyFactor.Carbone.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Uow;

namespace KeyFactor.Carbone.Configuration.Products
{
    [RemoteService(isEnabled:false, IsMetadataEnabled = false)]
    [Authorize(ConfigurationPermissions.Products.Default)]
    public class ProductAppService : ConfigurationAppService, IProductAppService
    {
        private readonly IProductRepository _repository;
        private readonly ProductManager _manager;
        
        public ProductAppService(IProductRepository repository, ProductManager manager)
        {
            _repository = repository ?? throw new ArgumentNullException("repository");
            _manager = manager ?? throw new ArgumentNullException("manager");
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _repository.GetAsync(id);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task<ProductDto> FindByNumber(string number)
        {
            var product = await _repository.FindByNumberAsync(number);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Product.Number);
            }

            var products = await _repository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await AsyncExecuter.CountAsync(
                _repository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    product => product.Number.Contains(input.Filter)
                )
            );

            return new PagedResultDto<ProductDto>(
                totalCount,
                ObjectMapper.Map<List<Product>, List<ProductDto>>(products)
            );
        }

        [Authorize(ConfigurationPermissions.Products.Create)]
        public async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            var product = await _manager.CreateAsync
            (
                number: input.Number,
                name: input.Name,
                fieldServiceProductType: input.FieldServiceProductType,
                productStructure: ProductStructure.Product,
                decimalPlaces: input.DecimalPlaces,
                description: input.Description,
                currentCost: input.CurrentCost,
                standardCost: input.StandardCost,
                isStockItem: input.IsStockItem,
                convertToCustomerAsset: input.ConvertToCustomerAsset,
                taxable: input.Taxable,
                purchaseName: input.PurchaseName,
                validFromDate: input.ValidFromDate,
                validToDate: input.ValidToDate
            );

            await _repository.InsertAsync(product);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }
       
        [Authorize(ConfigurationPermissions.Products.Update)]
        public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input)
        {
            var product = await _repository.GetAsync(id);
            if (product.Number != input.Number)
            {
                await _manager.ChangeNumberAsync(product, input.Number);
            }
            product.ConcurrencyStamp = input.ConcurrencyStamp;
            product.ConvertToCustomerAsset = input.ConvertToCustomerAsset;
            product.CurrentCost = input.CurrentCost;
            product.DecimalPlaces = input.DecimalPlaces;
            product.Description = input.Description;
            product.FieldServiceProductType = input.FieldServiceProductType;
            product.IsStockItem = input.IsStockItem;
            product.Name = input.Name;
            product.ProductStructure = input.ProductStructure;
            product.PurchaseName = input.PurchaseName;
            product.QuantityOnHand = input.QuantityOnHand;
            product.StandardCost = input.StandardCost;
            product.Taxable = input.Taxable;
            product.ValidFromDate = input.ValidFromDate;
            product.ValidToDate = input.ValidToDate;

            await _repository.UpdateAsync(product);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize(ConfigurationPermissions.Products.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        [Authorize(ConfigurationPermissions.Products.Create)]
        public async Task<IReadOnlyList<ValidationError>> ValidateCreateAsync(CreateProductDto input)
        {
            var errors = Validate(input);
            var existingProduct = await _repository.FindByNumberAsync(input.Number);
            if (existingProduct != null)
            {
                errors.Add(new ValidationError
                (
                    message: L[$"{ConfigurationErrorCodes.ProductNumberAlreadyExists}", input.Number],
                    memberNames: new List<string>() { "Number" }
                ));
            }
            return errors;

        }

        [Authorize(ConfigurationPermissions.Products.Update)]
        public async Task<IReadOnlyList<ValidationError>> ValidateUpdateAsync(Guid id, UpdateProductDto input)
        {
            var errors = Validate(input);
            var existingProduct = await _repository.FindByNumberAsync(input.Number);
            if (existingProduct != null && existingProduct.Id != id)
            {
                errors.Add(new ValidationError
                (
                    message: L[$"{ConfigurationErrorCodes.ProductNumberAlreadyExists}", input.Number],
                    memberNames: new List<string>() { "Number" }
                ));
            }
            return errors;
        }
    }
}
