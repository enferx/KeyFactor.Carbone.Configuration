using KeyFactor.Carbone.Configuration.Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class CreateDecimalProductPropertyDto : CreateProductPropertyBaseDto
    {
        public decimal? DefaultValueDecimal { get; set; }

        public decimal MaxDecimalValue { get; set; }

        public decimal MinDecimalValue { get; set; }
    }
}
