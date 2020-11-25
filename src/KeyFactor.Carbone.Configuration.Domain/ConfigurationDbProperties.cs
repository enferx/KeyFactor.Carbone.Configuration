namespace KeyFactor.Carbone.Configuration
{
    public static class ConfigurationDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Configuration";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Configuration";
    }
}
