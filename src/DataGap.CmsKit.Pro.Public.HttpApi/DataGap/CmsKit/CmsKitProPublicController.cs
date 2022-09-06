using DataGap.Jellog.AspNetCore.Mvc;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit;

public abstract class CmsKitProPublicController : JellogControllerBase
{
    protected CmsKitProPublicController()
    {
        LocalizationResource = typeof(CmsKitResource);
    }
}
