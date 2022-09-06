using DataGap.Jellog.Reflection;

namespace DataGap.CmsKit.Permissions;

public static class CmsKitProAdminPermissions
{
    public const string GroupName = CmsKitAdminPermissions.GroupName;

    public static class Newsletters
    {
        public const string Default = GroupName + ".Newsletter";
    }

    public static class Contact
    {
        private const string Default = GroupName + ".Contact";

        public const string SettingManagement = GroupName + ".SettingManagement";
    }

    public static class UrlShorting
    {
        public const string Default = GroupName + ".UrlShorting";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Polls
    {
        public const string Default = GroupName + ".Poll";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CmsKitProAdminPermissions));
    }
}
