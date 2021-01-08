using KeyFactor.Carbone.Configuration.Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class CreateProductPropertyDto : IValidatableObject
    {
        [Required]
        public Datatype DataType { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [NotEmpty]
        public Guid Productid { get; set; }

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if(DataType == Datatype.Decimal)
            {
                if (!MinDecimalValue.HasValue)
                {
                    errors.Add(new ValidationResult(
                        errorMessage: "Required",
                        memberNames: new string[] { nameof(MinDecimalValue) }
                    ));
                }
                if (!MaxDecimalValue.HasValue)
                {
                    errors.Add(new ValidationResult(
                        errorMessage: "Required",
                        memberNames: new string[] { nameof(MaxDecimalValue) }
                    ));
                }
                if(MinDecimalValue.HasValue && MaxDecimalValue.HasValue)
                {
                    if (MinDecimalValue.Value > MaxDecimalValue.Value)
                    {
                        errors.Add(new ValidationResult(
                            errorMessage: "The numbers between min and max are incorrect.",
                            memberNames: new string[] { nameof(MinDecimalValue), nameof(MaxDecimalValue) }
                        ));
                    }
                    else if(DefaultValueDecimal.HasValue && (DefaultValueDecimal.Value < MinDecimalValue.Value || DefaultValueDecimal.Value > MaxDecimalValue.Value))
                    {
                        errors.Add(new ValidationResult(
                            errorMessage: "The default value is outside range.",
                            memberNames: new string[] { nameof(DefaultValueDecimal) }
                        ));
                    }
                    if(IsRequired && !DefaultValueDecimal.HasValue)
                    {
                        errors.Add(new ValidationResult(
                            errorMessage: "The default value is not specified and it is required.",
                            memberNames: new string[] { nameof(DefaultValueDecimal) }
                        ));
                    }
                }
            }
            return errors;
        }
    }
}
