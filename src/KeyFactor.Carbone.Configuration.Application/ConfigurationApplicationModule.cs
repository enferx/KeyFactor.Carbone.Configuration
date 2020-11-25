using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace KeyFactor.Carbone.Configuration
{
    [DependsOn(
        typeof(ConfigurationDomainModule),
        typeof(ConfigurationApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class ConfigurationApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<ConfigurationApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                //options.AddMaps<ConfigurationApplicationModule>(validate: true);
                options.AddMaps<ConfigurationApplicationModule>();
            });
        }
    }
}
