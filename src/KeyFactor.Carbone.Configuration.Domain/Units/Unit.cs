using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KeyFactor.Carbone.Configuration.Units
{
    public class Unit : AuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }

        private Unit() {}

        internal Unit(Guid id, [NotNull] string name)
        {
            Id = id;
            SetName(name);
        }

        internal Unit ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(Name), maxLength: UnitConsts.MaxNameLength);
        }

    }
}
