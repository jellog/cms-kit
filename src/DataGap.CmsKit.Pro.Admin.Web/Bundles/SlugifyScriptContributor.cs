using DataGap.Jellog.AspNetCore.Mvc.UI.Bundling;

namespace DataGap.CmsKit.Pro.Admin.Web.Bundles;

public class SlugifyScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/libs/slugify/slugify.js");
    }
}
