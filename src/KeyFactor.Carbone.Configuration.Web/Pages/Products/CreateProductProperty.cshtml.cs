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
        private readonly CreateProductPropertyValidator _validator;
        public CreateProductPropertyModel(IProductAppService productAppService, CreateProductPropertyValidator validator) : base(new CreateProductPropertyDto())
        {
            _productAppService = Check.NotNull(productAppService, nameof(productAppService));
            _validator = validator;
        }

        protected override void ConfigureOnGet()
        {
            ViewData["Title"] = "Products Properties";
            ViewData["GoBackUrl"] = "/Products";
            ViewData["AddNewUrl"] = "/Products/CreateProductProperty";
            ViewData["SaveUrl"] = "/Products/EditProductProperty";

            Input.ProductId = Guid.Parse(Request.Query["ProductId"]);
        }

        protected override async Task<IActionResult> OnCreateAsync()
        {
            await _productAppService.CreateProductPropertyAsync(Input);
            return RedirectToPage("/Products/Index");
        }

        protected override async Task<IReadOnlyList<ValidationError>> OnValidateAsync(CreateProductPropertyDto input)
        {
            var errors = new List<ValidationError>();
            var result = _validator.Validate(input);
            if (result.IsValid)
            {
                return await Task.FromResult(errors);
            }
            else
            {
                return await Task.FromResult(result.Errors.Select(x =>
                    new ValidationError
                    (
                        x.ErrorMessage,
                        new List<string> { x.PropertyName })
                    ).ToList()
                );
            }
        }
    }
}
