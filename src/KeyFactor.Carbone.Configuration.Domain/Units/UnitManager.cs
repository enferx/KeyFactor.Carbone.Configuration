using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace KeyFactor.Carbone.Configuration.Units
{
    public class UnitManager : DomainService
    {
        private readonly IUnitRepository _repository;

        public UnitManager(IUnitRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> CreateAsync([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingName = await _repository.FindByNameAsync(name);
            if (existingName != null)
            {
                throw new UnitNameAlreadyExistsException(name);
            }

            return new Unit(id: GuidGenerator.Create(), name: name);
        }

        public async Task ChangeNameAsync(
            [NotNull] Unit unit,
            [NotNull] string newName)
        {
            Check.NotNull(unit, nameof(unit));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingUnit = await _repository.FindByNameAsync(newName);
            if (existingUnit != null && existingUnit.Id != unit.Id)
            {
                throw new UnitNameAlreadyExistsException(newName);
            }
            unit.ChangeName(newName);
        }
    }
}
