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
    public class EditProductPropertyModel : UpdateConfigurationPageModel<Guid, UpdateProductPropertyDto>
    {
        private readonly IProductAppService _productAppService;
        
        public EditProductPropertyModel(IProductAppService productAppService, UpdateProductPropertyValidator validator) : base(validator)
        {
            _productAppService = Check.NotNull(productAppService, nameof(productAppService));
        }

        protected override void ConfigureOnGet()
        {
            ViewData["Title"] = "Products Properties";
            ViewData["GoBackUrl"] = "/Products";
            ViewData["AddNewUrl"] = "/Products/CreateProductProperty";
            ViewData["SaveUrl"] = "/Products/EditProductProperty";

        }

        protected override async Task OnGetAsync()
        {
            var productProperty = await _productAppService.GetProductPropertyAsync(Id);
            Input = ObjectMapper.Map<ProductPropertyDto, UpdateProductPropertyDto>(productProperty);
        }

        protected override async Task<IActionResult> OnUpdateAsync()
        {
            await _productAppService.UpdateProductPropertyAsync(Id, Input);
            return this.RedirectToPage("/Products/Index");
        }
    }
}
