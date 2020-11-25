using KeyFactor.Carbone.Configuration.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Validation;

namespace KeyFactor.Carbone.Configuration.Products
{
    public interface IProductAppService : IApplicationService, IValidateCreate<CreateProductDto>, IValidateUpdate<Guid, UpdateProductDto>
    {
        Task<ProductDto> GetAsync(Guid id);

        Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input);

        Task<ProductDto> CreateAsync(CreateProductDto input);

        Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input);

        Task DeleteAsync(Guid id);
        
    }
}
