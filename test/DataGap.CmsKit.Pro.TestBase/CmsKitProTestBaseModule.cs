using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog;
using DataGap.Jellog.Authorization;
using DataGap.Jellog.Autofac;
using DataGap.Jellog.Data;
using DataGap.Jellog.Localization;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.Threading;
using DataGap.Jellog.UI.Navigation.Urls;
using DataGap.CmsKit.Localization;
using DataGap.CmsKit.Newsletters;

namespace DataGap.CmsKit.Pro;

[DependsOn(
    typeof(JellogAutofacModule),
    typeof(JellogTestBaseModule),
    typeof(JellogAuthorizationModule),
    typeof(CmsKitProDomainModule)
    )]
public class CmsKitProTestBaseModule : JellogModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        FeatureConfigurer.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = "https://localhost:44341/";
        });

        Configure<NewsletterOptions>(options =>
        {
            List<string> additionalPreferences = new List<string>();
            additionalPreferences.Add("Blog");
            additionalPreferences.Add("Community");
            additionalPreferences.Add("preference3");
            additionalPreferences.Add("preference2");
            additionalPreferences.Add("preference3");

            List<string> additionalPreferences2 = new List<string>();

            options.AddPreference("Community",
                new NewsletterPreferenceDefinition(
                    new LocalizableString(typeof(CmsKitResource), "Community"),
                    new LocalizableString(typeof(CmsKitResource), "CommunityDefinition"),
                    new LocalizableString(typeof(CmsKitResource), "privacy-policy-url"),
                    additionalPreferences));

            options.AddPreference("preference2",
                new NewsletterPreferenceDefinition(
                    new LocalizableString(typeof(CmsKitResource), "preference2"),
                    new LocalizableString(typeof(CmsKitResource), "definition2"),
                    new LocalizableString(typeof(CmsKitResource), "privacy-policy-url")));

            options.AddPreference("preference3",
                new NewsletterPreferenceDefinition(
                    new LocalizableString(typeof(CmsKitResource), "preference3"),
                    new LocalizableString(typeof(CmsKitResource), "definition3"),
                    additionalPreferences: additionalPreferences));

            options.AddPreference("blog",
                new NewsletterPreferenceDefinition(
                    new LocalizableString(typeof(CmsKitResource), "blog"),
                    new LocalizableString(typeof(CmsKitResource), "blog"),
                    additionalPreferences: additionalPreferences2));
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        SeedTestData(context);
    }

    private static void SeedTestData(ApplicationInitializationContext context)
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
