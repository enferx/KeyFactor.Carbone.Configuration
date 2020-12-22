using KeyFactor.Carbone.Configuration.Localization;
using KeyFactor.Carbone.Configuration.Permissions;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace KeyFactor.Carbone.Configuration.Web.Menus
{
    public class ConfigurationMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenu(context);
            }

            var l = context.GetLocalizer<ConfigurationResource>();
            if (await context.IsGrantedAsync(ConfigurationPermissions.Products.Default))
            {
                context.Menu.Items.Add(
                    new ApplicationMenuItem(
                        "Configuration",
                        l["Menu:Configuration"],
                        icon: "fa fa-book"
                    ).AddItem(
                        new ApplicationMenuItem(
                            "Configuration.Products",
                            l["Menu:Products"],
                            url: "/Products"
                        )
                    )
                );
            }
            if (await context.IsGrantedAsync(ConfigurationPermissions.Units.Default))
            {
                context.Menu.Items.Add(
                    new ApplicationMenuItem(
                        "Configuration",
                        l["Menu:Configuration"],
                        icon: "fa fa-book"
                    ).AddItem(
                        new ApplicationMenuItem(
                            "Configuration.Units",
                            l["Menu:Units"],
                            url: "/Units"
                        )
                    )
                );
            }
        }

        private Task ConfigureMainMenu(MenuConfigurationContext context)
        {
            //Add main menu items.

            return Task.CompletedTask;
        }
    }
}