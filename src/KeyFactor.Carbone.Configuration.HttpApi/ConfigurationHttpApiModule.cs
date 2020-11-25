using Localization.Resources.AbpUi;
using KeyFactor.Carbone.Configuration.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace KeyFactor.Carbone.Configuration
{
    [DependsOn(
        typeof(ConfigurationApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class ConfigurationHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(ConfigurationHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<ConfigurationResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
