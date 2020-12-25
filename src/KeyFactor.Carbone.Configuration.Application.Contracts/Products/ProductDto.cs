using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductDto : EntityDto<Guid>
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

        public FieldServiceProductType FieldServiceProductType { get; set; }

        public ProductStructure ProductStructure { get; set; }

        public int DecimalPlaces { get; set; }

        public string TimeZoneId { get; set; }

        public DateTime? ValidFromDate { get; set; }

        public DateTime? ValidToDate { get; set; }

        public Guid UnitId { get; set; }

        public string UnitName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
