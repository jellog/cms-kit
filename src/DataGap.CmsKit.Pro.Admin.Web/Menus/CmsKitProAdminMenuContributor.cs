using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGap.Jellog.Authorization.Permissions;
using DataGap.Jellog.GlobalFeatures;
using DataGap.Jellog.UI.Navigation;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Localization;
using DataGap.CmsKit.Permissions;

namespace DataGap.CmsKit.Pro.Admin.Web.Menus;

public class CmsKitProAdminMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        await AddCmsMenuAsync(context);
    }

    private Task AddCmsMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<CmsKitResource>();

        var cmsProMenus = new List<ApplicationMenuItem>();

        if (GlobalFeatureManager.Instance.IsEnabled<NewslettersFeature>())
        {
            cmsProMenus.Add(new ApplicationMenuItem(
                    CmsKitProAdminMenus.Newsletters.NewsletterMenu,
                    l["Newsletters"].Value,
                    "/CmsKit/Newsletters"
                ).RequirePermissions(CmsKitProAdminPermissions.Newsletters.Default)
            );
        }

        if (GlobalFeatureManager.Instance.IsEnabled<UrlShortingFeature>())
        {
            cmsProMenus.Add(new ApplicationMenuItem(
                    CmsKitProAdminMenus.UrlShorting.UrlShortingMenu,
                    l["UrlForwarding"].Value,
                    "/Cms/UrlShorting"
                ).RequirePermissions(CmsKitProAdminPermissions.UrlShorting.Default)
            );
        }
        if (GlobalFeatureManager.Instance.IsEnabled<PollsFeature>())
        {
            cmsProMenus.Add(new ApplicationMenuItem(
                    CmsKitProAdminMenus.Polls.PollMenu,
                    l["Polls"].Value,
                    "/Cms/Polls"
                ).RequirePermissions(CmsKitProAdminPermissions.Polls.Default)
            );
        }

        if (cmsProMenus.Any())
        {
            var cmsMenu = context.Menu.FindMenuItem(CmsKitProAdminMenus.GroupName);

            if (cmsMenu == null)
            {
                cmsMenu = new ApplicationMenuItem(
                    CmsKitProAdminMenus.GroupName,
                    l["Cms"],
                    icon: "far fa-newspaper");

                context.Menu.AddItem(cmsMenu);
            }

            foreach (var cmsProMenu in cmsProMenus)
            {
                cmsMenu.AddItem(cmsProMenu);
            }
        }

        return Task.CompletedTask;
    }
}
