using System;
using KeyFactor.Carbone.Configuration.Products;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace KeyFactor.Carbone.Configuration.EntityFrameworkCore
{
    public static class ConfigurationDbContextModelCreatingExtensions
    {
        public static void ConfigureConfiguration(
            this ModelBuilder builder,
            Action<ConfigurationModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ConfigurationModelBuilderConfigurationOptions(
                ConfigurationDbProperties.DbTablePrefix,
                ConfigurationDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<Product>(b =>
            {
                b.ToTable("Products", ConfigurationDbProperties.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(ProductConsts.MaxNameLength);
                b.Property(x => x.Number).IsRequired().HasMaxLength(ProductConsts.MaxNumberLength);
                b.HasIndex(x => x.Number).IsUnique();
                b.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();
            });

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
        }
    }
}