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
    public class EditProductPropertyModel : UpdateConfigurationPageModel<Guid, CreateUpdateProductPropertyDto>
    {
        private readonly IProductAppService _productAppService;
        
        public EditProductPropertyModel(IProductAppService productAppService, CreateUpdateProductPropertyValidator validator) : base(validator)
        {
            _productAppService = Check.NotNull(productAppService, nameof(productAppService));
        }

        protected override void ConfigureOnGet()
        {
            ViewData["Title"] = "Product Property";
            ViewData["GoBackUrl"] = "/Products";
            ViewData["SaveUrl"] = "/Products/EditProductProperty";
        }

        protected override async Task OnGetAsync()
        {
            var productProperty = await _productAppService.GetProductPropertyAsync(Id);
            Input = ObjectMapper.Map<ProductPropertyDto, CreateUpdateProductPropertyDto>(productProperty);
        }

        protected override async Task<List<ValidationError>> OnValidateAsync(Guid id, CreateUpdateProductPropertyDto productDto)
        {
            var errors = await base.OnValidateAsync(id, productDto);
            if (errors.Count > 0)
            {
                return errors;
            }
            return await Task.FromResult(new List<ValidationError>());
        }

        protected override async Task OnUpdateAsync()
        {
            await _productAppService.UpdateProductPropertyAsync(Id, Input);
        }
    }
}
