using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Validation;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductNumberAlreadyExistsException : BusinessException, IHasValidationErrors
    {
        public ProductNumberAlreadyExistsException(string number)
            : base(ConfigurationErrorCodes.ProductNumberAlreadyExists)
        {
            WithData("0", number);
        }

        public IList<ValidationResult> ValidationErrors => new List<ValidationResult>() 
        {
            new ValidationResult(this.Code, new List<string>() { "Number" })
        };
        
    }
}
