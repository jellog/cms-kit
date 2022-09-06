using JetBrains.Annotations;
using DataGap.Jellog.GlobalFeatures;

namespace DataGap.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class PollsFeature : GlobalFeature
{
    public const string Name = "CmsKitPro.Polls";

    internal PollsFeature(
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
