using KeyFactor.Carbone.Configuration.Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class CreateDoubleProductPropertyDto : CreateProductPropertyBaseDto
    {
        public double? DefaultValueDouble { get; set; }

        public double MaxDoubleValue { get; set; }

        public double MinDoubleValue { get; set; }
    }
}
