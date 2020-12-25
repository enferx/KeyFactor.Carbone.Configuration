using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class Product : AuditedAggregateRoot<Guid>
    {
        public string Number { get; private set; }

        public string Name { get; private set; }

        public string Description { get; set; }

        public decimal? CurrentCost { get; set; }

        public decimal? StandardCost { get; set; }

        public decimal? QuantityOnHand { get; set; }

        public bool IsStockItem { get; set; }

        public bool ConvertToCustomerAsset { get; set; }

        public bool Taxable { get; set; }

        public string PurchaseName { get; set; }

        public FieldServiceProductType  FieldServiceProductType { get; set; }

        public ProductStructure ProductStructure { get; set; }

        public int DecimalPlaces { get; set; }

        public string TimeZoneId { get; private set; }

        public DateTime? ValidFromDate { get; set; }

        public DateTime? ValidToDate { get; set; }

        public Guid UnitId { get; set; }

        private Product() {}

        internal Product(Guid id,
            [NotNull] string number,
            [NotNull] string name,
            FieldServiceProductType fieldServiceProductType,
            ProductStructure productStructure,
            int decimalPlaces,
            [CanBeNull] string description,
            decimal? currentCost,
            decimal? standardCost,
            bool isStockItem,
            bool convertToCustomerAsset,
            bool taxable,
            [CanBeNull] string purchaseName,
            DateTime? validFromDate,
            DateTime? validToDate,
            Guid unitId)
        {
            Id = id;
            SetName(name);
            SetNumber(number);
            FieldServiceProductType = fieldServiceProductType;
            ProductStructure = productStructure;
            DecimalPlaces = decimalPlaces;
            Description = description;
            CurrentCost = currentCost;
            StandardCost = standardCost;
            IsStockItem = isStockItem;
            ConvertToCustomerAsset = convertToCustomerAsset;
            Taxable = taxable;
            PurchaseName = purchaseName;
            ValidFromDate = validFromDate;
            ValidToDate = validToDate;
            TimeZoneId = TimeZoneInfo.Local.Id;
            UnitId = unitId;
        }

        internal Product ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        internal Product ChangeNumber([NotNull] string number)
        {
            SetNumber(number);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(Name), maxLength: ProductConsts.MaxNameLength);
        }

        private void SetNumber([NotNull] string number)
        {
            Number = Check.NotNullOrWhiteSpace(number, nameof(Number), maxLength: ProductConsts.MaxNumberLength);
        }

        public static implicit operator Task<object>(Product v)
        {
            throw new NotImplementedException();
        }
    }
}
