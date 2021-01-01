using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyFactor.Carbone.Configuration.Shared.Validators
{
    public class NotEmpty : ValidationAttribute
    {
        public string GetErrorMessage() => $"The field is required.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value.GetType() == typeof(Guid))
            {
                return (value as Guid?).Value != Guid.Empty ? ValidationResult.Success :
                    new ValidationResult(GetErrorMessage());
            }
            throw new NotImplementedException("The type is not managed.");
        }
    }
}
