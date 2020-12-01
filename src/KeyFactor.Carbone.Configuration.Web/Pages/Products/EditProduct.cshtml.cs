using KeyFactor.Carbone.Configuration.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Configuration.Products
{
    public class EditProductModel : UpdateConfigurationPageModel<Guid, UpdateProductDto, IProductAppService>
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateProductDto Product { get; set; }

        private readonly IProductAppService _productAppService;

        public EditProductModel(IProductAppService productAppService) : base("")
        {
            _productAppService = productAppService ?? throw new ArgumentNullException("productAppService");
        }

        protected override async Task OnGetAsync()
        {
            ViewData["Title"] = "Products";
            var bookDto = await _productAppService.GetAsync(Id);
            Product = ObjectMapper.Map<ProductDto, UpdateProductDto>(bookDto);
        }

        protected override async Task<IReadOnlyList<ValidationError>> OnValidateAsync()
        {
            ViewData["Title"] = "Products";
            return await ValidateUpdate(Id, Product, _productAppService);
        }

        protected override async Task<IActionResult> OnUpdateAsync()
        {
            await _productAppService.UpdateAsync(Id, Product);
            return this.RedirectToPage("/Products/Index");
        }
    }
}
