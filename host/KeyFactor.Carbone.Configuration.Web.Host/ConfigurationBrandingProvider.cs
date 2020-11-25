using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace KeyFactor.Carbone.Configuration
{
    [Dependency(ReplaceServices = true)]
    public class ConfigurationBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Configuration";
    }
}
