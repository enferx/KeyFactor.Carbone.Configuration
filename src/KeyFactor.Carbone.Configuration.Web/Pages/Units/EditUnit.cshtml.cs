using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyFactor.Carbone.Configuration.Units;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp;

namespace KeyFactor.Carbone.Configuration.Web.Pages.Units
{
    public class EditUnitModel : UpdateConfigurationPageModel<Guid, UpdateUnitDto>
    {
        private readonly IUnitAppService _unitAppService;
        public EditUnitModel(IUnitAppService unitAppService)
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

        protected override async Task OnGetAsync()
        {
            var unitDto = await _unitAppService.GetAsync(Id);
            Input = ObjectMapper.Map<UnitDto, UpdateUnitDto>(unitDto);
        }

        protected override async Task OnUpdateAsync()
        {
            await _unitAppService.UpdateAsync(Id, Input);
        }

        protected override async Task<IReadOnlyList<ValidationError>> OnValidateAsync(Guid id, UpdateUnitDto input)
        {
            return await _unitAppService.ValidateUpdateAsync(id, input);
        }
    }
}
