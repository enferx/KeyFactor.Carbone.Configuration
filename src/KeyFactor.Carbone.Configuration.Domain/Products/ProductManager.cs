using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;

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
            Guid unitId,
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
                validToDate: validToDate,
                unitId: unitId
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


        public void ChangeName(
            [NotNull] Product product,
            [NotNull] string newName)
        {
            Check.NotNull(product, nameof(product));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            product.ChangeName(newName);
        }

        public async Task<ProductProperty> CreateDecimalProductProperty
        (
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Guid productId,
            decimal? defaultValueDecimal,
            decimal minValueDecimal,
            decimal maxValueDecimal
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var product = await _repository.GetAsync(productId);
            if(product == null)
            {
                throw new EntityNotFoundException(typeof(Product), productId);
            }
            return new ProductProperty
            (
                id: GuidGenerator.Create(),
                name: name,
                description: description,
                isRequired: isRequired,
                isHidden: isHidden,
                isReadOnly: isReadOnly,
                product: product,
                defaultValueDecimal: defaultValueDecimal,
                minDecimalValue: minValueDecimal,
                maxDecimalValue: maxValueDecimal
            );
        }

        public async Task<ProductProperty> CreateDoubleProductProperty
        (
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Guid productId,
            double? defaultValueDouble,
            double minValueDouble,
            double maxValueDouble
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var product = await _repository.GetAsync(productId);
            if (product == null)
            {
                throw new EntityNotFoundException(typeof(Product), productId);
            }
            return new ProductProperty
            (
                id: GuidGenerator.Create(), 
                name: name,
                description: description,
                isRequired: isRequired,
                isHidden: isHidden,
                isReadOnly: isReadOnly,
                product: product,
                defaultValueDouble: defaultValueDouble,
                minDoubleValue: minValueDouble,
                maxDoubleValue: maxValueDouble
            );
        }

        public async Task<ProductProperty> CreateIntegerProductProperty
        (
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Guid productId,
            int? defaultValueInteger,
            int minValueInteger,
            int maxValueInteger
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var product = await _repository.GetAsync(productId);
            if (product == null)
            {
                throw new EntityNotFoundException(typeof(Product), productId);
            }
            return new ProductProperty
            (
                id: GuidGenerator.Create(), 
                name: name,
                description: description,
                isRequired: isRequired,
                isHidden: isHidden,
                isReadOnly: isReadOnly,
                product: product,
                defaultValueDouble: defaultValueInteger,
                minDoubleValue: minValueInteger,
                maxDoubleValue: maxValueInteger
            );
        }

        public async Task<ProductProperty> CreateStringProductProperty
        (
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Guid productId,
            string defaultValueString,
            int maxLengthString)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var product = await _repository.GetAsync(productId);
            if (product == null)
            {
                throw new EntityNotFoundException(typeof(Product), productId);
            }
            return new ProductProperty
            (
                id: GuidGenerator.Create(), 
                name: name,
                description: description,
                isRequired: isRequired,
                isHidden: isHidden,
                isReadOnly: isReadOnly,
                product: product,
                defaultValueString: defaultValueString,
                maxLengthString: maxLengthString
            );
        }
    }
}
