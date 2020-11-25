using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using KeyFactor.Carbone.Configuration.Localization;
using KeyFactor.Carbone.Configuration.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using KeyFactor.Carbone.Configuration.Permissions;
using Volo.Abp.AspNetCore.ExceptionHandling;
using KeyFactor.Carbone.Configuration.Web.Infrastructure;
using Volo.Abp;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Localization.ExceptionHandling;

namespace KeyFactor.Carbone.Configuration.Web
{
    [DependsOn(
        typeof(ConfigurationHttpApiModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule)
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
                options.Conventions.AuthorizePage("/Configuration/Products/Index", ConfigurationPermissions.Products.Default);
                options.Conventions.AuthorizePage("/Configuration/Products/CreateProduct", ConfigurationPermissions.Products.Create);
                options.Conventions.AuthorizePage("/Configuration/Products/EditProduct", ConfigurationPermissions.Products.Update);

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
