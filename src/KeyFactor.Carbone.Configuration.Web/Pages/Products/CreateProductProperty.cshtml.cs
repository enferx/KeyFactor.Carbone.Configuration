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

        public CreateProductPropertyModel(IProductAppService productAppService) : base(new CreateProductPropertyDto())
        {
            _productAppService = Check.NotNull(productAppService, nameof(productAppService));
        }

        protected override void ConfigureViewData()
        {
            ViewData["Title"] = "Products Properties";
            ViewData["GoBackUrl"] = "/Products";
            ViewData["AddNewUrl"] = "/Products/CreateProductProperty";
            ViewData["SaveUrl"] = "/Products/EditProductProperty";
        }

        protected override async Task<IActionResult> OnCreateAsync()
        {
            await _productAppService.CreateProductPropertyAsync(Input);
            return RedirectToPage("/Products/Index");
        }

        protected override async Task<IReadOnlyList<ValidationError>> OnValidateAsync(CreateProductPropertyDto input)
        {
            return await new List<ValidationError>();
        }
    }
}