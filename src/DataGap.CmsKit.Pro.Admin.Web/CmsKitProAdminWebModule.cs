using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog;
using DataGap.Jellog.AspNetCore.Mvc.Localization;
using DataGap.Jellog.AspNetCore.Mvc.UI.Bundling;
using DataGap.Jellog.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;
using DataGap.Jellog.AutoMapper;
using DataGap.Jellog.Http.ProxyScripting.Generators.JQuery;
using DataGap.Jellog.Localization;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.SettingManagement.Web;
using DataGap.Jellog.SettingManagement.Web.Pages.SettingManagement;
using DataGap.Jellog.UI.Navigation;
using DataGap.Jellog.VirtualFileSystem;
using DataGap.CmsKit.Admin.Web;
using DataGap.CmsKit.Localization;
using DataGap.CmsKit.Permissions;
using DataGap.CmsKit.Pro.Admin.Web.Menus;
using DataGap.CmsKit.Pro.Admin.Web.Settings;

namespace DataGap.CmsKit.Pro.Admin.Web;

[DependsOn(
    typeof(CmsKitAdminWebModule),
    typeof(CmsKitProAdminApplicationContractsModule),
    typeof(JellogSettingManagementWebModule)
    )]
public class CmsKitProAdminWebModule : JellogModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<JellogMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(CmsKitResource), typeof(CmsKitProAdminWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitProAdminWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<JellogNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CmsKitProAdminMenuContributor());
        });

        Configure<JellogVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitProAdminWebModule>();
        });

        Configure<SettingManagementPageOptions>(options =>
        {
            options.Contributors.Add(new CmsKitProSettingManagementPageContributor());
        });

        Configure<JellogBundlingOptions>(options =>
        {
            options.ScriptBundles
                .Configure(typeof(IndexModel).FullName,
                    configuration =>
                    {
                        configuration.AddFiles("/client-proxies/cms-kit-pro-admin-proxy.js");
                        configuration.AddFiles("/Pages/CmsKit/Components/CmsKitProSettingGroup/Default.js");
                    });
        });

        context.Services.AddAutoMapperObjectMapper<CmsKitProAdminWebModule>();
        Configure<JellogAutoMapperOptions>(options => { options.AddMaps<CmsKitProAdminWebModule>(validate: true); });

        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizeFolder("/CmsKit/Newsletters/", CmsKitProAdminPermissions.Newsletters.Default);

            options.Conventions.AuthorizeFolder("/CmsKit/Polls/", CmsKitProAdminPermissions.Polls.Default);
            options.Conventions.AuthorizePage("/CmsKit/Polls/Create", CmsKitProAdminPermissions.Polls.Create);
            options.Conventions.AuthorizePage("/CmsKit/Polls/Edit", CmsKitProAdminPermissions.Polls.Update);
            options.Conventions.AuthorizePage("/CmsKit/Polls/Result", CmsKitProAdminPermissions.Polls.Default);

            options.Conventions.AuthorizeFolder("/CmsKit/UrlShorting/", CmsKitProAdminPermissions.UrlShorting.Default);
            options.Conventions.AuthorizePage("/CmsKit/UrlShorting/Create", CmsKitProAdminPermissions.UrlShorting.Create);
            options.Conventions.AuthorizePage("/CmsKit/UrlShorting/Edit", CmsKitProAdminPermissions.UrlShorting.Update);

            options.Conventions.AddPageRoute("/CmsKit/Polls/Index", "/Cms/Polls");
            options.Conventions.AddPageRoute("/CmsKit/Polls/Create", "/Cms/Polls/Create");
            options.Conventions.AddPageRoute("/CmsKit/Polls/Edit", "/Cms/Polls/Edit/{Id}");
            options.Conventions.AddPageRoute("/CmsKit/Polls/Result", "/Cms/Polls/Result/{Id}");

            options.Conventions.AddPageRoute("/CmsKit/UrlShorting/Index", "/Cms/UrlShorting");
            options.Conventions.AddPageRoute("/CmsKit/UrlShorting/Create", "/Cms/UrlShorting/Create");
            options.Conventions.AddPageRoute("/CmsKit/UrlShorting/Edit", "/Cms/UrlShorting/Edit/{Id}");
        });

        Configure<JellogPageToolbarOptions>(options =>
        {
            options.Configure<Pages.CmsKit.Newsletters.IndexModel>(
                toolbar =>
                {
                    toolbar.AddButton(
                        LocalizableString.Create<CmsKitResource>("ExportCSV"),
                        icon: "download",
                        id: "ExportCsv"
                    );
                }
            );

            options.Configure<Pages.CmsKit.UrlShorting.IndexModel>(
                toolbar =>
                {
                    toolbar.AddButton(
                        LocalizableString.Create<CmsKitResource>("ForwardaUrl"),
                        icon: "plus",
                        name: "NewShortenedUrlButton",
                        id: "NewShortenedUrlButton",
                        requiredPolicyName: CmsKitProAdminPermissions.UrlShorting.Create
                    );
                }
            );

            options.Configure<Pages.CmsKit.Polls.IndexModel>(
                toolbar =>
                {
                    toolbar.AddButton(
                        LocalizableString.Create<CmsKitResource>("NewPoll"),
                        icon: "plus",
                        name: "NewPollButton",
                        id: "NewPollButton",
                        requiredPolicyName: CmsKitProAdminPermissions.Polls.Default
                    );
                }
            );
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(CmsKitProAdminRemoteServiceConsts.ModuleName);
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
