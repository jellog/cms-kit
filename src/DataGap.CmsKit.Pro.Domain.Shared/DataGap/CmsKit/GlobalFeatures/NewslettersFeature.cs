using JetBrains.Annotations;
using DataGap.Jellog.GlobalFeatures;

namespace DataGap.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class NewslettersFeature : GlobalFeature
{
    public const string Name = "CmsKitPro.Newsletter";

    internal NewslettersFeature(
        [NotNull] GlobalCmsKitProFeatures cmsKitPro
    ) : base(cmsKitPro)
    {
    }

    public override void Enable()
    {
        var userFeature = FeatureManager.Modules.CmsKit().User;
        if (!userFeature.IsEnabled)
        {
            userFeature.Enable();
        }

        base.Enable();
    }
}
