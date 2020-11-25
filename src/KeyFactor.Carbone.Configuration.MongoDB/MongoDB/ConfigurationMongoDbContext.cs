using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace KeyFactor.Carbone.Configuration.MongoDB
{
    [ConnectionStringName(ConfigurationDbProperties.ConnectionStringName)]
    public class ConfigurationMongoDbContext : AbpMongoDbContext, IConfigurationMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureConfiguration();
        }
    }
}