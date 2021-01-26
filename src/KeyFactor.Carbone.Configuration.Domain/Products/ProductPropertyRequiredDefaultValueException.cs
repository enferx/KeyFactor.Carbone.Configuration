using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Validation;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductPropertyRequiredDefaultValueException : BusinessException, IHasValidationErrors
    {
        private readonly DataType _dataType;

        public ProductPropertyRequiredDefaultValueException(DataType dataType) : 
            base(ConfigurationErrorCodes.ProductPropertyMaxStringLength)
        {
            WithData("0", dataType);
            _dataType = dataType;
        }

        public IList<ValidationResult> ValidationErrors =>
            _dataType == DataType.Decimal ?
            new List<ValidationResult>()
            {
                new ValidationResult(Code, new string[] { "DefaultValueDecimal" })
            } : _dataType == DataType.Double ?
            new List<ValidationResult>()
            {
                new ValidationResult(Code, new string[] { "DefaultValueDouble" })
            } : _dataType == DataType.Integer ?
            new List<ValidationResult>()
            {
                new ValidationResult(Code, new string[] { "DefaultValueDouble" })
            } : _dataType == DataType.String ?
            new List<ValidationResult>()
            {
                new ValidationResult(Code, new string[] { "DefaultValueString" })
            } :
            new List<ValidationResult>();


    }
}
