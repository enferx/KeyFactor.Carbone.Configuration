using KeyFactor.Carbone.Configuration.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Products
{
    public class CreateProductModel : CreateConfigurationPageModel<CreateProductDto>
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

        public CreateProductModel(IProductAppService productAppService) : base("", new CreateProductDto()) 
        {
            _productAppService = productAppService ?? throw new ArgumentNullException("productAppService");
        }

        protected override async Task<IReadOnlyList<ValidationError>> OnValidateAsync(CreateProductDto productDto)
        {
            return await _productAppService.ValidateCreateAsync(productDto);
        }

        protected override void ConfigureViewData()
        {
            ViewData["Title"] = "Products";
            ViewData["GoBackUrl"] = "/Products";
            ViewData["AddNewUrl"] = "/Products/CreateProduct";
            ViewData["SaveUrl"] = "/Products/EditProduct";
        }

        protected override Task OnGetAsync()
        {
            return Task.CompletedTask;
        }

        protected override async Task<IActionResult> OnCreateAsync()
        {
            await _productAppService.CreateAsync(Input);
            return RedirectToPage("/Products/Index");
        }
    }
}
