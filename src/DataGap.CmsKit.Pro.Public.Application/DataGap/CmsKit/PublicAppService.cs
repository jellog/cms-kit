using DataGap.Jellog.Application.Services;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit;

public abstract class PublicAppService : ApplicationService
{
    protected PublicAppService()
    {
        LocalizationResource = typeof(CmsKitResource);
        ObjectMapperContext = typeof(CmsKitProPublicApplicationModule);
    }
}
