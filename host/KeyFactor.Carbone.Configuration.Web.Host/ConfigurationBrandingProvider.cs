using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace KeyFactor.Carbone.Configuration
{
    [Dependency(ReplaceServices = true)]
    public class ConfigurationBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Configuration";
    }
}
