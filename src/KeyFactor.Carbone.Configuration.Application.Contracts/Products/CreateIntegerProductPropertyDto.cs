using KeyFactor.Carbone.Configuration.Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class CreateIntegerProductPropertyDto : CreateProductPropertyBaseDto
    {
        public int? DefaultValueInteger { get; set; }

        public int MaxIntegerValue { get; set; }

        public int MinIntegerValue { get; set; }
    }
}
