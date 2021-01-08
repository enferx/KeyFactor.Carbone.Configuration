using KeyFactor.Carbone.Configuration.Permissions;
using KeyFactor.Carbone.Configuration.Units;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace KeyFactor.Carbone.Configuration.Products
{
    [RemoteService(isEnabled: false, IsMetadataEnabled = false)]
    [Authorize(ConfigurationPermissions.Products.Default)]
    public class ProductAppService : ConfigurationAppService, IProductAppService
    {
        private readonly IProductRepository _repository;
        private readonly IUnitRepository _unitRepository;

        private readonly ProductManager _manager;

        public ProductAppService(IProductRepository repository, ProductManager manager, IUnitRepository unitRepository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _manager = Check.NotNull(manager, nameof(manager));
            _unitRepository = Check.NotNull(unitRepository, nameof(unitRepository));
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var query = from product in _repository
                        join unit in _unitRepository on product.UnitId equals unit.Id
                        where product.Id == id
                        select new { product, unit };

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Product), id);
            }
            var productDto = ObjectMapper.Map<Product, ProductDto>(queryResult.product);
            productDto.UnitName = queryResult.unit.Name;
            return productDto;
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
                input.Sorting = "product.Number";
            }

            // Create query with join.
            var query = from product in _repository
                        join unit in _unitRepository on product.UnitId equals unit.Id
                        select new { product, unit };

            // Filter, order and paging.
            query = query
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    item => item.product.Number.Contains(input.Filter)
                 )
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of ProductDto objects
            var productsDto = queryResult.Select(x =>
            {
                var productDto = ObjectMapper.Map<Product, ProductDto>(x.product);
                productDto.UnitName = x.unit.Name;
                return productDto;
            }).ToList();

            var totalCount = await AsyncExecuter.CountAsync(
              _repository.WhereIf(
                  !input.Filter.IsNullOrWhiteSpace(),
                  product => product.Number.Contains(input.Filter)
              )
            );

            return new PagedResultDto<ProductDto>(
                totalCount,
                productsDto
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
                validToDate: input.ValidToDate,
                unitId: input.UnitId
            );

            await _repository.InsertAsync(product);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize(ConfigurationPermissions.Products.Edit)]
        public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input)
        {
            var product = await _repository.GetAsync(id);
            if (product.Number != input.Number)
            {
                await _manager.ChangeNumberAsync(product, input.Number);
            }
            _manager.ChangeName(product, input.Name);
            product.ConcurrencyStamp = input.ConcurrencyStamp;
            product.ConvertToCustomerAsset = input.ConvertToCustomerAsset;
            product.CurrentCost = input.CurrentCost;
            product.DecimalPlaces = input.DecimalPlaces;
            product.Description = input.Description;
            product.FieldServiceProductType = input.FieldServiceProductType;
            product.IsStockItem = input.IsStockItem;
            product.ProductStructure = input.ProductStructure;
            product.PurchaseName = input.PurchaseName;
            product.QuantityOnHand = input.QuantityOnHand;
            product.StandardCost = input.StandardCost;
            product.Taxable = input.Taxable;
            product.ValidFromDate = input.ValidFromDate;
            product.ValidToDate = input.ValidToDate;
            product.UnitId = input.UnitId;
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

        [Authorize(ConfigurationPermissions.Products.Edit)]
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

        public async Task<ProductPropertyDto> GetProductPropertyAsync(Guid id)
        {
            var productProperty = await _repository.GetProductPropertyAsync(id);
            return ObjectMapper.Map<ProductProperty, ProductPropertyDto>(productProperty);
        }

        public Task<IReadOnlyList<ValidationError>> ValidateCreateAsync(CreateProductPropertyDto input)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ValidationError>> ValidateUpdateAsync(Guid id, CreateProductPropertyDto input)
        {
            throw new NotImplementedException();
        }


        public async Task<ProductPropertyDto> CreateProductPropertyAsync(CreateProductPropertyDto input)
        {
            ProductProperty productProperty = null;
            if(input.DataType == Datatype.Decimal)
            {
                productProperty = await _manager.CreateDecimalProductProperty
                (
                    name: input.Name,
                    description: input.Description,
                    isReadOnly: input.IsReadOnly,
                    isHidden: input.IsHidden,
                    isRequired: input.IsRequired,
                    defaultValueDecimal: input.DefaultValueDecimal,
                    minValueDecimal: input.MinDecimalValue.Value,
                    maxValueDecimal: input.MaxDecimalValue.Value,
                    productId: input.Productid
                );
            }
            else if(input.DataType == Datatype.Integer)
            {
                productProperty = await _manager.CreateIntegerProductProperty
                (
                    name: input.Name,
                    description: input.Description,
                    isReadOnly: input.IsReadOnly,
                    isHidden: input.IsHidden,
                    isRequired: input.IsRequired,
                    defaultValueInteger: input.DefaultValueInteger,
                    minValueInteger: input.MinIntegerValue.Value,
                    maxValueInteger: input.MaxIntegerValue.Value,
                    productId: input.Productid
                );
            }
            else if(input.DataType == Datatype.Double)
            {
                productProperty = await _manager.CreateDoubleProductProperty
                (
                    name: input.Name,
                    description: input.Description,
                    isReadOnly: input.IsReadOnly,
                    isHidden: input.IsHidden,
                    isRequired: input.IsRequired,
                    defaultValueDouble: input.DefaultValueDouble,
                    minValueDouble: input.MinDoubleValue.Value,
                    maxValueDouble: input.MaxDoubleValue.Value,
                    productId: input.Productid
                );
            }
            else
            {
                productProperty = await _manager.CreateStringProductProperty
                (
                    name: input.Name,
                    description: input.Description,
                    isReadOnly: input.IsReadOnly,
                    isHidden: input.IsHidden,
                    isRequired: input.IsRequired,
                    defaultValueString: input.DefaultValueString,
                    maxLengthString: input.MaxLengthString.Value,
                    productId: input.Productid
                );
            }
            productProperty = await _repository.CreateProductProperty(productProperty);
            return ObjectMapper.Map<ProductProperty, ProductPropertyDto>(productProperty);
        }

        public async Task<ProductPropertyDto> CreateDecimalProductPropertyAsync(CreateDecimalProductPropertyDto input)
        {
            var productProperty = await _manager.CreateDecimalProductProperty
            (
                name: input.Name,
                description: input.Description,
                isReadOnly: input.IsReadOnly,
                isHidden: input.IsHidden,
                isRequired: input.IsRequired,
                defaultValueDecimal: input.DefaultValueDecimal,
                minValueDecimal: input.MinDecimalValue,
                maxValueDecimal: input.MaxDecimalValue,
                productId: input.Productid
            );

            productProperty = await _repository.CreateProductProperty(productProperty);
            return ObjectMapper.Map<ProductProperty, ProductPropertyDto>(productProperty);
        }

        public async Task<ProductPropertyDto> CreateIntegerProductPropertyAsync(CreateIntegerProductPropertyDto input)
        {
            var productProperty = await _manager.CreateIntegerProductProperty
            (
                name: input.Name,
                description: input.Description,
                isReadOnly: input.IsReadOnly,
                isHidden: input.IsHidden,
                isRequired: input.IsRequired,
                defaultValueInteger: input.DefaultValueInteger,
                minValueInteger: input.MinIntegerValue,
                maxValueInteger: input.MaxIntegerValue,
                productId: input.Productid
            );

            productProperty = await _repository.CreateProductProperty(productProperty);
            return ObjectMapper.Map<ProductProperty, ProductPropertyDto>(productProperty);

        }

        public async Task<ProductPropertyDto> CreateDoubleProductPropertyAsync(CreateDoubleProductPropertyDto input)
        {
            var productProperty = await _manager.CreateDoubleProductProperty
            (
                name: input.Name,
                description: input.Description,
                isReadOnly: input.IsReadOnly,
                isHidden: input.IsHidden,
                isRequired: input.IsRequired,
                defaultValueDouble: input.DefaultValueDouble,
                minValueDouble: input.MinDoubleValue,
                maxValueDouble: input.MaxDoubleValue,
                productId: input.Productid
            );

            productProperty = await _repository.CreateProductProperty(productProperty);
            return ObjectMapper.Map<ProductProperty, ProductPropertyDto>(productProperty);

        }

        public async Task<ProductPropertyDto> CreateStringProductPropertyAsync(CreateStringProductPropertyDto input)
        {
            var productProperty = await _manager.CreateStringProductProperty
            (
                name: input.Name,
                description: input.Description,
                isReadOnly: input.IsReadOnly,
                isHidden: input.IsHidden,
                isRequired: input.IsRequired,
                defaultValueString: input.DefaultValueString,
                maxLengthString: input.MaxLengthString,
                productId: input.Productid
            );

            productProperty = await _repository.CreateProductProperty(productProperty);
            return ObjectMapper.Map<ProductProperty, ProductPropertyDto>(productProperty);
        }

    }
}
