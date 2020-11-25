using KeyFactor.Carbone.Configuration.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace KeyFactor.Carbone.Configuration
{
    public abstract class ConfigurationController : AbpController
    {
        protected ConfigurationController()
        {
            LocalizationResource = typeof(ConfigurationResource);
        }
    }
}
