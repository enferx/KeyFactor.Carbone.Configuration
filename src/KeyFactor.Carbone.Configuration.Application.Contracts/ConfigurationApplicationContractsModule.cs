using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace KeyFactor.Carbone.Configuration
{
    [DependsOn(
        typeof(ConfigurationDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class ConfigurationApplicationContractsModule : AbpModule
    {

    }
}
