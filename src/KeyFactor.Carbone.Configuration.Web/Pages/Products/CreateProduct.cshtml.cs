using KeyFactor.Carbone.Configuration.Products;
using KeyFactor.Carbone.Configuration.Units;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp;
using System.Linq;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Products
{
    public class CreateProductModel : CreateConfigurationPageModel<CreateProductDto>
    {
        private readonly IProductAppService _productAppService;
        private readonly IUnitAppService _unitAppService;
        
        [BindProperty]
        [HiddenInput]
        [DataType(DataType.Date)]
        public DateTime? ValidFromDateHidden { get; set; }

        [BindProperty]
        [HiddenInput]
        [DataType(DataType.Date)]
        public DateTime? ValidToDateHidden { get; set; }

        public CreateProductModel(IProductAppService productAppService, IUnitAppService unitAppService) : base(new CreateProductDto()) 
        {
            _productAppService = Check.NotNull(productAppService, nameof(productAppService));
            _unitAppService = Check.NotNull(unitAppService, nameof(unitAppService));

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

        protected override async Task<IActionResult> OnCreateAsync()
        {
            await _productAppService.CreateAsync(Input);
            return RedirectToPage("/Products/Index");
        }

        public async Task<IActionResult> OnGetSearchAsync(string term)
        {
            var units = await _unitAppService.GetListAsync(new GetUnitListDto() 
            { 
                MaxResultCount = 10, 
                Filter = term 
            });
            return new JsonResult(new 
            { 
                items = units.Items.Select(x => new { id = x.Id, text = x.Name }) 
            });
        }
    }
}
