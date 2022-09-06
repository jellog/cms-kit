using DataGap.Jellog.AspNetCore.Mvc.UI.RazorPages;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit.Pro.Public.Web.Pages;

public abstract class CmsKitProPublicPageModel : JellogPageModel
{
    public CmsKitProPublicPageModel()
    {
        LocalizationResourceType = typeof(CmsKitResource);
        ObjectMapperContext = typeof(CmsKitProPublicWebModule);
    }
}
