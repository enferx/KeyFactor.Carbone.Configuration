using KeyFactor.Carbone.Configuration.Localization;
using KeyFactor.Carbone.Configuration.Shared;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Volo.Abp.Http.Client;
using Volo.Abp.Validation;
using FluentValidation;

namespace KeyFactor.Carbone.Configuration.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class CreateConfigurationPageModel<T1> :
        ConfigurationPageModel, IValidateCreate<T1>
    {
        [BindProperty]
        public T1 Input { get; set; }

        private readonly AbstractValidator<T1> _validator;

        public CreateConfigurationPageModel(T1 input) : base("Input.")
        {
            Input = input;
        }

        public CreateConfigurationPageModel(T1 input, AbstractValidator<T1> validator) : base("Input.")
        {
            Input = input;
            _validator = validator;
        }

        public async Task<List<ValidationError>> ValidateCreateAsync(T1 input)
        {
           return await OnValidateAsync(input);
        }

        public async Task OnGet()
        {
            ConfigureOnGet();
            await OnGetAsync();
        }

        public async Task<JsonResult> OnPost()
        {
            ConfigureOnGet();
            List<ValidationError> errors = new List<ValidationError>();
            try
            {
                errors = await ValidateCreateAsync(Input);
                if (!errors.Any())
                {
                    await OnCreateAsync();
                }
            }
            catch (AbpRemoteCallException ex)
            {
                errors.AddRange(ex.Error.ValidationErrors
                    .Select(error => 
                        new ValidationError(message: ex.Message, memberNames: error.Members)
                    ));
            }
            catch (AbpValidationException ex)
            {
                errors.AddRange(ex.ValidationErrors
                   .Select(error =>
                        new ValidationError(message: error.ErrorMessage, memberNames: error.MemberNames.ToArray())
                   ));
            }
            return new JsonResult(new
            {
                Success = !errors.Any(),
                Errors = errors
            });
        }


        protected virtual async Task<List<ValidationError>> OnValidateAsync(T1 input)
        {
            var result = _validator?.Validate(input);
            return await Task.FromResult(result.Errors.Select(x =>
                new ValidationError
                (
                    x.ErrorMessage,
                    new List<string> { x.PropertyName })
                ).ToList()
            );
        }

        protected Task OnGetAsync() { return Task.CompletedTask; }

        protected abstract Task OnCreateAsync();
    }
}