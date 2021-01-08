using System;
using System.Collections.Generic;
using System.Text;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class CreateStringProductPropertyDto : CreateProductPropertyBaseDto
    {
        public string DefaultValueString { get; set; }
        
        public int MaxLengthString { get; set; }
    }
}
