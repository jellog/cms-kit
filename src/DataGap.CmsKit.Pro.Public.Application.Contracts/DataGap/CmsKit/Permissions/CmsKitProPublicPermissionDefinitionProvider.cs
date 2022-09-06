using DataGap.Jellog.Authorization.Permissions;
using DataGap.Jellog.Localization;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit.Permissions;

public class CmsKitProPublicPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CmsKitProPublicPermissions.GroupName, L("Permission:Public"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CmsKitResource>(name);
    }
}
