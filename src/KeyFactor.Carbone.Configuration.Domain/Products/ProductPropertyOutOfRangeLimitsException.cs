using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Validation;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductPropertyOutOfRangeLimitsException : BusinessException, IHasValidationErrors
    {
        private readonly DataType _dataType;

        public ProductPropertyOutOfRangeLimitsException(decimal minValue, decimal maxValue) 
            : base(ConfigurationErrorCodes.ProductPropertyOutOfRangeLimits)
        {
            WithData("0", minValue);
            WithData("1", maxValue);
            _dataType = DataType.Decimal;
        }
        
        public ProductPropertyOutOfRangeLimitsException(double minValue, double maxValue)
            : base(ConfigurationErrorCodes.ProductPropertyOutOfRangeLimits)
        {
            WithData("0", minValue);
            WithData("1", maxValue);
            _dataType = DataType.Double;
        }
        
        public ProductPropertyOutOfRangeLimitsException(int minValue, int maxValue)
            : base(ConfigurationErrorCodes.ProductPropertyOutOfRangeLimits)
        {
            WithData("0", minValue);
            WithData("1", maxValue);
            _dataType = DataType.Integer;
        }

        public IList<ValidationResult> ValidationErrors => _dataType == DataType.Decimal ?
            new List<ValidationResult>()
            {
                new ValidationResult(Code, new string[] { "MinDecimalValue", "MaxDecimalValue" })
            } :
            _dataType == DataType.Double ?
            new List<ValidationResult>()
            {
                new ValidationResult(Code, new string[] { "MinDoubleValue", "MaxDoubleValue" })
            } :
            _dataType == DataType.Integer ?
            new List<ValidationResult>()
            {
                new ValidationResult(Code, new string[] { "MinIntegerValue", "MaxintegerValue" })
            } :
            new List<ValidationResult>();
    }
}
