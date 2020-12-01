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
    public abstract class CreateConfigurationPageModel<T1, T2> : ConfigurationPageModel where T2: IValidateCreate<T1>
    {
        private string EntityPath { get; set; } = string.Empty;

        public CreateConfigurationPageModel(string entityPath)
        {
            EntityPath = entityPath;
        }

        public void SetEntityPath(string entityPath)
        {
            EntityPath = entityPath;
        }

        public async Task<IReadOnlyList<ValidationError>> ValidateCreate(T1 input, T2 service) 
        {
            if (ModelState.IsValid)
            {
                var results = await service.ValidateCreateAsync(input);
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
            return new List<ValidationError>();
        }

        public async Task OnGet()
        {
            await OnGetAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            IActionResult result = null;
            if (ModelState.IsValid)
            {
                try
                {
                    var errors = await OnValidateAsync();
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
                            ModelState.AddModelError(member, error.Message);
                        }
                    }
                }
            }
            return result ?? Page();
        }

        protected abstract Task<IReadOnlyList<ValidationError>> OnValidateAsync();

        protected abstract Task OnGetAsync();

        protected abstract Task<IActionResult> OnCreateAsync();
    }
}