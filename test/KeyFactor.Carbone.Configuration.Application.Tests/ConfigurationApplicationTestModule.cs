using Volo.Abp.Modularity;

namespace KeyFactor.Carbone.Configuration
{
    [DependsOn(
        typeof(ConfigurationApplicationModule),
        typeof(ConfigurationDomainTestModule)
        )]
    public class ConfigurationApplicationTestModule : AbpModule
    {

    }
}
