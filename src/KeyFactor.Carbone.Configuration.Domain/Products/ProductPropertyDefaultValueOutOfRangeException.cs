using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Validation;

namespace KeyFactor.Carbone.Configuration.Products
{
    public class ProductPropertyDefaultValueOutOfRangeException : BusinessException, IHasValidationErrors
    {
        private readonly DataType _dataType;

        public ProductPropertyDefaultValueOutOfRangeException(decimal minValue, decimal maxValue, decimal value)
            : base(ConfigurationErrorCodes.ProductPropertyDefaultValueOutOfRange)
        {
            WithData("0", minValue);
            WithData("1", maxValue);
            WithData("2", value);
            _dataType = DataType.Decimal;
        }

        public ProductPropertyDefaultValueOutOfRangeException(double minValue, double maxValue, double value)
            : base(ConfigurationErrorCodes.ProductPropertyDefaultValueOutOfRange)
        {
            WithData("0", minValue);
            WithData("1", maxValue);
            WithData("2", value);
            _dataType = DataType.Double;
        }

        public ProductPropertyDefaultValueOutOfRangeException(int minValue, int maxValue, int value)
            : base(ConfigurationErrorCodes.ProductPropertyDefaultValueOutOfRange)
        {
            WithData("0", minValue);
            WithData("1", maxValue);
            WithData("2", value);
            _dataType = DataType.Integer;
        }

        public IList<ValidationResult> ValidationErrors => 
            _dataType == DataType.Decimal ?
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