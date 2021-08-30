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
    public interface IProductAppService : IApplicationService, 
        IValidateCreate<CreateUpdateProductDto>, 
        IValidateUpdate<Guid, CreateUpdateProductDto>
    {
        Task<ProductDto> GetAsync(Guid id);

        Task<ProductDto> FindByNumber(string number);

        Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input);

        Task<ProductDto> CreateAsync(CreateUpdateProductDto input);

        Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input);

        Task DeleteAsync(Guid id);

        Task<ProductPropertyDto> GetProductPropertyAsync(Guid id);

        Task<ProductPropertyDto> CreateProductPropertyAsync(CreateUpdateProductPropertyDto input);

        Task<ProductPropertyDto> UpdateProductPropertyAsync(Guid id, CreateUpdateProductPropertyDto input);

        Task DeleteProductPropertyAsync(Guid id);
    }
}
