using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using KeyFactor.Carbone.Configuration.Products;
using KeyFactor.Carbone.Configuration.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Http.Client;
using Volo.Abp.Validation;

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

        public EditProductModel(IProductAppService productAppService) : base("Product.")
        {
            _productAppService = productAppService ?? throw new ArgumentNullException("productAppService");
        }

        protected override async Task OnGetAsync()
        {
            var bookDto = await _productAppService.GetAsync(Id);
            Product = ObjectMapper.Map<ProductDto, UpdateProductDto>(bookDto);
        }

        protected override async Task<IReadOnlyList<ValidationError>> OnValidateAsync()
        {
            return await ValidateUpdate(Id, Product, _productAppService);
        }

        protected override async Task<IActionResult> OnUpdateAsync()
        {
            await _productAppService.UpdateAsync(Id, Product);    
            return NoContent();
        }
    }
}
