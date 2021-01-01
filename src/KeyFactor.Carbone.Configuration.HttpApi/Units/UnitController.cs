using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace KeyFactor.Carbone.Configuration.Units
{
    [RemoteService]
    [Area("configuration")]
    [ControllerName("Unit")]
    [Route("api/configuration/units")]

    public class UnitController : ConfigurationController, IUnitAppService
    {
        private readonly IUnitAppService _unitAppService;

        public UnitController(IUnitAppService unitAppService)
        {
            _unitAppService = Check.NotNull(unitAppService, nameof(unitAppService));
        }        
        
        [HttpPost]
        public Task<UnitDto> CreateAsync(CreateUnitDto input)
        {
            return _unitAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _unitAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("Find/{name}")]
        public Task<UnitDto> FindByName(string name)
        {
            return _unitAppService.FindByName(name);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<UnitDto> GetAsync(Guid id)
        {
            return _unitAppService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<UnitDto>> GetListAsync(GetUnitListDto input)
        {
            return _unitAppService.GetListAsync(input);
        }

        [HttpPut()]
        [Route("{id}")]
        public Task<UnitDto> UpdateAsync(Guid id, UpdateUnitDto input)
        {
            return _unitAppService.UpdateAsync(id, input);
        }

        [HttpGet()]
        [Route("validatecreate")]
        public Task<IReadOnlyList<ValidationError>> ValidateCreateAsync(CreateUnitDto input)
        {
            return _unitAppService.ValidateCreateAsync(input);
        }

        [HttpGet()]
        [Route("validateupdate")]
        public Task<IReadOnlyList<ValidationError>> ValidateUpdateAsync(Guid id, UpdateUnitDto input)
        {
            return _unitAppService.ValidateUpdateAsync(id, input);
        }
    }
}
