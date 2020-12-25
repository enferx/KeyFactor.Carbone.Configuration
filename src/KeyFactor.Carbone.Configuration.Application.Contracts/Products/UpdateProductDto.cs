using KeyFactor.Carbone.Configuration.Shared.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(ProductConsts.MaxNumberLength)]
        public string Number { get; set; }

        [Required]
        [StringLength(ProductConsts.MaxNameLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? CurrentCost { get; set; }

        public decimal? StandardCost { get; set; }

        public decimal? QuantityOnHand { get; set; }

        public bool IsStockItem { get; set; }

        public bool ConvertToCustomerAsset { get; set; }

        public bool Taxable { get; set; }

        public string PurchaseName { get; set; }

        [Required]
        public FieldServiceProductType FieldServiceProductType { get; set; } = FieldServiceProductType.Inventory;

        [Required]
        public ProductStructure ProductStructure { get; set; } = ProductStructure.Product;

        [Required]
        [Range(0, 9)]
        public int DecimalPlaces { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ValidFromDate { get; set; }

        [GreaterThan(property: nameof(ValidToDate), propertyToCompare: nameof(ValidFromDate))]
        [DataType(DataType.Date)]
        public DateTime? ValidToDate { get; set; }

        public string ConcurrencyStamp { get; set; }

    }
}
