using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using DataGap.CmsKit.Pro.MultiTenancy;
using DataGap.CmsKit.Pro.Web;
using Microsoft.OpenApi.Models;
using Owl.reCAPTCHA;
using DataGap.Jellog;
using DataGap.Jellog.Account;
using DataGap.Jellog.Account.Public.Web;
using DataGap.Jellog.AspNetCore.Mvc.UI.Theme.Lepton;
using DataGap.Jellog.AspNetCore.Mvc.UI.Theme.Shared;
using DataGap.Jellog.AuditLogging.EntityFrameworkCore;
using DataGap.Jellog.Autofac;
using DataGap.Jellog.BlobStoring.Database.EntityFrameworkCore;
using DataGap.Jellog.Data;
using DataGap.Jellog.Emailing;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.Jellog.EntityFrameworkCore.SqlServer;
using DataGap.Jellog.FeatureManagement;
using DataGap.Jellog.FeatureManagement.EntityFrameworkCore;
using DataGap.Jellog.GlobalFeatures;
using DataGap.Jellog.Identity;
using DataGap.Jellog.Identity.EntityFrameworkCore;
using DataGap.Jellog.Identity.Web;
using DataGap.Jellog.LeptonTheme;
using DataGap.Jellog.Localization;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.MultiTenancy;
using DataGap.Jellog.PermissionManagement;
using DataGap.Jellog.PermissionManagement.EntityFrameworkCore;
using DataGap.Jellog.PermissionManagement.Identity;
using DataGap.Jellog.SettingManagement.EntityFrameworkCore;
using DataGap.Jellog.Threading;
using DataGap.Jellog.VirtualFileSystem;
using DataGap.Saas.EntityFrameworkCore;
using DataGap.Saas.Host;
using DataGap.Jellog.LeptonTheme.Management;
using DataGap.Jellog.SettingManagement;
using DataGap.Jellog.UI.Navigation;
using DataGap.Jellog.UI.Navigation.Urls;
using DataGap.CmsKit.Contact;
using DataGap.CmsKit.EntityFrameworkCore;
using DataGap.CmsKit.Localization;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Pro.Menus;
using DataGap.CmsKit.Pro.Public.Web;
using DataGap.CmsKit.Tags;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Pro.Public.Web.Middlewares;
using DataGap.CmsKit.Pro.Admin.Web;
using System;
using DataGap.Jellog.AspNetCore.Mvc.AntiForgery;
using DataGap.CmsKit.Polls;

namespace DataGap.CmsKit.Pro;

[DependsOn(
    typeof(CmsKitProWebModule),
    typeof(CmsKitProHttpApiModule),
    typeof(CmsKitProApplicationModule),
    typeof(CmsKitProEntityFrameworkCoreModule),
    typeof(JellogAuditLoggingEntityFrameworkCoreModule),
    typeof(JellogAutofacModule),
    typeof(JellogAccountPublicHttpApiModule),
    typeof(JellogAccountPublicWebModule),
    typeof(JellogAccountPublicApplicationModule),
    typeof(JellogEntityFrameworkCoreSqlServerModule),
    typeof(JellogSettingManagementEntityFrameworkCoreModule),
    typeof(JellogSettingManagementApplicationModule),
    typeof(JellogPermissionManagementEntityFrameworkCoreModule),
    typeof(JellogPermissionManagementApplicationModule),
    typeof(JellogIdentityHttpApiModule),
    typeof(JellogIdentityWebModule),
    typeof(JellogIdentityApplicationModule),
    typeof(JellogIdentityProEntityFrameworkCoreModule),
    typeof(JellogPermissionManagementDomainIdentityModule),
    typeof(LeptonThemeManagementHttpApiModule),
    typeof(LeptonThemeManagementWebModule),
    typeof(LeptonThemeManagementApplicationModule),
    typeof(LeptonThemeManagementDomainModule),
    typeof(SaasHostHttpApiModule),
    typeof(SaasHostWebModule),
    typeof(SaasHostApplicationModule),
    typeof(SaasEntityFrameworkCoreModule),
    typeof(JellogAspNetCoreMvcUiLeptonThemeModule),
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule),
    typeof(JellogFeatureManagementApplicationModule),
    typeof(JellogFeatureManagementEntityFrameworkCoreModule)
)]
public class ProWebUnifiedModule : JellogModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        FeatureConfigurer.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif

        Configure<JellogDbContextOptions>(options => { options.UseSqlServer(); });

        Configure<CmsKitTagOptions>(options =>
        {
            options.EntityTypes.Add(new TagEntityTypeDefiniton("Posts"));

            options.EntityTypes.Add(new TagEntityTypeDefiniton("Products"));
        });
        
        Configure<CmsKitPollingOptions>(options =>
        {
            options.AddWidget("poll-right");
            options.AddWidget("poll-left");
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<JellogVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Domain.Shared",
                            Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Application.Contracts",
                            Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Application", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProWebModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Web", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProAdminWebModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Admin.Web", Path.DirectorySeparatorChar)));

                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProPublicWebModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Public.Web", Path.DirectorySeparatorChar))
                    );
            });
        }

        context.Services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Pro API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

        context.Services.AddreCAPTCHAV3(x =>
        {
            x.SiteKey = "reCAPTCHA_SiteKey";
            x.SiteSecret = "reCAPTCHA_SecretKey";
        });

        Configure<JellogNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new CmsKitProMenuContributor());
        });

        Configure<JellogLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية", "ae"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština", flagIcon: "cz"));
            options.Languages.Add(new LanguageInfo("en", "en", "English", flagIcon: "gb"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish", "fi"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français", "fr"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português (Brasil)", flagIcon: "br"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak", flagIcon: "sk"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe", flagIcon: "tr"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "Chinese", flagIcon: "cn"));
        });

        Configure<JellogMultiTenancyOptions>(options => { options.IsEnabled = MultiTenancyConsts.IsEnabled; });

        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });

        Configure<NewsletterOptions>(options =>
        {
            List<string> additionalPreferences = new List<string>();
            additionalPreferences.Add("ExamplePreference2");
            additionalPreferences.Add("ExamplePreference2");
            additionalPreferences.Add("ExamplePreference1");
            additionalPreferences.Add("ExamplePreference3");
            additionalPreferences.Add("ExamplePreference5");

            List<string> additionalPreferences2 = new List<string>();

            // options.WidgetViewPath = "./2Default.cshtml";

            options.AddPreference("ExamplePreference1", new NewsletterPreferenceDefinition(new LocalizableString(typeof(CmsKitResource), "ExamplePreference1"), new LocalizableString(typeof(CmsKitResource), "ExampleDefinition1"), new LocalizableString(typeof(CmsKitResource), "I agree to the Terms & Conditions and <a href=\"https://commercial.jellog.io/Privacy\">Privacy Policy</a>."), additionalPreferences));
            options.AddPreference("ExamplePreference2", new NewsletterPreferenceDefinition(new LocalizableString(typeof(CmsKitResource), "ExamplePreference2"), new LocalizableString(typeof(CmsKitResource), "ExampleDefinition1"), new LocalizableString(typeof(CmsKitResource), "I agree to the Terms & Conditions and <a href=\"https://commercial.jellog.io/Privacy\">Privacy Policy</a>."), additionalPreferences));
            options.AddPreference("ExamplePreference3", new NewsletterPreferenceDefinition(new LocalizableString(typeof(CmsKitResource), "ExamplePreference3"), definition: new LocalizableString(typeof(CmsKitResource), "ExampleDefinition2"), null, additionalPreferences2));
        });

        //TODO - Remove after coding the UI
        Configure<JellogAntiForgeryOptions>(options =>
        {
            options.TokenCookie.Expiration = TimeSpan.FromDays(365);
            options.AutoValidateIgnoredHttpMethods.Add("POST");
            options.AutoValidateIgnoredHttpMethods.Add("PUT");

        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseMiddleware<UrlSortingMiddleware>();

        if (context.GetEnvironment().IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseErrorPage();
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseJellogRequestLocalization();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pro API"); });

        app.UseAuditing();

        app.UseConfiguredEndpoints();

        SeedData(context);
    }

    private void SeedData(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(async () =>
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                await scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>()
                    .SeedAsync();
            }
        });
    }
}
