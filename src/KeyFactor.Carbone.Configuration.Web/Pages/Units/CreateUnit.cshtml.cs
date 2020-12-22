using KeyFactor.Carbone.Configuration.Units;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Units
{
    public class CreateUnitModel : CreateConfigurationPageModel<CreateUnitDto>
    {
        private readonly IUnitAppService _unitAppService;

        public CreateUnitModel(IUnitAppService unitAppService) : base(entityPath:string.Empty, new CreateUnitDto())
        {
            _unitAppService = Check.NotNull(unitAppService, nameof(unitAppService));
        }

        protected override async Task<IActionResult> OnCreateAsync()
        {
            await _unitAppService.CreateAsync(Input);
            return RedirectToPage("/Units/Index");
        }

        protected override async Task<IReadOnlyList<ValidationError>> OnValidateAsync(CreateUnitDto input)
        {
            return await _unitAppService.ValidateCreateAsync(input);
        }
    }
}
