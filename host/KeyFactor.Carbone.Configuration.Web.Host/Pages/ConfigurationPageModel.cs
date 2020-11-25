using KeyFactor.Carbone.Configuration.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KeyFactor.Carbone.Configuration.Pages
{
    public abstract class ConfigurationPageModel : AbpPageModel
    {
        protected ConfigurationPageModel()
        {
            LocalizationResourceType = typeof(ConfigurationResource);
        }
    }
}