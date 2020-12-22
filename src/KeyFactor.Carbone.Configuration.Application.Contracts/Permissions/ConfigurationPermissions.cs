using Volo.Abp.Reflection;

namespace KeyFactor.Carbone.Configuration.Permissions
{
    public class ConfigurationPermissions
    {
        public const string GroupName = "Configuration";
        
        public static class Products
        {
            public const string Default = GroupName + ".Products";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Units
        {
            public const string Default = GroupName + ".Units";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ConfigurationPermissions));
        }
    }
}