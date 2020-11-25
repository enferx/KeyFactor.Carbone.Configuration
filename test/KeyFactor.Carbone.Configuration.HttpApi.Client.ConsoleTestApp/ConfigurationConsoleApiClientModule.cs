using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace KeyFactor.Carbone.Configuration
{
    [DependsOn(
        typeof(ConfigurationHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class ConfigurationConsoleApiClientModule : AbpModule
    {
        
    }
}
