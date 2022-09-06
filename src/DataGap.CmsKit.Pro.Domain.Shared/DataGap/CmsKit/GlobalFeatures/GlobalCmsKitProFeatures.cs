using JetBrains.Annotations;
using DataGap.Jellog.GlobalFeatures;

namespace DataGap.CmsKit.GlobalFeatures;

public class GlobalCmsKitProFeatures : GlobalModuleFeatures
{
    public const string ModuleName = "CmsKitPro";

    public NewslettersFeature Newsletter => GetFeature<NewslettersFeature>();

    public ContactFeature Contact => GetFeature<ContactFeature>();

    public GlobalCmsKitProFeatures([NotNull] GlobalFeatureManager featureManager)
        : base(featureManager)
    {
        AddFeature(new NewslettersFeature(this));
        AddFeature(new ContactFeature(this));
        AddFeature(new UrlShortingFeature(this));
        AddFeature(new PollsFeature(this));
    }
}
