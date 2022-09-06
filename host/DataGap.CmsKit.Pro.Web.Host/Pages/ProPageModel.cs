using DataGap.Jellog.AspNetCore.Mvc.UI.RazorPages;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit.Pro.Pages;

public abstract class ProPageModel : JellogPageModel
{
    protected ProPageModel()
    {
        LocalizationResourceType = typeof(CmsKitResource);
    }
}
