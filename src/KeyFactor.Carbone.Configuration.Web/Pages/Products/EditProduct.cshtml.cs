using KeyFactor.Carbone.Configuration.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Configuration.Products
{
    public class EditProductModel : UpdateConfigurationPageModel<Guid, CreateUpdateProductDto>
    {
        
        private readonly IProductAppService _productAppService;

        [BindProperty]
        [HiddenInput]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime? ValidFromDateHidden { get; set; }

        [BindProperty]
        [HiddenInput]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime? ValidToDateHidden { get; set; }

        public IReadOnlyList<ProductPropertyDto> Properties { get; set; }

        public EditProductModel(IProductAppService productAppService, CreateUpdateProductDtoValidator validator) : base(validator) 
        {
            _productAppService = productAppService ?? throw new ArgumentNullException(nameof(productAppService));
        }

        protected override void ConfigureOnGet()
        {
            ViewData["Title"] = "Products";
            ViewData["GoBackUrl"] = "/Products";
            ViewData["AddNewUrl"] = "/Products/CreateProduct";
            ViewData["SaveUrl"] = "/Products/EditProduct";
        }

        protected override async Task OnGetAsync()
        {
            var productDto = await _productAppService.GetAsync(Id);
            Input = ObjectMapper.Map<ProductDto, CreateUpdateProductDto>(productDto);
            Properties = productDto.Properties;
        }

        protected override async Task<List<ValidationError>> OnValidateAsync(Guid id, CreateUpdateProductDto productDto)
        {
            var errors = await base.OnValidateAsync(id, productDto);
            if (errors.Count > 0)
            {
                return errors;
            }
            return await _productAppService.ValidateUpdateAsync(Id, productDto);
        }

        protected override async Task OnUpdateAsync()
        {
            await _productAppService.UpdateAsync(Id, Input);
        }
    }
}
