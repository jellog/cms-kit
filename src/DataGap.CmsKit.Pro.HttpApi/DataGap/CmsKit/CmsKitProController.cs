using DataGap.Jellog.AspNetCore.Mvc;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit;

public abstract class CmsKitProController : JellogControllerBase
{
    protected CmsKitProController()
    {
        LocalizationResource = typeof(CmsKitResource);
    }
}
