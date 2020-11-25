using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace KeyFactor.Carbone.Configuration.EntityFrameworkCore
{
    public class ConfigurationHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<ConfigurationHttpApiHostMigrationsDbContext>
    {
        public ConfigurationHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ConfigurationHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Configuration"));

            return new ConfigurationHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
