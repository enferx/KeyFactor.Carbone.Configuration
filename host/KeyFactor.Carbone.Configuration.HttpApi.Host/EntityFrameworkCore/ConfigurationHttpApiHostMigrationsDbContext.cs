using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace KeyFactor.Carbone.Configuration.EntityFrameworkCore
{
    public class ConfigurationHttpApiHostMigrationsDbContext : AbpDbContext<ConfigurationHttpApiHostMigrationsDbContext>
    {
        public ConfigurationHttpApiHostMigrationsDbContext(DbContextOptions<ConfigurationHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureConfiguration();
            modelBuilder.ConfigureAuditLogging();
            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
        }
    }
}
