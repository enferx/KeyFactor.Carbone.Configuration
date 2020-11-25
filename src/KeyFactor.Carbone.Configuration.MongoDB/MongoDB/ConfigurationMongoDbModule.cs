using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace KeyFactor.Carbone.Configuration.MongoDB
{
    [DependsOn(
        typeof(ConfigurationDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class ConfigurationMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<ConfigurationMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
