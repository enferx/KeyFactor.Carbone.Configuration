using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace KeyFactor.Carbone.Configuration.MongoDB
{
    public class ConfigurationMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public ConfigurationMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}