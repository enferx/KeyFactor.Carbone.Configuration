using KeyFactor.Carbone.Configuration.Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyFactor.Carbone.Configuration.Products
{
    public abstract class CreateProductPropertyBaseDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [NotEmpty]
        public Guid Productid { get; set; }

        public string Description { get; set; }

        public bool IsRequired { get; set; }

        public bool IsReadOnly { get; set; }

        public bool IsHidden { get; set; }

    }
}
