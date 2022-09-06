using DataGap.Jellog.Application.Services;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit;

public abstract class CmsKitProAdminAppService : ApplicationService
{
    protected CmsKitProAdminAppService()
    {
        LocalizationResource = typeof(CmsKitResource);
        ObjectMapperContext = typeof(CmsKitProAdminApplicationModule);
    }
}
