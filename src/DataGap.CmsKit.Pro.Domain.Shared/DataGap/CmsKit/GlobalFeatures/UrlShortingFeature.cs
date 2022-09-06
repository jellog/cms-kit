using JetBrains.Annotations;
using DataGap.Jellog.GlobalFeatures;

namespace DataGap.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class UrlShortingFeature : GlobalFeature
{
    public const string Name = "CmsKitPro.UrlShorting";

    internal UrlShortingFeature(
        [NotNull] GlobalCmsKitProFeatures cmsKitPro
    ) : base(cmsKitPro)
    {

    }
}
