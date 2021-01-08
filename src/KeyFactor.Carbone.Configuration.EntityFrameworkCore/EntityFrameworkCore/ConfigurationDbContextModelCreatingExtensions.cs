﻿using System;
using KeyFactor.Carbone.Configuration.Products;
using KeyFactor.Carbone.Configuration.Units;
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
                b.HasOne<Unit>().WithMany().HasForeignKey(x => x.UnitId).IsRequired();
            });

            builder.Entity<Unit>(b =>
            {
                b.ToTable("Units", ConfigurationDbProperties.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(UnitConsts.MaxNameLength);
                b.HasIndex(x => x.Name).IsUnique();
                b.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();
            });

            builder.Entity<ProductProperty>(b =>
            {
                b.ToTable("ProductProperties", ConfigurationDbProperties.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(ProductPropertyConsts.MaxNameLength);
                b.Property(x => x.DefaultValueString).HasMaxLength(ProductPropertyConsts.MaxValueLength);
                b.Property(x => x.Description).HasMaxLength(ProductPropertyConsts.MaxDescriptionLength);
                b.HasOne(x => x.Product).WithMany(x => x.Properties).HasForeignKey(x => x.ProductId).IsRequired().OnDelete(DeleteBehavior.Cascade);
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