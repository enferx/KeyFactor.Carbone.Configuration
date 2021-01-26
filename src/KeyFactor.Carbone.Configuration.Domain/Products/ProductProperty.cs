using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductProperty : Entity<Guid>
    {
        public DataType DataType { get; set; }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public bool IsReadOnly { get; set; }

        public bool IsHidden { get; set; }

        public string Description { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public decimal? DefaultValueDecimal { get; set; }

        public double? DefaultValueDouble { get; set; }

        public string DefaultValueString { get; set; }

        public int? DefaultValueInteger { get; set; }

        public int? MaxLengthString { get; set; }

        public decimal? MaxDecimalValue { get; set; }

        public decimal? MinDecimalValue { get; set; }

        public double? MaxDoubleValue { get; set; }

        public double? MinDoubleValue { get; set; }

        public int? DoublePrecission { get; set; }

        public int? MaxIntegerValue { get; set; }

        public int? MinIntegerValue { get; set; }

        private ProductProperty()
        {

        }

        private ProductProperty
        (
            Guid id,
            [NotNull] string name,
            DataType datatype,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Product product
        )
        {
            Id = id;
            Name = Check.NotNull(name, nameof(name));
            DataType = datatype;
            Description = description;
            IsReadOnly = isReadOnly;
            IsHidden = isHidden;
            IsRequired = isRequired;
            Product = product;
        }

        internal ProductProperty
        (
            Guid id,
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Product product,
            int? defaultValueInteger,
            int minIntegerValue,
            int maxIntegerValue
        ) : this
        (
            id: id,
            name: name,
            datatype: DataType.Integer,
            description: description,
            isRequired: isRequired,
            isHidden: isHidden,
            isReadOnly: isReadOnly,
            product: product
        )
        {
            CheckConditions(isRequired, defaultValueInteger, minIntegerValue, maxIntegerValue);
            DefaultValueInteger = defaultValueInteger;
            MinIntegerValue = minIntegerValue;
            MaxIntegerValue = maxIntegerValue;
        }

        public static void CheckConditions(bool isRequired, int? defaultValueInteger, int minIntegerValue, int maxIntegerValue)
        {
            if (minIntegerValue > maxIntegerValue)
            {
                throw new ProductPropertyOutOfRangeLimitsException
                (
                    minValue: minIntegerValue,
                    maxValue: maxIntegerValue
                );
            }
            if (isRequired && !defaultValueInteger.HasValue)
            {
                throw new ProductPropertyRequiredDefaultValueException(DataType.Integer);
            }
            if (defaultValueInteger.HasValue)
            {
                if (defaultValueInteger.Value < minIntegerValue || defaultValueInteger.Value > maxIntegerValue)
                {
                    throw new ProductPropertyDefaultValueOutOfRangeException
                    (
                        minValue: minIntegerValue,
                        maxValue: maxIntegerValue,
                        value: defaultValueInteger.Value
                    );
                }
            }
        }

        internal ProductProperty
        (
            Guid id,
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Product product,
            decimal? defaultValueDecimal,
            decimal minDecimalValue,
            decimal maxDecimalValue
        ) : this
        (
            id: id,
            name: name,
            datatype: DataType.Decimal,
            description: description,
            isRequired: isRequired,
            isHidden: isHidden,
            isReadOnly: isReadOnly,
            product: product
        )
        {
            CheckConditions(isRequired, defaultValueDecimal, minDecimalValue, maxDecimalValue);
            DefaultValueDecimal = defaultValueDecimal;
            MinDecimalValue = minDecimalValue;
            MaxDecimalValue = maxDecimalValue;
        }

        public static void CheckConditions(bool isRequired, decimal? defaultValueDecimal, decimal minDecimalValue, decimal maxDecimalValue)
        {
            if (minDecimalValue > maxDecimalValue)
            {
                throw new ProductPropertyOutOfRangeLimitsException
                (
                    minValue: minDecimalValue,
                    maxValue: maxDecimalValue
                );
            }
            if (isRequired && !defaultValueDecimal.HasValue)
            {
                throw new ProductPropertyRequiredDefaultValueException(DataType.Decimal);
            }
            if (defaultValueDecimal.HasValue)
            {
                if (defaultValueDecimal.Value < minDecimalValue || defaultValueDecimal.Value > maxDecimalValue)
                {
                    throw new ProductPropertyDefaultValueOutOfRangeException
                    (
                        minValue: minDecimalValue,
                        maxValue: maxDecimalValue,
                        value: defaultValueDecimal.Value
                    );
                }
            }
        }

        internal ProductProperty
        (
            Guid id,    
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Product product,
            double? defaultValueDouble,
            double minDoubleValue,
            double maxDoubleValue
        ) : this
        (
            id: id,
            name: name,
            datatype: DataType.Double,
            description: description,
            isRequired: isRequired,
            isHidden: isHidden,
            isReadOnly: isReadOnly,
            product: product
        )
        {
            CheckConditions(isRequired, defaultValueDouble, minDoubleValue, maxDoubleValue);
            DefaultValueDouble = defaultValueDouble;
            MinDoubleValue = minDoubleValue;
            MaxDoubleValue = maxDoubleValue;
        }

        public static void CheckConditions(bool isRequired, double? defaultValueDouble, double minDoubleValue, double maxDoubleValue)
        {
            if (minDoubleValue > maxDoubleValue)
            {
                throw new ProductPropertyOutOfRangeLimitsException
                (
                    minValue: minDoubleValue,
                    maxValue: maxDoubleValue
                );
            }
            if (isRequired && !defaultValueDouble.HasValue)
            {
                throw new ProductPropertyRequiredDefaultValueException(DataType.Double);
            }

            if (defaultValueDouble.HasValue)
            {
                if (defaultValueDouble.Value < minDoubleValue || defaultValueDouble.Value > maxDoubleValue)
                {
                    throw new ProductPropertyDefaultValueOutOfRangeException
                    (
                        minValue: minDoubleValue,
                        maxValue: maxDoubleValue,
                        value: defaultValueDouble.Value
                    );
                }
            }
        }

        internal ProductProperty
        (
            Guid id,
            [NotNull] string name,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Product product,
            string defaultValueString,
            int maxLengthString
        ) : this
        (
            id: id,
            name: name,
            datatype: DataType.String,
            description: description,
            isRequired: isRequired,
            isHidden: isHidden,
            isReadOnly: isReadOnly,
            product: product
        )
        {
            CheckConditions(isRequired, defaultValueString, maxLengthString);
            DefaultValueString = defaultValueString;
            MaxLengthString = maxLengthString;
        }

        public static void CheckConditions(bool isRequired, string defaultValueString, int maxLengthString)
        {
            if (isRequired && string.IsNullOrWhiteSpace(defaultValueString))
            {
                throw new ProductPropertyRequiredDefaultValueException(DataType.String);
            }
            if (maxLengthString > ProductPropertyConsts.MaxDescriptionLength)
            {
                throw new ProductPropertyMaxStringLengthException();
            }
        }

        public ProductProperty ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(Name), maxLength: ProductPropertyConsts.MaxNameLength);
        }
    }
}
