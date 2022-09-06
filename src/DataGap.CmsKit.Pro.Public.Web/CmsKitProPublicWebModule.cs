using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog;
using DataGap.Jellog.AspNetCore.Mvc.Localization;
using DataGap.Jellog.AutoMapper;
using DataGap.Jellog.Http.ProxyScripting.Generators.JQuery;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.UI.Navigation;
using DataGap.Jellog.VirtualFileSystem;
using DataGap.CmsKit.Localization;
using DataGap.CmsKit.Pro.Public.Web.Menus;
using DataGap.CmsKit.Public;
using DataGap.CmsKit.Public.Web;

namespace DataGap.CmsKit.Pro.Public.Web;

[DependsOn(
    typeof(CmsKitProPublicApplicationContractsModule),
    typeof(CmsKitPublicWebModule)
    )]
public class CmsKitProPublicWebModule : JellogModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<JellogMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(CmsKitResource), typeof(CmsKitProPublicWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitProPublicWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<JellogNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CmsKitProPublicMenuContributor());
        });

        Configure<JellogVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitProPublicWebModule>("DataGap.CmsKit.Pro.Public.Web");
        });

        context.Services.AddAutoMapperObjectMapper<CmsKitProPublicWebModule>();
        Configure<JellogAutoMapperOptions>(options =>
        {
            options.AddMaps<CmsKitProPublicWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AddPageRoute("/Public/Newsletters/EmailPreferences", "cms/newsletter/email-preferences");

        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(CmsKitProPublicRemoteServiceConsts.ModuleName);
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
