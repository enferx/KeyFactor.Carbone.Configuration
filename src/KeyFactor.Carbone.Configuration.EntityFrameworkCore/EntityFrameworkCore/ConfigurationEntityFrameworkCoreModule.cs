using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace KeyFactor.Carbone.Configuration.EntityFrameworkCore
{
    [DependsOn(
        typeof(ConfigurationDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class ConfigurationEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ConfigurationDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

           Configure<AbpDbContextOptions>(options =>
           {
               /* The main point to change your DBMS.
                * See also BookStoreMigrationsDbContextFactory for EF Core tooling. */
               options.UseSqlServer();
           });
        }
    }
}