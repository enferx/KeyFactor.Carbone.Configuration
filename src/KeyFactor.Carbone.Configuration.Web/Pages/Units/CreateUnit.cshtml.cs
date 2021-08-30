using KeyFactor.Carbone.Configuration.Units;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Units
{
    public class CreateUnitModel : CreateConfigurationPageModel<CreateUpdateUnitDto>
    {
        private readonly IUnitAppService _unitAppService;

        public CreateUnitModel(IUnitAppService unitAppService) : base(new CreateUpdateUnitDto())
        {
            _unitAppService = Check.NotNull(unitAppService, nameof(unitAppService));
        }

        protected override void ConfigureOnGet()
        {
            ViewData["Title"] = "Units";
            ViewData["GoBackUrl"] = "/Units";
            ViewData["AddNewUrl"] = "/Units/CreateUnit";
            ViewData["SaveUrl"] = "/Units/EditUnit";
        }

        protected override async Task OnCreateAsync()
        {
            await _unitAppService.CreateAsync(Input);
        }

        protected override async Task<List<ValidationError>> OnValidateAsync(CreateUpdateUnitDto input)
        {
            return await _unitAppService.ValidateCreateAsync(input);
        }
    }
}
