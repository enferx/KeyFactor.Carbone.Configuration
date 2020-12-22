using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Validation;

namespace KeyFactor.Carbone.Configuration.Units
{
    public class UnitNameAlreadyExistsException : BusinessException, IHasValidationErrors
    {
        public UnitNameAlreadyExistsException(string name)
            : base(ConfigurationErrorCodes.UnitNameAlreadyExists)
        {
            WithData("0", name);
        }
        public IList<ValidationResult> ValidationErrors => new List<ValidationResult>()
        {
            new ValidationResult(Code, new List<string>() { "Name" })
        };
    }
}
