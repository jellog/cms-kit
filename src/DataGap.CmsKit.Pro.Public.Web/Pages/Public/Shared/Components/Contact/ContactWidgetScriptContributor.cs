using DataGap.Jellog.AspNetCore.Mvc.UI.Bundling;

namespace DataGap.CmsKit.Pro.Public.Web.Pages.Public.Shared.Components.Contact;

public class ContactWidgetScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add("/client-proxies/cms-kit-pro-proxy.js");
        context.Files.Add("/Pages/Public/Shared/Components/Contact/Default.js");
    }
}
