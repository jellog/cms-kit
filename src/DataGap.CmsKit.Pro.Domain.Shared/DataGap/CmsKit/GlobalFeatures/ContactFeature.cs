using JetBrains.Annotations;
using DataGap.Jellog.GlobalFeatures;

namespace DataGap.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class ContactFeature : GlobalFeature
{
    public const string Name = "CmsKitPro.Contact";

    internal ContactFeature(
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
