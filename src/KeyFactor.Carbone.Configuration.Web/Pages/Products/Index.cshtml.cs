using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using KeyFactor.Carbone.Configuration.Permissions;
using KeyFactor.Carbone.Configuration.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Authorization.Permissions;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }
    }
}
