using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc.UI.RazorPages;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit.Pro.Admin.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class AdminPageModel : JellogPageModel
{
    public AdminPageModel()
    {
        LocalizationResourceType = typeof(CmsKitResource);
        ObjectMapperContext = typeof(CmsKitProAdminWebModule);
    }
}
