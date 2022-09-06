using DataGap.Jellog.Reflection;

namespace DataGap.CmsKit.Permissions;

public class CmsKitProPublicPermissions
{
    public const string GroupName = "Public";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CmsKitProPublicPermissions));
    }
}
