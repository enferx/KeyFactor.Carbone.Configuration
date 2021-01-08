using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductProperty : Entity<Guid>
    {
        public Datatype DataType { get; set; }

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
            Datatype datatype,
            string description,
            bool isRequired,
            bool isHidden,
            bool isReadOnly,
            Product product
        )
        {
            Id = id;
            Check.NotNull(name, nameof(name));
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
            datatype: Datatype.Integer,
            description: description,
            isRequired: isRequired,
            isHidden: isHidden,
            isReadOnly: isReadOnly,
            product: product
        )
        {
            DefaultValueInteger = defaultValueInteger;
            MinIntegerValue = minIntegerValue;
            MaxIntegerValue = maxIntegerValue;
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
            datatype: Datatype.Decimal,
            description: description,
            isRequired: isRequired,
            isHidden: isHidden,
            isReadOnly: isReadOnly,
            product: product
        )
        {
            DefaultValueDecimal = defaultValueDecimal;
            MinDecimalValue = minDecimalValue;
            MaxDecimalValue = maxDecimalValue;
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
            datatype: Datatype.Double,
            description: description,
            isRequired: isRequired,
            isHidden: isHidden,
            isReadOnly: isReadOnly,
            product: product
        )
        {
            DefaultValueDouble = defaultValueDouble;
            MinDoubleValue = minDoubleValue;
            MaxDoubleValue = maxDoubleValue;
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
            datatype: Datatype.String,
            description: description,
            isRequired: isRequired,
            isHidden: isHidden,
            isReadOnly: isReadOnly,
            product: product
        )
        {
            DefaultValueString = defaultValueString;
            MaxLengthString = maxLengthString;
        }
    }
}
