using Localization.Resources.JellogUi;
using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog;
using DataGap.Jellog.Localization;
using DataGap.Jellog.Modularity;
using DataGap.CmsKit.Admin;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitProAdminApplicationContractsModule),
    typeof(CmsKitAdminHttpApiModule)
    )]
public class CmsKitProAdminHttpApiModule : JellogModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitProAdminHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<JellogLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CmsKitResource>()
                .AddBaseTypes(typeof(JellogUiResource));
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
