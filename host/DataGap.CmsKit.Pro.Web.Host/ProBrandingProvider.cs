using DataGap.Jellog.Ui.Branding;
using DataGap.Jellog.DependencyInjection;

namespace DataGap.CmsKit.Pro;

[Dependency(ReplaceServices = true)]
public class ProBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Pro";
}
