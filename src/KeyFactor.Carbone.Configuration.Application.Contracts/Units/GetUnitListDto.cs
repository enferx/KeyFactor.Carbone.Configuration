using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KeyFactor.Carbone.Configuration.Units
{
    public class GetUnitListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
