using KeyFactor.Carbone.Configuration.Localization;
using KeyFactor.Carbone.Configuration.Permissions;
using KeyFactor.Carbone.Configuration.Web.Infrastructure;
using KeyFactor.Carbone.Configuration.Web.Menus;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.FluentValidation;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace KeyFactor.Carbone.Configuration.Web
{
    [DependsOn(
        typeof(ConfigurationHttpApiModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpFluentValidationModule)
        )]
    public class ConfigurationWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(ConfigurationResource), typeof(ConfigurationWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(ConfigurationWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new ConfigurationMenuContributor());
            });
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<ConfigurationWebModule>();
            });
            
            context.Services.AddAutoMapperObjectMapper<ConfigurationWebModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ConfigurationWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/Products/Index", ConfigurationPermissions.Products.Default);
                options.Conventions.AuthorizePage("/Products/CreateProduct", ConfigurationPermissions.Products.Create);
                options.Conventions.AuthorizePage("/Products/EditProduct", ConfigurationPermissions.Products.Edit);
                options.Conventions.AuthorizePage("/Units/Index", ConfigurationPermissions.Units.Default);
                options.Conventions.AuthorizePage("/Units/CreateUnit", ConfigurationPermissions.Units.Create);
                options.Conventions.AuthorizePage("/Units/EditUnit", ConfigurationPermissions.Units.Edit);
                options.Conventions.AuthorizePage("/Products/CreateProductProperty", ConfigurationPermissions.Products.Create);
                options.Conventions.AuthorizePage("/Products/EditProductProperty", ConfigurationPermissions.Products.Edit);

            });
            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Configuration", typeof(ConfigurationResource));
            });

            Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = true;
            });

            context.Services.AddMvc(options =>
            {
                options.Filters.AddService(typeof(CarboneExceptionFilter), order: 1); 
            });
        }

    }
}
