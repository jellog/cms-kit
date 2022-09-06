using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DataGap.Jellog.Emailing;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.TextTemplating;

namespace DataGap.CmsKit.Pro;

[DependsOn(
    typeof(CmsKitProApplicationModule),
    typeof(CmsKitProDomainTestModule),
    typeof(JellogTextTemplatingModule),
    typeof(JellogEmailingModule)
)]
public class CmsKitProApplicationTestModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
    }
}
