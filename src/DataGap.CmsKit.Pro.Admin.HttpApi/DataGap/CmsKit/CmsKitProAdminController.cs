using DataGap.Jellog.AspNetCore.Mvc;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit;

public abstract class CmsKitProAdminController : JellogControllerBase
{
    protected CmsKitProAdminController()
    {
        LocalizationResource = typeof(CmsKitResource);
    }
}
