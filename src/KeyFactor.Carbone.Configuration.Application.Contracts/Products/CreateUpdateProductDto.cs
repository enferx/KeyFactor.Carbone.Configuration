using KeyFactor.Carbone.Configuration.Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class CreateUpdateProductDto
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? CurrentCost { get; set; }

        public decimal? StandardCost { get; set; }

        public decimal? QuantityOnHand { get; set; }

        public bool IsStockItem { get; set; }

        public bool ConvertToCustomerAsset { get; set; }

        public bool Taxable { get; set; }

        public string PurchaseName { get; set; }

        public FieldServiceProductType FieldServiceProductType { get; set; } = FieldServiceProductType.Inventory;

        public ProductStructure ProductStructure { get; set; } = ProductStructure.Product;

        public int DecimalPlaces { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime? ValidFromDate { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime? ValidToDate { get; set; }

        public Guid UnitId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
