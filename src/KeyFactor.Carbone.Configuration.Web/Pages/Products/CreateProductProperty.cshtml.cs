using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyFactor.Carbone.Configuration.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Products
{
    public class CreateProductPropertyModel : CreateConfigurationPageModel<CreateProductPropertyDto>
    {
        private readonly IProductAppService _productAppService;
        
        public CreateProductPropertyModel(IProductAppService productAppService, CreateProductPropertyValidator validator) : base(new CreateProductPropertyDto(), validator)
        {
            _productAppService = Check.NotNull(productAppService, nameof(productAppService));
        }

        protected override void ConfigureOnGet()
        {
            ViewData["Title"] = "Products Properties";
            ViewData["GoBackUrl"] = "/Products";
            ViewData["SaveUrl"] = "/Products/EditProductProperty";
            Input.ProductId = Guid.Parse(Request.Query["ProductId"]);
        }

        protected override async Task OnCreateAsync()
        {
            await _productAppService.CreateProductPropertyAsync(Input);
        }    
    }
}
