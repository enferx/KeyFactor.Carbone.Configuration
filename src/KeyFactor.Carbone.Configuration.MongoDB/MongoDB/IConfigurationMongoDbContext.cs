using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace KeyFactor.Carbone.Configuration.MongoDB
{
    [ConnectionStringName(ConfigurationDbProperties.ConnectionStringName)]
    public interface IConfigurationMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
