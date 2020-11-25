using KeyFactor.Carbone.Configuration.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace KeyFactor.Carbone.Configuration.Permissions
{
    public class ConfigurationPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(ConfigurationPermissions.GroupName, L("Permission:Configuration"));

            var productsPermission = myGroup.AddPermission(ConfigurationPermissions.Products.Default, L("Permission:Products"));
            productsPermission.AddChild(ConfigurationPermissions.Products.Create, L("Permission:Products.Create"));
            productsPermission.AddChild(ConfigurationPermissions.Products.Update, L("Permission:Products.Edit"));
            productsPermission.AddChild(ConfigurationPermissions.Products.Delete, L("Permission:Products.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ConfigurationResource>(name);
        }
    }
}