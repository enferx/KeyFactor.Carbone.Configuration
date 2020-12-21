using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace KeyFactor.Carbone.Configuration
{
    [DependsOn(
        typeof(ConfigurationApplicationModule),
        typeof(ConfigurationDomainTestModule),
        typeof(AbpAutoMapperModule)
        )]
    public class ConfigurationApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<ConfigurationApplicationTestModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                //options.AddMaps<ConfigurationApplicationModule>(validate: true);
                options.AddMaps<ConfigurationApplicationTestModule>();
            });

        }
    }
}
