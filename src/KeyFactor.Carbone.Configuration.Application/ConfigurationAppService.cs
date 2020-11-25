using KeyFactor.Carbone.Configuration.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace KeyFactor.Carbone.Configuration
{
    public abstract class ConfigurationAppService : ApplicationService
    {
        protected ConfigurationAppService()
        {
            LocalizationResource = typeof(ConfigurationResource);
            ObjectMapperContext = typeof(ConfigurationApplicationModule);
        }

        protected List<ValidationError> Validate(object input)
        {
            var errors = new List<ValidationError>();
            var context = new ValidationContext(input);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(input, context, validationResults, true);
            errors.AddRange(validationResults.Select(x => new ValidationError
            {
                Message = x.ErrorMessage,
                MemberNames = x.MemberNames.ToList()
            }));
            return errors;
        }

    }
}
