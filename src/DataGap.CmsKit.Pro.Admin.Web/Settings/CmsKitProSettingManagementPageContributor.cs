using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using DataGap.Jellog.SettingManagement.Web.Pages.SettingManagement;
using DataGap.CmsKit.Localization;
using DataGap.CmsKit.Permissions;
using DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.Components.CmsKitProSettingGroup;

namespace DataGap.CmsKit.Pro.Admin.Web.Settings;

public class CmsKitProSettingManagementPageContributor : ISettingPageContributor
{
    public virtual async Task ConfigureAsync(SettingPageCreationContext context)
    {
        if (!await CheckPermissionsAsync(context))
        {
            return;
        }

        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<CmsKitResource>>();
        context.Groups.Add(
            new SettingPageGroup(
                "DataGap.Jellog.CmsKitPro",
                l["Settings:Menu:CmsKit"],
                typeof(CmsKitProSettingGroupViewComponent)
            )
        );
    }

    public virtual async Task<bool> CheckPermissionsAsync(SettingPageCreationContext context)
    {
        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

        return await authorizationService.IsGrantedAsync(CmsKitProAdminPermissions.Contact.SettingManagement);
    }
}
