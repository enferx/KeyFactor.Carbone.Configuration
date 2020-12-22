using KeyFactor.Carbone.Configuration.Products;
using KeyFactor.Carbone.Configuration.Units;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace KeyFactor.Carbone.Configuration.EntityFrameworkCore
{
    [ConnectionStringName(ConfigurationDbProperties.ConnectionStringName)]
    public class ConfigurationDbContext : AbpDbContext<ConfigurationDbContext>, IConfigurationDbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Unit> Units { get; set; }

        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureConfiguration();
        }
    }
}