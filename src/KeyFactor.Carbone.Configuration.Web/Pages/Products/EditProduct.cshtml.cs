using KeyFactor.Carbone.Configuration.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Configuration.Products
{
    public class EditProductModel : UpdateConfigurationPageModel<Guid, UpdateProductDto>
    {
        
        private readonly IProductAppService _productAppService;

        [BindProperty]
        [HiddenInput]
        [DataType(DataType.Date)]
        public DateTime? ValidFromDateHidden { get; set; }

        [BindProperty]
        [HiddenInput]
        [DataType(DataType.Date)]
        public DateTime? ValidToDateHidden { get; set; }

        public EditProductModel(IProductAppService productAppService)
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
            var productDto = await _productAppService.GetAsync(Id);
            Input = ObjectMapper.Map<ProductDto, UpdateProductDto>(productDto);
        }

        protected override void OnBeforePost()
        {
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
