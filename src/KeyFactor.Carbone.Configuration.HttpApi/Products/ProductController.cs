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
    [Route("api/configuration/product")]
    public class ProductController : ConfigurationController, IProductAppService
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService ?? throw new ArgumentNullException("productAppService");
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
        [Route("{number}")]
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
        [Route("validateCreate")]
        public Task<IReadOnlyList<ValidationError>> ValidateCreateAsync(CreateProductDto input)
        {
            return _productAppService.ValidateCreateAsync(input);
        }

        [HttpGet()]
        [Route("validateUpdate")]
        public Task<IReadOnlyList<ValidationError>> ValidateUpdateAsync(Guid id, UpdateProductDto input)
        {
            return _productAppService.ValidateUpdateAsync(id, input);
        }
    }
}
