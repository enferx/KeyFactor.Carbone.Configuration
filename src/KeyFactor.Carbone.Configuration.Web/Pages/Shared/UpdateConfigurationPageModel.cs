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

        public UpdateConfigurationPageModel() : base("Input.")
        {
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
            ConfigureViewData();
            await OnGetAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            ConfigureViewData();
            OnBeforePost();
            IActionResult result = null;
            if (ModelState.IsValid)
            {
                try
                {
                    var errors = await ValidateUpdateAsync(Id, Input);
                    if (ModelState.IsValid)
                    {
                        result = await OnUpdateAsync();
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
            return ModelState.IsValid ? result : Page();
        }

        protected virtual void OnBeforePost()
        {
        }

        protected abstract Task<IReadOnlyList<ValidationError>> OnValidateAsync(T1 id, T2 input);
       
        protected abstract Task OnGetAsync();

        protected abstract Task<IActionResult> OnUpdateAsync();

    }
}