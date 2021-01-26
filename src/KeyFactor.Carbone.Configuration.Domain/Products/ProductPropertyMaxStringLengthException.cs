using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Validation;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductPropertyMaxStringLengthException : BusinessException, IHasValidationErrors
    {
        public ProductPropertyMaxStringLengthException() :
            base(ConfigurationErrorCodes.ProductPropertyMaxStringLength)
        {
            WithData("0", ProductPropertyConsts.MaxStringLength);
        }
        public IList<ValidationResult> ValidationErrors => new List<ValidationResult>()
        {
            new ValidationResult(Code, new string[]{ "MaxStringLength" })    
        };
    }
}
