using KeyFactor.Carbone.Configuration.Localization;
using KeyFactor.Carbone.Configuration.Shared;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Volo.Abp.Http.Client;
using System.ComponentModel.DataAnnotations;
using System.Data;
using KeyFactor.Carbone.Configuration.Products;
using Volo.Abp.Validation;
using FluentValidation;

namespace KeyFactor.Carbone.Configuration.Web.Pages
{
    public abstract class UpdateConfigurationPageModel<T1, T2> : ConfigurationPageModel,
        IValidateUpdate<T1, T2>
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public T1 Id { get; set; }

        [BindProperty]
        public T2 Input { get; set; }

        private readonly AbstractValidator<T2> _validator;

        public UpdateConfigurationPageModel() : base("Input.")
        {
        }

        public UpdateConfigurationPageModel(AbstractValidator<T2> validator) : base("Input.")
        {
            _validator = validator;
        }

        public async Task<IReadOnlyList<ValidationError>> ValidateUpdateAsync(T1 id, T2 input)
        {
            var results = await OnValidateAsync(Id, Input);
            if (results.Any())
            {
                foreach (var error in results)
                {
                    foreach (var member in error.MemberNames)
                    {
                        ModelState.AddModelError(EntityPath + member, error.Message);
                    }
                }
            }
            return results;
        }

        public async Task OnGet()
        {
            ConfigureOnGet();
            await OnGetAsync();
        }

        public async Task<JsonResult> OnPost()
        {
            ConfigureOnGet();
            OnBeforePost();
            if (ModelState.IsValid)
            {
                try
                {
                    var errors = await ValidateUpdateAsync(Id, Input);
                    if (ModelState.IsValid)
                    {
                        await OnUpdateAsync();
                    }
                }
                catch (AbpRemoteCallException ex)
                {
                    foreach (var error in ex.Error.ValidationErrors)
                    {
                        foreach (var member in error.Members)
                        {
                            ModelState.AddModelError(member, ex.Message);
                        }
                    }
                }
                catch (AbpValidationException ex)
                {
                    foreach (var error in ex.ValidationErrors)
                    {
                        foreach (var member in error.MemberNames)
                        {
                            ModelState.AddModelError(member, error.ErrorMessage);
                        }
                    }
                }
            }
            return ModelState.IsValid ?
                new JsonResult(new { Success = true }) :
                new JsonResult(new {
                    Success = false,
                    Errors = ModelState
                        .Where(x => x.Value.Errors.Any())
                        .Select(y => new
                        {
                            key = y.Key,
                            details = y.Value.Errors.Select(z => z.ErrorMessage)
                        })
                });
        }

        protected virtual void OnBeforePost()
        {
        }

        protected virtual async Task<IReadOnlyList<ValidationError>> OnValidateAsync(T1 id, T2 input)
        {
            var errors = await Task.FromResult(new List<ValidationError>());
            if (_validator == null)
            {
                return errors;
            }
            var result = _validator.Validate(input);
            if (result.IsValid)
            {
                return errors;
            }
            else
            {
                return await Task.FromResult(result.Errors.Select(x =>
                    new ValidationError
                    (
                        x.ErrorMessage,
                        new List<string> { x.PropertyName })
                    ).ToList()
                );
            }
        }

        protected abstract Task OnGetAsync();

        protected abstract Task OnUpdateAsync();

    }
}