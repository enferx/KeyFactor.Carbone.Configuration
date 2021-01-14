using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.FluentValidation;

namespace KeyFactor.Carbone.Configuration
{
    [DependsOn(
        typeof(ConfigurationDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpFluentValidationModule)
        )]
    public class ConfigurationApplicationContractsModule : AbpModule
    {

    }
}
