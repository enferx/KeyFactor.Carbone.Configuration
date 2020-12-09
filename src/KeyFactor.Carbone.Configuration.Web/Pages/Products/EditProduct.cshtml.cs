using KeyFactor.Carbone.Configuration.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Configuration.Products
{
    public class EditProductModel : UpdateConfigurationPageModel<Guid, UpdateProductDto>
    {
        
        private readonly IProductAppService _productAppService;

        public EditProductModel(IProductAppService productAppService) : base("")
        {
            _productAppService = productAppService ?? throw new ArgumentNullException("productAppService");
        }

        protected override void ConfigureViewData()
        {
            ViewData["Title"] = "Products";
            ViewData["GoBackUrl"] = "/Products";
            ViewData["AddNewUrl"] = "/Products/CreateProduct";
            ViewData["SaveUrl"] = "/Products/EditProduct";
        }

        protected override async Task OnGetAsync()
        {
            var bookDto = await _productAppService.GetAsync(Id);
            Input = ObjectMapper.Map<ProductDto, UpdateProductDto>(bookDto);
        }

        protected override async Task<IReadOnlyList<ValidationError>> OnValidateAsync(Guid id, UpdateProductDto productDto)
        {
            return await _productAppService.ValidateUpdateAsync(Id, productDto);
        }

        protected override async Task<IActionResult> OnUpdateAsync()
        {
            await _productAppService.UpdateAsync(Id, Input);
            return this.RedirectToPage("/Products/Index");
        }
    }
}
