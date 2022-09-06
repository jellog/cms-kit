using System;
using Localization.Resources.JellogUi;
using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog;
using DataGap.Jellog.AspNetCore.Mvc.AntiForgery;
using DataGap.Jellog.Localization;
using DataGap.Jellog.Modularity;
using DataGap.CmsKit.Localization;
using DataGap.CmsKit.Public;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitProPublicApplicationContractsModule),
    typeof(CmsKitPublicHttpApiModule)
    )]
public class CmsKitProPublicHttpApiModule : JellogModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitProPublicHttpApiModule).Assembly);
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
        //TODO - Remove after coding the UI
        Configure<JellogAntiForgeryOptions>(options =>
        {
            options.TokenCookie.Expiration = TimeSpan.FromDays(365);
            options.AutoValidateIgnoredHttpMethods.Add("POST");

        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
