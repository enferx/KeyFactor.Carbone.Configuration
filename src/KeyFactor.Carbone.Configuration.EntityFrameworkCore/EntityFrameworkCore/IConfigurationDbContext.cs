using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace KeyFactor.Carbone.Configuration.EntityFrameworkCore
{
    [ConnectionStringName(ConfigurationDbProperties.ConnectionStringName)]
    public interface IConfigurationDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}