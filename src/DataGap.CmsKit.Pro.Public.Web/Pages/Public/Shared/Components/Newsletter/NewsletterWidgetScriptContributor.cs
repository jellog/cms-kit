using DataGap.Jellog.AspNetCore.Mvc.UI.Bundling;

namespace DataGap.CmsKit.Pro.Public.Web.Pages.Public.Shared.Components.Newsletter;

public class NewsletterWidgetScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/client-proxies/cms-kit-pro-proxy.js");
        context.Files.Add("/Pages/Public/Shared/Components/Newsletter/Default.js");
    }
}
