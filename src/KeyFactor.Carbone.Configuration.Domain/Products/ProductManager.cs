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
            decimal minDecimalValue,
            decimal maxDecimalValue
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
                defaultValueDecimal: defaultValueDecimal,
                minDecimalValue: minDecimalValue,
                maxDecimalValue: maxDecimalValue
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
            double minDoubleValue,
            double maxDoubleValue
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
                minDoubleValue: minDoubleValue,
                maxDoubleValue: maxDoubleValue
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
            int minIntegerValue,
            int maxIntegerValue
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
                minDoubleValue: minIntegerValue,
                maxDoubleValue: maxIntegerValue
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

        public async Task<ProductProperty> UpdateDecimalProductProperty(
            Guid id,
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
            var property = await _repository.GetProductPropertyAsync(id);
            if (property == null)
            {
                throw new EntityNotFoundException(typeof(ProductProperty), id);
            }
            Check.NotNullOrWhiteSpace(name, nameof(name));
            ProductProperty.CheckConditions(isRequired, defaultValueDecimal, minValueDecimal, maxValueDecimal);
            ClearProductProperty(ref property);
            property.ChangeName(name);
            property.DataType = DataType.Decimal;
            property.IsHidden = isHidden;
            property.IsReadOnly = isReadOnly;
            property.IsRequired = isRequired;
            property.Description = description;
            property.DefaultValueDecimal = defaultValueDecimal;
            property.MaxDecimalValue = maxValueDecimal;
            property.MinDecimalValue = minValueDecimal;
            property.ProductId = productId;
            return property;
        }

        public async Task<ProductProperty> UpdateDoubleProductProperty
        (
            Guid id,
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Guid productId,
            double? defaultValueDouble,
            double minDoubleValue,
            double maxDoubbleValue
        )
        {
            var property = await _repository.GetProductPropertyAsync(id);
            if (property == null)
            {
                throw new EntityNotFoundException(typeof(ProductProperty), id);
            }
            Check.NotNullOrWhiteSpace(name, nameof(name));
            ProductProperty.CheckConditions(isRequired, defaultValueDouble, minDoubleValue, maxDoubbleValue);
            ClearProductProperty(ref property);
            property.ChangeName(name);
            property.DataType = DataType.Double;
            property.IsHidden = isHidden;
            property.IsReadOnly = isReadOnly;
            property.IsRequired = isRequired;
            property.Description = description;
            property.DefaultValueDouble = defaultValueDouble;
            property.MaxDoubleValue = maxDoubbleValue;
            property.MinDoubleValue = minDoubleValue;
            property.ProductId = productId;
            return property;
        }

        public async Task<ProductProperty> UpdateIntegerProductProperty
        (
            Guid id,
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Guid productId,
            int? defaultValueInteger,
            int minIntegerValue,
            int maxIntegerValue
        )
        {
            var property = await _repository.GetProductPropertyAsync(id);
            if (property == null)
            {
                throw new EntityNotFoundException(typeof(ProductProperty), id);
            }
            Check.NotNullOrWhiteSpace(name, nameof(name));
            ProductProperty.CheckConditions(isRequired, defaultValueInteger, minIntegerValue, maxIntegerValue);
            ClearProductProperty(ref property);
            property.ChangeName(name);
            property.DataType = DataType.Integer;
            property.IsHidden = isHidden;
            property.IsReadOnly = isReadOnly;
            property.IsRequired = isRequired;
            property.Description = description;
            property.DefaultValueInteger = defaultValueInteger;
            property.MaxIntegerValue = maxIntegerValue;
            property.MinIntegerValue = minIntegerValue;
            property.ProductId = productId;
            return property;
        }

        public async Task<ProductProperty> UpdateStringProductProperty
        (
            Guid id,
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Guid productId,
            string defaultValueString,
            int maxLengthString)
        {
            var property = await _repository.GetProductPropertyAsync(id);
            if (property == null)
            {
                throw new EntityNotFoundException(typeof(ProductProperty), id);
            }
            Check.NotNullOrWhiteSpace(name, nameof(name));
            ProductProperty.CheckConditions(isRequired, defaultValueString, maxLengthString);
            ClearProductProperty(ref property);
            property.ChangeName(name);
            property.DataType = DataType.Integer;
            property.IsHidden = isHidden;
            property.IsReadOnly = isReadOnly;
            property.IsRequired = isRequired;
            property.Description = description;
            property.MaxLengthString = maxLengthString;
            property.DefaultValueString = defaultValueString;
            property.ProductId = productId;
            return property;
        }

        private void ClearProductProperty(ref ProductProperty productProperty)
        {
            productProperty.DefaultValueDecimal = null;
            productProperty.DefaultValueDouble = null;
            productProperty.DefaultValueInteger = null;
            productProperty.DefaultValueString = null;

            productProperty.MaxDecimalValue = null;
            productProperty.MaxDoubleValue = null;
            productProperty.MaxIntegerValue = null;

            productProperty.MinDecimalValue = null;
            productProperty.MinDoubleValue = null;
            productProperty.MinIntegerValue = null;

            productProperty.MaxLengthString = null;
            productProperty.DoublePrecission = null;
        }
    }
}
