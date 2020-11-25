using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace KeyFactor.Carbone.Configuration.EntityFrameworkCore
{
    public class ConfigurationModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public ConfigurationModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}