using KeyFactor.Carbone.Configuration.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace KeyFactor.Carbone.Configuration
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(ConfigurationEntityFrameworkCoreTestModule)
        )]
    public class ConfigurationDomainTestModule : AbpModule
    {
        
    }
}
