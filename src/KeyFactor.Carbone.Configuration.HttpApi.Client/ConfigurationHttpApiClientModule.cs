using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace KeyFactor.Carbone.Configuration
{
    [DependsOn(
        typeof(ConfigurationApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class ConfigurationHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Configuration";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ConfigurationApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
