using System;
using Volo.Abp.Application.Dtos;

namespace KeyFactor.Carbone.Configuration.Units
{
    public class UnitDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
