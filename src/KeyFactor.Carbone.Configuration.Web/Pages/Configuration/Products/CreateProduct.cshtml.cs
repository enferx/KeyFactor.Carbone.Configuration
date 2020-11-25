using KeyFactor.Carbone.Configuration.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Products
{
    public class CreateProductModel : CreateConfigurationPageModel<CreateProductDto, IProductAppService>
    {
        [BindProperty]
        public CreateProductDto Product { get; set; }

        private readonly IProductAppService _productAppService;

        public CreateProductModel(IProductAppService productAppService) : base("Product.")
        {
            _productAppService = productAppService ?? throw new ArgumentNullException("productAppService");
        }

        protected override async Task<IReadOnlyList<ValidationError>> OnValidateAsync()
        {
            return await ValidateCreate(Product, _productAppService);
        }

        protected override Task OnGetAsync()
        {
            Product = new CreateProductDto();
            return Task.CompletedTask;
        }

        protected override async Task<IActionResult> OnCreateAsync()
        {
            await _productAppService.CreateAsync(Product);
            return NoContent();
        }
    }
}
