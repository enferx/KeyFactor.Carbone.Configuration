using KeyFactor.Carbone.Configuration.Localization;
using KeyFactor.Carbone.Configuration.Shared;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Volo.Abp.Http.Client;

namespace KeyFactor.Carbone.Configuration.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class CreateConfigurationPageModel<T1> :
        ConfigurationPageModel, IValidateCreate<T1>
    {
        [BindProperty]
        public T1 Input { get; set; }

        public CreateConfigurationPageModel(string entityPath, T1 input) : base(entityPath)
        {
            Input = input;
        }

        public async Task<IReadOnlyList<ValidationError>> ValidateCreateAsync(T1 input)
        {
            var results = await OnValidateAsync(input);
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
            IActionResult result = null;
            if (ModelState.IsValid)
            {
                try
                {
                    var errors = await ValidateCreateAsync(Input);
                    if (ModelState.IsValid)
                    {
                        result = await OnCreateAsync();
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
            }
            return result ?? Page();
        }

        protected abstract Task<IReadOnlyList<ValidationError>> OnValidateAsync(T1 input);

        protected abstract Task OnGetAsync();

        protected abstract Task<IActionResult> OnCreateAsync();
    }
}