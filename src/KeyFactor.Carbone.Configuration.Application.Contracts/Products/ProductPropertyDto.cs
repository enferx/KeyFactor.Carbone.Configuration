using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductPropertyDto : EntityDto<Guid>
    {
        public DataType DataType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsRequired { get; set; }

        public bool IsReadOnly { get; set; }

        public bool IsHidden { get; set; }

        public decimal? DefaultValueDecimal { get; set; }

        public double? DefaultValueDouble { get; set; }

        public string DefaultValueString { get; set; }

        public int? DefaultValueInteger { get; set; }
        
        public int? MaxLengthString { get; set; }

        public decimal? MaxDecimalValue { get; set; }

        public decimal? MinDecimalValue { get; set; }

        public double? MaxDoubleValue { get; set; }

        public double? MinDoubleValue { get; set; }

        public int? MaxIntegerValue { get; set; }

        public int? MinIntegerValue { get; set; }

        public Guid ProductId { get; set; }
    }
}
