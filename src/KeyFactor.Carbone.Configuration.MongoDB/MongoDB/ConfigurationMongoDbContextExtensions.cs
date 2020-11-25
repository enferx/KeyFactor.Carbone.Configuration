using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace KeyFactor.Carbone.Configuration.MongoDB
{
    public static class ConfigurationMongoDbContextExtensions
    {
        public static void ConfigureConfiguration(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ConfigurationMongoModelBuilderConfigurationOptions(
                ConfigurationDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}