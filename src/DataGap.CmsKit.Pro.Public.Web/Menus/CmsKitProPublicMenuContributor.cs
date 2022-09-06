using System.Threading.Tasks;
using DataGap.Jellog.UI.Navigation;

namespace DataGap.CmsKit.Pro.Public.Web.Menus;

public class CmsKitProPublicMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.

        return Task.CompletedTask;
    }
}
