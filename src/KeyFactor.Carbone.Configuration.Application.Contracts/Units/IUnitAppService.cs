using KeyFactor.Carbone.Configuration.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KeyFactor.Carbone.Configuration.Units
{
    public interface IUnitAppService : IApplicationService, IValidateCreate<CreateUnitDto>, IValidateUpdate<Guid, UpdateUnitDto>
    {
        Task<UnitDto> GetAsync(Guid id);

        Task<UnitDto> FindByName(string name);

        Task<PagedResultDto<UnitDto>> GetListAsync(GetUnitListDto input);

        Task<UnitDto> CreateAsync(CreateUnitDto input);

        Task<UnitDto> UpdateAsync(Guid id, UpdateUnitDto input);

        Task DeleteAsync(Guid id);
    }
}
