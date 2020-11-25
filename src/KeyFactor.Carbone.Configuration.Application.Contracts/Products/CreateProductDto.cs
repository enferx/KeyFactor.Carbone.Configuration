﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class CreateProductDto
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
        public int DecimalPlaces { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ValidFromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ValidToDate { get; set; }
    }
}
