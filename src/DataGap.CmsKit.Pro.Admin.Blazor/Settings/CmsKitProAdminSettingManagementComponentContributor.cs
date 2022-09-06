using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using DataGap.Jellog.GlobalFeatures;
using DataGap.Jellog.SettingManagement.Blazor;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Localization;
using DataGap.CmsKit.Permissions;
using DataGap.CmsKit.Pro.Admin.Blazor.Pages.CmsKitProAdminSettingGroup;

namespace DataGap.CmsKit.Pro.Admin.Blazor.Settings;

public class CmsKitProAdminSettingManagementComponentContributor : ISettingComponentContributor
{
    public async Task ConfigureAsync(SettingComponentCreationContext context)
    {
        if (!await CheckPermissionsInternalAsync(context))
        {
            return;
        }

        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<CmsKitResource>>();
        context.Groups.Add(
            new SettingComponentGroup(
                "DataGap.CmsKit.Pro",
                l["Settings:Menu:CmsKit"],
                typeof(CmsKitProAdminSettingManagementComponent)
            )
        );
    }

    public async Task<bool> CheckPermissionsAsync(SettingComponentCreationContext context)
    {
        return await CheckPermissionsInternalAsync(context);
    }

    private async Task<bool> CheckPermissionsInternalAsync(SettingComponentCreationContext context)
    {
        if (!GlobalFeatureManager.Instance.IsEnabled<ContactFeature>())
        {
            return false;
        }

        var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

        return await authorizationService.IsGrantedAsync(CmsKitProAdminPermissions.Contact.SettingManagement);
    }
}
