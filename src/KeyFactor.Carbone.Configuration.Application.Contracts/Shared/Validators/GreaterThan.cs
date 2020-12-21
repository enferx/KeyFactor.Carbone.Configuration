using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeyFactor.Carbone.Configuration.Shared.Validators
{
    public class GreaterThan : ValidationAttribute
    {
        public string Property { get; private set; }

        public string PropertyToCompare { get; private set; }

        public GreaterThan(string property, string propertyToCompare)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            PropertyToCompare = propertyToCompare ?? throw new ArgumentNullException(nameof(propertyToCompare));
        }

        public string GetErrorMessage() =>
        $"The field {Property} must be greater than the field {PropertyToCompare}.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var anotherValue = validationContext.ObjectInstance.GetType().GetProperty(PropertyToCompare)
                .GetValue(validationContext.ObjectInstance, null);
            if(value == null || anotherValue == null)
            {
                return ValidationResult.Success;
            }
            if(value.GetType() != anotherValue.GetType())
            {
                throw new InvalidOperationException("Check the properties types. They are different and can't be compared.");
            }
            if(value.GetType() == typeof(DateTime) && anotherValue.GetType() == typeof(DateTime))   
            {
                return (value as DateTime?).Value > (anotherValue as DateTime?) ?
                        ValidationResult.Success :
                        new ValidationResult(GetErrorMessage());
            }
            throw new NotImplementedException("The type is not managed.");
        }
    }
}
