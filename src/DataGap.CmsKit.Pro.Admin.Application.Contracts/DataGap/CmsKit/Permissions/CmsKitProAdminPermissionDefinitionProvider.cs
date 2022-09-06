using DataGap.Jellog.Authorization.Permissions;
using DataGap.Jellog.GlobalFeatures;
using DataGap.Jellog.Localization;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit.Permissions;

public class CmsKitProAdminPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var cmsGroup = context.GetGroupOrNull(CmsKitProAdminPermissions.GroupName)
                       ?? context.AddGroup(CmsKitProAdminPermissions.GroupName, L("Permission:CmsKit"));

        cmsGroup.AddPermission(CmsKitProAdminPermissions.Newsletters.Default, L("Permission:NewsletterManagement"))
            .RequireGlobalFeatures(typeof(NewslettersFeature));

        var pollGroup = cmsGroup.AddPermission(CmsKitProAdminPermissions.Polls.Default, L("Permission:Poll"))
            .RequireGlobalFeatures(typeof(PollsFeature));
        pollGroup.AddChild(CmsKitProAdminPermissions.Polls.Create, L("Permission:Poll.Create"))
            .RequireGlobalFeatures(typeof(PollsFeature));
        pollGroup.AddChild(CmsKitProAdminPermissions.Polls.Update, L("Permission:Poll.Update"))
            .RequireGlobalFeatures(typeof(PollsFeature));
        pollGroup.AddChild(CmsKitProAdminPermissions.Polls.Delete, L("Permission:Poll.Delete"))
            .RequireGlobalFeatures(typeof(PollsFeature));

        cmsGroup.AddPermission(CmsKitProAdminPermissions.Contact.SettingManagement, L("Permission:Contact:SettingManagement"))
            .RequireGlobalFeatures(typeof(ContactFeature));

        var urlShortingGroup = cmsGroup.AddPermission(CmsKitProAdminPermissions.UrlShorting.Default, L("Permission:UrlShorting"))
            .RequireGlobalFeatures(typeof(UrlShortingFeature));
        urlShortingGroup.AddChild(CmsKitProAdminPermissions.UrlShorting.Create, L("Permission:UrlShorting.Create"))
            .RequireGlobalFeatures(typeof(UrlShortingFeature));
        urlShortingGroup.AddChild(CmsKitProAdminPermissions.UrlShorting.Update, L("Permission:UrlShorting.Update"))
            .RequireGlobalFeatures(typeof(UrlShortingFeature));
        urlShortingGroup.AddChild(CmsKitProAdminPermissions.UrlShorting.Delete, L("Permission:UrlShorting.Delete"))
            .RequireGlobalFeatures(typeof(UrlShortingFeature));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CmsKitResource>(name);
    }
}
