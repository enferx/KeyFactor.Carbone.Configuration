using KeyFactor.Carbone.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using System.Linq;

namespace KeyFactor.Carbone.Configuration.Units
{
    [RemoteService(isEnabled: false, IsMetadataEnabled = false)]
    [Authorize(ConfigurationPermissions.Units.Default)]
    public class UnitAppService : ConfigurationAppService, IUnitAppService
    {
        private readonly IUnitRepository _repository;
        private readonly UnitManager _manager;

        public UnitAppService(IUnitRepository repository, UnitManager unitManager)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _manager = Check.NotNull(unitManager, nameof(unitManager));
        }

        [Authorize(ConfigurationPermissions.Units.Create)]
        public async Task<UnitDto> CreateAsync(CreateUnitDto input)
        {
            var unit = await _manager.CreateAsync(name: input.Name);
            await _repository.InsertAsync(unit);
            return ObjectMapper.Map<Unit, UnitDto>(unit);
        }

        [Authorize(ConfigurationPermissions.Units.Edit)]
        public async Task<UnitDto> UpdateAsync(Guid id, UpdateUnitDto input)
        {
            var unit = await _repository.GetAsync(id);
            if (unit.Name != input.Name)
            {
                await _manager.ChangeNameAsync(unit, input.Name);
            }
            unit.ConcurrencyStamp = input.ConcurrencyStamp;
            
            await _repository.UpdateAsync(unit);
            return ObjectMapper.Map<Unit, UnitDto>(unit);
        }

        [Authorize(ConfigurationPermissions.Units.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<UnitDto> FindByName(string name)
        {
            var unit = await _repository.FindByNameAsync(name);
            return ObjectMapper.Map<Unit, UnitDto>(unit);
        }

        public async Task<UnitDto> GetAsync(Guid id)
        {
            var unit = await _repository.GetAsync(id);
            return ObjectMapper.Map<Unit, UnitDto>(unit);
        }

        public async Task<PagedResultDto<UnitDto>> GetListAsync(GetUnitListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Unit.Name);
            }

            var units = await _repository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await AsyncExecuter.CountAsync(
                _repository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    unit => unit.Name.Contains(input.Filter)
                )
            );

            return new PagedResultDto<UnitDto>(
                totalCount,
                ObjectMapper.Map<List<Unit>, List<UnitDto>>(units)
            );
        }

        [Authorize(ConfigurationPermissions.Units.Create)]
        public async Task<IReadOnlyList<ValidationError>> ValidateCreateAsync(CreateUnitDto input)
        {
            var errors = Validate(input);
            var existingProduct = await _repository.FindByNameAsync(input.Name);
            if (existingProduct != null)
            {
                errors.Add(new ValidationError
                (
                    message: L[$"{ConfigurationErrorCodes.UnitNameAlreadyExists}", input.Name],
                    memberNames: new List<string>() { "Name" }
                ));
            }
            return errors;
        }

        [Authorize(ConfigurationPermissions.Units.Edit)]
        public async Task<IReadOnlyList<ValidationError>> ValidateUpdateAsync(Guid id, UpdateUnitDto input)
        {
            var errors = Validate(input);
            var existingProduct = await _repository.FindByNameAsync(input.Name);
            if (existingProduct != null && existingProduct.Id != id)
            {
                errors.Add(new ValidationError
                (
                    message: L[$"{ConfigurationErrorCodes.UnitNameAlreadyExists}", input.Name],
                    memberNames: new List<string>() { "Name" }
                ));
            }
            return errors;
        }
    }
}
