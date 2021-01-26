using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace KeyFactor.Carbone.Configuration.Products
{
    [RemoteService]
    [Area("configuration")]
    [ControllerName("Product")]
    [Route("api/configuration/products")]
    public class ProductController : ConfigurationController, IProductAppService
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = Check.NotNull(productAppService, nameof(productAppService));
        }

        [HttpPost]
        public Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            return _productAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _productAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("Find/{number}")]
        public Task<ProductDto> FindByNumber(string number)
        {
            return _productAppService.FindByNumber(number);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProductDto> GetAsync(Guid id)
        {
            return _productAppService.GetAsync(id);
        }
        
        [HttpGet]
        public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            return await _productAppService.GetListAsync(input);
        }

        [HttpPut()]
        [Route("{id}")]
        public Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input)
        {
            return _productAppService.UpdateAsync(id, input);
        }

        [HttpGet()]
        [Route("validate/create")]
        public Task<IReadOnlyList<ValidationError>> ValidateCreateAsync(CreateProductDto input)
        {
            return _productAppService.ValidateCreateAsync(input);
        }
        
        [HttpGet()]
        [Route("validate/update")]
        public Task<IReadOnlyList<ValidationError>> ValidateUpdateAsync(Guid id, UpdateProductDto input)
        {
            return _productAppService.ValidateUpdateAsync(id, input);
        }

        [HttpGet]
        [Route("productproperty/{id}")]
        public Task<ProductPropertyDto> GetProductPropertyAsync(Guid id)
        {
            return _productAppService.GetProductPropertyAsync(id);
        }

        [HttpPost()]
        [Route("createproductproperty/decimal")]
        public Task<ProductPropertyDto> CreateDecimalProductPropertyAsync(CreateDecimalProductPropertyDto input)
        {
            return _productAppService.CreateDecimalProductPropertyAsync(input);
        }

        [HttpPost()]
        [Route("createproductproperty/double")]
        public Task<ProductPropertyDto> CreateDoubleProductPropertyAsync(CreateDoubleProductPropertyDto input)
        {
            return _productAppService.CreateDoubleProductPropertyAsync(input);
        }

        [HttpPost()]
        [Route("createproductproperty/integer")]
        public Task<ProductPropertyDto> CreateIntegerProductPropertyAsync(CreateIntegerProductPropertyDto input)
        {
            return _productAppService.CreateIntegerProductPropertyAsync(input);
        }

        [HttpPost()]
        [Route("createproductproperty/string")]
        public Task<ProductPropertyDto> CreateStringProductPropertyAsync(CreateStringProductPropertyDto input)
        {
            return _productAppService.CreateStringProductPropertyAsync(input);
        }

        [HttpPost()]
        [Route("productproperty")]
        public Task<ProductPropertyDto> CreateProductPropertyAsync(CreateProductPropertyDto input)
        {
            return _productAppService.CreateProductPropertyAsync(input);
        }

        [HttpPut()]
        [Route("productproperty/{id}")]
        public Task<ProductPropertyDto> UpdateProductPropertyAsync(Guid id, UpdateProductPropertyDto input)
        {
            return _productAppService.UpdateProductPropertyAsync(id, input);
        }
    }
}
