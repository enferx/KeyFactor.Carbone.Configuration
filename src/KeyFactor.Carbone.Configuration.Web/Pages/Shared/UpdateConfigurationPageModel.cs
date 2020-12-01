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

namespace KeyFactor.Carbone.Configuration.Web.Pages
{
    public abstract class UpdateConfigurationPageModel<T1, T2, T3> : ConfigurationPageModel where T3: IValidateUpdate<T1, T2>
    {
        private string EntityPath { get; set; } = string.Empty;

        public UpdateConfigurationPageModel(string entityPath)
        {
            EntityPath = entityPath;    
        }
        
        public void SetEntityPath(string entityPath)
        {
            EntityPath = entityPath;
        }

        protected async Task<IReadOnlyList<ValidationError>> ValidateUpdate(T1 id, T2 input, T3 service) 
        {
            if (ModelState.IsValid)
            {
                var results = await service.ValidateUpdateAsync(id, input);
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
                        result = await OnUpdateAsync();
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

            return ModelState.IsValid ? result : Page();
        }

        protected abstract Task<IReadOnlyList<ValidationError>> OnValidateAsync();
       
        protected abstract Task OnGetAsync();

        protected abstract Task<IActionResult> OnUpdateAsync();
    }
}