using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using KeyFactor.Carbone.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Authorization.Permissions;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IPermissionStore _permissionStore;
        public IndexModel(IAuthorizationService authorizationService, IPermissionStore permissionStore)
        {
            _authorizationService = authorizationService;
            _permissionStore = permissionStore;
        }
        public async Task OnGetAsync()
        {
            var a = await _authorizationService.IsGrantedAsync(ConfigurationPermissions.Products.Default);
        }      
    }
}
