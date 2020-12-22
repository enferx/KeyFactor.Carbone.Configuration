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
            productsPermission.AddChild(ConfigurationPermissions.Products.Edit, L("Permission:Products.Edit"));
            productsPermission.AddChild(ConfigurationPermissions.Products.Delete, L("Permission:Products.Delete"));

            var unitsPermission = myGroup.AddPermission(ConfigurationPermissions.Units.Default, L("Permission:Units"));
            unitsPermission.AddChild(ConfigurationPermissions.Units.Create, L("Permission:Units.Create"));
            unitsPermission.AddChild(ConfigurationPermissions.Units.Edit, L("Permission:Units.Edit"));
            unitsPermission.AddChild(ConfigurationPermissions.Units.Delete, L("Permission:Units.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ConfigurationResource>(name);
        }
    }
}